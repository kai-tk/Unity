using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ResumeButton : MonoBehaviour
{
    public GameObject MenuUI;

    public void OnClicked()
    {
        GameObject.Find("SE").GetComponent<SEController>().code();
        GameObject.Find("GameController").GetComponent<UIController>().resumeGame();
        GetComponent<Image>().color = Color.white;
        GetComponentInChildren<Text>().color = Color.black;
        MenuUI.SetActive(false);
    }
}
