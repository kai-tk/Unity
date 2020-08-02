using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movex : MonoBehaviour
{
    public float distance;
    float minx;
    float maxx;
    Vector3 pos;
    float speed = 2f;

    bool reverse;

    // Start is called before the first frame update
    void Start()
    {
        pos = transform.position;
        minx = pos.x;
        maxx = minx + distance;
        reverse = false;
        if (distance<0)
        {
            float t = minx;
            minx = maxx;
            maxx = t;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (!reverse)
        {
            pos.x += Time.deltaTime * speed;
            if (pos.x > maxx)
            {
                reverse = true;
                pos.x = maxx;
            }
            transform.position = pos;
        }
        else
        {
            pos.x -= Time.deltaTime * speed;
            if (pos.x < minx)
            {
                reverse = false;
                pos.x = minx;
            }
            transform.position = pos;
        }
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag == "mainPlayer")
        {
            other.transform.SetParent(transform);
        }
    }
    void OnCollisionExit2D(Collision2D other)
    {
        if (other.gameObject.tag == "mainPlayer")
        {
            other.transform.SetParent(null);
        }
    }
}
