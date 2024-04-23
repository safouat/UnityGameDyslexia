using UnityEngine;

public class Spawner : MonoBehaviour
{
    public Pipes prefab;
    public float spawnRate = 1f;
    public float minHeightEasy = -2f;
    public float maxHeightEasy = 2f;
    public float minHeightMedium = -2f;
    public float maxHeightMedium = 3f;
    public float minHeightHard = -3f;
    public float maxHeightHard = 4f;
    public float verticalGapEasy = 20f; // Adjusted vertical gap for easy level
    public float verticalGapMedium = 8f; // Adjusted vertical gap for medium level
    public float verticalGapHard = 2f; // Adjusted vertical gap for hard level
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
        if (score < 2)
        {
            minHeight = minHeightEasy;
            maxHeight = maxHeightEasy;
        }
        else if (score >= 1 && score < 5)
        {
            minHeight = minHeightMedium;
            maxHeight = maxHeightMedium;
        }
        else if (score >= 10 && score < 15)
        {
            minHeight = minHeightHard;
            float maxHeight = maxHeightHard;
        }
        // Add more conditions as needed for higher difficulty levels
        else
        {
            // Define your own logic for even higher difficulty levels
            // For example, you could keep increasing the difficulty gradually
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
            return verticalGapHard;
        }
    }
}
