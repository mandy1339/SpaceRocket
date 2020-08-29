using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rocket : MonoBehaviour
{
    private float _tiltValue;
    [SerializeField] private float rocketSideSpeed;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void FixedUpdate()
    {

        if(!Settings.getIsMotionControlOn())    // if motion controls is off use touch 
        {
            if (Input.touchCount > 0)
            {
                Touch touch = Input.GetTouch(0);
                if (touch.position.x < Screen.width / 2)
                {
                    transform.Translate(Vector2.left * Time.fixedDeltaTime * rocketSideSpeed);
                }
                else
                {
                    transform.Translate(Vector2.right * Time.fixedDeltaTime * rocketSideSpeed);
                }
            }
        }
        else  // use motion controls
        {
            _tiltValue = GyroscopeManager.GetXFlatTilt();
            if (_tiltValue < 0)
            {
                if (_tiltValue >= -15)
                {
                    transform.Translate(Vector2.left * Time.fixedDeltaTime * (Mathf.Log10(-_tiltValue + 1) * 2.5f), Space.World);  // if you change speedrange you should change 4.6
                }
                else if (_tiltValue < 15)
                {
                    // move left at max speed allowed
                    transform.Translate(Vector2.left * Time.fixedDeltaTime * 3.01f, Space.World);    //log(11) * 4.6 = 4.79
                }
                else
                {
                    // don't move
                }
            }
            if (_tiltValue > 0)
            {
                //transform.Rotate(Vector3.up, Time.fixedDeltaTime * angularSpeed * tiltValue);
                if (_tiltValue <= 15)
                {
                    // move right slowly
                    // transform.Translate(Vector2.right * Time.fixedDeltaTime * _tiltValue * speedRange / 11);
                    transform.Translate(Vector2.left * Time.fixedDeltaTime * -(Mathf.Log10(_tiltValue + 1) * 2.5f), Space.World);  // if you change speedrange you should change 4.6
                }
                else if (_tiltValue > 15)
                {
                    // move right at max speed allowed
                    transform.Translate(Vector2.right * Time.fixedDeltaTime * 3.01f, Space.World);
                }
                else
                {
                    // don't move
                }
            }
        }

        //transform.Rotate(Vector3.forward * _tiltValue * Time.fixedDeltaTime * -1);
        float cappedTiltValue = _tiltValue;
        if(_tiltValue < -15)
        {
            cappedTiltValue = -15;
        }
        else if(_tiltValue > 15)
        {
            cappedTiltValue = 15;
        }
        transform.eulerAngles = new Vector3(0,0, -cappedTiltValue * 1.2f);
    }
}
