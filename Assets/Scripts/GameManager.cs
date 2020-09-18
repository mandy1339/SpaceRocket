using Assets;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;
using System.Runtime.InteropServices;

public class GameManager : MonoBehaviour
{
    private int _score;
    private bool _readyToShootLaser;
    private float _maxCoolDown = 8;
    [SerializeField] private float _spawnFrequency = 5;
    private float timeLeftToGenerate = 0;
    [SerializeField] private float timeMultiplier = 1;
    [SerializeField] private GameObject rocket;
    private Animator rocketAnimator;
    [SerializeField] private TMP_Text scoreText;
    [SerializeField] private TMP_Text gameOverScoreTextPTS;
    [SerializeField] private TMP_Text gameOverHighScoreTextPTS;
    [SerializeField] private TMP_Text winGameScoreTextPTS;
    [SerializeField] private TMP_Text winGameHighScoreTextPTS;
    [SerializeField] private TMP_Text text321;
    private int text321Int;
    private GameObject rocketSprite;
    private GameObject rocketFire;
    [SerializeField] private GameObject rocketExplosion;
    [SerializeField] private GameObject playUI;
    [SerializeField] private GameObject gameOverUI;
    [SerializeField] private GameObject ui321;
    [SerializeField] private GameObject gameStartUI;
    [SerializeField] private GameObject howToPlayUI;
    [SerializeField] private GameObject creditsUI;
    [SerializeField] private GameObject laserBar;
    [SerializeField] private Image redFillBar;
    [SerializeField] private GameObject redGlow;
    private GameObject planetEarthReference;         // this will be null until it's instantiated on level cleared
    [SerializeField] private GameObject planetEarth;    // wired prefab 
    [SerializeField] private GameObject winGameUI;

    private SoundManager sm;
    private bool _IsGameOver = false;
    private SpaceObjectProperties[,] _spawnerInfoArr_1 = new SpaceObjectProperties[130,10];          // for the first row of spawner game objects
    private SpaceObjectProperties[,] _spawnerInfoArr_2 = new SpaceObjectProperties[130,10];         // for the second row of spawner game objects

    [SerializeField] GameObject obst1;
    [SerializeField] GameObject obst2;
    [SerializeField] GameObject obst3;
    [SerializeField] GameObject obst4;
    [SerializeField] GameObject obst5;
    [SerializeField] GameObject meteoriteTest;
    [SerializeField] GameObject obst6;
    [SerializeField] GameObject obst7;

    List<GameObject> obst1List;
    List<GameObject> obst2List;
    List<GameObject> obst3List;
    List<GameObject> obst4List;
    List<GameObject> obst5List;
    List<GameObject> meteoriteTestList;
    List<GameObject> obst6List;
    List<GameObject> obst7List;

    public KeyValuePair<int, GameObject>[] arrayForMirrors1 = new KeyValuePair<int, GameObject>[11];       // index: the column of the spawner, key: int cycle #,  value: GameObject objReference for mirror
    public KeyValuePair<int, GameObject>[] arrayForMirrors2 = new KeyValuePair<int, GameObject>[10];       // int cycle #,  GameObject objReference for mirror

    // Start is called before the first frame update
    void Start()
    {
        // TODO DELETE THIS
        PlayerPrefs.SetInt("HighScore", 0);
        
        
        // Disable rocket controls
        rocket.GetComponent<Rocket>().enabled = false;
        rocketAnimator = rocket.GetComponent<Animator>();
        rocketAnimator.enabled = false;
        

        



        text321Int = 3;
        _readyToShootLaser = false;
        Time.timeScale = 0;
        _score = 0;
        timeLeftToGenerate = _spawnFrequency;
        gameStartUI.SetActive(true);
        gameOverUI.SetActive(false);
        howToPlayUI.SetActive(false);
        creditsUI.SetActive(false);
        winGameUI.SetActive(false);
        ui321.SetActive(false);
        sm = GameObject.FindGameObjectWithTag("sound_manager_tag").GetComponent<SoundManager>();
        sm.PlayMainMenuMusic();
        FillUpSpawnerInfoArr(_spawnerInfoArr_1);
        FillUpSpawnerInfoArr(_spawnerInfoArr_2);

        obst1List     = new List<GameObject>(120);
        obst2List     = new List<GameObject>(300);
        obst3List     = new List<GameObject>(300);
        obst4List     = new List<GameObject>(300);
        obst5List     = new List<GameObject>(400);
        meteoriteTestList  = new List<GameObject>(300);
        obst6List = new List<GameObject>(400);
        obst7List = new List<GameObject>(400);

        GameObject tempObj;

        for (int i = 0; i < obst1List.Capacity; i++)
        {
            tempObj = Instantiate(obst1);
            tempObj.SetActive(false);
            obst1List.Add(tempObj);
        }
        for (int i = 0; i < obst2List.Capacity; i++)
        {
            tempObj = Instantiate(obst2);
            tempObj.SetActive(false);
            obst2List.Add(Instantiate(tempObj));
        }
        for (int i = 0; i < obst3List.Capacity; i++)
        {
            tempObj = Instantiate(obst3);
            tempObj.SetActive(false);
            obst3List.Add(Instantiate(tempObj));
        }
        for (int i = 0; i < obst4List.Capacity; i++)
        {
            tempObj = Instantiate(obst4);
            tempObj.SetActive(false);
            obst4List.Add(Instantiate(tempObj));
        }for (int i = 0; i < obst5List.Capacity; i++)
        {
            tempObj = Instantiate(obst5);
            tempObj.SetActive(false);
            obst5List.Add(Instantiate(tempObj));
        }for (int i = 0; i < meteoriteTestList.Capacity; i++)
        {
            tempObj = Instantiate(meteoriteTest);
            tempObj.SetActive(false);
            meteoriteTestList.Add(Instantiate(tempObj));
        }for (int i = 0; i < obst6List.Capacity; i++)
        {
            tempObj = Instantiate(obst6);
            tempObj.SetActive(false);
            obst6List.Add(Instantiate(tempObj));
        }
        for (int i = 0; i < obst7List.Capacity; i++)
        {
            tempObj = Instantiate(obst7);
            tempObj.SetActive(false);
            obst7List.Add(Instantiate(tempObj));
        }


    }

    // ---------------
    // START OF COROUTINES
    // --------------------------
    IEnumerator Coroutine321()
    {
        while(text321Int >= 0)
        {
            if (text321Int == 0)
            {
                text321.text = "START";
                text321.color = Color.green;
                sm.PlayBackgroundMusic();
            }
            else
            {
                text321.text = text321Int.ToString();
                sm.PlayTimeTick();
            }
                
            
            text321Int--;
            if(text321Int == -1)
            {
                rocket.GetComponent<Rocket>().enabled = true;
            }
            yield return new WaitForSeconds(1.5f);
        }
        ui321.SetActive(false);
        yield return null;
    }

    IEnumerator CoroutineMoveRocketUp()
    {
        // y: -3.5
        while(Vector3.Distance(rocket.transform.position, new Vector3(rocket.transform.position.x, -3.5f, rocket.transform.position.z)) > 0.05f) 
        {
            rocket.transform.position = Vector3.Lerp(rocket.transform.position, new Vector3(rocket.transform.position.x, -3.5f, rocket.transform.position.z), 1f * Time.deltaTime);
            yield return null;
        }
        yield return null;
    }

    IEnumerator CoroutineFillRedBar()
    {
        float countDown = _maxCoolDown;       // maxCoolDown can be changed from outside that's why I cache it first to not affect the current cooldown calculations
        float initialCountDown = _maxCoolDown;
        while (countDown > 0)
        {
            countDown -= Time.deltaTime;
            redFillBar.fillAmount = (initialCountDown - countDown) / initialCountDown;
            yield return null;
        }
        _readyToShootLaser = true;
        redGlow.SetActive(true);
        yield return null;
    }

    IEnumerator CoroutineMovePlanetEarth()
    {
        while (Vector3.Distance(planetEarthReference.transform.position, new Vector3(0,-0.5f,0)) > 0.05f)
        {
            planetEarthReference.transform.position = Vector3.Lerp(planetEarthReference.transform.position, new Vector3(0, -.05f, 0), Time.deltaTime * .4f);
            yield return null;
        }
        yield return null;
    }

    IEnumerator CoroutineLerpRocketX0()
    {
        while(Vector3.Distance(rocket.transform.position, new Vector3(0, rocket.transform.position.y, rocket.transform.position.z)) > 0.05f)
        {
            rocket.transform.position = Vector3.Lerp(rocket.transform.position, new Vector3(0, rocket.transform.position.y, rocket.transform.position.z), 1f * Time.deltaTime);
            yield return null;
        }
        yield return null;
    }

    IEnumerator DiplayWinGameUIAfterShortDelay()
    {
        for (int i = 0; i < 2; i++)
        {
            if (i == 1)
            {
                winGameUI.SetActive(true);
            }
            else
            {
                yield return new WaitForSeconds(5);
            }
        }
        yield return null;
    }

    // ----------------------
    // END OF COROUTINES
    // ----------------





    void Update()
    {
//        if (!_IsGameOver)
//            Time.timeScale = timeMultiplier;

//#if UNITY_EDITOR
//        Time.timeScale = timeMultiplier;
//#endif

        timeLeftToGenerate -= Time.deltaTime;
        if(timeLeftToGenerate <= 0 && !_IsGameOver)
        {
            IncrementScore();
            timeLeftToGenerate = _spawnFrequency;
            if (_score >= 83)
            {
                LevelCleared();
            }
        }
    }



    public void IncrementScore()
    {
        if (Time.time > 5f && !_IsGameOver)
        {
            _score++;
            scoreText.text = _score.ToString();
        }
    }

    public float getSpawnFrequency()
    {
        return _spawnFrequency;
    }

    public bool isReadyToShootLaser()
    {
        return _readyToShootLaser;
    }

    public void GameOver()
    {
        if (!_IsGameOver)
        {
            sm.StopSpaceEngine();
            sm.StopBackgroundMusic();
            GameObject explosionParticles = Instantiate(rocketExplosion, rocket.transform.position, Quaternion.identity).transform.GetChild(0).gameObject;
            explosionParticles.GetComponent<ParticleSystem>().Play();
            sm.PlayExplosion();
            rocket.SetActive(false);
            gameOverUI.SetActive(true);
            playUI.SetActive(false);
            _IsGameOver = true;
            gameOverScoreTextPTS.text = _score.ToString();
            int historyHighScore = PlayerPrefs.GetInt("HighScore", 0);
            if (_score > historyHighScore)
            {
                PlayerPrefs.SetInt("HighScore", _score);
            }
            gameOverHighScoreTextPTS.text = PlayerPrefs.GetInt("HighScore", 0).ToString();
        }
    }

    public void LevelCleared()
    {
        rocket.GetComponent<CapsuleCollider2D>().enabled = false;   // you can't crash after you win the game if the collider is disabled
        sm.StopBackgroundMusic();
        sm.PlayWinGameSound();
        //winGameUI.SetActive(true);
        StartCoroutine(DiplayWinGameUIAfterShortDelay());
        rocketAnimator.enabled = true;
        planetEarthReference = Instantiate(planetEarth, new Vector3(0, 7.5f, 0), Quaternion.identity);
        // call coroutine to lerp rocket to x position 0
        StartCoroutine(CoroutineLerpRocketX0());
        // call coroutine to lerp down the earth
        StartCoroutine(CoroutineMovePlanetEarth());

        rocket.GetComponent<Rocket>().enabled = false;
        playUI.SetActive(false);
        _IsGameOver = true;
        winGameScoreTextPTS.text = _score.ToString();
        int historyHighScore = PlayerPrefs.GetInt("HighScore", 0);
        if (_score > historyHighScore)
        {
            PlayerPrefs.SetInt("HighScore", _score);
        }
        winGameHighScoreTextPTS.text = PlayerPrefs.GetInt("HighScore", 0).ToString();
    }

    public void OnClickRestartButton()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public GameObject GetObjectForSpawner(int row, int col, ref SpaceObjectProperties _objProperties, int spawnerRowNumber)
    {
        GameObject obj;
        if (spawnerRowNumber == 1)
        {
            SpaceObjectProperties spcObjProperties = _spawnerInfoArr_1[row, col];
            _objProperties = spcObjProperties;
            if (_objProperties != null && _objProperties.willSpawn == true)
            {
                obj = GetObjectForNumber(spcObjProperties.objNumber);
                AttachableSpcObjProperties propsAttachedToObj = obj.GetComponent<AttachableSpcObjProperties>();
                if (propsAttachedToObj == null) 
                {
                    propsAttachedToObj = obj.AddComponent<AttachableSpcObjProperties>();
                }
                propsAttachedToObj.objNumber = spcObjProperties.objNumber;
                propsAttachedToObj.baseSpeed = spcObjProperties.baseSpeed;
                propsAttachedToObj.speedDifference = spcObjProperties.speedDifference;
                propsAttachedToObj.rotationSpeed = spcObjProperties.rotationSpeed;
                propsAttachedToObj.willSpawn = spcObjProperties.willSpawn;
                propsAttachedToObj.mirrorObstacle = null;
                
                // if we already cached the mirror for the current cycle (key is cycle #)
                if (arrayForMirrors1[col].Key == row && arrayForMirrors1[col].Value != null) 
                {
                    // point this object to the mirror
                    propsAttachedToObj.mirrorObstacle = arrayForMirrors1[col].Value;
                    // point the mirror object to this object
                    arrayForMirrors1[col].Value.GetComponent<AttachableSpcObjProperties>().mirrorObstacle = obj;
                }
                else
                {
                    // cache this object so that you can make the link later when you get to the mirror
                    arrayForMirrors1[col] = new KeyValuePair<int, GameObject>(row, obj);
                }
            }
            else
            {
                obj = null;
            }
            
        }
        else if (spawnerRowNumber == 2)
        {
            SpaceObjectProperties spcObjProperties = _spawnerInfoArr_2[row, col];
            _objProperties = spcObjProperties;
            if (_objProperties != null && _objProperties.willSpawn == true)
            {
                obj = GetObjectForNumber(spcObjProperties.objNumber);
                AttachableSpcObjProperties propsAttachedToObj = obj.AddComponent<AttachableSpcObjProperties>();
                propsAttachedToObj.objNumber = spcObjProperties.objNumber;
                propsAttachedToObj.baseSpeed = spcObjProperties.baseSpeed;
                propsAttachedToObj.speedDifference = spcObjProperties.speedDifference;
                propsAttachedToObj.rotationSpeed = spcObjProperties.rotationSpeed;
                propsAttachedToObj.willSpawn = spcObjProperties.willSpawn;
                // if we already cached the mirror for the current cycle (key is cycle #)
                if (arrayForMirrors2[col].Key == row && arrayForMirrors2[col].Value != null)
                {
                    // point this object to the mirror
                    propsAttachedToObj.mirrorObstacle = arrayForMirrors2[col].Value;
                    // point the mirror object to this object
                    arrayForMirrors2[col].Value.GetComponent<AttachableSpcObjProperties>().mirrorObstacle = obj;
                }
                else
                {
                    // cache this object so that you can make the link later when you get to the mirror
                    arrayForMirrors2[col] = new KeyValuePair<int, GameObject>(row, obj);
                }
            }
            else
            {
                obj = null;
            }
        }
        else 
        {
            obj = null;
        }

        return obj;
    }

    private void FillUpSpawnerInfoArr(SpaceObjectProperties[,] arr)
    {
        // CYCLES 1 - 10
        for (int i = 0; i < 10; i++)
        {
            for (int j = 0; j < arr.GetLength(1); j++)
            {
                // TODO Randomly generate all of these except maybe objNumber
                SpaceObjectProperties tempObj = new SpaceObjectProperties();
                int objNumber = 1;
                float baseSpeed = 3;
                float speedDifference = Random.value * 2 - 1;   // float between -1 and 1
                float rotationSpeed = Random.value * 200 - 75;
                bool willSpawn = Random.value <= .42f ? true : false;
                tempObj.objNumber = objNumber;
                tempObj.baseSpeed = baseSpeed;
                tempObj.speedDifference = speedDifference;
                tempObj.rotationSpeed = rotationSpeed;
                tempObj.willSpawn = willSpawn;
                arr[i, j] = tempObj;
            }
        }

        // CYCLES 11 - 22
        for (int i = 10; i < 22; i++)
        {
            for (int j = 0; j < arr.GetLength(1); j++)
            {
                // TODO Randomly generate all of these except maybe objNumber
                SpaceObjectProperties tempObj = new SpaceObjectProperties();
                int objNumber = 2;
                float baseSpeed = 3.3f;
                float speedDifference = Random.value * 2 - 1;
                float rotationSpeed = Random.value * 200 - 75;
                bool willSpawn = Random.value <= .47f ? true : false;
                tempObj.objNumber = objNumber;
                tempObj.baseSpeed = baseSpeed;
                tempObj.speedDifference = speedDifference;
                tempObj.rotationSpeed = rotationSpeed;
                tempObj.willSpawn = willSpawn;
                arr[i, j] = tempObj;
            }
        } 
        
        // CYCLES 23 - 50
        for (int i = 22; i < 35; i++)
        {
            for (int j = 0; j < arr.GetLength(1); j++)
            {
                // TODO Randomly generate all of these except maybe objNumber
                SpaceObjectProperties tempObj = new SpaceObjectProperties();
                int objNumber = 3;
                float baseSpeed = 3.75f;
                float speedDifference = Random.value * 2 - 1; // float between -1 and 1
                float rotationSpeed = Random.value * 200 - 75;
                bool willSpawn = Random.value <= .53f ? true : false;
                tempObj.objNumber = objNumber;
                tempObj.baseSpeed = baseSpeed;
                tempObj.speedDifference = speedDifference;
                tempObj.rotationSpeed = rotationSpeed;
                tempObj.willSpawn = willSpawn;
                arr[i, j] = tempObj;
            }
        }

        // CYCLES 36 - 47
        for (int i = 35; i < 47; i++)
        {
            for (int j = 0; j < arr.GetLength(1); j++)
            {
                // TODO Randomly generate all of these except maybe objNumber
                SpaceObjectProperties tempObj = new SpaceObjectProperties();
                int objNumber = 4;
                float baseSpeed = 4f;
                float speedDifference = Random.value * 2.2f - 1; // float between -1 and 1
                float rotationSpeed = Random.value * 200 - 75;
                bool willSpawn = Random.value <= .46f ? true : false;
                tempObj.objNumber = objNumber;
                tempObj.baseSpeed = baseSpeed;
                tempObj.speedDifference = speedDifference;
                tempObj.rotationSpeed = rotationSpeed;
                tempObj.willSpawn = willSpawn;
                arr[i, j] = tempObj;
            }
        }

        // CYCLES 48 - 59
        for (int i = 47; i < 59; i++)
        {
            for (int j = 0; j < arr.GetLength(1); j++)
            {
                // TODO Randomly generate all of these except maybe o   bjNumber
                SpaceObjectProperties tempObj = new SpaceObjectProperties();
                int objNumber = 5;
                float baseSpeed = 4.6f;
                float speedDifference = Random.value * 2.2f - 1; // float between -1 and 1
                float rotationSpeed = Random.value * 200 - 75;
                bool willSpawn = Random.value <= .81f ? true : false;
                tempObj.objNumber = objNumber;
                tempObj.baseSpeed = baseSpeed;
                tempObj.speedDifference = speedDifference;
                tempObj.rotationSpeed = rotationSpeed;
                tempObj.willSpawn = willSpawn;
                arr[i, j] = tempObj;
            }
        }

        // CYCLES 60 - 71
        for (int i = 59; i < 71; i++)
        {
            for (int j = 0; j < arr.GetLength(1); j++)
            {
                // TODO Randomly generate all of these except maybe objNumber
                SpaceObjectProperties tempObj = new SpaceObjectProperties();
                int objNumber = 6;
                float baseSpeed = 4.95f;
                float speedDifference = Random.value * 2 - 1; 
                float rotationSpeed = Random.value * 200 - 75;
                bool willSpawn = Random.value <= .71f ? true : false;
                tempObj.objNumber = objNumber;
                tempObj.baseSpeed = baseSpeed;
                tempObj.speedDifference = speedDifference;
                tempObj.rotationSpeed = rotationSpeed;
                tempObj.willSpawn = willSpawn;
                arr[i, j] = tempObj;
            }
        }

        // CYCLES 71 - 83
        for (int i = 71; i < 83; i++)
        {
            for (int j = 0; j < arr.GetLength(1); j++)
            {
                // TODO Randomly generate all of these except maybe objNumber
                SpaceObjectProperties tempObj = new SpaceObjectProperties();
                int objNumber = 7;
                float baseSpeed = 5.7f;
                float speedDifference = Random.value * 2.1f - 1; 
                float rotationSpeed = Random.value * 200 - 75;
                bool willSpawn = Random.value <= .78f ? true : false;
                tempObj.objNumber = objNumber;
                tempObj.baseSpeed = baseSpeed;
                tempObj.speedDifference = speedDifference;
                tempObj.rotationSpeed = rotationSpeed;
                tempObj.willSpawn = willSpawn;
                arr[i, j] = tempObj;
            }
        }
    }


    private GameObject GetObjectForNumber(int n)
    {
        // TODO add all the game objects here
        GameObject obj;
        switch (n)
        {
            case 1:
                obj = ExtractObstacle("obst1");
                break;
            case 2:
                obj = ExtractObstacle("obst2");
                break;
            case 3:
                obj = ExtractObstacle("obst3");
                break;
            case 4:
                obj = ExtractObstacle("obst4");
                break;
            case 5:
                obj = ExtractObstacle("obst5");
                break;
            case 6:
                obj = ExtractObstacle("obst6");
                break;
            case 7:
                obj = ExtractObstacle("obst7");
                break;
                    
            
            
            default:
                obj = ExtractObstacle("meteoriteTest");
                break;
        }
        return obj;
    }

    public void StoreObstacle(GameObject obj)
    {
        string obstacleName = obj.name;
        if (obstacleName.StartsWith("obst1"))
        {
            obj.SetActive(false);
            obst1List.Add(obj);
        } else if (obstacleName.StartsWith("obst2")){
            obj.SetActive(false);
            obst2List.Add(obj);
        }else if (obstacleName.StartsWith("obst3"))
        {
            obj.SetActive(false);
            obst3List.Add(obj);
        }else if (obstacleName.StartsWith("obst4"))
        {
            obj.SetActive(false);
            obst4List.Add(obj);
        }else if (obstacleName.StartsWith("obst5"))
        {
            obj.SetActive(false);
            obst5List.Add(obj);
        }else if (obstacleName.StartsWith("meteoriteTest"))
        {
            obj.SetActive(false);
            meteoriteTestList.Add(obj);
        }else if (obstacleName.StartsWith("obst6"))
        {
            obj.SetActive(false);
            obst6List.Add(obj);
        }else if (obstacleName.StartsWith("obst7"))
        {
            obj.SetActive(false);
            obst7List.Add(obj);
        }
    }

    public GameObject ExtractObstacle(string obstacleName)
    {
        GameObject obj;
        switch (obstacleName)
        {
            case "obst1":
                obj = obst1List[0];
                obj.SetActive(true);
                obst1List.RemoveAt(0);
                return obj;
            case "obst2":
                obj = obst2List[0];
                obj.SetActive(true);
                obst2List.RemoveAt(0);
                return obj;
            case "obst3":
                obj = obst3List[0];
                obj.SetActive(true);
                obst3List.RemoveAt(0);
                return obj;
            case "obst4":
                obj = obst4List[0];
                obj.SetActive(true);
                obst4List.RemoveAt(0);
                return obj;
            case "obst5":
                obj = obst5List[0];
                obj.SetActive(true);
                obst5List.RemoveAt(0);
                return obj;
            case "meteoriteTest":
                obj = meteoriteTestList[0];
                obj.SetActive(true);
                meteoriteTestList.RemoveAt(0);
                return obj;
            case "obst6":
                obj = obst6List[0];
                obj.SetActive(true);
                obst6List.RemoveAt(0);
                return obj;
            case "obst7":
                obj = obst7List[0];
                obj.SetActive(true);
                obst7List.RemoveAt(0);
                return obj;
            default:
                return null;

        }
    }

    public void OnClickStartButton()
    {
        rocket.SetActive(true);
        StartCoroutine(CoroutineMoveRocketUp());
        StartCoroutine(CoroutineFillRedBar());
        sm.StopMainMenuMusic();
        sm.PlaySpaceEngine();
        Time.timeScale = 1;
        ui321.SetActive(true);
        playUI.SetActive(true);
        gameStartUI.SetActive(false);

        
        // do coroutine to enable rocket control after 3 2 1 is finished
        StartCoroutine(Coroutine321());
    }

    public void OnClickExitButton()
    {
        Application.Quit();
    }

    public void OnClickHowToPlayButton()
    {
        gameStartUI.SetActive(false);
        howToPlayUI.SetActive(true);
    }
    public void OnClickOKHowToPlayButton()
    {
        gameStartUI.SetActive(true);
        howToPlayUI.SetActive(false);
    }

    

    public void OnClickInfoButton()
    {
        gameStartUI.SetActive(false);
        creditsUI.SetActive(true);
    }
    public void OnClickOKCreditsButton()
    {
        gameStartUI.SetActive(true);
        creditsUI.SetActive(false);
    }



    public bool IsGameOver()
    {
        return _IsGameOver;
    }

    public void LaserCooldownStart()
    {
        _readyToShootLaser = false;
        redGlow.SetActive(false);
        StartCoroutine(CoroutineFillRedBar());
    }


}
