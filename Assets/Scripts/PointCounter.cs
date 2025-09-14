using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PointCounter : MonoBehaviour
{
    // A reference to your HUD, assigned in the Inspector
    [SerializeField] private PointHUD pointHUD;

    // Private variables to keep track of the score
    private int playerPoints = 0;
    private int enemyPoints = 0;

    public void ScorePoints(string goalTag)
    {
        if (goalTag == "EnemyGoal")
        {
            Debug.Log("Player scored a goal!");
            playerPoints += 1;
        }
        else if (goalTag == "PlayerGoal")
        {
            Debug.Log("Enemy scored a goal!");
            enemyPoints += 1;
        }

        // Update the HUD
        pointHUD.UpdateScores(playerPoints, enemyPoints);
    }
}
