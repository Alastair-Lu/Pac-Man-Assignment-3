using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveGameManager : MonoBehaviour
{
    const string highScorePoints = "Points";
    const string highScoreTime = "Time";
    public UIManager uimanager;
    // Start is called before the first frame update
    void Awake()
    {
        int numManager = FindObjectsOfType<SaveGameManager>().Length;
        if (numManager != 1)
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
        uimanager.setBestTime(getBestTime());
        uimanager.setBestScore(getBestScore());
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void setScore(int score)
    {
        PlayerPrefs.SetInt(highScorePoints, score);
    }
    public int getBestScore()
    {
        return PlayerPrefs.GetInt(highScorePoints);
    }
    public void setTime(float time)
    {
        PlayerPrefs.SetFloat(highScoreTime, time);
    }
    public float getBestTime()
    {
        return PlayerPrefs.GetFloat(highScoreTime);
    }
}
