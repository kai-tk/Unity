using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NextButton : MonoBehaviour
{
    public int id = 0;

    public void OnClicked()
    {
        GameObject.Find("SE").GetComponent<SEController>().code();
        SceneManager.LoadScene("Stage"+(id+1).ToString());
    }
}
