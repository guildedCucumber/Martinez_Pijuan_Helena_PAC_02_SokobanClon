using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridManager : MonoBehaviour
{

    // GRID INFO.
    [SerializeField] private int width = 5;
    [SerializeField] private int height = 5;
    [SerializeField] private int numLevel;

    [SerializeField] private Tile tile;                     // Reference to Tile script
    [SerializeField] private ParseLevel level;              // Reference to ParseLevel script

    [SerializeField] private Transform cam;

    [SerializeField] private GameObject player;
    [SerializeField] private GameObject box;
    [SerializeField] private GameObject finish;

    // MOVABLE OBJECTS.
    private List<GameObject> boxes;                         // Stores tile content

    // IMMOVABLE OBJECTS.
    private List<Vector3> wallPositions;                    // Stores wall positions
    private List<Vector3> finishPositions;                  // Stores finish positions

    private bool isWall;
    private int numBox;                      
    private int numFinish;
    private bool levelCompleted;

    void OnEnable()
    {
        Debug.Log("GridManager enabled");
        numLevel = level.GetNumLevel();

        boxes = new List<GameObject>();
        wallPositions = new List<Vector3>();
        finishPositions = new List<Vector3>();

        numBox = 1;
        numFinish = 1;

        levelCompleted = false;

        GenerateGrid();

    }

    void OnDisable()
    {
        Debug.Log("GridManager disabled");

        ResetAllValues();

        this.enabled = true;
    }

    void Update()
    {

    }

    void GenerateGrid()
    {

        for (int x = 0; x < width; x++) 
        { 
            for (int y = 0; y < height; y++)
            {
                Vector3 currentPos = new Vector3(x, y);
                var spawnedTile = Instantiate(tile, currentPos, Quaternion.identity);
                spawnedTile.name = $"Tile {x} {y}";

                // Find and assign wall positions
                wallPositions = level.GetInitialWallPos();

                isWall = wallPositions.Contains(currentPos);
                spawnedTile.Init(isWall);

                // Find and assign player position
                if (level.GetInitialPlayerPos() == currentPos)
                {
                    Debug.Log("CURRENT POSITION TO ASSIGN PLAYER: " + currentPos);
                    SetPlayer(currentPos);
                }

                // Find and assign box positions
                if (level.GetInitialBoxPos().Contains(currentPos))
                {
                    SetBox(currentPos);
                }

                // Find and assign finish positions
                if (level.GetFinishPos().Contains(currentPos))
                {
                    SetFinish(currentPos);
                }

            }
        }

        // Allign camera with grid
        cam.transform.position = new Vector3((float)width / 2 - 0.5f, (float)height / 2 - 0.5f, -10);
    }

    private void SetPlayer(Vector3 pos)
    {
        player = Instantiate(player, pos, Quaternion.identity);
        player.name = $"Player";
        Debug.Log("PLAYER OBJECT CREATED!");
    }

    private void SetBox(Vector3 pos)
    {
        var boxTile = Instantiate(box, pos, Quaternion.identity);
        boxTile.name = $"Box {numBox}";
        Debug.Log("BOX Nº " + numBox + " CREATED!");

        numBox++;

        boxes.Add(boxTile);
    }

    private void SetFinish(Vector3 pos)
    {
        var finishTile = Instantiate(finish, pos, Quaternion.identity);
        finishTile.name = $"Finish {numFinish}";
        Debug.Log("FINISH POINT Nº " + numFinish + " CREATED!");

        numFinish++;

        finishPositions.Add(pos);
    }

    private bool FindBox(Vector3 pos)
    {
        for (int i = 0; i < boxes.Count; i++)
        {
            if (boxes[i].transform.position == pos)
            {
                return true;
            }
        }
        return false;
    }

    private List<Vector3> FindBoxPositions()
    {
        List<Vector3> boxPositions = new List<Vector3>();

        for (int i = 0; i < boxes.Count; i++)
        {
            boxPositions.Add(boxes[i].transform.position);
        }
        
        return boxPositions;
    }

    public void MovePlayer(Vector3 pos)
    {
        player.transform.position = pos;
    }

    public void MoveBox(Vector3 boxPos, Vector3 direction)
    {
        Vector3 nextBoxPos = new Vector3(boxPos.x + direction.x, boxPos.y + direction.y);

        for (int i = 0; i < boxes.Count; i++)
        {
            if (boxes[i].transform.position == boxPos)
            {
                boxes[i].transform.position = nextBoxPos;
            }
        }
    }

    public string CheckNextTile(Vector3 pos)
    {        
        // Next tile is a wall
        if (wallPositions.Contains(pos))
        {
            return "wall";
        }
        
        // Next tile has a finish point
        if (finishPositions.Contains(pos))
        {
            return "finish";
        }

        // Next tile has a box
        if (FindBox(pos))
        {
            return "box";
        }

        return null;
    }

    public int GetFinishPoints()
    {
        return finishPositions.Count;
    }

    public void FinishBox()
    {
        List<Vector3> currentBoxPos = FindBoxPositions();

        if (CompareLists(currentBoxPos, finishPositions))
        {
            levelCompleted = true;

            Debug.Log("LEVEL COMPLETED!");
            LevelManager.NextLevel();

            level.enabled = false;
            this.enabled = false;
        }
    }    

    private bool CompareLists(List<Vector3> A, List<Vector3> B)
    {
        if (A.Count != B.Count)
        {
            return false;
        }

        //A.Sort();
        //B.Sort();

        for (int i = 0; i < A.Count; i++)
        {
            if (A[i] != B[i])
            //if (A[i].x.CompareTo(B[i].x) != 0 && A[i].y.CompareTo(B[i].y) != 0)
            {
                return false;
            }
        }

        return true;
    }

    public bool LevelCompleted()
    {
        return levelCompleted;
    }

    private void ResetAllValues()
    {
        // Empty lists of positions and items on the scene
        boxes.Clear();
        wallPositions.Clear();
        finishPositions.Clear();

        // Destroy all tiles on scene
        var tilesOnScene = GameObject.FindGameObjectsWithTag("Tile");
        for (int i = 0; i < tilesOnScene.Length; i++)
        {
            Destroy(tilesOnScene[i]);
        }

        // Destroy all items on scene
        var itemsOnScene = GameObject.FindGameObjectsWithTag("Item");
        for (int i = 0; i < itemsOnScene.Length; i++)
        {
            Destroy(itemsOnScene[i]);
        }

        Destroy(player);
    }

}
