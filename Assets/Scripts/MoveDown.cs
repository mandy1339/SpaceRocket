using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveDown : MonoBehaviour
{
    [SerializeField] private float _speed;
    [SerializeField] private float _rotateSpeed;

    public void SetBaseSpeed(float n)
    {
        this._speed = n;
    }
    public float GetBaseSpeed()
    {
        return _speed;
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // move down
        transform.position += (Vector3.down * _speed * Time.deltaTime);

        // rotate
        //transform.RotateAround(Vector3.forward, _rotateSpeed * Time.deltaTime);
        transform.Rotate(Vector3.forward, _rotateSpeed * Time.deltaTime);
    }

    public void ChangeSpeedBy(float amt)
    {
        this._speed += amt;
    }

    public void SetRotationTo(float amt)
    {
        this._rotateSpeed = amt;
    }

    public float GetRotation()
    {
        return this._rotateSpeed;
    }
}
