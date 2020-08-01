using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CameraMover : MonoBehaviour
{
    int count = 0;
    bool flag;

    public void setFlag()
    {
        flag = false;
    }

    void Start()
    {
        flag = true;
    }

    // Update is called once per frame
    void Update()
    {
        float delta = Time.deltaTime * 3f;

        if (flag)
        {
            if (Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.A))
            {
                if (transform.position.x > -1)
                {
                    transform.position -= new Vector3(delta, 0f, 0f);
                }
            }
            if (Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.D))
            {
                if (transform.position.x < 1)
                {
                    transform.position += new Vector3(delta, 0f, 0f);
                }
            }
            if (Input.GetKey(KeyCode.UpArrow) || Input.GetKey(KeyCode.W))
            {
                if (transform.position.y < 5)
                {
                    transform.position += new Vector3(0f, delta, 0f);
                }
            }
            if (Input.GetKey(KeyCode.DownArrow) || Input.GetKey(KeyCode.S))
            {
                if (transform.position.y > 1)
                {
                    transform.position -= new Vector3(0f, delta, 0f);
                }
            }
            if (Input.GetKey(KeyCode.Escape))
            {
                SceneManager.LoadScene("gameTitle");
            }
        }
    }
}
