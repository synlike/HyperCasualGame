using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HelicoController : MonoBehaviour
{
    [SerializeField] private float forwardSpeed;
    [SerializeField] private float swerveSpeed;

    [SerializeField] private float rotationDegreeClamp;

    private SwipeControl swipe;

    private Rigidbody rb;
    private CharacterController controller;
    private Animator anim;

    private Quaternion originRot;

    private bool isHit = false;


    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        controller = GetComponent<CharacterController>();
        swipe = GetComponent<SwipeControl>();
        anim = GetComponent<Animator>();

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

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Obstacle"))
        {
            if(!isHit)
            {
                isHit = true;
                anim.SetTrigger("Hit");
                forwardSpeed /= 2;
            }
        }
    }
}
