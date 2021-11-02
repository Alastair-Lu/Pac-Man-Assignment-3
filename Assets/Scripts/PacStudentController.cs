using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PacStudentController : MonoBehaviour
{
    private static int lastInput = 0;
    private static int currentInput;
    public Tweener tweener;
    public Animator animator;
    public LevelGenerator grid;
    public GameObject sub;
    private Vector3 check;
    private Vector3 forwardCheck;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
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
        if (tweener.TweenDone())
        {
            if (!(Physics.Raycast(this.transform.position, check, 1f, ~3)) && lastInput != currentInput)
            {
                AddMovement(lastInput);
            }
            else
            {
                if (!Physics.Raycast(this.transform.position,forwardCheck,1f,~3))
                {
                    AddMovement(currentInput);
                }
                else
                {
                    currentInput = 0;
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
            sub.transform.position = new Vector3(sub.transform.position.x , sub.transform.position.y - grid.getY() + 1, 0);
            currentInput = 2;
        }
        if (collision.gameObject.name.Equals("TeleporterDown") && tweener.TweenDone())
        {
            sub.transform.position = new Vector3(sub.transform.position.x, sub.transform.position.y + grid.getY() - 1, 0);
            currentInput = 4;
        }
    }


    private void AddMovement(int input)
    {
        switch (input)
        {
            case 1:
                tweener.AddTween(this.transform, this.transform.position, (Vector2)this.transform.position + new Vector2(-1.0f, 0f), 0.35f);
                currentInput = input;
                forwardCheck = Vector3.left;
                if (!animator.GetCurrentAnimatorStateInfo(0).IsName("SubLeft"))
                {
                    animator.SetTrigger("Left");
                }                
                break;
            case 2:
                tweener.AddTween(this.transform, this.transform.position, (Vector2)this.transform.position + new Vector2(0f, 1.0f), 0.35f);
                currentInput = input;
                forwardCheck = Vector3.up;
                if (!animator.GetCurrentAnimatorStateInfo(0).IsName("SubUp"))
                {
                    animator.SetTrigger("Up");
                }
                break;
            case 3:
                tweener.AddTween(this.transform, this.transform.position, (Vector2)this.transform.position + new Vector2(1.0f, 0f), 0.35f);
                currentInput = input;
                forwardCheck = Vector3.right;
                if (!animator.GetCurrentAnimatorStateInfo(0).IsName("SubRIght"))
                {
                    animator.SetTrigger("Right");
                }
                break;
            case 4:
                tweener.AddTween(this.transform, this.transform.position, (Vector2)this.transform.position + new Vector2(0f, -1.0f), 0.35f);
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