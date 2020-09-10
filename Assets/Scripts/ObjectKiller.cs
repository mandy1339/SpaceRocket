using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectKiller : MonoBehaviour
{
    [SerializeField] private GameObject gameManager;
    private GameManager gm;
    // Start is called before the first frame update
    void Start()
    {
        gm = gameManager.GetComponent<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("obstacle_tag"))
        {
            gm.StoreObstacle(collision.gameObject);
        }
        //Collider2D[] allColliders = new Collider2D[5];
        //collision.GetContacts(allColliders);
        //foreach (Collider2D collider in allColliders)
        //{
        //    if (collider.CompareTag("obstacle_tag"))
        //    {
        //        gm.StoreObstacle(collider.gameObject);
        //    }
        //}
    }



}
