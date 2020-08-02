using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stretchx : MonoBehaviour
{
    public int length;
    public bool goExtend = true;
    public bool goRight = true;
    Vector3 pos;
    Vector3 scale;
    float speed = 2f;

    bool reverse;

    // Start is called before the first frame update
    void Start()
    {
        pos = transform.position;
        scale = transform.localScale;
        reverse = false;
        if (!goExtend)
        {
            reverse = true;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (!reverse)
        {
            scale.x += Time.deltaTime * speed;
            if (goRight)
            {
                pos.x += Time.deltaTime * speed / 2;
            }
            else
            {
                pos.x -= Time.deltaTime * speed / 2;
            }
            if (scale.x > length)
            {
                reverse = true;
                scale.x = length;
            }
            transform.localScale = scale;
            transform.position = pos;
        }
        else
        {
            scale.x -= Time.deltaTime * speed;
            if (goRight)
            {
                pos.x -= Time.deltaTime * speed / 2;
            }
            else
            {
                pos.x += Time.deltaTime * speed / 2;
            }
            if (scale.x < 0)
            {
                reverse = false;
                scale.x = 0;
            }
            transform.localScale = scale;
            transform.position = pos;
        }
    }
}
