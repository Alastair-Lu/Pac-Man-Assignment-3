using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoMove : MonoBehaviour
{
    private Tweener tweener;
    [SerializeField]
    private GameObject sub;
    int counter;
    // Start is called before the first frame update
    void Start()
    {
        tweener = GetComponent<Tweener>();
        counter = 0;
    }

    // Update is called once per frame
    void Update()
    {
        
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


     
}
