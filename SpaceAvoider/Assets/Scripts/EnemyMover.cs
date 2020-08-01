using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMover : MonoBehaviour
{
    int point;
    int border;
    float speed;
    float minsp;
    float maxsp;

    // Start is called before the first frame update
    void Start()
    {
        point = 0;
        border = 10000;
        minsp = 20f;
        maxsp = 40f;
        Init();
    }

    // Update is called once per frame
    void Update()
    {
        point += (int)(Time.deltaTime * 1000);
        float z = Time.deltaTime * speed;
        transform.position -= new Vector3(0, 0, z);
        if (transform.position.z < -30)
        {
            if (point >= border)
            {
                LevelUp();
            }
            Init();
        }
    }

    void LevelUp()
    {
        border += 10000;
        if (maxsp >= 80)
        {
            minsp += 10;
        }
        maxsp += 10;
    }

    void Init()
    {
        speed = Random.Range(minsp, maxsp);
        float x = Random.Range(-1f, 1f);
        float y = Random.Range(0.5f, 5f);
        float z = 50;
        transform.position = new Vector3(x, y, z);
    }
}
