using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AudioManager2 : MonoBehaviour
{
    AudioSource[] Audio;
    AudioSource Base;
    AudioSource Normal;
    AudioSource High;
    AudioSource Goal;
    private bool flag = false;

    float nV = 0f;
    float hV = 0f;

    public void Start()
    {
        Audio = GetComponents<AudioSource>();
        Base = Audio[0];
        Normal = Audio[1];
        High = Audio[2];
        Goal = Audio[3];
    }
    public void musicStart()
    {
        if (!flag)
        {
            Base.Play();
            Normal.Play();
            High.Play();
            flag = true;
            Debug.Log("Yes");
        }
        Normal.volume = 0;
        High.volume = 0;
        Debug.Log("No");
    }

    void Update()
    {
        Normal.volume = nV;
        High.volume = hV;
    }

    public void setAisac(float c)
    {
        if (c <= 0.3)
        {
            nV = 2 * c / 3;
            hV = 0;
        }else if (c <= 0.5)
        {
            nV = 4 * c - 1;
            hV = 0;
        }else if (c <= 0.8)
        {
            nV = -2 * c + 2;
            hV = (4 * c - 2) / 3;
        }
        else
        {
            nV = -2 * c + 2;
            hV = 3 * c - 2;
        }
        if (c == 1)
        {
            Goal.Play();
        }
        Debug.Log(c + " " + nV + " " + hV);
    }
}
