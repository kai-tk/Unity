﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameStart : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        GameObject.Find("SE").GetComponent<SEController>().setController(GameObject.Find("Empty"));
        SceneManager.LoadScene("Title");
    }
}
