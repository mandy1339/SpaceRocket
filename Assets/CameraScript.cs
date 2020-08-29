using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScript : MonoBehaviour
{
    [SerializeField] private GameObject rocket;
    private Transform rocketTransform;
    // Start is called before the first frame update
    void Start()
    {
        rocketTransform = rocket.transform;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void LateUpdate()
    {
        this.transform.position = new Vector3(rocket.transform.position.x, this.transform.position.y, this.transform.position.z);

    }
}
