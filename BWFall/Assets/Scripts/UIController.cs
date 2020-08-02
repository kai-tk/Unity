using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
    public int limitTime=60;
    float countDown;

    int item;
    public bool goal;
    public bool menu;

    public GameObject GameUI;
    public GameObject MenuUI;
    public GameObject GoalUI;
    public GameObject FailedUI;

    // Start is called before the first frame update
    void Start()
    {
        countDown = limitTime;
        item = 0;
        goal = false;
        menu = false;

        MenuUI.SetActive(false);
        GoalUI.SetActive(false);
        FailedUI.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (!goal)
        {
            if (!menu)
            {
                countDown -= Time.deltaTime;
                GameUI.transform.Find("Time").gameObject.GetComponent<Text>().text = ((int)countDown).ToString();
            }

            //ESC
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                menu = true;
                GameObject.Find("SE").GetComponent<SEController>().code();
                MenuUI.SetActive(true);
            }

            if ((int)countDown == 0)
            {
                GameObject.Find("SE").GetComponent<SEController>().minorPlay();
                GameObject.Find("BGM").GetComponent<BGMController>().end();
                GameUI.SetActive(false);
                FailedUI.SetActive(true);
                goal = true;
            }
        }
    }

    public void resumeGame()
    {
        menu = false;
    }

    public void getPoint()
    {
        item += 1;
    }

    public void Clear()
    {
        GameObject.Find("BGM").GetComponent<BGMController>().end();
        GameUI.SetActive(false);
        GoalUI.SetActive(true);
        goal = true;

        string result;

        int itemPoint = (item * 1000);
        int timePoint= ((int)countDown) * 100;

        string itemResult = ("Item : " + item.ToString() + "×1000 = " + itemPoint.ToString());
        string timeResult = ("Time : " + ((int)countDown).ToString() + "×100 = " + timePoint.ToString());
        string scoreResult = ("Score : " + (itemPoint+timePoint).ToString());

        result = itemResult + "\n" + timeResult + "\n\n" + scoreResult;

        GoalUI.transform.Find("Score").gameObject.GetComponent<Text>().text = result;
    }
}
