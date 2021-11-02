using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tween
{
    public Transform Target { get; private set; }
    public Vector2 StartPos { get; private set; }
    public Vector2 EndPos { get; private set; }
    public float StartTime { get; private set; }
    public float Duration { get; private set; }
    public bool Destroy { get; private set; }

 

    public Tween(Transform target, Vector2 startPos, Vector2 endPos, float startTime, float duration, bool destroy)
    {
        Target = target;
        StartPos = startPos;
        EndPos = endPos;
        StartTime = startTime;
        Duration = duration;
        Destroy = destroy;
    }
}
