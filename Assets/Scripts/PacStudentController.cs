using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PacStudentController : MonoBehaviour
{
    private static int lastInput = 0;
    private static int currentInput = 0;
    public Tweener tweener;
    public Animator animator;
    public LevelGenerator grid;
    public GameObject sub;
    private Vector3 check;
    private Vector3 forwardCheck;
    public ParticleSystem bubbles;
    private UIManager uimanager;
    private Vector3 initialPos;
    public AudioSource audioSource;
    private float deadTimer = 3;
    public AudioSource collect;
    public AudioPlayer audioPlayer;
    public AudioSource bump;
    private int bumpAmount = 0;
    // Start is called before the first frame update
    void Start()
    {
        uimanager = FindObjectOfType<UIManager>();
        initialPos = sub.transform.position;
    }    
    // Update is called once per frame
    void Update()
    {
        if(GameStateManager.currentGameState != (int)GameStateManager.GameState.Dead && GameStateManager.currentGameState != (int)GameStateManager.GameState.GameOver && GameStateManager.currentGameState != (int)GameStateManager.GameState.LevelStart)
        {
            if (Input.GetKeyDown(KeyCode.A))
            {
                lastInput = 1;
                check = Vector3.left;
            }
            if (Input.GetKeyDown(KeyCode.W))
            {
                lastInput = 2;
                check = Vector3.up;
            }
            if (Input.GetKeyDown(KeyCode.D))
            {
                lastInput = 3;
                check = Vector3.right;
            }
            if (Input.GetKeyDown(KeyCode.S))
            {
                lastInput = 4;
                check = Vector3.down;
            }
        }
        else
        {
            lastInput = 0;
            currentInput = 0;
        }

        if (lastInput != currentInput)
        {
            bumpAmount = 0;
        }
        if (tweener.TweenDone())
        {
            if (!(Physics.Raycast(this.transform.position, check, 1f, ~3)) && lastInput != currentInput)
            {
                animator.speed = 1;
                AddMovement(lastInput);
                bubbles.Play();
                audioSource.Play();
            }
            else
            {
                if (!Physics.Raycast(this.transform.position,forwardCheck,1f,~3) && currentInput != 0)
                {
                    animator.speed = 1;
                    AddMovement(currentInput);
                    bubbles.Play();
                }
                else if (GameStateManager.currentGameState != (int)GameStateManager.GameState.Dead)
                {
                    currentInput = 0;
                    lastInput = 0;
                    audioSource.Stop();
                    animator.speed = 0;
                    bubbles.Stop();
                   /* if (!bump.isPlaying && bumpAmount == 0)
                    {
                        bump.Play();
                        bumpAmount += 1;
                    }*/
                }
            }
        }
        if(GameStateManager.currentGameState == (int)GameStateManager.GameState.Dead)
        {
            if (animator.GetCurrentAnimatorStateInfo(0).IsName("SubDead"))
            {
                deadTimer -= Time.deltaTime;
                if(deadTimer <= 0)
                {
                    deadTimer = 3;
                    sub.transform.position = initialPos;
                    uimanager.lifeCount -= 1;
                    GameStateManager.setGameState((int)GameStateManager.GameState.Default);
                    animator.SetTrigger("Alive");
                }
            }
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        Debug.Log(collision.name);
    }

    private void OnTriggerStay(Collider collision)
    {
        if(GameStateManager.currentGameState == (int)GameStateManager.GameState.Default || GameStateManager.currentGameState == (int)GameStateManager.GameState.Scared)
        {
            if (collision.gameObject.name.Equals("TeleporterLeft") && tweener.TweenDone())
            {
                sub.transform.position = new Vector3(sub.transform.position.x + grid.getX() - 1, sub.transform.position.y, 0);
                currentInput = 1;
            }
            if (collision.gameObject.name.Equals("TeleporterRight") && tweener.TweenDone())
            {
                sub.transform.position = new Vector3(sub.transform.position.x - grid.getX() + 1, sub.transform.position.y, 0);
                currentInput = 3;
            }
            if (collision.gameObject.name.Equals("TeleporterUp") && tweener.TweenDone())
            {
                sub.transform.position = new Vector3(sub.transform.position.x, sub.transform.position.y - grid.getY() + 1, 0);
                currentInput = 2;
            }
            if (collision.gameObject.name.Equals("TeleporterDown") && tweener.TweenDone())
            {
                sub.transform.position = new Vector3(sub.transform.position.x, sub.transform.position.y + grid.getY() - 1, 0);
                currentInput = 4;
            }
            if (collision.gameObject.tag.Equals("Pellet"))
            {
                uimanager.scoreValue += 10;
                Destroy(collision.gameObject);
                uimanager.pelletCount += 1;
                collect.Play();
            }
            if (collision.gameObject.tag.Equals("Big Pellet"))
            {
                uimanager.scoreValue += 100;
                Destroy(collision.gameObject);
                GameStateManager.setGameState((int)GameStateManager.GameState.Scared);
                uimanager.pelletCount += 1;
                Debug.Log(uimanager.pelletCount + " " + uimanager.totalPelletCount);
                collect.Play();
                //audioPlayer.PlayScared();

            }
            if (collision.gameObject.tag.Equals("Cherry"))
            {
                uimanager.scoreValue += 100;
                Destroy(collision.gameObject);
                collect.Play();
            }
            if (collision.gameObject.tag.Equals("Ghost"))
            {
                if (GameStateManager.currentGameState == (int)GameStateManager.GameState.Scared)
                {
                    collision.gameObject.GetComponent<Ghost>().ghost = (int)GameStateManager.GhostState.Dead;
                    //audioPlayer.PlayDead();
                }
                else
                {
                    animator.SetTrigger("SubDead");
                    GameStateManager.setGameState((int)GameStateManager.GameState.Dead);
                    lastInput = 0;
                    currentInput = 0;
                    animator.speed = 1;
                    bubbles.Stop();
                }
            }
        }
        
    }


    private void AddMovement(int input)
    {
        switch (input)
        {
            case 1:
                tweener.AddTween(this.transform, this.transform.position, (Vector2)this.transform.position + new Vector2(-1.0f, 0f), 0.35f, false);
                currentInput = input;
                forwardCheck = Vector3.left;
                if (!animator.GetCurrentAnimatorStateInfo(0).IsName("SubLeft"))
                {
                    animator.SetTrigger("Left");
                }                
                break;
            case 2:
                tweener.AddTween(this.transform, this.transform.position, (Vector2)this.transform.position + new Vector2(0f, 1.0f), 0.35f, false);
                currentInput = input;
                forwardCheck = Vector3.up;
                if (!animator.GetCurrentAnimatorStateInfo(0).IsName("SubUp"))
                {
                    animator.SetTrigger("Up");
                }
                break;
            case 3:
                tweener.AddTween(this.transform, this.transform.position, (Vector2)this.transform.position + new Vector2(1.0f, 0f), 0.35f, false);
                currentInput = input;
                forwardCheck = Vector3.right;
                if (!animator.GetCurrentAnimatorStateInfo(0).IsName("SubRIght"))
                {
                    animator.SetTrigger("Right");
                }
                break;
            case 4:
                tweener.AddTween(this.transform, this.transform.position, (Vector2)this.transform.position + new Vector2(0f, -1.0f), 0.35f, false);
                currentInput = input;
                forwardCheck = Vector3.down;
                if (!animator.GetCurrentAnimatorStateInfo(0).IsName("SubDown"))
                {
                    animator.SetTrigger("Down");
                }
                break;
        }
    }
}