//Tạo hình ảnh bằng cách hiển thị các Sprite theo thời gian
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Render_Sprites : MonoBehaviour
{
    private SpriteRenderer spriteRenderer; // hiển thị các sprites trên GameObject

    public Sprite freezeSprite; //Sprite dùng khi không di chuyển
    public Sprite[] renderSprites; // Tạo một mảng chứa các sprite dùng cho các hoạt ảnh

    public float renderTime = 0.25f; //Khoảng thời gian giữa mỗi frame của hoạt ảnh

    private int renderFrame;

    public bool loop = true; // nếu là true thì lặp lại các hoạt ảnh ở mảng renderSprites, false thì dừng lại   
    public bool freeze = true; // nếu là true idleSprite sẽ được dùng, false thì dùng ở renderSprites 

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>(); // tham chiếu đến SpriteRenderer trong GameObject
    }

    private void OnEnable()
    {
        spriteRenderer.enabled = true; // đảm bảo spriteRenderer được bật từ đầu
    }

    private void OnDisable()
    {
        spriteRenderer.enabled=false; // tắt spriteRenderer
    }

    private void Start()
    {
        InvokeRepeating(nameof(NextFrame), renderTime, renderTime); // khi script được kích hoạt, lặp lại nextframe() với khoảng tg renderTime
    }

    private void NextFrame()
    {
        renderFrame ++;
        if(loop && renderFrame >= renderSprites.Length)
        {
            renderFrame = 0; //reset về 0 để lặp lại các hoạt ảnh
        }
        if(freeze)
        {
            spriteRenderer.sprite = freezeSprite; // hiển thị freezeSprite
        }
        else if (renderFrame >= 0 && renderFrame < renderSprites.Length)
        {
            spriteRenderer.sprite = renderSprites[renderFrame]; // hiển thị sprite tương ứng trong mảng renderSprites
        }
    }
}
