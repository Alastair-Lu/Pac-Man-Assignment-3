using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tweener : MonoBehaviour
{
    private Tween activeTween;
    private bool ready = true;
    // Start is called before the first frame update
    void Start()
    {
              
    }

    // Update is called once per frame
    void Update()
    {

       
        if (activeTween != null)
        {
            float timer = Time.time - activeTween.StartTime;
            float ratio = timer / activeTween.Duration;
            
            Tween current = activeTween;            
            if (Vector3.Distance(current.Target.position, current.EndPos) > 0.05f)
            {
                current.Target.position = Vector3.Lerp(current.StartPos,current.EndPos, ratio);
                ready = false;
                
            }
            else
            {
                current.Target.position = current.EndPos;
                activeTween = null;
                ready = true;           
            }
            
        }
    }

    
    public void AddTween(Transform targetObject, Vector2 startPos, Vector2 endPos, float duration)
    {
        
        {
            activeTween = new Tween(targetObject, startPos, endPos, Time.time, duration);
        }

    }

    public bool TweenDone()
    {
        return activeTween == null; 
    }
}
