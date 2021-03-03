using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreManager
{
    public static int highScore { get; set; } = 0;

    public static void CompareHighscore(int score)
    {
        highScore = score > highScore ? score : highScore;
    }
}
