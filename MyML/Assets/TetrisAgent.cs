using System.Collections.Generic;
using UnityEngine;
using Unity.MLAgents;
using Unity.MLAgents.Sensors;

public class TetrisAgent : Agent
{
    public GameObject game;
    Mino minoScript;
    gameManagement gameScript;
    float preRate;
    int preHeight;

    float count;

    int width = 10;
    int height = 20;

    int nowDestroyLine = 0;

    void Start()
    {
        minoScript = game.GetComponent<Mino>();
        gameScript = game.GetComponent<gameManagement>();
    }

    public override void OnEpisodeBegin()
    {
        // 初期化
        minoScript.initialize();
        gameScript.initialize();
        preRate = 0;
        preHeight = 0;
        count = 0;
        nowDestroyLine = 0;
    }

    public override void CollectObservations(VectorSensor sensor)
    {
        sensor.AddObservation(minoScript.minoType);
        /*
        for(int i = 0; i < height; i++)
        {
            for(int j = 0; j < width; j++)
            {
                sensor.AddObservation(minoScript.board[j,i]);
            }
        }
        */
    }

    public override void OnActionReceived(float[] vectorAction)
    {
        // 移動量
        var n = vectorAction[0];
        var mode = 0;

        if (n < 5)
        {
            mode = 1;
        }
        else
        {
            mode = 2;
            n -= 5;
        }

        for (int i = 0; i < n; i++)
        {
            minoScript.MinoMovement(mode);
        }

        // 回転量
        var r = vectorAction[1];

        for (int i = 0; i < r; i++)
        {
            minoScript.MinoMovement(4);
        }

        // 落とす
        while (true)
        {
            if (!minoScript.MinoMovement(3))
            {
                break;
            }
        }

        /*
          うまく学習できていないから
          ・穴の数
          ・高さの合計
          ・隣り合う列の高さの差の合計
          を取り入れた方が良いかも
        */

        // 列が消えたら報酬を得る
        if (gameScript.line > nowDestroyLine)
        {
            AddReward(Mathf.Pow(2, gameScript.line - nowDestroyLine) * 0.1f);
        }
        // 消えていない場合占有率によって報酬を得る
        else
        {
            if (gameScript.rate > 0.8)
            {
                AddReward(0.005f);
            }
            if (minoScript.maxHeight <= 5)
            {
                AddReward(0.01f);
            }
            if (minoScript.numHole() == 0)
            {
                AddReward(0.015f);
            }
        }

        AddReward(-0.0001f);

        preRate = gameScript.rate;
        preHeight = minoScript.maxHeight;

        //ゲームが終了したらエピソードが終了
        if (gameScript.gameEnd)
        {
            if (gameScript.line > gameScript.clear)
            {
                SetReward(1f-gameScript.score*-0.01f);
            }
            if (gameScript.rate < 0.4f)
            {
                SetReward(-1f);
            }
            EndEpisode();
        }
    }

    public override void Heuristic(float[] actionsOut)
    {
        actionsOut[0] = Random.Range(0, 10);
        actionsOut[1] = Random.Range(0, 4);
    }
}
