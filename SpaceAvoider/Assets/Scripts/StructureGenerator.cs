using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StructureGenerator : MonoBehaviour
{
    public GameObject structurePrefab;

    // Start is called before the first frame update
    void Start()
    {
        for(int i = 0; i < 100; i++)
        {
            float size;
            Vector3 pos;
            GameObject obj;

            size = Random.Range(0.3f, 2f);
            pos = new Vector3(
                Random.Range(-0.1f, 0.1f) + 2f,
                Random.Range(0f, 4f),
                Random.Range(-10f, 50f)
            );
            obj = Instantiate(structurePrefab, pos, Quaternion.identity);
            obj.transform.localScale = new Vector3(1f, size, size);

            size = Random.Range(0.3f, 2f);
            pos = new Vector3(
                Random.Range(-0.1f, 0.1f) - 2f,
                Random.Range(0f, 4f),
                Random.Range(-10f, 50f)
            );
            obj = Instantiate(structurePrefab, pos, Quaternion.identity);
            obj.transform.localScale = new Vector3(1f, size, size);

            size = Random.Range(1f, 2f);
            pos = new Vector3(
                Random.Range(-1f, 1f),
                0,
                Random.Range(-10f, 50f)
            );
            obj = Instantiate(structurePrefab, pos, Quaternion.identity);
            obj.transform.localScale = new Vector3(size, 0.1f, size);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
