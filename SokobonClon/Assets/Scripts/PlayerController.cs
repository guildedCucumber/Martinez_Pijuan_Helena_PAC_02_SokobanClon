using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private GridManager grid;

    private Vector3 playerPos;
    private Vector3 nextPos;

    // Start is called before the first frame update
    void OnEnable()
    {
        grid = GameObject.Find("GridManager").GetComponent<GridManager>();
    }

    void OnDisable()
    {
        this.enabled = true;
    }

    // Update is called once per frame
    void Update()
    {
        playerPos = transform.position;

        // Player controller input
        if (Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.W))
        {
            nextPos = playerPos + Vector3.up;

            if (nextPos.x >= 0 && nextPos.y >= 0 && nextPos.x < 5 && nextPos.y < 5)
            {
                // Check if the next position is a wall
                if (grid.CheckNextTile(nextPos) != "wall")
                {
                    if (grid.CheckNextTile(nextPos) == null || grid.CheckNextTile(nextPos) == "finish")
                    {
                        // Move player
                        grid.MovePlayer(nextPos);
                        Debug.Log("Player moves to " + nextPos);
                    }

                    // Assign temporary box-adjacent position
                    Vector3 boxPosAdjacent = nextPos + Vector3.up;

                    // Check if box is moving outside of the grid
                    if (boxPosAdjacent.x >= 0 && boxPosAdjacent.y >= 0 && boxPosAdjacent.x < 5 && boxPosAdjacent.y < 5)
                    {
                        // Check if the next position contains a box
                        if (grid.CheckNextTile(nextPos) == "box" || grid.CheckNextTile(nextPos) == "finish")
                        {
                            // Check that the box is not next to a wall or another box
                            if (grid.CheckNextTile(boxPosAdjacent) != "wall" && grid.CheckNextTile(boxPosAdjacent) != "box")
                            {
                                // Move box
                                grid.MoveBox(nextPos, Vector3.up);
                                // Move player
                                grid.MovePlayer(nextPos);
                                Debug.Log("Player moves to " + nextPos);

                                // Check if the next tile has a finish point
                                if (grid.CheckNextTile(boxPosAdjacent) == "finish")
                                {
                                    // Check if level is finished
                                    grid.FinishBox();

                                    if (grid.LevelCompleted())
                                    {
                                        ResetPlayer();
                                    }
                                }
                            }
                        }
                    }
                }
                else
                {
                    Debug.Log("Wall found at " + nextPos);
                }
            }
            else
            {
                Debug.Log("Cannot walk outside the grid!");
            }

        }
        else if (Input.GetKeyDown(KeyCode.DownArrow) || Input.GetKeyDown(KeyCode.S))
        {
            nextPos = playerPos + Vector3.down;

            if (nextPos.x >= 0 && nextPos.y >= 0 && nextPos.x < 5 && nextPos.y < 5)
            {
                // Check if the next position is a wall
                if (grid.CheckNextTile(nextPos) != "wall")
                {
                    if (grid.CheckNextTile(nextPos) == null || grid.CheckNextTile(nextPos) == "finish")
                    {
                        // Move player
                        grid.MovePlayer(nextPos);
                        Debug.Log("Player moves to " + nextPos);
                    }

                    // Assign temporary box-adjacent position
                    Vector3 boxPosAdjacent = nextPos + Vector3.down;

                    // Check if box is moving outside of the grid
                    if (boxPosAdjacent.x >= 0 && boxPosAdjacent.y >= 0 && boxPosAdjacent.x < 5 && boxPosAdjacent.y < 5)
                    {
                        // Check if the next position contains a box
                        if (grid.CheckNextTile(nextPos) == "box" || grid.CheckNextTile(nextPos) == "finish")
                        {
                            // Check that the box is not next to a wall or another box
                            if (grid.CheckNextTile(boxPosAdjacent) != "wall" && grid.CheckNextTile(boxPosAdjacent) != "box")
                            {
                                // Move box
                                grid.MoveBox(nextPos, Vector3.down);
                                // Move player
                                grid.MovePlayer(nextPos);
                                Debug.Log("Player moves to " + nextPos);

                                // Check if the next tile has a finish point
                                if (grid.CheckNextTile(boxPosAdjacent) == "finish")
                                {
                                    // Check if level is finished
                                    grid.FinishBox();

                                    if (grid.LevelCompleted())
                                    {
                                        ResetPlayer();
                                    }
                                }
                            }
                        }
                    }
                }
                else
                {
                    Debug.Log("Wall found at " + nextPos);
                }
            }
            else
            {
                Debug.Log("Cannot walk outside the grid!");
            }
        }
        else if (Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.A))
        {
            nextPos = playerPos + Vector3.left;

            if (nextPos.x >= 0 && nextPos.y >= 0 && nextPos.x < 5 && nextPos.y < 5)
            {
                // Check if the next position is a wall
                if (grid.CheckNextTile(nextPos) != "wall")
                {
                    if (grid.CheckNextTile(nextPos) == null || grid.CheckNextTile(nextPos) == "finish")
                    {
                        // Move player
                        grid.MovePlayer(nextPos);
                        Debug.Log("Player moves to " + nextPos);
                    }

                    // Assign temporary box-adjacent position
                    Vector3 boxPosAdjacent = nextPos + Vector3.left;

                    // Check if box is moving outside of the grid
                    if (boxPosAdjacent.x >= 0 && boxPosAdjacent.y >= 0 && boxPosAdjacent.x < 5 && boxPosAdjacent.y < 5)
                    {
                        // Check if the next position contains a box
                        if (grid.CheckNextTile(nextPos) == "box" || grid.CheckNextTile(nextPos) == "finish")
                        {
                            // Check that the box is not next to a wall or another box
                            if (grid.CheckNextTile(boxPosAdjacent) != "wall" && grid.CheckNextTile(boxPosAdjacent) != "box")
                            {
                                // Move box
                                grid.MoveBox(nextPos, Vector3.left);
                                // Move player
                                grid.MovePlayer(nextPos);
                                Debug.Log("Player moves to " + nextPos);

                                // Check if the next tile has a finish point
                                if (grid.CheckNextTile(boxPosAdjacent) == "finish")
                                {
                                    // Check if level is finished
                                    grid.FinishBox();

                                    if (grid.LevelCompleted())
                                    {
                                        ResetPlayer();
                                    }
                                }
                            }
                        }
                    }
                }
                else
                {
                    Debug.Log("Wall found at " + nextPos);
                }
            }
            else
            {
                Debug.Log("Cannot walk outside the grid!");
            }
        }
        else if (Input.GetKeyDown(KeyCode.RightArrow) || Input.GetKeyDown(KeyCode.D))
        {
            nextPos = playerPos + Vector3.right;

            if (nextPos.x >= 0 && nextPos.y >= 0 && nextPos.x < 5 && nextPos.y < 5)
            {
                // Check if the next position is a wall
                if (grid.CheckNextTile(nextPos) != "wall")
                {
                    if (grid.CheckNextTile(nextPos) == null || grid.CheckNextTile(nextPos) == "finish")
                    {
                        // Move player
                        grid.MovePlayer(nextPos);
                        Debug.Log("Player moves to " + nextPos);
                    }

                    // Assign temporary box-adjacent position
                    Vector3 boxPosAdjacent = nextPos + Vector3.right;

                    // Check if box is moving outside of the grid
                    if (boxPosAdjacent.x >= 0 && boxPosAdjacent.y >= 0 && boxPosAdjacent.x < 5 && boxPosAdjacent.y < 5)
                    {
                        // Check if the next position contains a box
                        if (grid.CheckNextTile(nextPos) == "box" || grid.CheckNextTile(nextPos) == "finish")
                        {
                            // Check that the box is not next to a wall or another box
                            if (grid.CheckNextTile(boxPosAdjacent) != "wall" && grid.CheckNextTile(boxPosAdjacent) != "box")
                            {
                                // Move box
                                grid.MoveBox(nextPos, Vector3.right);
                                // Move player
                                grid.MovePlayer(nextPos);
                                Debug.Log("Player moves to " + nextPos);

                                // Check if the next tile has a finish point
                                if (grid.CheckNextTile(boxPosAdjacent) == "finish")
                                {
                                    // Check if level is finished
                                    grid.FinishBox();
                                    if (grid.LevelCompleted())
                                    {
                                        ResetPlayer();
                                    }
                                }
                            }
                        }
                    }
                }
                else
                {
                    Debug.Log("Wall found at " + nextPos);
                }
            }
            else
            {
                Debug.Log("Cannot walk outside the grid!");
            }
        }
    }

    private void ResetPlayer()
    {
        this.enabled = false;
    }
}
