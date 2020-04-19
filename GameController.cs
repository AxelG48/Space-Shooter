using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    public GameObject[] hazards;
    public Vector3 spawnValues;
    public int hazardCount;
    public float spawnWait;
    public float startWait;
    public float waveWait;

    public Text ScoreText;
    private int score;

    public Text restartText;
    public Text gameOverText;
    public Text winText;

    private bool gameOver;
    private bool restart;

    public ParticleSystem starfield0;
    public ParticleSystem starfield1;
    public MeshCollider player;
    public GameObject background;
    private BGScroller scroller;

    public AudioClip winAudio;
    public AudioClip loseAudio;
    public AudioSource audioSource;
    public float timer = 6;
    public Text timerText;
    public GameObject powerup;
    void Start()
    {
        gameOver = false;
        restart = false;
        restartText.text = "";
        gameOverText.text = "";
        winText.text = "";
        scroller = background.GetComponent<BGScroller>();
        audioSource = GetComponent<AudioSource>();
        score = 0;
        UpdateScore();
        StartCoroutine(SpawnWaves());
    }
    private void Update()
    {
        timer -= Time.deltaTime;
        timerText.text = "Time Left: " + timer.ToString("##");
        if (timer <= 0)
        {
            timerText.text = "Time Left: 0";
            if (gameOver == false)
            {
                GameOver();
            }
        }
        if (restart)
        {
            if (Input.GetKeyDown(KeyCode.F))
               {
                SceneManager.LoadScene("SampleScene");
            }
        }
        if (Input.GetKeyDown(KeyCode.Escape))
            {
            Application.Quit();
        }
    }

    IEnumerator SpawnWaves()
    {
        yield return new WaitForSeconds(startWait);

        while (true)
        {
            Instantiate(powerup, new Vector3(Random.Range(-spawnValues.x, spawnValues.x), spawnValues.y, spawnValues.z), Quaternion.identity);
            for (int i = 0; i < hazardCount; i++)
            {
                GameObject hazard = hazards[Random.Range(0, hazards.Length)];
                Vector3 spawnPosition = new Vector3(Random.Range(-spawnValues.x, spawnValues.x), spawnValues.y, spawnValues.z);
                Quaternion spawnRotation = Quaternion.identity;
                Instantiate(hazard, spawnPosition, spawnRotation);
                yield return new WaitForSeconds(spawnWait);
            }
            yield return new WaitForSeconds(waveWait);
            if (gameOver)
            {
                restartText.text = "Press 'F' for Restart";
                restart = true;
                break;
            }
        }
    }

    public void AddScore(int newScoreValue)
    {
        score += newScoreValue;
        UpdateScore();
    }

    void UpdateScore()
    {
        ScoreText.text = "Points: " + score;
        if(score >= 100)
        {
            winText.text = "GAME CREATED BY ALEXANDER GRANT";
            var main0 = starfield0.main;
            main0.simulationSpeed = 100f;
            var main1 = starfield1.main;
            main1.simulationSpeed = 100;
            player.enabled = false;
            scroller.scrollSpeed = -10;
            audioSource.clip = winAudio;
            audioSource.Play();
            audioSource.volume = 1;
            gameOver = true;
            restart = true;
        }
    }
  
    public void GameOver ()
    {
        audioSource.clip = loseAudio;
        audioSource.Play();
        gameOverText.text = "Game Over!";
        gameOver = true;
    }
    public void AddTime(int timeadded)
    {
        timer += timeadded;
    }
}