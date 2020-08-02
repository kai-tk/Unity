using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GoBackTitleButton : MonoBehaviour
{
    public void OnClicked()
    {
        GameObject.Find("SE").GetComponent<SEController>().code();
        SceneManager.LoadScene("Title");
    }
}