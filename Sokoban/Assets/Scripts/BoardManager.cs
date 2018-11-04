using UnityEngine;
using UnityEngine.SceneManagement;

public class BoardManager : MonoBehaviour
{
    public GameObject crate;
    public GameObject floor;
    public GameObject goal;
    public GameObject player;
    public GameObject wall;
    
    static string[] levelneg1 = {
            "#########",
            "#....***#",
            "#....*#*#",
            "#....***#",
            "#.ooo...#",
            "#.o@o...#",
            "#.ooo...#",
            "#.......#",
            "#########"
        };
    
    static string[] level0 = {
        "#########",
        "#....***#",
        "#...#*#*#",
        "#....***#",
        "#.ooo.#.#",
        "#.o@o...#",
        "#.ooo...#",
        "#.......#",
        "#########"
    };

    static string[] level1 = {
        "#########",
        "#....***#",
        "#.#.#*#*#",
        "#.#..***#",
        "#.ooo.#.#",
        "#.o@o...#",
        "#.ooo##.#",
        "#.......#",
        "#########"
    };
    
    static readonly string[][] levels1 = { level1 };
    static readonly string[][] levels0 = { level0 };
    static readonly string[][] levelsneg1 = { levelneg1 };

    Transform board;

    public int SetupBoard(int levelIndex)
    {
        float scale = GameManager.scale;
        int goals = 0;
        
        board = new GameObject("Board").transform;

        string[] level = levels0[levelIndex];

        if (SceneManager.GetActiveScene().name.Equals("Level"))
        {
           level = levels1[levelIndex];
        }
        
        else  if (SceneManager.GetActiveScene().name.Equals("Level2"))
        {
            level = levels0[levelIndex];
        }

        else
        {
            level = levelsneg1[levelIndex];
        }
        
        float maxY = level.Length;
        float maxX = 0;

        for (int y = 0; y < level.Length; y++)
        {
            string row = level[y];
            maxX = Mathf.Max(row.Length, maxX);
            for (int x = 0; x < row.Length; x++)
            {
                GameObject tile = null;
                switch (row[x])
                {
                    case '#':
                        tile = wall;
                        break;
                    case '@':
                        tile = player;
                        break;
                    case 'o':
                        tile = crate;
                        break;
                    case '*':
                        tile = goal;
                        break;
                    case '.':
                        tile = floor;
                        break;

                }

                GameObject instance = Instantiate(tile, new Vector3(x / scale, (level.Length - y) / scale, 0), Quaternion.identity);
                instance.transform.SetParent(board);

                if (tile != floor)
                {
                    instance = Instantiate(floor, new Vector3(x / scale, (level.Length - y) / scale, 0), Quaternion.identity);
                    instance.transform.SetParent(board);

                    if (tile == goal)
                    {
                        instance.tag = "Untagged";
                        goals += 1;
                    }
                }
            }
        }
        
        float halfTile = 1 / (scale);
        board.position = new Vector3(-(maxX / 2 - halfTile) / scale, -(maxY / 2 + halfTile) / scale, 0);

        return goals;
    }
}