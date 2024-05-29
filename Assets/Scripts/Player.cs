﻿using UnityEngine;
using System.Collections.Generic;

public class Player : MonoBehaviour
{
    public Sprite[] sprites;
    public float strength = 5f;
    public float gravity = -9.81f;
    public float tilt = 5f;
    private int i = 0;

    private SpriteRenderer spriteRenderer;
    private Vector3 direction;
    private int spriteIndex;
    private string[] list = { "cat", "bob", "car", "volvo", "fkj", "kdfijd", "dksf" };

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void Start()
    {
        InvokeRepeating(nameof(AnimateSprite), 0.15f, 0.15f);
    }

    private void OnEnable()
    {
        Vector3 position = transform.position;
        position.y = 0f;
        transform.position = position;
        direction = Vector3.zero;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButtonDown(0))
        {
            direction = Vector3.up * strength;
        }

        // Apply gravity and update the position
        direction.y += gravity * Time.deltaTime;
        transform.position += direction * Time.deltaTime;

        // Tilt the bird based on the direction
        Vector3 rotation = transform.eulerAngles;
        rotation.z = direction.y * tilt;
        transform.eulerAngles = rotation;
    }

    private void AnimateSprite()
    {
        spriteIndex++;

        if (spriteIndex >= sprites.Length)
        {
            spriteIndex = 0;
        }

        if (spriteIndex < sprites.Length && spriteIndex >= 0)
        {
            spriteRenderer.sprite = sprites[spriteIndex];
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Top") || other.gameObject.CompareTag("Bottom"))
        {
            Pipes pipe = other.gameObject.transform.parent.GetComponent<Pipes>();
            string letter = other.gameObject.GetComponent<TextMesh>().text;
            
            if (!pipe.isCollided)
            {
                other.gameObject.SetActive(false);
                pipe.TopText.GetComponent<BoxCollider2D>().enabled = false;
                pipe.BottomText.GetComponent<BoxCollider2D>().enabled = false;
                pipe.isCollided = true;
            } 
            //Debug.LogError(letter);
            GameManager.Instance.HandleScore(letter);

        }
        if (other.gameObject.CompareTag("Obstacle"))
        {
            GameManager.Instance.GameOver();
        }
        else if (other.gameObject.CompareTag("Scoring"))
        {
            //GameManager.Instance.IncreaseScore();

            //GameManager.Instance.ShowWords(); // Pass wordIndex and list to ShowWords method
            i++; // Increment wordIndex for the next word
        }



    }


}
