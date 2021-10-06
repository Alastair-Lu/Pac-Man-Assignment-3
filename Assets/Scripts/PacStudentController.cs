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
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

        Debug.Log(animator.GetCurrentAnimatorStateInfo(0).IsName("SubUp"));
        if (lastInput != 0)
        {
            timer += Time.deltaTime;
        }
        if (Input.GetKeyDown(KeyCode.A))
        {
            lastInput = 1;
        }
        if (Input.GetKeyDown(KeyCode.W))
        {
            lastInput = 2;
        }
        if (Input.GetKeyDown(KeyCode.D))
        {
            lastInput = 3;
        }
        if (Input.GetKeyDown(KeyCode.S))
        {
            lastInput = 4;
        }
        if (tweener.TweenDone() && timer > 0.3f)
        {
            AddMovement();
            timer = 0;
        }
    }

    private void AddMovement()
    {
        switch (lastInput)
        {
            case 1:
                tweener.AddTween(sub.transform, sub.transform.position, (Vector2)sub.transform.position + new Vector2(-1.0f, 0f), 0.35f);
                if (!animator.GetCurrentAnimatorStateInfo(0).IsName("SubLeft"))
                {
                    animator.SetTrigger("Left");
                }                
                break;
            case 2:
                tweener.AddTween(sub.transform, sub.transform.position, (Vector2)sub.transform.position + new Vector2(0f, 1.0f), 0.35f);
                if (!animator.GetCurrentAnimatorStateInfo(0).IsName("SubUp"))
                {
                    animator.SetTrigger("Up");
                }
                break;
            case 3:
                tweener.AddTween(sub.transform, sub.transform.position, (Vector2)sub.transform.position + new Vector2(1.0f, 0f), 0.35f);
                if (!animator.GetCurrentAnimatorStateInfo(0).IsName("SubRIght"))
                {
                    animator.SetTrigger("Right");
                }
                break;
            case 4:
                tweener.AddTween(sub.transform, sub.transform.position, (Vector2)sub.transform.position + new Vector2(0f, -1.0f), 0.35f);
                if (!animator.GetCurrentAnimatorStateInfo(0).IsName("SubDown"))
                {
                    animator.SetTrigger("Down");
                }
                break;
        }
    }

    private void TriggerReset()
    {
        animator.ResetTrigger("Left");
        animator.ResetTrigger("Up");
        animator.ResetTrigger("Right");
        animator.ResetTrigger("Down");
    }
}