using System.Collections;
using System.Collections.Generic;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine;
using UnityEngine.UI;
using System;

public class CameraMove : MonoBehaviour
{
    UnityEvent mouseDownEvent, mouseUpEvent;
    // Start is called before the first frame update
    private Vector3 start, end;
    void Start()
    {
        if (mouseDownEvent == null)
            mouseDownEvent = new UnityEvent();



        mouseDownEvent.AddListener(downListener);


        if (mouseUpEvent == null)
            mouseUpEvent = new UnityEvent();

        mouseUpEvent.AddListener(upListener);
    }
    void downListener() {
        start = Input.mousePosition;
    }
    void upListener() {
        end = Input.mousePosition;
        var difference = end - start;
        int step = 0;
        float rotateValue = 10.0f;
        if (difference.x > step)
        {
            transform.Rotate(0.0f, rotateValue, 0.0f, Space.World);
        }
        else
        {
            transform.Rotate(0.0f, -rotateValue, 0.0f, Space.World);

        }
        if (difference.y > step)
        {
            transform.Rotate(rotateValue, 0.0f, 0.0f, Space.World);
        }
        else
        {
            transform.Rotate(-rotateValue, 0.0f, 0.0f, Space.World);

        }
    }
    // Update is called once per frame
    void Update()
    {
            if (Input.GetMouseButtonDown(0) && mouseDownEvent != null)
        {
            mouseDownEvent.Invoke();}



        if (Input.GetMouseButtonUp(0) && mouseDownEvent != null)
        {
            mouseUpEvent.Invoke();


        }
    }
}
