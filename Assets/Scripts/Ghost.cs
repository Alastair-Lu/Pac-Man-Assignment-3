using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ghost : MonoBehaviour
{
    public Animator animator;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (GameStateManager.currentGameState == (int)GameStateManager.GameState.Scared)
        {
            animator.SetTrigger("PelletPickup");
        }
    }
}
