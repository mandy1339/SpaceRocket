using Assets;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleSpawner : MonoBehaviour
{
    private float spawnFrequency;
    private float timeLeftToGenerate = 0;
    [SerializeField] private GameObject gameManager;
    private GameManager gm;
    [SerializeField] GameObject meteoriteA;
    [SerializeField] GameObject meteoriteB;
    [SerializeField] GameObject meteoriteC;
    [SerializeField] GameObject miniMeteorites;
    private int _row;
    private int _col;
    SpaceObjectProperties _objProperties;
    private int _cycleNumber = 0;

    // Start is called before the first frame update
    void Start()
    {
        timeLeftToGenerate = spawnFrequency;
        gm = gameManager.GetComponent<GameManager>();
        spawnFrequency = gm.getSpawnFrequency();
        string[] rawPosition = this.gameObject.name.Split('/');
        _row = Int32.Parse(rawPosition[0]);
        _col = Int32.Parse(rawPosition[1]);
    }

    // Update is called once per frame
    void Update()
    {
        timeLeftToGenerate -= Time.deltaTime;
        if(timeLeftToGenerate <= 0 && !gm.IsGameOver())
        {
            //GameObject spcObj = Instantiate(meteoriteA, transform.position, Quaternion.identity);
            GameObject spcObj = gm.GetObjectForSpawner(_row-1 + _cycleNumber, _col-1, ref _objProperties, _row);
            if (spcObj != null)
            {
                spcObj.transform.position = this.transform.position;
                spcObj.GetComponent<MoveDown>().SetBaseSpeed(_objProperties.baseSpeed);
                spcObj.GetComponent<MoveDown>().ChangeSpeedBy(_objProperties.speedDifference);
                spcObj.GetComponent<MoveDown>().SetRotationTo(_objProperties.rotationSpeed);
            }
            timeLeftToGenerate = spawnFrequency;
            _cycleNumber++;
            //if (_cycleNumber > 60)
            //{
            //    gm.LevelCleared();
            //}
        }
    }


}


