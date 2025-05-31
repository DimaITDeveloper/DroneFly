using UnityEngine;
using UnityEngine.UI;

public class BackgroundImageFlasher : MonoBehaviour
{
    public SpriteRenderer backgroundImage;
    public Color color1 = Color.black;
    public Color color2 = Color.red;
    public float flashSpeed = 1f;

    private float timer = 0f;

    void Update()
    {
        if (backgroundImage == null) return;

        timer += Time.deltaTime * flashSpeed;
        backgroundImage.color = Color.Lerp(color1, color2, Mathf.PingPong(timer, 1));
    }
}
