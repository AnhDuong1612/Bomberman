using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Movement_Controller : MonoBehaviour
{
    public new Rigidbody2D rigidbody { get; private set; }
    private Vector2 direction = Vector2.down; // Hướng di chuyển mặc định là đi xuống
    public KeyCode inputUp = KeyCode.W;
    public KeyCode inputDown = KeyCode.S;
    public KeyCode inputLeft = KeyCode.A;
    public KeyCode inputRight = KeyCode.D;
    public float speed { get; private set; } = 5f; // Tốc độ di chuyển của nhân vật

    public Render_Sprites spriteRenderUp;
    public Render_Sprites spriteRenderDown;
    public Render_Sprites spriteRenderLeft;
    public Render_Sprites spriteRenderRight;
    public Render_Sprites spriteRenderDeath;
    public Render_Sprites activeSpriteRender;

    private void Awake()
    {
        rigidbody = GetComponent<Rigidbody2D>();
        activeSpriteRender = spriteRenderDown;
        InitializeSpriteRenderers();
    }

    private void InitializeSpriteRenderers()
    {
        // Kiểm tra và gán giá trị cho tất cả các SpriteRenderers
        if (spriteRenderUp == null) spriteRenderUp = GetComponentInChildren<Render_Sprites>();
        if (spriteRenderDown == null) spriteRenderDown = GetComponentInChildren<Render_Sprites>();
        if (spriteRenderLeft == null) spriteRenderLeft = GetComponentInChildren<Render_Sprites>();
        if (spriteRenderRight == null) spriteRenderRight = GetComponentInChildren<Render_Sprites>();

        // Gán giá trị mặc định cho activeSpriteRender nếu nó là null
        if (activeSpriteRender == null) activeSpriteRender = spriteRenderDown;
    }

    private void Update() // Thiết lập di chuyển nhân vật 
    {
        if (Input.GetKey(inputUp))
        {
            SetDirection(Vector2.up, spriteRenderUp);
        }
        else if (Input.GetKey(inputDown))
        {
            SetDirection(Vector2.down, spriteRenderDown);
        }
        else if (Input.GetKey(inputLeft))
        {
            SetDirection(Vector2.left, spriteRenderLeft);
        }
        else if (Input.GetKey(inputRight))
        {
            SetDirection(Vector2.right, spriteRenderRight);
        }
        else
        {
            SetDirection(Vector2.zero, activeSpriteRender);
        }
    }

    private void FixedUpdate() // Cập nhật vị trí của nhân vật dựa trên hướng và tốc độ di chuyển
    {
        Vector2 position = rigidbody.position; // Lấy vị trí hiện tại của nhân vật
        Vector2 translation = direction * speed * Time.fixedDeltaTime; // Tính toán vị trí sau khi di chuyển của nhân vật

        rigidbody.MovePosition(position + translation); // Di chuyển nhân vật đến vị trí mới
    }

    public void SetDirection(Vector2 newDirection, Render_Sprites spriteRenderer) // Xác định hướng di chuyển của nhân vật
    {
        direction = newDirection;

        if (spriteRenderUp != null) spriteRenderUp.enabled = spriteRenderer == spriteRenderUp;
        if (spriteRenderDown != null) spriteRenderDown.enabled = spriteRenderer == spriteRenderDown;
        if (spriteRenderLeft != null) spriteRenderLeft.enabled = spriteRenderer == spriteRenderLeft;
        if (spriteRenderRight != null) spriteRenderRight.enabled = spriteRenderer == spriteRenderRight;

        if (activeSpriteRender != null)
        {
            activeSpriteRender.freeze = direction == Vector2.zero;
        }

        activeSpriteRender = spriteRenderer;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.layer == LayerMask.NameToLayer("Explosion"))
        {
            Death();
        }
    }

    private void Death()
    {
        enabled = false;
        GetComponent<Bomb_Controller>().enabled = false;

        spriteRenderDown.enabled = false;
        spriteRenderLeft.enabled = false;
        spriteRenderRight.enabled = false;
        spriteRenderUp.enabled = false;
        spriteRenderDeath.enabled = true;

        Invoke(nameof(EndGame), 1.25f);
    }

    private void EndGame()
    {
        gameObject.SetActive(false);
        FindAnyObjectByType<Game_Manager>().CheckWin();
    }

    internal void SetDirection(Vector2 newDirection)
    {
        throw new NotImplementedException();
    }
}
