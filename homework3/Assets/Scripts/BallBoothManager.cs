using UnityEngine;
using TMPro;
using UnityEngine.Rendering;
using Unity.VisualScripting;

public class BallBoothManager : MonoBehaviour
{
    public CanFallDetector[] cans;
    public int throwsRemaining = 3;
    public int score = 0;

    public TextMeshProUGUI scoreboardText;

    public void BallThrown()
    {
        throwsRemaining--;

        Invoke("EvaluateThrow", 1f); // Wait 1 sec for physics
    }

    void EvaluateThrow()
    {
        int fallenThisThrow = 0;

        foreach (CanFallDetector can in cans)
        {
            can.CheckIfFallen();

            if (can.hasFallen)
                fallenThisThrow++;
        }

        score = fallenThisThrow;
        UpdateUI();

        if (throwsRemaining <= 0)
        {
            EndGame();
        }
    }

    void UpdateUI()
    {
        scoreboardText.text =
            "Score: " + score +
            "\nThrows Left: " + throwsRemaining;
    }

    void EndGame()
    {
        
        // Tell GameManager this booth is complete
    }
}
