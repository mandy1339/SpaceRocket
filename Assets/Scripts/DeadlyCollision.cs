using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeadlyCollision : MonoBehaviour
{
    [SerializeField] private GameManager gm;
    // Start is called before the first frame update
    void Start()
    {
        gm = GameObject.FindGameObjectWithTag("game_manager_tag").GetComponent<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.collider.CompareTag("rocket_tag"))
        {
            gm.GameOver();
        }
    }
}
