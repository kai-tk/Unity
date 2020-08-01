using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class soundScript : MonoBehaviour
{
    public bool DontDestroyEnabled = true;
    AudioSource se;

    // Start is called before the first frame update
    void Start()
    {
        se = GetComponent<AudioSource>();
        if (DontDestroyEnabled)
        {
            DontDestroyOnLoad(this);
        }
        
    }

    public void Play()
    {
        se.Play();
    }
}
