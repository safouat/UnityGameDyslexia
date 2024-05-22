using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Pipes : MonoBehaviour
{
    public Transform top;
    public Transform bottom;
    public TextMesh TopText;
    public TextMesh BottomText;
    public float speed = 1000f;
    public float gap = 3f;
    public string word;
    private float leftEdge;
    public bool isCollided = false;

    private void Start()
    {
        leftEdge = Camera.main.ScreenToWorldPoint(Vector3.zero).x - 1f;
        top.position += Vector3.up * gap / 2;
        //string word = GameManager.Instance.GetLetter();

        // Generate a random index within the bounds of the word string
        int missing_idx = UnityEngine.Random.Range(0, word.Length);
        string missing_letter = word[missing_idx].ToString();
        //Debug.LogError(missing_letter); // Retrieve the character at the random index and convert it to a string

        string random_letter = ((char)UnityEngine.Random.Range(0x61, 0x71)).ToString();

        if (UnityEngine.Random.Range(0, 100) > 5)
        {
            TopText.text = missing_letter;
            BottomText.text = random_letter;
        }
        else
        {
            TopText.text = random_letter;
            BottomText.text = missing_letter;
        }
        bottom.position += Vector3.down * gap / 2;
    }

    private void Update()
    {
        transform.position += speed * Time.deltaTime * Vector3.left;

        if (transform.position.x < leftEdge)
        {
            Destroy(gameObject);
        }
    }


}
