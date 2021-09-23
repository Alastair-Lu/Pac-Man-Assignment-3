using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tweener : MonoBehaviour
{
    private List<Tween> activeTweens = new List<Tween>();
    private bool ready;
    // Start is called before the first frame update
    void Start()
    {
        ready = true;
    }

    // Update is called once per frame
    void Update()
    {

       
        if (!EmptyList())
        {
            float timer = Time.time - activeTweens[0].StartTime;
            float ratio = timer / activeTweens[0].Duration;
            
            Tween current = activeTweens[0];            
            if (Vector3.Distance(current.Target.position, current.EndPos) > 0.05f)
            {
                current.Target.position = Vector3.Lerp(current.StartPos,current.EndPos, ratio);
                ready = false;
                
            }
            else
            {
                current.Target.position = current.EndPos;
                activeTweens.Remove(current);
                ready = true;           
            }
            
        }
    }

    
    public void AddTween(Transform targetObject, Vector2 startPos, Vector2 endPos, float duration)
    {
        
        {
            activeTweens.Add(new Tween(targetObject, startPos, endPos, Time.time, duration));
        }

    }

    public bool EmptyList()
    {
        if (activeTweens.Count == 0)
            return true;
        return false;
            
        
    }

    public bool TweenDone()
    {
        return ready; 

    }
}
