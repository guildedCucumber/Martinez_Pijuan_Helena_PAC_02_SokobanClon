using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class ParseLevel : MonoBehaviour
{
    public LevelData levelData;                         // Reference to level data

    [SerializeField] private TextAsset[] levelFile;     // Array of json files

    [SerializeField] private int currentLevel;

    private Vector3 playerPos;                          // Stores initial player position
    private List<Vector3> boxPos;                       // Stores initial box positions
    private List<Vector3> wallPos;                      // Stores initial box positions
    private List<Vector3> finishPos;                    // Stores initial box positions

    private int gridSize = 25;                          // The grid size is always 25 for the purposes of this exercise, but a new method should be implemented to allow different level sizes


    public static LevelData CreateFromJSON(string jsonString)
    {
        return JsonUtility.FromJson<LevelData>(jsonString);
    }

    void OnEnable()
    {
        Debug.Log("ParseLevel enabled");

        currentLevel = LevelManager.GetLevel();
        Debug.Log("CURRENT LEVEL: " + currentLevel);

        GetLevelData(currentLevel);

        // Retrieve all positions at the begining of the level
        FindPlayer();
        FindWalls();
        FindBoxes();
        FindFinish();
    }

    void OnDisable()
    {
        Debug.Log("ParseLevel disabled");

        boxPos.Clear();
        wallPos.Clear();
        finishPos.Clear();

        this.enabled = true;
    }

    private void GetLevelData(int level)
    {
        levelData = CreateFromJSON(levelFile[level - 1].text);
        Debug.Log("FILE PARSED");
    }

    public int GetNumLevel() { return currentLevel; }

    // Find player position in current level
    private void FindPlayer()
    {
        for (int i = 0; i < gridSize; i++)
        {
            if (levelData.tiles[i].content == "player")
            {
                Vector3 pos = new Vector3(levelData.tiles[i].x, levelData.tiles[i].y);
                playerPos = pos;
                Debug.Log("PLAYER FOUND AT: " + pos);

            }
        }
    }

    // Create a list for all wall positions in current level
    private void FindWalls()
    {
        wallPos = new List<Vector3>();

        for (int i = 0; i < gridSize; i++)
        {
            if (levelData.tiles[i].wall)
            {
                Vector3 pos = new Vector3(levelData.tiles[i].x, levelData.tiles[i].y);
                wallPos.Add(pos);
                //Debug.Log("WALL FOUND AT: " + pos);

            }
        }
    }

    // Create a list for all box positions in current level
    private void FindBoxes()
    {
        boxPos = new List<Vector3>();

        for (int i = 0; i < gridSize; i++)
        {
            if (levelData.tiles[i].content == "box")
            {
                Vector3 pos = new Vector3(levelData.tiles[i].x, levelData.tiles[i].y);
                boxPos.Add(pos);
                Debug.Log("BOX FOUND AT: " + pos);

            }
        }
    }

    // Create a list for all finish points' positions in current level
    private void FindFinish()
    {
        finishPos = new List<Vector3>();

        for (int i = 0; i < gridSize; i++)
        {
            if (levelData.tiles[i].content == "finish")
            {
                Vector3 pos = new Vector3(levelData.tiles[i].x, levelData.tiles[i].y);
                finishPos.Add(pos);
                Debug.Log("FINISH POINT FOUND AT: " + pos);

            }
        }
    }

    // Retrieve player position
    public Vector3 GetInitialPlayerPos() { return playerPos; }

    // Retrieve list of wall positions
    public List<Vector3> GetInitialWallPos() { return wallPos; }

    // Retrieve list of box positions
    public List<Vector3> GetInitialBoxPos() { return boxPos; }

    // Retrieve list of finish points positions
    public List<Vector3> GetFinishPos() { return finishPos; }



}
