using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StructureMover : MonoBehaviour
{
    float speed = 30;

    // Update is called once per frame
    void Update()
    {
        float z = Time.deltaTime * speed;
        transform.position -= new Vector3(0, 0, z);
        if (transform.position.z < -10)
        {
            transform.position += new Vector3(0, 0, 50);
        }
    }
}
