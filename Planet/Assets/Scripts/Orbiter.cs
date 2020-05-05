using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Orbiter : MonoBehaviour
{
    public GameObject centerObject;
    public float radius;
    public float cycle;

    float angle = 0;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        angle -= Time.deltaTime * 2 * Mathf.PI / cycle / 50;
        float x = Mathf.Sin(angle) * radius;
        float z = Mathf.Cos(angle) * radius;

        Vector3 center = centerObject.transform.position;
        transform.position = center + new Vector3(x, 0, z);
    }
}
