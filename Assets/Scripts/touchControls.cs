using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class touchControls : MonoBehaviour
{
    private Touch theTouch;
    private float timeTouchEnded;
    public float sensitivity = 13;

    void Start()
    {
        
    }

 
    void Update()
    {
        Movement();
        checkPosition();
    }

    void Movement()
    {
        if (Input.touchCount > 0)
        {
            theTouch = Input.GetTouch(0);
            if (theTouch.phase == TouchPhase.Began)
            {
                //print("dokundu");
                // print(theTouch.position);
            }
            if (theTouch.phase == TouchPhase.Stationary || theTouch.phase == TouchPhase.Moved)
            {
                // transform.position.x += (thetTouch.deltaPosition.magnitude / Time.deltaTime)
                //print(theTouch.deltaPosition.x / Screen.width);
                if (theTouch.deltaPosition.x < 0)
                {
                    if (transform.position.x > -4.75)
                    {
                        transform.position += new Vector3((theTouch.deltaPosition.x / Screen.width * sensitivity), 0, 0);
                    }
                    else
                    {
                        transform.position = new Vector3(-4.75f, transform.position.y, transform.position.z);
                    }
                }
                else
                {
                    if (transform.position.x < 4.75f)
                    {
                        transform.position += new Vector3((theTouch.deltaPosition.x / Screen.width * sensitivity), 0, 0);
                    }
                    else
                    {
                        transform.position = new Vector3(4.75f, transform.position.y, transform.position.z);
                    }
                }
            }
            if (theTouch.phase == TouchPhase.Ended)
            {
                //print("dokunma bitti");
            }
        }
    }

    void checkPosition()
    {
        if (transform.position.x > 4.75f)
        {
            transform.position = new Vector3(4.75f, transform.position.y, transform.position.z);
        }
        else if(transform.position.x < -4.75f)
        {
            transform.position = new Vector3(-4.75f, transform.position.y, transform.position.z);
        }
    }
}