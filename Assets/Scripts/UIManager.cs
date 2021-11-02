using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public static float timerValue;
    private bool runTimer = false;
    private Text timer;
    public Text score;
    public int scoreValue = 0;
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

    // Update is called once per frame
    void Update()
    {
        if (runTimer)
        {
            //Debug.Log(timerValue);
            timerValue += Time.deltaTime;
            displayTime(timerValue);
            score.text = scoreValue.ToString();
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
            runTimer = true;
            SceneManager.sceneLoaded -= OnsceneLoaded;
        }
        if(scene.buildIndex == 0)
        {
            runTimer = false;
            timerValue = 0;
            Button button = GameObject.FindGameObjectWithTag("Level 1").GetComponent<Button>();
            button.onClick.AddListener(LoadFirstLevel);
            SceneManager.sceneLoaded -= OnsceneLoaded;
        }
    }
    public void displayTime(float time)
    {
        float minute = Mathf.FloorToInt(time / 60);
        float second = Mathf.FloorToInt(time % 60);
        float microsecond = Mathf.FloorToInt(time * 100 % 100);
        timer.text = "Time " + string.Format("{0:00}:{1:00}:{2:00}", minute, second, microsecond);
    }

    public void QuitToStart()
    {
        SceneManager.LoadSceneAsync(0,LoadSceneMode.Single);
        SceneManager.sceneLoaded += OnsceneLoaded;
    }
}
