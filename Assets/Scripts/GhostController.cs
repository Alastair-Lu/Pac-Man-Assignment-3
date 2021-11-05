using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhostController : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    public bool TargetLocation(Vector3 startpos, Vector3 targetpos, bool[] possibleMoves, int invalidDirection, Ghost ghost)
    {
        List<Vector3> moves = PossibleMoves(possibleMoves);
        if(startpos == targetpos)
        {
            return true;
        }
        else
        {
            if (targetpos.y > startpos.y)
            {
                if (moves.Contains(Vector3.up) && invalidDirection != 1)
                {
                    ghost.tweener.AddTween(ghost.gameObject.transform,startpos, startpos + Vector3.up, 0.35f, false);
                }
            }
            else if (targetpos.y < startpos.y)
            {
                if (moves.Contains(Vector3.down) && invalidDirection != 3)
                {
                    ghost.tweener.AddTween(ghost.gameObject.transform, startpos, startpos + Vector3.down, 0.35f, false);
                }
            }
            else
            {
                if(targetpos.x < startpos.x)
                {
                    if(moves.Contains(Vector3.left) && invalidDirection != 4)
                    {
                        ghost.tweener.AddTween(ghost.gameObject.transform, startpos, startpos + Vector3.left, 0.35f, false);
                    }
                }
                else
                {
                    if (moves.Contains(Vector3.right) && invalidDirection != 2)
                    {
                        ghost.tweener.AddTween(ghost.gameObject.transform, startpos, startpos + Vector3.left, 0.35f, false);
                    }
                }
            }
        }
        return false;
    }
    private List<Vector3> PossibleMoves(bool[] possibleMoves)
    {
        List<Vector3> moveList = new List<Vector3>();
        for(int i = 0; i < possibleMoves.Length; i++)
        {
            if (possibleMoves[i])
            {
                moveList.Add(Case(i));
            }
        }
        return moveList;
    }

    private Vector3 Case(int i)
    {
        switch (i)
        {
            case 1:
                return Vector3.up;
            case 2:
                return Vector3.right;
            case 3:
                return Vector3.down;
            case 4:
                return Vector3.left;
        }
        return Vector3.zero;
    }
    
}
