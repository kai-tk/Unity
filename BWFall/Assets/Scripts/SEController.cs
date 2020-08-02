using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SEController : MonoBehaviour
{
    public bool DontDestroyEnabled = true;
    AudioSource[] se;

    public GameObject GameController;
    GameController script;

    // Start is called before the first frame update
    void Start()
    {
        se = GetComponents<AudioSource>();
        script = GameController.GetComponent<GameController>();
        if (DontDestroyEnabled)
        {
            DontDestroyOnLoad(this);
        }
    }

    public void setController(GameObject obj)
    {
        GameController = obj;
        script = obj.GetComponent<GameController>();
    }

    public void code()
    {
        if (script.getMode())
        {
            se[0].Play();
        }
        else
        {
            se[1].Play();
        }

    }

    public void octcode()
    {
        if (script.getMode())
        {
            se[2].Play();
        }
        else
        {
            se[3].Play();
        }
    }

    public void play()
    {
        se[0].Play();
    }

    public void minorPlay()
    {
        se[1].Play();
    }
}