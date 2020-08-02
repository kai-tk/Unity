using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movey : MonoBehaviour
{
    public float distance;
    float miny;
    float maxy;
    Vector3 pos;
    float speed = 2f;

    bool reverse;

    // Start is called before the first frame update
    void Start()
    {
        pos = transform.position;
        miny = pos.y;
        maxy = miny + distance;
        reverse = false;
        if (distance<0)
        {
            float t = miny;
            miny = maxy;
            maxy = t;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (!reverse)
        {
            pos.y += Time.deltaTime * speed;
            if (pos.y > maxy)
            {
                reverse = true;
                pos.y = maxy;
            }
            transform.position = pos;
        }
        else
        {
            pos.y -= Time.deltaTime * speed;
            if (pos.y < miny)
            {
                reverse = false;
                pos.y = miny;
            }
            transform.position = pos;
        }
    }
}
