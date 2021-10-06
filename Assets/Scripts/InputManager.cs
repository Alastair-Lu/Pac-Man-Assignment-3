using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    private static int lastMovement;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
            lastMovement = 1;
        }
        if (Input.GetKeyDown(KeyCode.W))
        {
            lastMovement = 2;
        }
        if (Input.GetKeyDown(KeyCode.D))
        {
            lastMovement = 3;
        }
        if (Input.GetKeyDown(KeyCode.S))
        {
            lastMovement = 4;
        }

    }
}
