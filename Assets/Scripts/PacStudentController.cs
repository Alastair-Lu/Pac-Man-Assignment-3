using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PacStudentController : MonoBehaviour
{
    private static int lastInput = 0;
    private int currentTween = 0;
    public Tweener tweener;
    public Animator animator;
    private Vector3 offset;
    public LevelGenerator grid;
    public GameObject sub;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
            Debug.DrawRay(this.transform.position + offset, Vector3.left * 1f);
            if (!(Physics.Raycast(this.transform.position + offset, Vector3.left, 1f, ~3)))
            {
                lastInput = 1;
            }
        }
        if (Input.GetKeyDown(KeyCode.W))
        {
            Debug.DrawRay(this.transform.position + offset,Vector3.up * 1f);
            if (!(Physics.Raycast(this.transform.position + offset, Vector3.up, 1f, ~3)))
            {
                lastInput = 2;
            }

        }
        if (Input.GetKeyDown(KeyCode.D))
        {
            Debug.DrawRay(this.transform.position + offset,  Vector3.right * 1f);
            if (!(Physics.Raycast(this.transform.position + offset, Vector3.right, 1f, ~3)))
            {
                lastInput = 3;
            }
        }
        if (Input.GetKeyDown(KeyCode.S))
        {
            Debug.DrawRay(this.transform.position + offset,  Vector3.down * 1f);
            if (!(Physics.Raycast(this.transform.position + offset, Vector3.down, 1f, ~3)))
            {
                lastInput = 4;
            }
        }
        if (tweener.TweenDone())
        {
            AddMovement();
        }

        if (Physics.Raycast(this.transform.position, offset, 1f) && currentTween == lastInput)
        {
            lastInput = 0;
            offset = offset * 0;
        }

    }

    private void OnTriggerStay(Collider collision)
    {
        if (collision.gameObject.name.Equals("TeleporterLeft") && tweener.TweenDone())
        {
            sub.transform.position = new Vector3(sub.transform.position.x + grid.getX() - 1, sub.transform.position.y, 0);           
            lastInput = 1;
        }
        if (collision.gameObject.name.Equals("TeleporterRight") && tweener.TweenDone())
        {
            sub.transform.position = new Vector3(sub.transform.position.x - grid.getX() + 1, sub.transform.position.y, 0);
            lastInput = 3;
        }
        if (collision.gameObject.name.Equals("TeleporterTop") && tweener.TweenDone())
        {
            sub.transform.position = new Vector3(sub.transform.position.x , sub.transform.position.y - grid.getY() + 1, 0);
            lastInput = 2;
        }
        if (collision.gameObject.name.Equals("TeleporterDown") && tweener.TweenDone())
        {
            sub.transform.position = new Vector3(sub.transform.position.x, sub.transform.position.y + grid.getY() - 1, 0);
            lastInput = 4;
        }
    }


    private void AddMovement()
    {
        switch (lastInput)
        {
            case 1:
                tweener.AddTween(this.transform, this.transform.position, (Vector2)this.transform.position + new Vector2(-1.0f, 0f), 0.35f);
                offset = Vector3.left * 0.50f;
                if (!animator.GetCurrentAnimatorStateInfo(0).IsName("SubLeft"))
                {
                    animator.SetTrigger("Left"); 
                    currentTween = lastInput;
                }                
                break;
            case 2:
                tweener.AddTween(this.transform, this.transform.position, (Vector2)this.transform.position + new Vector2(0f, 1.0f), 0.35f);
                offset = Vector3.up * 0.50f;
                if (!animator.GetCurrentAnimatorStateInfo(0).IsName("SubUp"))
                {
                    animator.SetTrigger("Up");
                    currentTween = lastInput;
                }
                break;
            case 3:
                tweener.AddTween(this.transform, this.transform.position, (Vector2)this.transform.position + new Vector2(1.0f, 0f), 0.35f);
                offset = Vector3.right * 0.50f;
                if (!animator.GetCurrentAnimatorStateInfo(0).IsName("SubRIght"))
                {
                    animator.SetTrigger("Right");
                    currentTween = lastInput;
                }
                break;
            case 4:
                tweener.AddTween(this.transform, this.transform.position, (Vector2)this.transform.position + new Vector2(0f, -1.0f), 0.35f);
                offset = Vector3.down * 0.50f;
                if (!animator.GetCurrentAnimatorStateInfo(0).IsName("SubDown"))
                {
                    animator.SetTrigger("Down");
                    currentTween = lastInput;
                }
                break;
        }
    }
}