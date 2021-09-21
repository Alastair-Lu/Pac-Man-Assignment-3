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
    public Quaternion StartRot { get; private set; }
    public Quaternion EndRot { get; private set; }

    public Tween(Transform target, Vector2 startPos, Vector2 endPos, float startTime, float duration, Quaternion startRot, Quaternion endRot)
    {
        Target = target;
        StartPos = startPos;
        EndPos = endPos;
        StartTime = startTime;
        Duration = duration;
        StartRot = startRot;
        EndRot = endRot;
    }
}
