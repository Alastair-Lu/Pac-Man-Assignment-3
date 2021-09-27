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
        
        
        if (tweener.TweenDone() && counter <= 3)
        {
            animator.SetBool("TU", true);
            animator.SetBool("TR", false);
            animator.SetBool("TL", false);
            animator.SetBool("TD", false);            
            tweener.AddTween(sub.transform, sub.transform.position, (Vector2)sub.transform.position + new Vector2(0f, 1.0f), 0.35f);
            counter++;            
        }
        else if (tweener.TweenDone() && counter >3 && counter <= 8  )
        {
            animator.SetBool("TR", true);
            animator.SetBool("TU", false);            
            animator.SetBool("TL", false);
            animator.SetBool("TD", false);
            tweener.AddTween(sub.transform, sub.transform.position, (Vector2)sub.transform.position + new Vector2(1.0f, 0f), 0.35f);
            counter++;

        }
        else if (tweener.TweenDone() && counter > 8 && counter <= 12)
        {
            animator.SetBool("TD", true);
            animator.SetBool("TU", false);
            animator.SetBool("TR", false);
            animator.SetBool("TL", false);
            tweener.AddTween(sub.transform, sub.transform.position, (Vector2)sub.transform.position + new Vector2(0f, -1.0f), 0.35f);
            counter++;
        }
        else if (tweener.TweenDone() && counter > 12 && counter <= 17)
        {
            animator.SetBool("TL", true);
            animator.SetBool("TU", false);
            animator.SetBool("TR", false);            
            animator.SetBool("TD", false);
            tweener.AddTween(sub.transform, sub.transform.position, (Vector2)sub.transform.position + new Vector2(-1.0f, 0f), 0.35f);
            counter++;
        }
        else if (counter == 18)
        {
            counter = 0;
        }
        
       
    }

   


     
}
