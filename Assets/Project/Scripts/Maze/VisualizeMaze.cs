using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VisualizeMaze : MazeGame
{
    public GameObject mazePosPrefab;
    public Transform player;
    private int endPointX;
    private int endPointY;
    //public GameObject winPopup;
    //public GameObject mazeGrid;
    private MazePosition[,] finalmaze;
    private List<bool> test = new List<bool>();
    private List<bool> test1 = new List<bool>();
    private List<bool> test2 = new List<bool>();
    private List<bool> test3 = new List<bool>();





    private void Visualize()
    {
        for (int x = 0; x < Maze.MAZE_WIDTH; x++)
        {
            for (int y = 0; y < Maze.MAZE_HEIGHT; y++)
            {
                MazePosition pos = m.GetPosition(x, y);
                endPointX = m.endPos.x;
                endPointY = m.endPos.y;
                if (pos == null)
                {
                    continue;
                }
                var newPosObj = Instantiate(mazePosPrefab, transform);
                newPosObj.transform.localPosition = new Vector2(x, y);
                var newObjScript = newPosObj.GetComponent<VisualizePosition>();
                if (m.startPos.x == x && m.startPos.y == y || m.endPos.x == x && m.endPos.y == y)
                {
                    pos = new MazePosition(pos);
                    if (x == 0)
                    {
                        pos.canMoveWest = true;
                    }
                    if (x == Maze.MAZE_WIDTH - 1)
                    {
                        pos.canMoveEast = true;
                    }
                    if (y == 0)
                    {
                        pos.canMoveSouth = true;
                    }
                    if (y == Maze.MAZE_HEIGHT - 1)
                    {
                        pos.canMoveNorth = true;
                    }
                }
                newObjScript.Visualize(pos);
                test.Add(pos.canMoveNorth);
                test1.Add(pos.canMoveSouth);
                test2.Add(pos.canMoveEast);
                test3.Add(pos.canMoveWest);

                //Debug.Log(pos.canMoveWest);
                // finalmaze = new MazePosition[Maze.MAZE_WIDTH, Maze.MAZE_HEIGHT];
                // finalmaze[x, y] = pos;
                // for (int i = 0; i < Maze.MAZE_WIDTH; i++)
                // {
                //     for (int j = 0; j < Maze.MAZE_HEIGHT; j++)
                //     {
                //         Debug.Log(finalmaze[i, j]);
                //     }
                // }
            }
        }
        List<bool> canMoveNorthList = new List<bool>{true,true,false,false,true,true,true,false,true,true,true,true,true,true,false,true,false,false,true,false,false,false,true,false,true,false,false,true,false,false,true,false,true,false,false,true,false,true,true,true,false,true,false,false,false,true,false,true,false,false,false,false,true,true,false,true,false,false,false,false,true,false,false,true,false,true,false,true,false,true,false,true,false,true,false,false,true,false,false,true,false,true,true,true,false,false,true,false,true,false,false,false,true,false,false,false,true,false,true,true,true,false,true,false,false,true,false,false,true,false,true,false,false,false,true,true,false,true,true,false,false,true,false,true,true,false,true,false,false,false,false,true,false,false,false,false,true,true,true,false,true,false,true,false,false,true,true,false,true,false,false,true,true,false,false,true,false,true,true,false,false,false,false,true,false,true,false,false,false,false,true,true,true,true,false,false,true,true,true,false,true,true,false,false,true,true,false,false,true,false,false,true,true,false,false,false,true,true,true,true,false,true,true,false,true,false,true,false,true,false,true,true,true,false,true,true,true,true,true,true,true,false,true,true,false};
        
        List<bool> canMoveSouthList = new List<bool>{};
        
        int j =0;
        foreach(var i in test)
        {
            Debug.Log("item for test at"+ j+"="+i);
            j= j+1;
        }

        int k =0;
        foreach(var i in test1)
        {
            Debug.Log("item for test1 at"+ k+"="+i);
            k= k+1;
        }

        int r =0;
        foreach(var i in test2)
        {
            Debug.Log("item for test2 at"+ r+"="+i);
            r= r+1;
        }

        int n =0;
        foreach(var i in test3)
        {
            Debug.Log("item for test3 at"+ n+"="+i);
            n= n+1;
        }



    }

    // Start is called before the first frame update
    public override void Start()
    {
        base.Start();
        Visualize();
    }

    // Update is called once per frame
    public override void Update()
    {
        base.Update();
        //player.localPosition = new Vector3(playerPos.x, playerPos.y, 0);
        //if(playerPos.x == endPointX && playerPos.y == endPointY)
        {
            //winPopup.SetActive(true);
            //mazeGrid.SetActive(false);
        }
        
    }
}
