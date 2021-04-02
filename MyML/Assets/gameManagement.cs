using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class gameManagement : MonoBehaviour
{
    public Text scoreText;
    public Text clearText;

    public int score;
    public float rate;
    public int line;

    static int width = 10;
    public int ceil = 18;
    public int clear = 40;

    public bool gameEnd;
    int s;

    // Start is called before the first frame update
    void Start()
    {
        s = 0;
        //initialize();
    }

    public void initialize()
    {
        gameEnd = false;
        score = 0;
        rate = 0;
        line = 0;
        scoreText.text = "Score: " + score.ToString() + "\nRate: " + rate.ToString() + "\nLine: " + line.ToString();
        clearText.text = "";
    }

    public bool addScore(int i, int maxHeight, int minos)
    {
        if (i > 0)
        {
            score += 100 * (int)Mathf.Pow(2, i - 1);
        }
        score += 1;
        rate = (float)minos / (maxHeight * width);
        line += i;
        scoreText.text = "Score: " + score.ToString() + "\nRate: " + rate.ToString() + "\nLine: " + line.ToString();
        if (maxHeight >= ceil)
        {
            clearText.text = "failed";
            gameEnd = true;
            return false;
        }
        if (line >= clear)
        {
            s++;
            Debug.Log("clear" + s);
            clearText.text = "clear!";
            gameEnd = true;
            return false;
        }
        return true;
    }
}
