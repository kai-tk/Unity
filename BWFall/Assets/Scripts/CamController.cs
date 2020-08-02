using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamController : MonoBehaviour
{
    GameObject player;
    Vector3 pos;
    float offset;
    bool flag;
    public GameObject bottom;
    float limit;

    private void Start()
    {
        flag = false;
        limit = bottom.transform.position.y + 4.5f;
    }

    // Update is called once per frame
    void Update()
    {
        if (flag)
        {
            pos.y = player.transform.position.y + offset;
            if (pos.y < limit)
            {
                pos.y = limit;
            }
            if (transform.position.y - pos.y > 0.1)
            {
                transform.position = pos;
            }
        }
    }

    public void setMainPlayer()
    {
        player = GameObject.FindGameObjectWithTag("mainPlayer");
        pos = transform.position;
        offset = pos.y - player.transform.position.y;
        flag = true;
    }

    public void changeMainPlayer()
    {
        player = GameObject.FindGameObjectWithTag("mainPlayer");
    }
}
