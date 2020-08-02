using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using OpenCvSharp;

public class changeButton : MonoBehaviour
{
    public GameObject obj;
    CVCamController script;
    bool change = false;

    private void Start()
    {
        script = obj.GetComponent<CVCamController>();
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
