using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TitleBotton : MonoBehaviour
{
    public void gameStart()
    {
        GameObject.Find("SE").GetComponent<SEController>().play();
        SceneManager.LoadScene("Stage1");
    }
}
