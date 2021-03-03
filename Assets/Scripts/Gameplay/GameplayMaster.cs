using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameplayMaster : MonoBehaviour
{
    [Header("Objects")]
    [Tooltip("Biar rapi aja sih")] public Transform trainsParent;
    public TrainHead trainHead;

    [Header("Gameplay")]
    public float timeCountdown;

    public int scorePoints { get; private set; } = 0;
    public int scoreDelta = 100;

    [Header("Attachment Spawner")]
    public AttachmentSpawner attachmentSpawner;

    [Header("Words")]
    public int wordsCount = 1;

    [Header("Cursor UI")]
    public GameObject cursorUI;

    [Header("Gameplay UI")]
    public GameObject gameplayUI;
    public Text pointsUI;
    public Text highscoreUI;
    public Text wordsUI;
    public Text formedWordUI;

    [Header("Pause UI")]
    public GameObject pauseUI;

    [Header("Game Over UI")]
    public GameObject gameOverUI;
    public Text gameOverHighscoreUI;
    public Text gameOverScoreUI;

    public static GameplayMaster Instance { get; private set; }
    public static bool isGameOver
    {
        get; private set;
    } = false;

    public static bool isPaused { get; set; } = false;
    public static bool isPlaying { get { return !isGameOver && !isPaused; } }

    private static List<string> words { get; set; } = new List<string>();

    private void Awake()
    {
        Application.targetFrameRate = 60;

        Instance = this;

        scorePoints = 0;
        isGameOver = false;
        isPaused = false;

        Time.timeScale = 1.0f;
    }

    private void Start()
    {
        GenerateDictionaryWords();
    }

    private void Update()
    {
        if (!isGameOver)
        {
            CheckPauseInput();
            TrackAlphabet();
        }

        UpdatePauseUI();
        UpdateGameOverUI();
        UpdateGameplayUI();

        TimeScaleControl();

        if (isGameOver)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            }
        }
    }

    private void OnDestroy()
    {
        Time.timeScale = 1.0f;
    }

    private void TimeScaleControl()
    {
        Time.timeScale = isPlaying ? 1.0f : 0.0f;
    }

    private void UpdatePauseUI()
    {
        pauseUI?.SetActive(isPaused);
    }

    private void UpdateGameOverUI()
    {
        gameOverUI?.SetActive(isGameOver);
    }

    private void CheckPauseInput()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            isPaused = !isPaused;
        }
    }

    private void UpdateGameplayUI()
    {
        pointsUI.text = "Score : " + scorePoints.ToString();
        highscoreUI.text = "Highscore : " + ScoreManager.highScore.ToString();
        wordsUI.text = "";
        foreach (var w in words)
        {
            wordsUI.text += w + "\n";
        }
        formedWordUI.text = GetFormedWord().ToUpper();
    }

    public static int CheckWord(string word)
    {
        if (Instance)
        {
            for (int i = 0; i < words.Count; i++)
            {
                if (words[i] == word)
                {
                    ReplaceWordByRandomOn(i);
                    AddPoints(word.Length * Instance.scoreDelta);
                    return word.Length;
                }
            }
        }
        return 0;
    }

    public static void ReplaceWordByRandomOn(int index)
    {
        words[index] = WordsDictionary.GetRandomWord();
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
            Instance.scorePoints += point;
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

    private void TrackAlphabet()
    {
        string formed = GetFormedWord();

        if (formed.Length >= 0 && formed.Length < words[0].Length)
        {
            attachmentSpawner.tracker.gameObject.SetActive(true);
            attachmentSpawner.TrackController(words[0][formed.Length]);
        } else
        {
            attachmentSpawner.tracker.gameObject.SetActive(false);
        }
    }

    public static void GameOver()
    {
        isGameOver = true;
        if (Instance)
        {
            ScoreManager.CompareHighscore(Instance.scorePoints);

            Instance.cursorUI.SetActive(false);
            Instance.gameplayUI.SetActive(false);
            Instance.pauseUI.SetActive(false);

            Instance.gameOverScoreUI.text = "Score : " + Instance.scorePoints.ToString();
            Instance.gameOverHighscoreUI.text = "Highscore : " + ScoreManager.highScore.ToString();
        }
    }
}
