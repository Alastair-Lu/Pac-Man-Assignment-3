using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ghost : MonoBehaviour
{
    public Animator animator;
    private float timer = 5;
    public int ghost = (int)GameStateManager.GhostState.Alive;
    private UIManager uimanager;
    private Vector3 initialPos;
    private bool[] checks = new bool[4];
    private Transform ghostPos;
    public Tweener tweener;
    private Vector3 targetPos = new Vector3(-0.5f, 3, 0);
    public GhostController ghostController;
    private int ghostdirection = 3;
    public AudioPlayer audioPlayer;
    void Start()
    {
        uimanager = FindObjectOfType<UIManager>();
        initialPos = this.gameObject.transform.position;
        ghostPos = this.gameObject.transform;
    }

    // Update is called once per frame
    void Update()
    {
        if (tweener.TweenDone())
        {
            checks[0] = Physics.Raycast(ghostPos.position, Vector3.up, 1f, ~3);
            checks[1] = Physics.Raycast(ghostPos.position, Vector3.right, 1f, ~3);
            checks[2] = Physics.Raycast(ghostPos.position, Vector3.down, 1f, ~3);
            checks[3] = Physics.Raycast(ghostPos.position, Vector3.left, 1f, ~3);
        }
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
                GameStateManager.setDeadGhost(GameStateManager.getDeadGhost() + 1);
            }
            if (timer <= 0)
            {
                ghost = (int)GameStateManager.GhostState.Alive;
                animator.SetTrigger("Revive");
                timer = 5;
                GameStateManager.setDeadGhost(GameStateManager.getDeadGhost() - 1);
                if (GameStateManager.getDeadGhost() == 0)
                {
                   // audioPlayer.PlayNormal();
                }
            }
        }
    }
    private void OnTriggerStay(Collider other)
    {
        if((GameStateManager.currentGameState == (int)GameStateManager.GameState.Default || GameStateManager.currentGameState == (int)GameStateManager.GameState.Scared) && ghost == (int)GameStateManager.GhostState.Alive && other.name.Equals("SpawnArea") && tweener.TweenDone())
        {
            Debug.Log("triggering");
            ghostController.TargetLocation(this.gameObject.transform.position, this.gameObject.transform.position+Vector3.up, checks, ghostdirection, this);
        }
    }
}
