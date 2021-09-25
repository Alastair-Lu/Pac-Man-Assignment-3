using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using Random = UnityEngine.Random;

public class LevelGen : MonoBehaviour
{
    List<List<int>> Grid = new List<List<int>>();
    private bool HasTeleporterBottom;
    private bool HasTeleporterSide = true;
    public Sprite[] square;
    public GameObject GridObj;
    public GameObject TilePrefab;
    public GameObject ExistingPP;
    public GameObject ExistingGrid;
    public GameObject PowerPellet;
    // Start is called before the first frame update
    void Start()
    {
        ExistingGrid.SetActive(false);
        ExistingPP.SetActive(false);
        var rows = File.ReadAllLines("Assets/Scripts/PacManLevel.csv");
        

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
        HasTeleporterBottom = TeleBotCheck();
        
        for (int i = 0; i < Grid.Count; i++)
        {
            if (Grid[i][Grid[i].Count - 1] == 2 || Grid[i][Grid[i].Count - 1] == 7)
            {
                HasTeleporterSide = false;
                break;
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
            Debug.Log(Grid[0][0] + " " + Grid[1][0] + " " + Grid[2][0] + " " + Grid[3][0] + " " + Grid[4][0] + " " + Grid[5][0] + " " + Grid[6][0] + " " + Grid[7][0] + " " + Grid[8][0] + " " +
                Grid[9][0] + " " + Grid[10][0] + " " + Grid[11][0] + " " + Grid[12][0] + " " + Grid[13][0] + " " + Grid[14][0] + " " + Grid[15][0] + " " + Grid[16][0] + " " + Grid[17][0] + " " +
                Grid[18][0] + " " + Grid[19][0] + " " + Grid[20][0]);
            Debug.Log(Grid[0][0] + " " + Grid[0][1] + " " + Grid[0][2] + " " + Grid[0][3] + " " + Grid[0][4] + " " + Grid[0][5] + " " + Grid[0][6] + " " + Grid[0][7] + " " + Grid[0][8] + " " +
                Grid[0][9] + " " + Grid[0][10] + " " + Grid[0][11] + " " + Grid[0][12] + " " + Grid[0][13] + " " + Grid[0][14] + " " + Grid[0][15] + " " + Grid[0][16] + " " + Grid[0][17] + " " +
                Grid[0][18] + " " + Grid[0][19] + " " + Grid[0][20]);
        }
        else
        {
            for (int i = Grid.Count; i > 0; i--)
            {
                Grid.Add(Grid[i - 1]);

            }            
        }

        for(int i = 0; i < Grid.Count;i++)
        {
            for(int j = 0; j < Grid[i].Count; j++)
            {
                float colourValue = Random.Range(0.0f, 1.0f);
                SpawnTile(j, i, colourValue);
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
            Debug.Log("value "+Grid[1][1]);
            Debug.Log(HasTeleporterSide);
        }
        
    }

    private bool TeleBotCheck()
    {
        if (!(Grid[Grid.Count - 1].Contains(2)) && !(Grid[Grid.Count - 1].Contains(7)))
        {
            return true;
        }
        return false;
    }

    private Vector2 WorldPosition(int x, int y)
    {
        return new Vector2((float)x - (((float)Grid[y].Count) / 2) + 0.5f, ((float)y - (float)Grid.Count / 2) + 1.0f);
    }
    private void SpawnTile(int x, int y, float value)
    {

        GameObject tile = Instantiate(TilePrefab, WorldPosition(x, y), Quaternion.identity);
        tile.transform.parent = GridObj.transform;
        var s = tile.GetComponent<SpriteRenderer>();
        s.sprite = square[Grid[y][x]];
        if(Grid[y][x] == 6)
        {
            Instantiate(PowerPellet, WorldPosition(x, y), Quaternion.identity);
        }
        
    }

  

}
