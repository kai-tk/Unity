using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mino : MonoBehaviour
{
    float nowTime;
    float previousTime;
    public float fallTime = 1f;

    private static int width = 10;
    private static int height = 20;

    Vector3 rotationPoint;
    public GameObject[] Minos;
    GameObject moveMino;

    private static Transform[,] grid = new Transform[width, height];
    public int[,] board = new int[width, height];
    public int[] line = new int[height];
    public int maxHeight;
    public int minoType;

    gameManagement script;

    // Start is called before the first frame update
    void Start()
    {
        script = GetComponent<gameManagement>();

        //initialize();
    }

    public void initialize()
    {
        nowTime = 0;
        previousTime = 0;
        maxHeight = 0;

        for(int i = 0; i < height; i++)
        {
            for(int j = 0; j < width; j++)
            {
                if (grid[j, i] != null)
                {
                    Destroy(grid[j, i].gameObject);
                    grid[j, i] = null;
                }
                board[j, i] = 0;
            }
            line[i] = 0;
        }

        setMino();
    }

    // Update is called once per frame
    void Update()
    {
        nowTime += Time.deltaTime;

        if (moveMino != null)
        {
            if (Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.A))
            {
                MinoMovement(1);
            }
            else if (Input.GetKeyDown(KeyCode.RightArrow) || Input.GetKeyDown(KeyCode.D))
            {
                MinoMovement(2);
            }
            else if (Input.GetKeyDown(KeyCode.DownArrow) || Input.GetKeyDown(KeyCode.S))
            {
                MinoMovement(3);
            }
            else if (Input.GetKeyDown(KeyCode.Space))
            {
                MinoMovement(4);
            }

            if (nowTime - previousTime >= fallTime)
            {
                previousTime = nowTime;
                MinoMovement(3);
            }
        }
    }

    public bool MinoMovement(int mode)
    {
        if (moveMino != null) {
            switch (mode)
            {
                case 1:
                    moveMino.transform.position += new Vector3(-1, 0, 0);
                    if (!ValidMovement())
                    {
                        moveMino.transform.position += new Vector3(1, 0, 0);
                    }
                    break;
                case 2:
                    moveMino.transform.position += new Vector3(1, 0, 0);
                    if (!ValidMovement())
                    {
                        moveMino.transform.position += new Vector3(-1, 0, 0);
                    }
                    break;
                case 3:
                    moveMino.transform.position += new Vector3(0, -1, 0);
                    if (!ValidMovement())
                    {
                        moveMino.transform.position += new Vector3(0, 1, 0);
                        addGrid();
                        var destroyLine = checkLines();
                        var minos = 0;
                        for (int i = 0; i <= maxHeight; i++)
                        {
                            minos += line[i];
                        }
                        bool gameContinue = script.addScore(destroyLine, maxHeight + 1, minos);
                        if (gameContinue)
                        {
                            setMino();
                        }
                        else
                        {
                            moveMino = null;
                        }
                    }
                    return false;
                case 4:
                    moveMino.transform.RotateAround(moveMino.transform.TransformPoint(rotationPoint), new Vector3(0, 0, 1), 270);
                    if (!ValidMovement())
                    {
                        moveMino.transform.RotateAround(moveMino.transform.TransformPoint(rotationPoint), new Vector3(0, 0, 1), 90);
                    }
                    break;
                default:
                    break;
            }
            return true;
        }
        else
        {
            return false;
        }
    }

    bool ValidMovement()
    {
        foreach (Transform children in moveMino.transform)
        {
            int roundX = Mathf.RoundToInt(children.transform.position.x);
            int roundY = Mathf.RoundToInt(children.transform.position.y);

            // minoがステージよりはみ出さないように制御
            if (roundX < 0 || roundX >= width || roundY < 0 || roundY >= height)
            {
                return false;
            }
            if (grid[roundX, roundY] != null)
            {
                return false;
            }
        }
        return true;
    }

    void addGrid()
    {
        foreach (Transform children in moveMino.transform)
        {
            int roundX = Mathf.RoundToInt(children.transform.position.x);
            int roundY = Mathf.RoundToInt(children.transform.position.y);

            grid[roundX, roundY] = children;
            board[roundX, roundY] = 1;
            line[roundY] += 1;
            maxHeight = Mathf.Max(maxHeight, roundY);
        }
    }

    int checkLines()
    {
        int destroyLine = 0;
        for(int i = height - 1; i >= 0; i--)
        {
            if (line[i]==width)
            {
                DeleteLine(i);
                RowDown(i);
                destroyLine += 1;
            }
        }
        maxHeight -= destroyLine;
        return destroyLine;
    }

    void DeleteLine(int i)
    {
        for(int j = 0; j < width; j++)
        {
            Destroy(grid[j, i].gameObject);
            grid[j, i] = null;
            board[j, i] = 0;
        }
    }

    void RowDown(int i)
    {
        for (int y = i + 1; y < height; y++)
        {
            for (int j = 0; j < width; j++)
            {
                if (grid[j, y] != null)
                {
                    grid[j, y - 1] = grid[j, y];
                    grid[j, y] = null;
                    grid[j, y - 1].transform.position -= new Vector3(0, 1, 0);
                    board[j, y - 1] = board[j, y];
                    board[j, y] = 0;
                }
            }
            line[y - 1] = line[y];
            line[y] = 0;
        }
    }

    void setMino()
    {
        minoType = Random.Range(0, Minos.Length);
        if (minoType == 0)
        {
            rotationPoint = new Vector3(0.5f, 0.5f, 0);
        }
        else
        {
            rotationPoint = Vector3.zero;
        }
        moveMino = Instantiate(Minos[minoType], transform.position, Quaternion.identity);
    }

    public int numHole()
    {
        var count = 0;
        int[] dirx = { 0, 1, 0, -1 };
        int[] diry = { 1, 0, -1, 0 };
        bool[,] check = new bool[width, height];

        for(int i = 0; i < height; i++)
        {
            for(int j = 0; j < width; j++)
            {
                check[j,i]= false;
            }
        }

        var stack = new Stack<int[]>();

        for (int i = 0; i < height; i++)
        {
            for (int j = 0; j < width; j++)
            {
                if(board[j,i]==0 && !check[j, i])
                {
                    count++;
                    int[] array = { j, i };
                    stack.Push(array);
                    while (stack.Count > 0)
                    {
                        var point = stack.Pop();
                        check[point[0], point[1]] = true;
                        for(int k = 0; k < 4; k++)
                        {
                            var nx = point[0] + dirx[k];
                            var ny = point[1] + diry[k];
                            if (nx< 0 || nx >= width || ny < 0 || ny >= height)
                            {
                                continue;
                            }
                            if (board[nx,ny]==0 && !check[nx, ny])
                            {
                                array[0] = nx;
                                array[1] = ny;
                                stack.Push(array);
                            }
                        }
                    }
                }
            }
        }
        return count;
    }
}
