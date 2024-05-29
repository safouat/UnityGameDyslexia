using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using static ChatGPTJson;

[DefaultExecutionOrder(-1)]
public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    [SerializeField] private Player player;
    [SerializeField] private PipeSpawner spawner;

    [SerializeField] private Text scoreText;
    [SerializeField] public Text wordText;
    [SerializeField] public Text levelText;

    [SerializeField] public Text waitText;

    [SerializeField] private GameObject playButton;
    [SerializeField] private GameObject gameOver;


    private int lv;
    private int score;
    public int Score => score;
    private int i;
    private List<string> list = new List<string>();

    public Queue<string> pipeQueue;
    public string last_letter;
    public ChatGPTJson chatGPTJson;


    private void GetListWords()
    {
        string url = "http://localhost:8000/api/v1/";
        using (HttpClient client = new HttpClient())
        {
            string json = client.GetStringAsync(url).Result;
            Debug.Log(json);
            chatGPTJson = JsonUtility.FromJson<ChatGPTJson>(json);
            Debug.Log(chatGPTJson.words);
            for (int j = 0; j < chatGPTJson.words.Count; j++)
            {
                Debug.Log(j);
                list.Add(chatGPTJson.words[j]);
            }
        }
    }

    private void Awake()
    {
        if (Instance != null)
        {
            DestroyImmediate(gameObject);
        }
        else
        {
            Instance = this;
            Application.targetFrameRate = 60;
            DontDestroyOnLoad(gameObject);
            GetListWords();
            Debug.Log(list);
            waitText.text = "";
            Pause();
        }
    }

    public string GetLetter()
    {
        return pipeQueue.Dequeue();
    }

    public void HandleScore(string letter)
    {
        if (wordText.text.Contains(letter))
        {
            IncreaseScore();
            ShowWords();
        }
        else
        {
            DecreaseScore();
            ShowWords();
        }
        spawner.SetDifficulty();
    }

    public void Play()
    {
        pipeQueue = new Queue<string>(list);
        i = 0;
        score = 0;
        lv = 0;
        scoreText.text = score.ToString();

        playButton.SetActive(false);
        gameOver.SetActive(false);

        Time.timeScale = 1f;
        player.enabled = true;

        Pipes[] pipes = FindObjectsOfType<Pipes>();

        for (int i = 0; i < pipes.Length; i++)
        {
            Destroy(pipes[i].gameObject);
        }

        ShowWords();
    }

    public void GameOver()
    {
        playButton.SetActive(true);
        gameOver.SetActive(true);

        Pause();
    }

    public void Pause()
    {
        Time.timeScale = 0f;
        player.enabled = false;
    }

    public void IncreaseScore()
    {
        score++;
        scoreText.text = score.ToString();
        spawner.UpdateScore(score);
    }

    public void DecreaseScore()
    {
        score--;
        scoreText.text = score.ToString();
        spawner.UpdateScore(score);
    }

    public void ShowWords()
    {
        if (wordText == null)
        {
            Debug.LogError(list[i]);
            return;
        }

        Debug.Log(i);
        wordText.text = list[i].ToString();
        i++;
    }

    public void SetLv(string lv)
    {
        levelText.text = "Level: " + lv.ToString();
    }



}
