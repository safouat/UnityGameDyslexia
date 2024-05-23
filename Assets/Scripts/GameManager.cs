using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

[DefaultExecutionOrder(-1)]
public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    [SerializeField] private Player player;
    [SerializeField] private PipeSpawner spawner;

    [SerializeField] private Text scoreText;
    [SerializeField] public Text wordText;
    [SerializeField] public Text levelText;

    [SerializeField] private GameObject playButton;
    [SerializeField] private GameObject gameOver;


    private int lv;
    private int score;
    public int Score => score;
    private int i;
    private static string[] list = {
    "cat", "dog", "sun", "run", "ball",
    "cake", "book", "fish", "milk", "tree",
    "house", "happy", "sleep", "apple", "chair",
    "flower", "pencil", "music", "rabbit", "cookie",
    "elephant", "guitar", "pizza", "spider", "butterfly",
    "umbrella", "mountain", "dragon", "fireworks", "telephone",
    "skyscraper", "astronaut", "microscope", "helicopter", "watermelon",
    "whale", "volcano", "telescope", "electricity", "motorcycle",
    "kangaroo", "parachute", "zookeeper", "xylophone", "quicksand",
    "hurricane", "razzmatazz", "rhinoceros", "xylophonist", "jackhammer",
    "abracadabra", "acropolis", "algorithm", "barracuda", "blacksmith",
    "catastrophe", "chocolate", "doppelganger", "flabbergasted", "gobbledygook",
    "haphazardly", "heliocentric", "insurmountable", "juxtaposition", "kleptomaniac",
    "labyrinthine", "mnemonic", "onomatopoeia", "pandemonium", "quintessential",
    "sesquipedalian", "tintinnabulation", "unprecedented", "vexatious", "wunderkind",
    "xenophobic", "yesterday", "zoologically", "aphrodisiac", "bureaucracy",
    "cannibalistic", "dystopian", "eccentricity", "flibbertigibbet", "gastropod",
    "hallucinogenic", "indubitably", "juggernaut", "kaleidoscope", "laconically",
    "magnanimous", "nefarious", "obfuscation", "peregrination", "quixotically",
    "recalcitrant", "serendipity", "transmogrify", "ubiquitous", "verisimilitude"
};

    public Queue<string> pipeQueue;
    public string last_letter;
    

        private string[] GetListWords()
        {
            string url = "https://example.com/api/words";
            using (HttpClient client = new HttpClient())
            {
                string json = client.GetStringAsync(url).Result;
                string[] words = JsonUtility.FromJson<string[]>(json);
                Debug.Log(words);
                return words;
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


        wordText.text = list[i].ToString();
        i++;
    }

    public void SetLv(string lv) {
        levelText.text = "Level: " + lv.ToString();
    }



}
