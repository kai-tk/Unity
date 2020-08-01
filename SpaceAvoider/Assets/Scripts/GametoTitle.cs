using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GametoTitle : MonoBehaviour
{
    public void GameTitle()
    {
        GameObject.Find("SoundObject").GetComponent<soundScript>().Play();
        SceneManager.LoadScene("gameTitle");
    }
}
