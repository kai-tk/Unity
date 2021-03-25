using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


[RequireComponent(typeof(CriAtomSource))]
public class AudioManager : MonoBehaviour
{
    public CriAtomSource atomSource;

    [Range(0f, 1f)]
    public float currentAisac00 = 0f;

    private CriAtomExPlayback criAtomExPlayback;

    private bool flag = false;

    public void musicStart()
    {
        if (!flag)
        {
            criAtomExPlayback = atomSource.Play();
            flag = true;
        }
        currentAisac00 = 0f;
    }

    void Update()
    {
        atomSource.SetAisacControl("AisacControl_00", currentAisac00);
    }

    public void setAisac(float c)
    {
        currentAisac00 = c;
        if (c == 1f)
        {
            GetComponent<AudioSource>().Play();
        }
    }
}
