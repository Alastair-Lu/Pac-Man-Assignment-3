using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameStateManager : MonoBehaviour
{
    // Start is called before the first frame update
    public enum GameState { Start, LevelStart, Default, Scared, Dead, GameOver}
    public enum GhostState { Alive, Scared, Dead}
    public static int currentGameState = (int)GameState.Start;
    void Awake()
    {
        int numManager = FindObjectsOfType<UIManager>().Length;
        if (numManager != 1)
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
        
    }
    public static void setGameState(int state)
    {
        currentGameState = state;
    }
}
