using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleManager : MonoBehaviour
{
    public GameObject Player;
    public GameObject Gate;
    public GameObject Door;

    PlayerController PlayerScripts;
    private int score;

    // Start is called before the first frame update
    void Start()
    {
        PlayerScripts = Player.GetComponent<PlayerController>();
        Door.SetActive(false);
    }

    // Update is called once per frame  
    void Update()
    {
        score = PlayerScripts.getScore();
        if (score >= 12)
        {
            Destroy(Gate);
        }
        if (score >= 16)
        {
            Door.SetActive(true);
        }
    }
}
