using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyGenerator : MonoBehaviour
{
    public GameObject enemyPrefab;

    IEnumerator createEnemy()
    {
        yield return new WaitForSeconds(1);
        for (int i = 0; i < 5; i++)
        {
            GameObject enemy = Instantiate(enemyPrefab, Vector3.zero, Quaternion.identity);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(createEnemy());
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
