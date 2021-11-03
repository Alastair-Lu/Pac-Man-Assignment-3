using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public static float timerValue;
    private float ghostTimer = 10;
    private Text timer;
    public Text score;
    private Text scaredTimer;
    public int scoreValue = 0;
    private float startTimer = 3;
    private Text startTimerTxt;
    private Image[] life;
    private Image[] resetLife;
    public int lifeCount = 3;
    private int lifeImage = 3;
    public int pelletCount = 0;
    public int totalPelletCount;
    public Text bestTimeTxt;
    public Text bestScoreTxt;
    private float bestTime;
    private int bestScore;
    public SaveGameManager saveGM;
    // Start is called before the first frame update
    void Awake()
    {
        int numManager = FindObjectsOfType<UIManager>().Length;
        if(numManager != 1)
        {
            Destroy(this);
        }
        else
        {
            DontDestroyOnLoad(this);
        }
    }
    void Start()
    {
        bestTimeTxt.text = displayTime(bestTime);
        bestScoreTxt.text = bestScore.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log(GameStateManager.currentGameState);
        if (GameStateManager.currentGameState == (int)GameStateManager.GameState.Default || GameStateManager.currentGameState == (int)GameStateManager.GameState.Scared)
        {
            //Debug.Log(timerValue);
            timerValue += Time.deltaTime;
            timer.text = displayTime(timerValue);
            score.text = scoreValue.ToString();
            if (lifeCount != lifeImage)
            {
                Destroy(life[lifeCount].gameObject);
                lifeImage -= 1;
            }
            if (lifeCount == 0)
            {
                GameStateManager.setGameState((int)GameStateManager.GameState.GameOver);
                pelletCount = 0;
            }
        }

        if (GameStateManager.currentGameState == (int)GameStateManager.GameState.Scared)
        {            
            scaredTimer.enabled = true;
            if (ghostTimer >= 0)
            {
                ghostTimer -= Time.deltaTime;
                displayScaredTime(ghostTimer);
            }
            else
            {
                GameStateManager.setGameState((int)GameStateManager.GameState.Default);
                scaredTimer.enabled = false;
                ghostTimer = 10;
            }
        }
        if(GameStateManager.currentGameState == (int)GameStateManager.GameState.LevelStart)
        {
            startTimer -= Time.deltaTime;
            if(startTimer >= 0)
            {
                startTimerTxt.text = ((int)startTimer + 1).ToString();
            }
            else if (startTimer >= -1)
            {
                startTimerTxt.text = "GO!";
            }
            else
            {
                startTimerTxt.enabled = false;
                startTimer = 3;
                GameStateManager.setGameState((int)GameStateManager.GameState.Default);
            }
        }
        if(pelletCount == totalPelletCount && (GameStateManager.currentGameState == (int)GameStateManager.GameState.Default || GameStateManager.currentGameState == (int)GameStateManager.GameState.Scared))
        {
            GameStateManager.setGameState((int)GameStateManager.GameState.GameOver);
            pelletCount = 0;
        }
        if(GameStateManager.currentGameState == (int)GameStateManager.GameState.GameOver)
        {
            if (saveGM.getBestTime() < timerValue)
            {
                saveGM.setTime(timerValue);
                bestTime = timerValue;
            }
            if (saveGM.getBestScore() < scoreValue)
            {
                Debug.Log(saveGM.getBestScore());
                saveGM.setScore(scoreValue);
                bestScore = scoreValue;
            }
            startTimerTxt.enabled = true;
            startTimerTxt.text = "GameOver";
            startTimer -= Time.deltaTime;
            if (startTimer <= 0)
            {
                QuitToStart();
            }
            
        }
    }
    public void LoadFirstLevel()
    {
        if (SceneManager.GetActiveScene().buildIndex == 0)
        {
            SceneManager.LoadSceneAsync(1,LoadSceneMode.Single);
            SceneManager.sceneLoaded += OnsceneLoaded;
        }
    }

    public void OnsceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if(scene.buildIndex == 1)
        {
            Button button = GameObject.FindGameObjectWithTag("QuitButton").GetComponent<Button>();
            button.onClick.AddListener(QuitToStart);
            timer = GameObject.FindGameObjectWithTag("Timer").GetComponent<Text>();
            score = GameObject.FindGameObjectWithTag("Value").GetComponent<Text>();
            scaredTimer = GameObject.FindGameObjectWithTag("Scared Timer").GetComponent<Text>();
            scaredTimer.enabled = false;
            startTimerTxt = GameObject.Find("StartTimer").GetComponent<Text>();
            GameStateManager.currentGameState = (int)GameStateManager.GameState.LevelStart;
            life = GameObject.Find("Lives").GetComponentsInChildren<Image>();
            resetLife = life;
            startTimerTxt.enabled = true;
            SceneManager.sceneLoaded -= OnsceneLoaded;
        }
        if(scene.buildIndex == 0)
        {
            pelletCount = 0;
            startTimer = 3;
            lifeCount = 3;
            lifeImage = 3;
            scoreValue = 0;
            life = resetLife;
            timerValue = 0;
            Button button = GameObject.FindGameObjectWithTag("Level 1").GetComponent<Button>();
            button.onClick.AddListener(LoadFirstLevel);
            bestScoreTxt = GameObject.Find("BestScoreValue").GetComponent<Text>();
            bestTimeTxt = GameObject.Find("BestTimeValue").GetComponent<Text>();
            bestTimeTxt.text = displayTime(bestTime);
            bestScoreTxt.text = bestScore.ToString();
            SceneManager.sceneLoaded -= OnsceneLoaded;
        }
    }
    public string displayTime(float time)
    {
        float minute = Mathf.FloorToInt(time / 60);
        float second = Mathf.FloorToInt(time % 60);
        float microsecond = Mathf.FloorToInt(time * 100 % 100);
        return string.Format("{0:00}:{1:00}:{2:00}", minute, second, microsecond);
    }
    public void displayScaredTime(float time)
    {
        float second = Mathf.FloorToInt(time % 60) + 1;
        scaredTimer.text = string.Format("{0:00}", second);
    }

    public void QuitToStart()
    {
        SceneManager.LoadSceneAsync(0,LoadSceneMode.Single);
        SceneManager.sceneLoaded += OnsceneLoaded;
        GameStateManager.setGameState((int)GameStateManager.GameState.Start);
    }
    public void setBestTime(float time)
    {
        bestTime = time;
    }
    public void setBestScore(int score)
    {
        bestScore = score;
    }
}
