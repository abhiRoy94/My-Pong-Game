using UnityEngine;
using UnityEngine.UI;

public class PointHUD : MonoBehaviour
{
    // Text for the player and enemy score
    [SerializeField] private Text playerPointText;
    [SerializeField] private Text enemyPointText;

    public void UpdateScores(int playerPoints, int enemyPoints)
    {
        if (playerPointText)
        {
            playerPointText.text = playerPoints.ToString();
        }
        
        if (enemyPointText)
        {
            enemyPointText.text = enemyPoints.ToString();
        }
    }
}
