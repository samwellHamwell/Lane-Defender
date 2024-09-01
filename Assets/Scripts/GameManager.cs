using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField] private GameObject[] Enemies = new GameObject[3];
    [SerializeField] private GameObject[] SpawnPoints = new GameObject[5];
    [SerializeField] private TMP_Text Scoretext;
    [SerializeField] private TMP_Text HighScoretext;
    [SerializeField] private TMP_Text BigScoretext;
    [SerializeField] private TMP_Text BigHighScoreText;
    private int Score;
    private int HighScore = 0;
    [SerializeField] private float SpawnTimer;
    private float timerMax;
    private int lives = 3;
    [SerializeField] private TMP_Text LivesText;
    [SerializeField] GameObject GameOverScreen;
    [SerializeField] GameObject InGameUI;
    private PlayerController playerController;
    SoundManager SM;
    // Start is called before the first frame update
    void Start()
    {
        playerController = FindObjectOfType<PlayerController>();
        timerMax = SpawnTimer;
        GameOverScreen.SetActive(false);
        HighScore = PlayerPrefs.GetInt("HighScore");
        HighScoretext.text = "High Score: " + HighScore.ToString();
        SM = FindObjectOfType<SoundManager>();
    }
    public void UpdateScore(int scoreAdd)
    {
        Score += scoreAdd;
        Scoretext.text = "Score: " + Score.ToString();
    }
    void SpawnEnemy()
    {
        int offset = Random.Range(0, 5);
        Transform spawnPos = SpawnPoints[offset].transform;
        int enemyChoice = Random.Range(0, 3);
        Instantiate(Enemies[enemyChoice], spawnPos);
    }
    public void LoseLife()
    {
        if (lives > 0)
        {
            lives--;
            AudioSource.PlayClipAtPoint(SM.LifeLost1, gameObject.transform.position);
            LivesText.text = "Lives: " + lives.ToString();
            if(lives == 0)
            {
                GameOver();
            }
        }
    }
    public void Play()
    {
        SceneManager.LoadScene(0);
    }
    void GameOver()
    {
        GameOverScreen.SetActive (true);
        InGameUI.SetActive(false);
        playerController.StopListening();
        playerController.enabled = false;
        if(Score > HighScore)
        {
            //Saves the highscore key
            PlayerPrefs.SetInt("HighScore", Score);
            //Sets the variable to the key
            HighScore = PlayerPrefs.GetInt("HighScore");
            BigHighScoreText.GetComponent<RectTransform>().localEulerAngles = new Vector3 (0, 0, 10);
        }
        BigScoretext.text = "Score: " + Score.ToString();
        BigHighScoreText.text = "HighScore: " + HighScore.ToString();
    }
    void OnRestart()
    {
        SceneManager.LoadScene(0);
    }
    void OnQuit()
    {
        Quit();
    }
    public void Quit()
    {
        Application.Quit();
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
