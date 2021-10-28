using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HelicoController : MonoBehaviour
{
    [SerializeField] private float forwardSpeed;
    public float ForwardSpeed { get { return forwardSpeed; } set { forwardSpeed = value; } }
    [SerializeField] private float swerveSpeed;

    [SerializeField] private float rotationDegreeClamp;

    [SerializeField] private float boostDuration = 2f;

    [SerializeField] private GameObject shieldEffect;

    private SwipeControl swipe;

    private Rigidbody rb;
    private CharacterController controller;
    private Animator anim;

    private Quaternion originRot;

    private bool isHit = false;

    private bool isInvicible = false;

    private bool isBoost = false;

    private bool hasShield = false;


    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        controller = GetComponent<CharacterController>();
        swipe = GetComponent<SwipeControl>();
        anim = GetComponent<Animator>();

        GameManager.powerupDelegate += Powerup;

        originRot = transform.rotation;
    }


    private void Update()
    {
        if(GameManager.Instance.IsPlaying)
        {
            if (swipe.swipeRight || swipe.swipeLeft)
            {
                Rotate();
            }
            else
            {
                ResetRotation();
            }

            controller.Move(transform.forward * Time.deltaTime * forwardSpeed);
        }
    }

    void FixedUpdate()
    {

    }

    void Rotate()
    {
        float angleToRotate = (100 / (swipe.MaxSwipeValue / swipe.SwipeDeltaX)) / 100 * rotationDegreeClamp;

        Quaternion rotationAngle = Quaternion.Euler(0, angleToRotate, 0);
        Quaternion targetRotation = originRot * rotationAngle;

        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, 0.1f);
    }

    void ResetRotation()
    {
        transform.rotation = Quaternion.Slerp(transform.rotation, originRot, 0.1f);
    }

    public void ResetHit()
    {
        isHit = false;
        forwardSpeed *= 2;
    }

    public void Powerup()
    {
        if(GameManager.Instance.CurrentPowerUp == GameManager.Powerups.Boost)
        {
            StartCoroutine(BoostCoroutine());

        }
        else if(GameManager.Instance.CurrentPowerUp == GameManager.Powerups.Shield)
        {
            hasShield = true;
            shieldEffect.SetActive(true);
        }
    }

    private void RemoveShield()
    {
        hasShield = false;
        shieldEffect.SetActive(false);
        StartCoroutine(InvicibleCoroutine());
    }

    IEnumerator BoostCoroutine()
    {
        CameraFollow cam = Camera.main.GetComponent<CameraFollow>();

        cam.unlerpFOV = false;
        cam.lerpFOV = true;

        forwardSpeed *= 1.5f;
        yield return new WaitForSeconds(boostDuration);
        forwardSpeed /= 1.5f;

        cam.lerpFOV = false;
        cam.unlerpFOV = true;
    }

    IEnumerator InvicibleCoroutine()
    {
        yield return new WaitForSecondsRealtime(1.5f);
        isInvicible = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Obstacle"))
        {
            if(!isHit && !isInvicible)
            {
                if (hasShield)
                {
                    isInvicible = true;
                    RemoveShield();
                }
                else
                {
                    isHit = true;
                    anim.SetTrigger("Hit");
                    forwardSpeed /= 2;
                }
            }
        }
        else if(other.CompareTag("Finish"))
        {
            GameManager.Instance.Win();
        }
    }


    private void OnDestroy()
    {
        GameManager.powerupDelegate -= Powerup;
    }
}
