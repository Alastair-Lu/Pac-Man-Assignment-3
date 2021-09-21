using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tweener : MonoBehaviour
{
    private List<Tween> activeTweens = new List<Tween>();
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
        
        if (activeTweens.Count != 0)
        {
            float timer = Time.time - activeTweens[0].StartTime;
            float ratio = timer / activeTweens[0].Duration;
            if (Vector3.Distance(activeTweens[0].Target.position, activeTweens[0].EndPos) > 0.05f)
            {
                activeTweens[0].Target.position = Vector3.Lerp(activeTweens[0].StartPos, activeTweens[0].EndPos, ratio);
            }
            else
            {
                activeTweens[0].Target.position = activeTweens[0].EndPos;
                activeTweens.RemoveAt(0);

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
}
