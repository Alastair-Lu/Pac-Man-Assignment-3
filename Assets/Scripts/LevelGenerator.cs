using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using Random = UnityEngine.Random;

public class LevelGenerator : MonoBehaviour
{
    public bool CsvReaderMode;
    List<List<int>> Grid = new List<List<int>>();
    private bool HasTeleporterBottom;
    private bool HasTeleporterSide = true;
    public Sprite[] square;
    public GameObject GridObj;
    public GameObject TilePrefab;
    public GameObject ExistingPP;
    public GameObject ExistingGrid;
    public GameObject PowerPellet;
    private List<List<Transform>> ObjList = new List<List<Transform>>();
    // Start is called before the first frame update
    void Start()
    {
        ExistingGrid.SetActive(false);
        ExistingPP.SetActive(false);
        //select which mode to read in procedural values in unity, reads csv by default
        if (CsvReaderMode)
        {
            var rows = File.ReadAllLines("Assets/Scripts/PacMan Level Map.csv");


            foreach (var row in rows)
            {
                List<int> values = new List<int>();
                var columns = row.Split(',');
                foreach (string value in columns)
                {
                    values.Add(Int32.Parse(value));
                }
                Grid.Add(values);

            }
        }
        else
        {
            int[,] intarray = new int[,] { { 1, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 7 }, 
                { 2, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 4 }, { 2, 5, 3, 4, 4, 3, 5, 3, 4, 4, 4, 3, 5, 4 }, 
                { 2, 6, 4, 0, 0, 4, 5, 4, 0, 0, 0, 4, 5, 4 }, { 2, 5, 3, 4, 4, 3, 5, 3, 4, 4, 4, 3, 5, 3 }, 
                { 2, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5 }, { 2, 5, 3, 4, 4, 3, 5, 3, 3, 5, 3, 4, 4, 4 }, 
                { 2, 5, 3, 4, 4, 3, 5, 4, 4, 5, 3, 4, 4, 3 }, { 2, 5, 5, 5, 5, 5, 5, 4, 4, 5, 5, 5, 5, 4 }, 
                { 1, 2, 2, 2, 2, 1, 5, 4, 3, 4, 4, 3, 0, 4 }, { 0, 0, 0, 0, 0, 2, 5, 4, 3, 4, 4, 3, 0, 3 }, 
                { 0, 0, 0, 0, 0, 2, 5, 4, 4, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 2, 5, 4, 4, 0, 3, 4, 4, 0 }, 
                { 2, 2, 2, 2, 2, 1, 5, 3, 3, 0, 4, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0, 5, 0, 0, 0, 4, 0, 0, 0 }, };
            for (int i = 0; i < intarray.GetLength(0); i++)
            {
                List<int> row = new List<int>();
                for(int j = 0;j < intarray.GetLength(1); j++)
                {
                    row.Add(intarray[i,j]);
                }
                Grid.Add(row);
            }
        }
        
        HasTeleporterBottom = TeleBotCheck();
        
        for (int i = 0; i < Grid.Count; i++)
        {
            if (Grid[i][Grid[i].Count - 1] == 2 || Grid[i][Grid[i].Count - 1] == 7 || Grid[i][Grid[i].Count - 1] == 1)
            {
                HasTeleporterSide = false;
                break;
            }
            else
            {
                HasTeleporterSide = true;
            }
            
            
        }
        
        if (HasTeleporterSide)
        {
            foreach(List<int> row in Grid)
            {
                for(int i = row.Count; i > 1; i--)
                {
                    row.Add(row[i - 2]);
                }
            }
        }
        else
        {
            foreach (List<int> row in Grid)
            {
                for (int i = row.Count; i > 0; i--)
                {
                    row.Add(row[i - 1]);
                    
                }
            }
            
        }

        if (HasTeleporterBottom)
        {
            for(int i = Grid.Count; i > 1; i--)
            {
                Grid.Add(Grid[i - 2]);
            }
        }
        else
        {
            for (int i = Grid.Count; i > 0; i--)
            {
                Grid.Add(Grid[i - 1]);

            }            
        }
        //Debug.Log("bottom " + HasTeleporterBottom + " side " + HasTeleporterSide);
        for (int i = 0; i < Grid.Count;i++)
        {
            ObjList.Add(new List<Transform>());
            
            for (int j = 0; j < Grid[i].Count; j++)
            {                
                ObjList[i].Add( SpawnTile(j, i).transform);
                
            }            
        }


    }

    // Update is called once per frame


    private bool TeleBotCheck()
    {
        if (!(Grid[Grid.Count - 1].Contains(2)) && !(Grid[Grid.Count - 1].Contains(7)) && !(Grid[Grid.Count - 1].Contains(1)))
        {
            return true;
        }
        return false;
    }

    private Vector2 WorldPosition(int x, int y)
    {
        return new Vector2((float)x - (((float)Grid[y].Count) / 2) + 0.5f, ((float)Grid.Count / 2)- (float)y  );
    }
    private GameObject SpawnTile(int x, int y)
    {

        GameObject tile = Instantiate(TilePrefab, WorldPosition(x, y), Quaternion.identity);
        tile.name = ("X: " + x + " Y: " + y);
        tile.transform.parent = GridObj.transform;
        var s = tile.GetComponent<SpriteRenderer>();
        s.sprite = square[Grid[y][x]];
        if (Grid[y][x] == 6)
        {
            Instantiate(PowerPellet, WorldPosition(x, y), Quaternion.identity).transform.parent = GridObj.transform;
        }
        Rotation(x, y, tile);
        if (x != 0 && y != 0 && x != Grid[y].Count - 1 && y != Grid.Count - 1)
        {
            InnerRotate(x, y, tile);
        }
        return tile;
    }

    private void Rotation(int x, int y, GameObject tile)
    {
        Vector3 rotate0 = new Vector3(0, 0, 0);
        Vector3 rotate90 = new Vector3(0, 0, 90);
        Vector3 rotate270 = new Vector3(0, 0, -90);
        Vector3 rotate180 = new Vector3(0, 0, 180);
        int grid = Grid[y][x];
        if (x == Grid[y].Count-1 && y == 0)
        {
            tile.transform.eulerAngles = rotate270;
        }
        if (x == 0 && y == Grid.Count-1)
        {
            tile.transform.eulerAngles = rotate90;
        }
        if (x == Grid[y].Count-1 && y == Grid.Count - 1)
        {
            tile.transform.eulerAngles = rotate180;
        }
        if (y == 0 && x>0 && x < Grid[y].Count-1)
        {
            int gridL = Grid[y][x - 1];
            int gridR = Grid[y][x + 1];
            Transform gridLO = ObjList[y][x - 1];
            if (gridL != 0 || gridR != 0 )
            {
                tile.transform.eulerAngles = rotate90;
            }
            if (grid == 7)
            {
                tile.transform.eulerAngles = rotate90;
            }
            if(grid == 1)
            {
                if ((gridL == 2 && gridLO.eulerAngles == rotate90) || gridL == 7 || (gridL == 1 && gridLO.eulerAngles == rotate0))
                {
                    tile.transform.eulerAngles = rotate270;
                }
                else
                {
                    tile.transform.eulerAngles = rotate0;
                }
            }
        }
        if (y == Grid.Count-1 && x > 0 && x < Grid[y].Count - 1)
        {
            int gridL = Grid[y][x - 1];
            int gridR = Grid[y][x + 1];
            Transform gridLO = ObjList[y][x - 1];
            if (gridL != 0 || gridR != 0)
            {
                tile.transform.eulerAngles = rotate90;
            }
            if (grid == 7)
            {
                tile.transform.eulerAngles = rotate270;
            }
            if (grid == 1)
            {
                if ((gridL == 2 && gridLO.eulerAngles == rotate90) || gridL == 7 || (gridL == 1 && gridLO.eulerAngles == rotate90))
                {
                    tile.transform.eulerAngles = rotate180;
                }
                else
                {
                    tile.transform.eulerAngles = rotate90;
                }
            }
        }
        if (x == 0 && y > 0 && y < Grid.Count - 1)
        {
            int gridT = Grid[y - 1][x];
            Transform gridTO = ObjList[y - 1][x];
            if (grid == 7)
            {
                tile.transform.eulerAngles = rotate180;
            }
            if (grid == 1)
            {
                if((gridT == 2 && gridTO.eulerAngles == rotate0) || gridT == 7 || (gridT == 1 && gridTO.eulerAngles == rotate0))
                {
                    tile.transform.eulerAngles = rotate90;
                }
            }
            if(grid == 2)
            {
                if(Grid[y - 1][x] == 0 || Grid[y + 1][x] == 0)
                {
                    tile.transform.eulerAngles = rotate90;
                }
            }
        }
        if (x == Grid[y].Count-1 && y > 0 && y < Grid.Count - 1)
        {
            int gridT = Grid[y - 1][x];
            Transform gridTO = ObjList[y - 1][x];
            if (grid == 1)
            {
                if ((gridT == 2 && gridTO.eulerAngles == rotate0) || gridT == 7 || (gridT == 1)&&!(gridT == 1 && gridTO.eulerAngles == rotate180))
                {
                    tile.transform.eulerAngles = rotate180;
                }
                if(gridT == 1 && gridTO.eulerAngles == rotate180) 
                {
                    tile.transform.eulerAngles = rotate270;
                }
            }
            if (grid == 2)
            {
                if (Grid[y - 1][x] == 0 || Grid[y + 1][x] == 0)
                {
                    tile.transform.eulerAngles = rotate90;
                }
            }
        }
        
    }
    private void InnerRotate(int x, int y, GameObject tile)
    {
        Vector3 rotate0 = new Vector3(0, 0, 0);
        Vector3 rotate90 = new Vector3(0, 0, 90);
        Vector3 rotate270 = new Vector3(0, 0, 270);
        Vector3 rotate180 = new Vector3(0, 0, 180);
        int grid = Grid[y][x];

        int gridL = Grid[y][x - 1];
        Transform gridLO = ObjList[y][x - 1];
        int gridT = Grid[y - 1][x];
        Transform gridTO = ObjList[y - 1][x];
        int gridR = Grid[y][x + 1];        
        int gridD = Grid[y + 1][x];
        if (x > 0 && x < Grid[y].Count - 1 && y > 0 && y < Grid.Count - 1)
        {
            if (grid == 1)
            {
                if ((gridT == 2 && gridTO.eulerAngles == rotate0) || (gridT == 7 && (gridTO.eulerAngles == rotate0 || gridTO.eulerAngles == rotate180)) || (gridT == 1 && (gridTO.eulerAngles == rotate0 || gridTO.eulerAngles == rotate270)))
                {
                    if((gridL==2&&gridLO.eulerAngles==rotate90)|| (gridL == 7 && (gridLO.eulerAngles == rotate90 || gridLO.eulerAngles == rotate270)) || (gridL == 1 && (gridLO.eulerAngles == rotate0 || gridLO.eulerAngles == rotate90)))
                    {
                        tile.transform.eulerAngles = rotate180;
                    }
                    else
                    {
                        tile.transform.eulerAngles = rotate90;
                    }
                }
                else
                {
                    if ((gridL == 2 && gridLO.eulerAngles == rotate90) || (gridL == 7 && (gridLO.eulerAngles == rotate90 || gridLO.eulerAngles == rotate270)) || (gridL == 1 && (gridLO.eulerAngles == rotate0 || gridLO.eulerAngles == rotate90)))
                    {
                        tile.transform.eulerAngles = rotate270;
                    }
                }
            }
            if(grid == 2)
            {
                if ((gridL == 2 && gridLO.eulerAngles == rotate90) || (gridL == 7 && (gridLO.eulerAngles == rotate90 || gridLO.eulerAngles == rotate270)) || (gridL == 1 && (gridLO.eulerAngles == rotate0 || gridLO.eulerAngles == rotate90)))
                {
                    tile.transform.eulerAngles = rotate90;
                }
            }
            if (grid == 7)
            {
                if (gridT == 3 || gridT == 4)
                {
                    tile.transform.eulerAngles = rotate270;
                }
                if (gridR == 3 || gridR == 4)
                {
                    tile.transform.eulerAngles = rotate180;
                }
                if (gridD == 3 || gridD == 4)
                {
                    tile.transform.eulerAngles = rotate90;
                }

            }
            if (grid == 4)
            {
                if(gridT==0 || gridT == 5 || gridT == 6 || gridD==0 || gridD == 5 || gridD == 6)
                {
                    tile.transform.eulerAngles = rotate90;
                }
            }

            if (grid == 3)
            {
                if (gridT == 0 || gridT == 5 || gridT == 6||!((gridT == 3 && (gridTO.eulerAngles == rotate0 || gridTO.eulerAngles == rotate270)) || (gridT == 4 && gridTO.eulerAngles == rotate0) || (gridT == 7 && gridTO.eulerAngles == rotate90)))
                {
                    if ((gridL == 4 && gridLO.eulerAngles == rotate90) || (gridL == 7 && gridLO.eulerAngles == rotate180) || (gridL == 3 && (gridLO.eulerAngles == rotate0 || gridLO.eulerAngles == rotate90)))
                    {
                        tile.transform.eulerAngles = rotate270;
                    }
                }
                else if ((gridT == 3 && (gridTO.eulerAngles == rotate0||gridTO.eulerAngles==rotate270)) || (gridT == 4&&gridTO.eulerAngles==rotate0) || (gridT == 7 && gridTO.eulerAngles == rotate90))
                {
                    if ((gridL == 4 && gridLO.eulerAngles == rotate90) || (gridL == 7 && gridLO.eulerAngles == rotate180) || (gridL == 3 && (gridLO.eulerAngles == rotate0 || gridLO.eulerAngles == rotate90)))
                    {
                        tile.transform.eulerAngles = rotate180;
                    }
                    else
                    {
                        tile.transform.eulerAngles = rotate90;
                    }
                }
            }
        }
    }

}
