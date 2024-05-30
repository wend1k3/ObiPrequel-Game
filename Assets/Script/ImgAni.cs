using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
public class ImgAni : MonoBehaviour
{
    public Sprite[] sprites;
    public int spritePerFrame = 10;
    public bool loop = false;
    public bool destroyOnEnd = false;

    private int index = 0;
    private Image image;
    private int frame = 0;

    public bool finished;

    void Awake()
    {
        image = GetComponent<Image>();
    }

    void Update()
    {
        if (!loop && index == sprites.Length) return;
        frame++;
        if (frame < spritePerFrame) return;
        image.sprite = sprites[index];
        frame = 0;
        index++;
        if (index >= sprites.Length)
        {
            if (loop) index = 0;
            if (destroyOnEnd) Destroy(gameObject);

        }
    }
}
