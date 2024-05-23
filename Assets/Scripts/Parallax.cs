using UnityEngine;
using Voxell.Speech.TTS;

public class Parallax : MonoBehaviour
{
    // Add this line

    public float animationSpeed = 1f;
    private MeshRenderer meshRenderer;
    public TextToSpeech textToSpeech;

    private void Awake()
    {
        meshRenderer = GetComponent<MeshRenderer>();
        string a = "AAAAAAAAAAAAAAAAAAAAAAAAAAAAA";
        textToSpeech.Speak(a);
    }

    private void Update()
    {
        meshRenderer.material.mainTextureOffset += new Vector2(animationSpeed * Time.deltaTime, 0);
    }

}
