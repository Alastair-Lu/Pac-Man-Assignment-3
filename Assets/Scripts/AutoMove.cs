using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoMove : MonoBehaviour
{
    private Animator animator;
    private Tweener tweener;
    [SerializeField]
    private GameObject sub;
    private int counter;
    // Start is called before the first frame update
    void Start()
    {
        tweener = GetComponent<Tweener>();
        counter = 0;
        animator = sub.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        
        rotation();
        if (tweener.TweenDone() && counter < 4)
        {
            tweener.AddTween(sub.transform, sub.transform.position, (Vector2)sub.transform.position + new Vector2(0f, 1.0f), 0.35f);
            counter++;
            
        }
        else if (tweener.TweenDone() && counter >3 && counter < 9  )
        {
            tweener.AddTween(sub.transform, sub.transform.position, (Vector2)sub.transform.position + new Vector2(1.0f, 0f), 0.35f);            
            counter++;
        }
        else if (tweener.TweenDone() && counter > 8 && counter < 13)
        {
            tweener.AddTween(sub.transform, sub.transform.position, (Vector2)sub.transform.position + new Vector2(0f, -1.0f), 0.35f);            
            counter++;
        }
        else if (tweener.TweenDone() && counter > 12 && counter < 18)
        {
            tweener.AddTween(sub.transform, sub.transform.position, (Vector2)sub.transform.position + new Vector2(-1.0f, 0f), 0.35f);
            counter++;
        }
        else if (counter == 18)
        {
            counter = 0;
        }



    }

    void rotation()
    {
        switch (counter)
        {
            case 0:
                animator.SetTrigger("TurnUp");
                Debug.Log(counter);
                break;
            case 5:
                animator.SetTrigger("TurnRight");
                Debug.Log(counter);
                break;
            case 10:
                animator.SetTrigger("TurnDown");
                Debug.Log(counter);
                break;
            case 14:
                animator.SetTrigger("TurnLeft");
                Debug.Log(counter);
                break;
            default:
                break;
        }
    } 


     
}
