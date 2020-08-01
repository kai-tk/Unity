using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TitletoGame : MonoBehaviour
{
    public void StartGame()
    {
        GameObject.Find("SoundObject").GetComponent<soundScript>().Play();
        SceneManager.LoadScene("main");
    }
}
