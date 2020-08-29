using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Parallax : MonoBehaviour
{
    public float paraSpeed = .5f;
    public float imageHeight;
    

    // Start is called before the first frame update
    void Start()
    {
        imageHeight = GetComponent<SpriteRenderer>().bounds.size.y;
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.down * Time.deltaTime * paraSpeed);
        if (this.transform.position.y <= -imageHeight)
        {
            this.transform.position = new Vector3(this.transform.position.x, this.transform.position.y + imageHeight * 2, this.transform.position.z);
        }
    }
}
