using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RetryButton : MonoBehaviour
{
    public void OnClicked()
    {
        GameObject.Find("SE").GetComponent<SEController>().code();
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}