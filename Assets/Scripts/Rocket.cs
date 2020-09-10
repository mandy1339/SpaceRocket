using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Rocket : MonoBehaviour
{
    private float _tiltValue;
    [SerializeField] private float rocketSideSpeed;
    private SoundManager sm;
    [SerializeField] private GameObject laser;
    private List<GameObject> containerOfLasers = new List<GameObject>(2);
    private Transform rocketGunTransform;
    private GameManager gm;
    public static event Action<Rocket> OnRocketLoopRight = delegate { };
    public static event Action<Rocket> OnRocketLoopLeft = delegate { };
    [SerializeField] private float rotationLerpFactor = 10;


    // Start is called before the first frame update
    void Start()
    {
        sm = GameObject.FindGameObjectWithTag("sound_manager_tag").GetComponent<SoundManager>();
        rocketGunTransform = this.transform.GetChild(2);
        gm = GameObject.FindGameObjectWithTag("game_manager_tag").GetComponent<GameManager>();
        GameObject tempLaser = Instantiate(laser);
        tempLaser.SetActive(false);
        containerOfLasers.Add(tempLaser);
        tempLaser = Instantiate(laser);
        tempLaser.SetActive(false);
        containerOfLasers.Add(tempLaser);
    }

    // Update is called once per frame
    void Update()
    {
        // SIDE LOOPING LOGIC
        if (this.transform.position.x > 5)
        {
            this.transform.position = new Vector3(this.transform.position.x - 10, this.transform.position.y, this.transform.position.z);
            // FIRE EVENT FOR LOOPING ON THE RIGHT
            OnRocketLoopRight(this);
        }
        else if (this.transform.position.x < -5)
        {
            this.transform.position = new Vector3(this.transform.position.x + 10, this.transform.position.y, this.transform.position.z);
            // FIRE EVENT FOR LOOPING ON THE LEFT
            OnRocketLoopLeft(this);
        }

        // LASER SHOOT LOGIC
        if (Input.GetMouseButtonDown(0))
        {
            if(gm.isReadyToShootLaser() && !gm.IsGameOver())
            {
                sm.PlayLaserSound();
                //Instantiate(laser, rocketGunTransform.position, this.transform.rotation);
                GameObject extractedLaser = containerOfLasers[0];
                containerOfLasers.RemoveAt(0);
                extractedLaser.SetActive(true);
                extractedLaser.transform.position = rocketGunTransform.position;
                extractedLaser.transform.rotation = rocketGunTransform.rotation;
                gm.LaserCooldownStart();
            }
        }
    }

    public void StoreLaser(GameObject lzr)
    {
        this.containerOfLasers.Add(lzr);
        lzr.SetActive(false);
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

        // HANDLE ROCKET ROTATION
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
        // transform.eulerAngles = new Vector3(0,0, -cappedTiltValue * 1.2f);
        transform.eulerAngles = new Vector3(0, 0, Mathf.LerpAngle(transform.eulerAngles.z, -cappedTiltValue * 1.2f, rotationLerpFactor * Time.deltaTime) );
    }

}
