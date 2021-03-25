using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    int x;
    int y;
    int d;
    int goalDistance;
    public GameObject makeDungeon;
    MakeDungeon script;
    public GameObject AudioSource;
    AudioManager2 audioScript;
    bool initiateFlag = false;
    bool updateFlag = false;

    void Start()
    {
        script = makeDungeon.GetComponent<MakeDungeon>();
        audioScript = AudioSource.GetComponent<AudioManager2>();
    }

    public void initiate()
    {
        initiateFlag = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (!initiateFlag)
        {
            updateFlag = false;
            if (script.getFlag())
            {
                initiateFlag = true;
            }
        }
        else
        {
            if (!updateFlag)
            {
                x = script.getStartX();
                y = script.getStartY();
                d = script.getD();
                goalDistance = script.getDistance();
                updateFlag = true;
            }
            else
            {
                var dx = 0;
                var dy = 0;
                if (Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.W))
                {
                    dy++;
                }
                else if (Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.A))
                {
                    dx--;
                }
                else if (Input.GetKeyDown(KeyCode.DownArrow) || Input.GetKeyDown(KeyCode.S))
                {
                    dy--;
                }
                else if (Input.GetKeyDown(KeyCode.RightArrow) || Input.GetKeyDown(KeyCode.D))
                {
                    dx++;
                }
                if (!(dx == 0 && dy == 0) && !script.IsOutOfBounds(x + dx, y + dy) && script.getCells(x + dx, y + dy) >= 0)
                {
                    this.transform.position = new Vector3(x + dx + d, y + dy + d, 0);
                    x += dx;
                    y += dy;
                    audioScript.setAisac(1 - (goalDistance - script.getCells(x, y)) / (float)goalDistance);
                }
            }
        }
    }
}
