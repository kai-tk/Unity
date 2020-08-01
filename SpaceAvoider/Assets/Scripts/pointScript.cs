using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class pointScript : MonoBehaviour
{
    int point;
    Text Score;

    // Start is called before the first frame update
    void Start()
    {
        point = 0;
        Score = GetComponent<Text>();
    }

    // Update is called once per frame
    void Update()
    {
        point += (int)(Time.deltaTime * 1000);
        Score.text = point.ToString();
    }
}
