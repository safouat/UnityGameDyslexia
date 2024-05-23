using UnityEngine;

public class PipeSpawner : MonoBehaviour
{
    public Pipes prefab;
    public float spawnRate = 3f;
    public float minHeightEasy = -2f;
    public float maxHeightEasy = 2f;
    public float minHeightMedium = -2f;
    public float maxHeightMedium = 3f;
    public float minHeightHard = -3f;
    public float maxHeightHard = 4f;
    public float minHeightInsane = -4f;
    public float maxHeightInsane = 5f;
    public float verticalGapEasy = 20f; // Adjusted vertical gap for easy level
    public float verticalGapMedium = 10f; // Adjusted vertical gap for medium level
    public float verticalGapHard = 7f; // Adjusted vertical gap for hard level
    public float verticalGapInsane = 5f; // Adjusted vertical gap for insane level
    public float minHeight;
    public float maxHeight;


    private int score;

    private void OnEnable()
    {
        InvokeRepeating(nameof(Spawn), spawnRate, spawnRate);
    }

    private void OnDisable()
    {
        CancelInvoke(nameof(Spawn));
    }


    private void Spawn()
    {
        Pipes pipes = Instantiate(prefab, transform.position, Quaternion.identity);
        pipes.word = GameManager.Instance.GetLetter();
        SetDifficulty();
        pipes.transform.position += Vector3.up * Random.Range(minHeight, maxHeight);
        pipes.gap = GetVerticalGap();
    }

    public void UpdateScore(int newScore)
    {
        score = newScore;
    }

    private void SetDifficulty()
    {
        if (score < 5)
        {
            minHeight = minHeightEasy;
            maxHeight = maxHeightEasy;
            spawnRate = 3f;
            GameManager.Instance.SetLv("Easy");
            OnDisable();
            OnEnable();
        }
        else if (score >= 5 && score < 10)
        {
            minHeight = minHeightMedium;
            maxHeight = maxHeightMedium;
            spawnRate = 2f;
            GameManager.Instance.SetLv("Medium");
            OnDisable();
            OnEnable();
        }
        else if (score >= 10 && score < 15)
        {
            minHeight = minHeightHard;
            maxHeight = maxHeightHard;
            spawnRate = 1f;
            GameManager.Instance.SetLv("Hard");
            OnDisable();
            OnEnable();
        }
        else {
            minHeight = minHeightInsane;
            maxHeight = maxHeightInsane;
            spawnRate = 0.5f;
            GameManager.Instance.SetLv("Insane");
            OnDisable();
            OnEnable();
        }
    }
    private float GetVerticalGap()
    {
        if (score < 5)
        {
            return verticalGapEasy;
        }
        else if (score >= 5 && score < 10)
        {
            return verticalGapMedium;
        }
        else if (score >= 10 && score < 15)
        {
            return verticalGapHard;
        }
        // Add more conditions as needed for higher difficulty levels
        else
        {
            // Define your own logic for even higher difficulty levels
            // For example, you could keep decreasing the vertical gap gradually
            return verticalGapInsane;
        }
    }
}
