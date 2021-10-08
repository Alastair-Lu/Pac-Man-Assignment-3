using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PacStudentController : MonoBehaviour
{
    private static int lastInput = 0;
    public Tweener tweener;
    public GameObject sub;
    public Animator animator;
    private float timer = 0;
    private Vector3 offset;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Physics.Raycast(sub.transform.position, offset,1f))
        {
            lastInput = 0;
        }
         
        if (Input.GetKeyDown(KeyCode.A))
        {
            Debug.DrawRay(sub.transform.position + offset, Vector3.left * 1f);
            if (!(Physics.Raycast(sub.transform.position + offset, Vector3.left, 1f)))
            {
                lastInput = 1;
            }
        }
        if (Input.GetKeyDown(KeyCode.W))
        {
            Debug.DrawRay(sub.transform.position + offset,Vector3.up * 1f);
            if (!(Physics.Raycast(sub.transform.position + offset, Vector3.up, 1f)))
            {
                lastInput = 2;
            }

        }
        if (Input.GetKeyDown(KeyCode.D))
        {
            Debug.DrawRay(sub.transform.position + offset,  Vector3.right * 1f);
            if (!(Physics.Raycast(sub.transform.position + offset, Vector3.right, 1f)))
            {
                lastInput = 3;
            }
        }
        if (Input.GetKeyDown(KeyCode.S))
        {
            Debug.DrawRay(sub.transform.position + offset,  Vector3.down * 1f);
            if (!(Physics.Raycast(sub.transform.position + offset, Vector3.down, 1f)))
            {
                lastInput = 4;
            }
        }
        if (tweener.TweenDone())
        {
            AddMovement();
        }
    }

    private void AddMovement()
    {
        switch (lastInput)
        {
            case 1:
                tweener.AddTween(sub.transform, sub.transform.position, (Vector2)sub.transform.position + new Vector2(-1.0f, 0f), 0.35f);
                offset = Vector3.left * 0.2f;
                if (!animator.GetCurrentAnimatorStateInfo(0).IsName("SubLeft"))
                {
                    animator.SetTrigger("Left");
                }                
                break;
            case 2:
                tweener.AddTween(sub.transform, sub.transform.position, (Vector2)sub.transform.position + new Vector2(0f, 1.0f), 0.35f);
                offset = Vector3.up * 0.2f;
                if (!animator.GetCurrentAnimatorStateInfo(0).IsName("SubUp"))
                {
                    animator.SetTrigger("Up");
                }
                break;
            case 3:
                tweener.AddTween(sub.transform, sub.transform.position, (Vector2)sub.transform.position + new Vector2(1.0f, 0f), 0.35f);
                offset = Vector3.right * 0.2f;
                if (!animator.GetCurrentAnimatorStateInfo(0).IsName("SubRIght"))
                {
                    animator.SetTrigger("Right");
                }
                break;
            case 4:
                tweener.AddTween(sub.transform, sub.transform.position, (Vector2)sub.transform.position + new Vector2(0f, -1.0f), 0.35f);
                offset = Vector3.down * 0.2f;
                if (!animator.GetCurrentAnimatorStateInfo(0).IsName("SubDown"))
                {
                    animator.SetTrigger("Down");
                }
                break;
        }
    }
}