using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioPlayer : MonoBehaviour
{
    public AudioSource StartMain;
    public AudioSource StartScared;
    public AudioSource StartDead;
    private bool startPlaying = false;
    private float timer;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            StartMain.pitch += 1.0f;
        }

    }
    public void PlayNormal()
    {
        StartMain.Play();
        StartScared.Stop();
        StartDead.Stop();
    }
    public void PlayScared()
    {
        StartMain.Stop();
        StartScared.Play();
        StartDead.Stop();
    }
    public void PlayDead()
    {
        StartMain.Stop();
        StartScared.Stop();
        StartDead.Play();
    }
}
