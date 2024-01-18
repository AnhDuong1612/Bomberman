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
    private float _speed = 5f;

    //public float speed { get; private set; } = 5f;
    public float speed // Tốc độ di chuyển của nhân vật
    {
        get { return _speed; }
        set { _speed = value; }
    } 

    public Render_Sprites spriteRenderUp;
    public Render_Sprites spriteRenderDown;
    public Render_Sprites spriteRenderLeft;
    public Render_Sprites spriteRenderRight;
    public Render_Sprites spriteRenderDeath;
    public Render_Sprites activeSpriteRender;
    //thiết lập các trạng thái khởi đầu và cấu hình đối tượng trước khi trò chơi bắt đầu
    private void Awake()
    {
        //tham chiếu đến thành phần Rigidbody2D và gán vào biến rigidbody
        rigidbody = GetComponent<Rigidbody2D>();
        //hiển thị hình ảnh của đối tượng trong trạng thái khởi đầu là down
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

    private void SetDirection(Vector2 newDirection, Render_Sprites spriteRenderer) // Xác định hướng di chuyển của nhân vật
    {
        direction = newDirection; //lưu trữ hướng di chuyển hiện tại
        // kiểm traxem sprite renderer nào nên được kích hoạt dựa trên sprite renderer được truyền vào.
        if (spriteRenderUp != null) spriteRenderUp.enabled = spriteRenderer == spriteRenderUp;
        if (spriteRenderDown != null) spriteRenderDown.enabled = spriteRenderer == spriteRenderDown;
        if (spriteRenderLeft != null) spriteRenderLeft.enabled = spriteRenderer == spriteRenderLeft;
        if (spriteRenderRight != null) spriteRenderRight.enabled = spriteRenderer == spriteRenderRight;
        // Nếu activeSpriteRender khác null, kiểm tra xem nhân vật có đang đứng yên ko. Nếu đúng kích hoạt trạng thái freeze
        if (activeSpriteRender != null)
        {
            activeSpriteRender.freeze = direction == Vector2.zero;
        }
        // cập nhật activeSpriteRender thành sprite renderer mới
        activeSpriteRender = spriteRenderer;
    }

    //Phương thức này được gọi tự động khi đối tượng này va chạm với một Collider2D khác.
    private void OnTriggerEnter2D(Collider2D other)
    {
        //Kiểm tra xem đối tượng va chạm có layer là "Explosion" không. Nếu có, gọi phương thức Death().
        if (other.gameObject.layer == LayerMask.NameToLayer("Explosion"))
        {
            Death();
        }
    }

    private void Death()
    {
        //Vô hiệu hóa script của đối tượng hiện tại (enabled = false).
        enabled = false;
        //Tắt script Bomb_Controller
        GetComponent<Bomb_Controller>().enabled = false;
        // Vô hiệu hóa tất cả các sprite renderer của nhân vật và kích hoạt sprite renderer cho hiệu ứng chết 
        spriteRenderDown.enabled = false;
        spriteRenderLeft.enabled = false;
        spriteRenderRight.enabled = false;
        spriteRenderUp.enabled = false;
        spriteRenderDeath.enabled = true;
        // Sử dụng Invoke để gọi phương thức EndGame sau một khoảng thời gian (1.25 giây).
        Invoke(nameof(EndGame), 1.25f);
    }

    private void EndGame()
    {
        // vô hiệu hóa nv
        gameObject.SetActive(false);
        //Sử dụng FindAnyObjectByType để tìm đối tượng có kiểu là Game_Manager và gọi phương thức CheckWin của nó.
        FindAnyObjectByType<Game_Manager>().CheckWin();
    }

}
