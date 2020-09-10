using Assets;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Laser : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 13;
    private GameManager gm;
    private SoundManager sm;
    private Rocket rocket;
    private Vector3 direction;

    private void Awake()
    {
        rocket = FindObjectOfType<Rocket>();
        Rocket.OnRocketLoopLeft += HandleRocketLoopingLeft;
        Rocket.OnRocketLoopRight += HandleRocketLoopingRight;
    }
    private void HandleRocketLoopingLeft(Rocket r)
    {
        this.transform.Translate(Vector3.right * 10);
    }
    private void HandleRocketLoopingRight(Rocket r)
    {
        this.transform.Translate(Vector3.left * 10);
    }

    void Start()
    {
        gm = GameObject.FindGameObjectWithTag("game_manager_tag").GetComponent<GameManager>();
        sm = GameObject.FindGameObjectWithTag("sound_manager_tag").GetComponent<SoundManager>();
        direction = Vector3.up;
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(direction * Time.deltaTime * moveSpeed, Space.Self);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("obstacle_tag"))
        {
            collision.gameObject.GetComponent<Obstacle>()?.Explode();
            gm.StoreObstacle(collision.gameObject);
            if (collision.gameObject.GetComponent<AttachableSpcObjProperties>().mirrorObstacle != null)
            {
                gm.StoreObstacle(collision.gameObject.GetComponent<AttachableSpcObjProperties>().mirrorObstacle); // store the mirror too
            }
            sm.PlayExplosionObstSound();
            rocket.StoreLaser(this.gameObject);
        }
    }

    private void OnBecameInvisible()
    {
        rocket.StoreLaser(this.gameObject);
    }
    private void OnDestroy()
    {
        Rocket.OnRocketLoopLeft -= HandleRocketLoopingLeft;
        Rocket.OnRocketLoopRight -= HandleRocketLoopingRight;
    }
}
