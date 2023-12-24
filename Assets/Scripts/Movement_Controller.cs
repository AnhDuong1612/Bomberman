using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement_Controller : MonoBehaviour
{
    public new Rigidbody2D rigidbody {  get; private set; }
    private Vector2 direction = Vector2.down; //huong di chuyen mac dinh se la di xuong
    public KeyCode inputUp = KeyCode.W;
    public KeyCode inputDown = KeyCode.S;
    public KeyCode inputLeft = KeyCode.A;
    public KeyCode inputRight = KeyCode.D;
    public float speed { get; private set; } = 5f; // Toc do di chuyen cua nhan vat

    public Render_Sprites spriteRenderUp;
    public Render_Sprites spriteRenderDown;
    public Render_Sprites spriteRenderLeft;
    public Render_Sprites spriteRenderRight;
    public Render_Sprites activeSpriteRender;
    private void Awake()
    {
        rigidbody = GetComponent<Rigidbody2D>();
        activeSpriteRender = spriteRenderDown;
    }
    private void Update() // thiet lap di chuyen nhan vat 
    {
        if (Input.GetKey(inputUp))
        {
            SetDirection(Vector2.up, spriteRenderUp);
        } else if (Input.GetKey(inputDown))
        {
            SetDirection(Vector2.down, spriteRenderDown);
        } else if (Input.GetKey(inputLeft)) 
        {
            SetDirection(Vector2.left, spriteRenderLeft);
        } else if (Input.GetKey(inputRight))
        {
            SetDirection(Vector2.right, spriteRenderRight);    
        } else
        {
            SetDirection(Vector2.zero, activeSpriteRender);
        }
    }
    private void FixedUpdate() //cap nhat vi tri cua nhan vat dua theo huong va toc do di chuyen
    {
        Vector2 position = rigidbody.position; //lay vi tri hien tai cua nhan vat
        Vector2 translation = direction * speed * Time.deltaTime; // tinh toan vi tri sau khi di chuyen cua nhan vat

        rigidbody.MovePosition(position + translation); //dich chuyen nhan vat den vi tri moi
    }
    private void SetDirection( Vector2 newDirection, Render_Sprites spriteRenderer) //xac dinh huong di chuyen cua nhan vat
    {
        direction = newDirection;

        spriteRenderUp.enabled = spriteRenderer == spriteRenderUp;
        spriteRenderDown.enabled = spriteRenderer == spriteRenderDown;
        spriteRenderLeft.enabled = spriteRenderer == spriteRenderLeft;
        spriteRenderRight.enabled = spriteRenderer == spriteRenderRight;
        activeSpriteRender = spriteRenderer;
        activeSpriteRender.freeze = direction == Vector2.zero;
    }
}
