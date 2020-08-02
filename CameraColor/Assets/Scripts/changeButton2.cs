using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using OpenCvSharp;

public class changeButton2 : MonoBehaviour
{
    public GameObject obj;
    hsvtest script;
    bool change = false;

    private void Start()
    {
        script = obj.GetComponent<hsvtest>();
    }

    public void OnClick()
    {
        if (!change)
        {
            GetComponentInChildren<Text>().text = "戻す";
            script.changeColor();
        }
        else
        {
            GetComponentInChildren<Text>().text = "変更";
            script.cancelChangeColor();
        }
        change = !change;
        script.pauseCam();
    }
}
