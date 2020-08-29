using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField] private GameObject rocket;
    private GameObject rocketSprite;
    private GameObject rocketFire;
    [SerializeField] private GameObject rocketExplosion;
    [SerializeField] private GameObject GameOverUI;
    private SoundManager sm;
    private bool _IsGameOver = false;

    // Start is called before the first frame update
    void Start()
    {
        GameOverUI.SetActive(false);
        sm = GameObject.FindGameObjectWithTag("sound_manager_tag").GetComponent<SoundManager>();
        sm.PlayBackgroundMusic();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void GameOver()
    {
        if (!_IsGameOver)
        {
            GameObject explosionParticles = Instantiate(rocketExplosion, rocket.transform.position, Quaternion.identity).transform.GetChild(0).gameObject;
            explosionParticles.GetComponent<ParticleSystem>().Play();
            sm.PlayExplosion();
            rocket.SetActive(false);
            GameOverUI.SetActive(true);
            _IsGameOver = true;
        }
    }

    public void OnClickRestartButton()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
