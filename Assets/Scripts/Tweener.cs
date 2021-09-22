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
            float ratio2 = timer / 0.1f;
            Tween current = activeTweens[0];
            /* if (Quaternion.Angle(current.StartRot, current.EndRot) > 10f)
             {
                 current.Target.rotation = Quaternion.Lerp(current.StartRot, current.EndRot, ratio2);
             }
             else
             {
                 current.Target.rotation = current.EndRot;
             }*/
            Debug.Log(current.Target.rotation);
            current.Target.rotation = current.EndRot;
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

    
    public void AddTween(Transform targetObject, Vector2 startPos, Vector2 endPos, float duration, Quaternion startRot, Quaternion endRot)
    {
        
        {
            activeTweens.Add(new Tween(targetObject, startPos, endPos, Time.time, duration, startRot, endRot));
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
