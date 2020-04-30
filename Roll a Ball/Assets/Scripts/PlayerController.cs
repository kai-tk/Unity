using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    public float speed = 20;
    public Text ScoreText;
    public Text winText;

    private Rigidbody rb;
    private int score;
    private bool flag;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();

        score = 0;
        SetCountText();
        winText.text = "";
        flag = false;
        rb.velocity = new Vector3(0, -5, 0);
    }

    // Update is called once per frame
    void Update()
    {
        var playerY = getY();

        if (flag)
        {
            var moveHorizontal = Input.GetAxis("Horizontal");
            var moveVertical = Input.GetAxis("Vertical");

            var movement = new Vector3(moveHorizontal, 0, moveVertical);

            rb.AddForce(movement * speed);
        }
        else
        {
            if (playerY <= 1.5)
            {
                flag = true;
            }
        }

        if (playerY <= -30)
        {
            flag = false;
            this.transform.position = new Vector3(0, 5, 0);
            rb.velocity = new Vector3(0, -5, 0);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Pick Up"))
        {
            other.gameObject.SetActive(false);
            score += 1;
            SetCountText();
        }
    }
    void SetCountText()
    {
        ScoreText.text = "Count: " + score.ToString();
        if (score >= 16)
        {
            winText.text = "You Win!";
        }
    }

    float getY()
    {
        return this.transform.position.y;
    }

    public int getScore()
    {
        return score;
    }
}
