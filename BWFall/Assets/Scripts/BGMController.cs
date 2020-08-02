using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BGMController : MonoBehaviour
{
    AudioSource major;
    AudioSource minor;

    public GameObject GameController;
    GameController script;

    // Start is called before the first frame update
    void Start()
    {
        major = GetComponents<AudioSource>()[0];
        minor = GetComponents<AudioSource>()[1];
        script = GameController.GetComponent<GameController>();

        major.volume = 1;
        minor.volume = 0;
    }

    // Update is called once per frame
    public void change()
    {
        if (script.getMode())
        {
            major.volume = 0;
            minor.volume = 1;
        }
        else
        {
            minor.volume = 0;
            major.volume = 1;
        }
    }

    public void end()
    {
        major.volume = 0;
        minor.volume = 0;
    }
}
