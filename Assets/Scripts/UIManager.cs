using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    bool sceneLoaded = false;
    // Start is called before the first frame update
    void Start()
    {
        DontDestroyOnLoad(this);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void LoadFirstLevel()
    {
        if (SceneManager.GetActiveScene().buildIndex == 0)
        {
            SceneManager.LoadSceneAsync(1);
            SceneManager.sceneLoaded += OnsceneLoaded;
        }
    }

    public void OnsceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if(scene.buildIndex == 1)
        {
            Button button = GameObject.FindGameObjectWithTag("QuitButton").GetComponent<Button>();
            button.onClick.AddListener(QuitToStart);
        }
    }

    public void QuitToStart()
    {
        SceneManager.LoadSceneAsync(0);
        SceneManager.sceneLoaded += OnsceneLoaded;
    }
}
