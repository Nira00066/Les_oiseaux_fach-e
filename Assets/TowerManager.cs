using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerManager : MonoBehaviour
{
    [SerializeField] private GameObject[] towerBlocks; // Array of tower blocks
    [SerializeField] private Transform[] initialPositions; // Array of initial positions for the tower blocks
    [SerializeField] private GameObject ball; // The ball GameObject
    [SerializeField] private Transform ballInitialPosition; // Initial position of the ball

    private void Start()
    {
        if (towerBlocks.Length != initialPositions.Length)
        {
            Debug.LogError("The number of tower blocks and initial positions must be the same.");
        }

        ResetTower();
        ResetBall();
    }

    private void Update()
    {
        CheckIfAllBlocksFallen();
    }

    private void CheckIfAllBlocksFallen()
    {
        foreach (GameObject block in towerBlocks)
        {
            if (block.transform.position.y > -1) // Adjust this value based on your ground level
            {
                return; // If any block is not fallen, return
            }
        }

        // If all blocks are fallen, reset the tower and the ball
        ResetTower();
        ResetBall();
    }

    private void ResetTower()
    {
        for (int i = 0; i < towerBlocks.Length; i++)
        {
            towerBlocks[i].transform.position = initialPositions[i].position;
            towerBlocks[i].GetComponent<Rigidbody2D>().velocity = Vector2.zero; // Reset velocity
            towerBlocks[i].GetComponent<Rigidbody2D>().angularVelocity = 0; // Reset angular velocity
        }

        Debug.Log("Tower reset!");
    }

    private void ResetBall()
    {
        ball.transform.position = ballInitialPosition.position;
        ball.GetComponent<Rigidbody2D>().velocity = Vector2.zero; // Reset velocity
        ball.GetComponent<Rigidbody2D>().angularVelocity = 0; // Reset angular velocity

        Debug.Log("Ball reset!");
    }
}
