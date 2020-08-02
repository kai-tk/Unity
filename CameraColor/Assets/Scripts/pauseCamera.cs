/*
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using OpenCvSharp;

public class pauseCamera : MonoBehaviour
{
    public GameObject obj;
    CVCamController script;
    public GameObject button;
    changeButton script2;
    bool mode = false;

    private void Start()
    {
        script = obj.GetComponent<CVCamController>();
        script2 = button.GetComponent<changeButton>();
    }

    public void OnClick()
    {
        bool move = script.pauseCam();
        if (!mode && move)
        {
            GetComponentInChildren<Text>().text = "再開";
            script2.setPause();
        }
        else if (mode && !move)
        {
            GetComponentInChildren<Text>().text = "停止";
            script2.setMove();
        }
        mode = !mode;
    }
}
*/