using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stretchy : MonoBehaviour
{
    public int length;
    public bool goExtend = true;
    public bool goUp = true;
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
            scale.y += Time.deltaTime * speed;
            if (goUp)
            {
                pos.y += Time.deltaTime * speed / 2;
            }
            else
            {
                pos.y -= Time.deltaTime * speed / 2;
            }
            if (scale.y > length)
            {
                reverse = true;
                scale.y = length;
            }
            transform.localScale = scale;
            transform.position = pos;
        }
        else
        {
            scale.y -= Time.deltaTime * speed;
            if (goUp)
            {
                pos.y -= Time.deltaTime * speed / 2;
            }
            else
            {
                pos.y += Time.deltaTime * speed / 2;
            }
            if (scale.y < 0)
            {
                reverse = false;
                scale.y = 0;
            }
            transform.localScale = scale;
            transform.position = pos;
        }
    }
}
