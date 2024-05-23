using Microsoft.Unity.VisualStudio.Editor;
using UnityEngine;

public class Parallax : MonoBehaviour
{
    // Add this line

    public float animationSpeed = 1f;
    private MeshRenderer meshRenderer;
    public Texture2D backgroundTexture1;
    public Texture2D backgroundTexture2;
    private int i = 0;

    private void Awake()
    {
        meshRenderer = GetComponent<MeshRenderer>();
    }

    public void ToggleBackground()
    {
        if (i == 0) {
            meshRenderer.material.mainTexture = backgroundTexture2;
            i = 1;
        } else {
            meshRenderer.material.mainTexture = backgroundTexture1;
            i = 0;
        }
        meshRenderer.material.mainTextureOffset = Vector2.zero;
    }

    private void Update()
    {
        meshRenderer.material.mainTextureOffset += new Vector2(animationSpeed * Time.deltaTime, 0);
    }

}
