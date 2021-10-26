using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwipeControl : MonoBehaviour
{
    public bool tap, swipeRight, swipeLeft;

    public bool isDragging;

    public Vector2 startTouch, swipeDelta;

    private float swipeDeltaX;
    public float SwipeDeltaX { get { return swipeDeltaX; } }

    [SerializeField]
    private float maxSwipeValue = 150f;
    public float MaxSwipeValue { get { return maxSwipeValue; } }


    private Vector2 inputClamped;

    void Start()
    {
        
    }


    void Update()
    {
        //tap = swipeLeft = swipeRight = false;

        if(Input.GetMouseButtonDown(0))
        {
            tap = true;
            isDragging = true;
            startTouch = Input.mousePosition;
        }
        else if(Input.GetMouseButtonUp(0))
        {
            isDragging = false;
            Reset();
        }

        if(Input.touches.Length != 0)
        {
            if(Input.touches[0].phase == TouchPhase.Began)
            {
                tap = true;
                isDragging = true;
                startTouch = Input.touches[0].position;
            }
            else if(Input.touches[0].phase == TouchPhase.Ended || Input.touches[0].phase == TouchPhase.Canceled)
            {
                isDragging = false;
                Reset();
            }
        }

        // Swipe Distance
        swipeDelta = Vector2.zero;
        if(isDragging)
        {
            if (Input.touches.Length > 0)
            {
                swipeDelta = Input.touches[0].position - startTouch;

                if(swipeDelta.x > maxSwipeValue)
                {
                    startTouch.x = Input.touches[0].position.x - maxSwipeValue;
                }
                else if(swipeDelta.x < -maxSwipeValue)
                {
                    startTouch.x = Input.touches[0].position.x + maxSwipeValue;
                }

            }
            else if (Input.GetMouseButton(0))
            {
                swipeDelta = (Vector2)Input.mousePosition - startTouch;

                if (swipeDelta.x > maxSwipeValue)
                {
                    startTouch.x = Input.mousePosition.x - maxSwipeValue;
                }
                else if (swipeDelta.x < -maxSwipeValue)
                {
                    startTouch.x = Input.mousePosition.x + maxSwipeValue;
                }
            }

            // Direction of Swipe

            float x = swipeDelta.x;

            swipeDeltaX = swipeDelta.x;
            swipeDeltaX = Mathf.Clamp(swipeDeltaX, -maxSwipeValue, maxSwipeValue);


            if (x < 0)
            {
                swipeLeft = true;
                swipeRight = false;
            }
            else if (x > 0)
            {
                swipeRight = true;
                swipeLeft = false;
            }
        }
        else
        {
            swipeLeft = false;
            swipeRight = false;
        }
    }

    private void Reset()
    {
        startTouch = swipeDelta = Vector2.zero;
        isDragging = false;
    }
}
