using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class GameOver : MonoBehaviour
{
    AudioSource se;
    bool flag;

    public GameObject score;
    public GameObject point;
    public GameObject panel;
    public GameObject resulttext;
    public GameObject resultpoint;
    public GameObject retrybutton;
    public GameObject titlebutton;

    // Start is called before the first frame update
    void Start()
    {
        panel.SetActive(false);
        resulttext.SetActive(false);
        resultpoint.SetActive(false);
        retrybutton.SetActive(false);
        titlebutton.SetActive(false);
        se = GetComponents<AudioSource>()[0];
        flag = true;
    }

    public void gameOver()
    {
        if (flag)
        {
            se.Play();
            flag = false;
            int result = int.Parse(GameObject.Find("Point").GetComponent<Text>().text);
            score.SetActive(false);
            point.SetActive(false);
            panel.SetActive(true);
            resulttext.SetActive(true);
            resultpoint.SetActive(true);
            retrybutton.SetActive(true);
            titlebutton.SetActive(true);
            GameObject.Find("ResultPoint").GetComponent<Text>().text = result.ToString();
            Camera.main.GetComponent<CameraMover>().setFlag();
        }
    }
}
