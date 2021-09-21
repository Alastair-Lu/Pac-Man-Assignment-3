using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoMove : MonoBehaviour
{
    private Tweener tweener;
    [SerializeField]
    private GameObject sub;
    // Start is called before the first frame update
    void Start()
    {
        tweener = GetComponent<Tweener>();
    }

    // Update is called once per frame
    void Update()
    {
        if (tweener.EmptyList())
        {
            AddTweens();
        }


    }

    public void AddTweens()
    {
        for (int i = 0; i < 4; i++)
        {
            tweener.AddTween(sub.transform, sub.transform.position, (Vector2)sub.transform.position + new Vector2(0f, 1.0f), 0.35f, sub.transform.rotation, new Quaternion(0, 0, 0, 0));
        }
        for (int i = 0; i < 5; i++)
        {
            tweener.AddTween(sub.transform, sub.transform.position, (Vector2)sub.transform.position + new Vector2(1.0f, 0f), 0.35f, sub.transform.rotation, new Quaternion(0, 0, 0, 0));
        }
    }
}
