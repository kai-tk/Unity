using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using System.Linq;

public class MakeDungeon : MonoBehaviour
{
    // 設定する値
    public int max = 5;        //縦横のサイズ ※必ず奇数にすること
    public GameObject wall;    //壁用オブジェクト
    public GameObject floor;   //床用オブジェクト
    public GameObject start;   //スタート地点に配置するオブジェクト
    public GameObject goal;    //ゴール地点に配置するオブジェクト
    public GameObject distance; //距離を表す内部UI

    // 内部パラメータ
    private int[,] cells;
    private bool[,] toGoal;
    private bool[,] check;
    private bool flag=false;
    private int d;

    private Vector2Int startPos;    //スタートの座標
    private Vector2Int goalPos;     //ゴールの座標

    public void initiate()
    {
        flag = false;
    }
    public void makeDungeon(int m)
    {
        foreach (Transform n in this.transform)
        {
            GameObject.Destroy(n.gameObject);
        }


        max = m;

        //マップ状態初期化
        cells = new int[max, max];
        toGoal = new bool[max, max];
        check = new bool[max, max];

        for (int i = 0; i < max; i++)
        {
            for (int j = 0; j < max; j++)
            {
                cells[i, j] = -1;
                toGoal[i, j] = false;
                check[i, j] = false;
            }
        }

        //スタート地点の取得
        startPos = GetStartPosition();

        //通路の生成
        MakeMapInfo(startPos);

        goalPos = SetGoal();

        //マップの状態に応じて壁と通路を生成する
        BuildDungeon();

        var x = this.transform.position.x;
        var y = this.transform.position.y;
        d = (int)((19 - max) / 2);
        this.transform.position = new Vector3(x + d, y + d, this.transform.position.z);

        //スタート地点とゴール地点にオブジェクトを配置する
        start.transform.position = new Vector3(startPos.x+d, startPos.y+d, 0);
        goal.transform.position = new Vector3(goalPos.x+d, goalPos.y+d, 2);

        flag = true;
    }


    // スタート地点の取得
    private Vector2Int GetStartPosition()
    {
        //ランダムでx,yを設定
        int randomX = Random.Range(0, max);
        int randomY = Random.Range(0, max);

        //x、yが両方共偶数になるまで繰り返す
        while (!(randomX % 2 == 0 && randomY % 2 == 0))
        {
            randomX = Mathf.RoundToInt(Random.Range(0, max));
            randomY = Mathf.RoundToInt(Random.Range(0, max));
        }

        return new Vector2Int(randomX, randomY);
    }


    // マップ生成
    private void MakeMapInfo(Vector2Int _startPos)
    {
        var tmpStartPos = _startPos;
        var stack = new Stack<Vector2Int>();
        stack.Push(tmpStartPos);

        cells[tmpStartPos.x, tmpStartPos.y] = 0;

        while (stack.Count > 0)
        {

            //スタート位置配列を複製
            tmpStartPos = stack.Pop();

            //移動可能な座標のリストを取得
            var movablePositions = GetMovablePositions(tmpStartPos);

            //移動可能な座標がなくなるまで探索を繰り返す
            while (movablePositions != null)
            {
                //移動可能な座標からランダムで1つ取得し通路にする
                var tmpPos = movablePositions[Random.Range(0, movablePositions.Count)];
                if (movablePositions.Count > 1)
                {
                    stack.Push(tmpStartPos);
                }
                cells[tmpPos.x, tmpPos.y] = cells[tmpStartPos.x, tmpStartPos.y] + 2;

                //元の地点と通路にした座標の間を通路にする
                var xPos = tmpPos.x + (tmpStartPos.x - tmpPos.x) / 2;
                var yPos = tmpPos.y + (tmpStartPos.y - tmpPos.y) / 2;
                cells[xPos, yPos] = cells[tmpStartPos.x, tmpStartPos.y] + 1;

                //移動後の座標を一時変数に格納し、再度移動可能な座標を探索する
                tmpStartPos = tmpPos;
                movablePositions = GetMovablePositions(tmpStartPos);
            }
        }
    }

    private Vector2Int SetGoal()
    {
        //ランダムでx,yを設定
        int randomX = Random.Range(0, max);
        int randomY = Random.Range(0, max);

        int[] dirx = { 0, 1, 0, -1 };
        int[] diry = { 1, 0, -1, 0 };

        //ある程度離れた通路になるまで繰り返す
        while (cells[randomX,randomY] < max*max/4)
        {
            randomX = Mathf.RoundToInt(Random.Range(0, max));
            randomY = Mathf.RoundToInt(Random.Range(0, max));
        }

        var nextX = 0;
        var nextY = 0;

        while (true)
        {
            for(int i = 0; i < 4; i++)
            {
                nextX = randomX + dirx[i];
                nextY = randomY + diry[i];
                if(!IsOutOfBounds(nextX,nextY) && cells[nextX, nextY] > cells[randomX, randomY])
                {
                    randomX = nextX;
                    randomY = nextY;
                    break;
                }
            }
            if (randomX != nextX) break;
        }

        var goalPos = new Vector2Int(randomX, randomY);
        toGoal[randomX, randomY] = true;

        while(cells[randomX, randomY] > 0)
        {
            for (int i = 0; i < 4; i++)
            {
                nextX = randomX + dirx[i];
                nextY = randomY + diry[i];
                if (!IsOutOfBounds(nextX, nextY) && cells[nextX, nextY] != -1 && cells[nextX, nextY] < cells[randomX, randomY])
                {
                    randomX = nextX;
                    randomY = nextY;
                    toGoal[randomX, randomY] = true;
                    break;
                }
            }
        }

        var stack = new Stack<Vector2Int>();
        stack.Push(new Vector2Int(randomX, randomY));

        while (stack.Count > 0)
        {
            var tmpPos = stack.Pop();
            check[tmpPos.x, tmpPos.y] = true;
            for (int i = 0; i < 4; i++)
            {
                nextX = tmpPos.x + dirx[i];
                nextY = tmpPos.y + diry[i];
                if (!IsOutOfBounds(nextX, nextY) && !check[nextX,nextY] && cells[nextX, nextY] > 0)
                {
                    stack.Push(new Vector2Int(nextX, nextY));
                    if (!toGoal[nextX, nextY])
                    {
                        cells[nextX, nextY] = cells[tmpPos.x, tmpPos.y] - 1;
                    }
                }
            }
        }

        return goalPos;
    }


    // 移動可能な座標のリストを取得する
    private List<Vector2Int> GetMovablePositions(Vector2Int _startPos)
    {
        //可読性のため座標を変数に格納
        var x = _startPos.x;
        var y = _startPos.y;

        //移動方向毎に2つ先のx,y座標を仮計算
        var positions = new List<Vector2Int> {
            new Vector2Int(x, y + 2),
            new Vector2Int(x, y - 2),
            new Vector2Int(x + 2, y),
            new Vector2Int(x - 2, y)
        };

        //移動方向毎に移動先の座標が範囲内かつ壁であるかを判定する
        //真であれば、返却用リストに追加する
        var movablePositions = positions.Where(p => !IsOutOfBounds(p.x, p.y) && cells[p.x, p.y] == -1);

        return movablePositions.Count() != 0 ? movablePositions.ToList() : null;
    }


    //与えられたx、y座標が範囲外の場合真を返す
    public bool IsOutOfBounds(int x, int y) => (x < 0 || y < 0 || x >= max || y >= max);


    //パラメータに応じてオブジェクトを生成する
    private void BuildDungeon()
    {
        //縦横1マスずつ大きくループを回し、外壁とする
        for (int i = -1; i <= max; i++)
        {
            for (int j = -1; j <= max; j++)
            {
                //範囲外、または壁の場合に壁オブジェクトを生成する
                if (IsOutOfBounds(i, j) || cells[i, j] == -1)
                {
                    var wallObj = Instantiate(wall, new Vector3(i, j, 0), Quaternion.identity);
                    wallObj.transform.parent = this.transform;
                }

                //全ての場所に床オブジェクトを生成
                var floorObj = Instantiate(floor, new Vector3(i, j, 1), Quaternion.identity);
                floorObj.transform.parent = this.transform;

                /*
                //距離表示オブジェクト
                var distanceObj = Instantiate(distance, new Vector3(i, j, -1), Quaternion.identity);
                distanceObj.transform.SetParent(this.transform);

                if (!IsOutOfBounds(i, j) && cells[i,j]!=-1)
                {
                    distanceObj.transform.GetChild(0).gameObject.GetComponent<Text>().text = cells[i, j].ToString();
                }
                */
            }
        }
    }

    public int getCells(int i,int j)
    {
        return cells[i, j];
    }

    public int getDistance()
    {
        return cells[goalPos.x, goalPos.y];
    }

    public bool getFlag()
    {
        return flag;
    }

    public int getStartX()
    {
        return startPos.x;
    }
    public int getStartY()
    {
        return startPos.y;
    }
    public int getD()
    {
        return d;
    }
}