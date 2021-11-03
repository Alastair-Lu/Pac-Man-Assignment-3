using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ghost : MonoBehaviour
{
    public Animator animator;
    private float timer = 5;
    public int ghost = (int)GameStateManager.GhostState.Alive;
    private UIManager uimanager;
    void Start()
    {
        uimanager = FindObjectOfType<UIManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if (GameStateManager.currentGameState == (int)GameStateManager.GameState.Scared && animator.GetCurrentAnimatorStateInfo(0).IsName("Shark"))
        {
            ghost = (int)GameStateManager.GhostState.Scared;
            animator.SetTrigger("PelletPickup");
        }
        if (GameStateManager.currentGameState != (int)GameStateManager.GameState.Scared && animator.GetCurrentAnimatorStateInfo(0).IsName("SharkReverting"))
        {
            ghost = (int)GameStateManager.GhostState.Alive;
            animator.SetTrigger("Alive");
        }
        if (ghost == (int)GameStateManager.GhostState.Dead)
        {
            timer -= Time.deltaTime;
            if (!animator.GetCurrentAnimatorStateInfo(0).IsName("SharkDead"))
            {
                animator.SetTrigger("Dead");
                uimanager.scoreValue += 300;
            }
            if (timer <= 0)
            {
                ghost = (int)GameStateManager.GhostState.Alive;
                animator.SetTrigger("Revive");
            }
        }
    }
}
