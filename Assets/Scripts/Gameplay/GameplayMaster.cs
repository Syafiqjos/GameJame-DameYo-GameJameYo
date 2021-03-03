using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameplayMaster : MonoBehaviour
{
    public static GameplayMaster Instance { get; private set; }

    [Header("Objects")]
    public TrainHead trainHead;
    public Text pointsUI;
    public Text wordsUI;

    [Header("Gameplay")]
    public float timeCountdown;

    public int scorePoints { get; private set; } = 0;
    public int scoreDelta = 100;

    [Header("Words")]
    public int wordsCount = 0;
    private static List<string> words { get; set; } = new List<string>();


    public static bool isGameOver { get; set; } = false;
    public static bool isPaused { get; set; } = false;

    public static bool isPlaying { get { return !isGameOver && !isPaused; } }


    private void Awake()
    {
        Application.targetFrameRate = 60;

        Instance = this;

        isGameOver = false;
        isPaused = false;
    }

    private void Start()
    {
        GenerateDictionaryWords();
    }

    private void Update()
    {
        UpdateUI();
    }

    private void UpdateUI()
    {
        pointsUI.text = scorePoints.ToString();
        wordsUI.text = "";
        foreach (var w in words)
        {
            wordsUI.text += w + "\n";
        }
    }

    public static void CheckWord(string word)
    {
        foreach (var w in words)
        {
            if (w == word)
            {
                AddPoints(word.Length);
                return;
            }
        }
    }

    public static string GetFormedWord()
    {
        if (Instance)
        {
            return Instance.trainHead?.formedWord;
        }

        return string.Empty;
    }

    public static void AddPoints(int point)
    {
        if (Instance)
        {
            Instance.scorePoints += point * Instance.scoreDelta;
        }
    }

    private void GenerateDictionaryWords()
    {
        words.Clear();
        for (int i = 0;i < wordsCount; i++)
        {
            words.Add(WordsDictionary.GetRandomWord());
        }
    }
}
