using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BWPlayer : MonoBehaviour
{
    Rigidbody2D rb;
    float speed = 5f;
    Vector2 Left;
    Vector2 Right;
    Vector2 Stop;
    bool flag;
    bool goal;

    GameObject PlayerPrefab;

    bool menu;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        Left = new Vector2(-1, 0);
        Right = new Vector2(1, 0);
        Stop = new Vector2(0, 0);
        flag = false;

        PlayerPrefab = (GameObject)Resources.Load("Prefabs/Player");
    }

    // Update is called once per frame
    void Update()
    {
        menu = GameObject.Find("GameController").GetComponent<UIController>().menu;
        goal = GameObject.Find("GameController").GetComponent<UIController>().goal;

        //move
        if (menu || goal)
        {
            rb.velocity = Stop;
        }
        else if (rb.velocity.y == 0)
        {
            if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
            {
                rb.velocity = Left * speed;
            }
            else if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
            {
                rb.velocity = Right * speed;
            }
            else
            {
                rb.velocity = Stop;
            }
        }

        //make clone
        if (flag) {
            if (transform.position.x > 4f)
            {
                Vector3 pos = transform.position;
                pos.x -= 9f;
                GameObject Player = Instantiate(PlayerPrefab, pos, Quaternion.identity);
                Player.name = PlayerPrefab.name;
                flag = false;
            } else if (transform.position.x<-4f)
            {
                Vector3 pos = transform.position;
                pos.x += 9f;
                GameObject Player = Instantiate(PlayerPrefab, pos, Quaternion.identity);
                Player.name = PlayerPrefab.name;
                flag = false;
            }
        }else if(transform.position.x<=4f && transform.position.x >= -4f)
        {
            flag = true;
        }
        if(transform.position.x>5f || transform.position.x<-5f)
        {
            this.tag = "destroy";
            Camera.main.GetComponent<CamController>().changeMainPlayer();
            Destroy(this.gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("jewel"))
        {
            other.gameObject.SetActive(false);
            GameObject.Find("SE").GetComponent<SEController>().octcode();
            GameObject.Find("GameController").GetComponent<UIController>().getPoint();
        }else if (other.gameObject.CompareTag("goal"))
        {
            GameObject.Find("SE").GetComponent<SEController>().code();
            GameObject.Find("GameController").GetComponent<UIController>().Clear();
        }
    }
}
