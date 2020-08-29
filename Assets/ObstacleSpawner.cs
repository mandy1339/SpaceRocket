using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleSpawner : MonoBehaviour
{
    public float spawnFrequency = 5;
    public float timeLeftToGenerate = 0;
    [SerializeField] GameObject meteoriteA;
    [SerializeField] GameObject meteoriteB;
    [SerializeField] GameObject meteoriteC;
    [SerializeField] GameObject miniMeteorites;

    // Start is called before the first frame update
    void Start()
    {
        timeLeftToGenerate = spawnFrequency;
    }

    // Update is called once per frame
    void Update()
    {
        timeLeftToGenerate -= Time.deltaTime;
        if(timeLeftToGenerate <= 0)
        {
            GameObject spcObj = Instantiate(meteoriteA, transform.position, Quaternion.identity);
            timeLeftToGenerate = spawnFrequency;
        }
    }
}
