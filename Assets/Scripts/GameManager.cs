using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField] private GameObject EnemySpawner;
    [SerializeField] private GameObject[] Enemies = new GameObject[3];
    [SerializeField] private float SpawnTimer;
    private float timerMax;
    private int lives = 3;
    [SerializeField] private TMP_Text LivesText;
    [SerializeField] GameObject GameOverScreen;
    private PlayerController playerController;
    // Start is called before the first frame update
    void Start()
    {
        playerController = FindObjectOfType<PlayerController>();
        timerMax = SpawnTimer;
        GameOverScreen.SetActive(false);
    }

    void SpawnEnemy()
    {
        float offset = Random.Range(-4f, 5f);
        Vector2 spawnPos = new Vector2(EnemySpawner.transform.position.x, EnemySpawner.transform.position.y + offset);
        int enemyChoice = Random.Range(0, 3);
        Instantiate(Enemies[enemyChoice], spawnPos, Quaternion.identity);
    }

    public void LoseLife()
    {
        lives--;
        LivesText.text = "Lives: " + lives.ToString();
        switch (lives)
        {
            case 0:
                GameOver();
                break;
        }
    }
    public void Play()
    {
        SceneManager.LoadScene(0);
    }
    void GameOver()
    {
        GameOverScreen.SetActive (true);
        playerController.StopListening();
        playerController.enabled = false;
    }
    // Update is called once per frame
    void Update()
    {
        SpawnTimer -= Time.deltaTime;
        if (SpawnTimer < 0)
        {
            SpawnEnemy();
            SpawnTimer = timerMax;
        }
    }
}
