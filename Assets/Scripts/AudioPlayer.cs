using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioPlayer : MonoBehaviour
{
    public AudioSource StartMusic;
    public AudioSource StartMain;
    private bool startPlaying = false;
    private float timer;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (!StartMusic.isPlaying)
        {
            timer += Time.deltaTime;
        }
        if (!StartMusic.isPlaying && !startPlaying &&timer > 0.75f)
        {
            StartMain.Play();
            startPlaying = true;
        }
            
    }
}
