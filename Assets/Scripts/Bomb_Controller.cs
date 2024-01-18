using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Bomb_Controller : MonoBehaviour
{
    [Header("Bomb")]
    public GameObject bombPrefabs; //Prefab của quả bom
    public KeyCode inputKey = KeyCode.Space;
    public float bombFuseTime = 3.0f;
    public int bombAmount = 1;
    private int bombRemain;

    [Header("Explosion")]
    public Explosion_Controller explosionPrefab; //Prefab cho hiệu ứng nổ
    public LayerMask explosionLayerMask;//xác định nơi bom có thể tạo nổ.
    public float explosionDuration = 1f;
    public int explosionRadius = 1;

    [Header("Destructible")]
    public Tilemap destructibleTilemaps; //Tilemap chứa các ô gạch có thể phá hủy.
    public Destructible DestructiblePrefab; //Prefab của đối tượng có thể phá hủy.


    // sfx
    AudioManager audioManager;

    private void Awake()
    {
        audioManager = GameObject.FindGameObjectWithTag("audio").GetComponent<AudioManager>();
    }



    private void OnEnable()
    {
        bombRemain = bombAmount;
    }
    private void Update()
    {
        if ( bombRemain > 0 && Input.GetKeyDown(inputKey)) //Kiểm tra xem còn quả bom nào có thể đặt và người chơi có nhấn phím đặt bom không
        {
            StartCoroutine(PlaceBomb()); //Gọi placebomb
        }
    }
    private IEnumerator PlaceBomb()
    {
        // đặt bom tại vị trí của người chơi
        Vector2 position = transform.position;
        position.x = Mathf.Round(position.x);
        position.y = Mathf.Round(position.y);
        GameObject bomb = Instantiate(bombPrefabs, position, Quaternion.identity);
        bombRemain--;
        // chờ đến khi bom hết tg đếm ngược
        yield return new WaitForSeconds(bombFuseTime);
        // hiệu ứng nổ của bom (lên, xuống, trái, phải)
        position = bomb.transform.position;
        position.x = Mathf.Round(position.x);
        position.y = Mathf.Round(position.y);
        Explosion_Controller explosion = Instantiate(explosionPrefab, position, Quaternion.identity);
        explosion.SetActiveRenderer(explosion.start);
        explosion.DestroyAfter(explosionDuration);

        Explode(position, Vector2.up, explosionRadius);
        Explode(position, Vector2.down, explosionRadius);
        Explode(position, Vector2.left, explosionRadius);
        Explode(position, Vector2.right, explosionRadius);
        // phá hủy bom và trả lại giá trị bom còn lại
        Destroy( bomb );
        bombRemain++;
    }
    //tạo hiệu ứng bom nổ
    private void Explode(Vector2 position, Vector2 direction, int length)
    {
        if (length <= 0)
        {
            return;
        }
        position += direction;
        //Sử dụng Physics2D.OverlapBox để kiểm tra xem có đối tượng nào nằm trong hình hộp có kích thước Vector2.one/2f tại vị trí position không.
        if (Physics2D.OverlapBox(position, Vector2.one/2f, 0f, explosionLayerMask))
        {
            //phá hủy đối tượng đó
            DestroyDestructible(position);
            return;
        }

        Explosion_Controller explosion = Instantiate(explosionPrefab, position, Quaternion.identity);
        //Thiết lập hiển thị của explosion dựa trên giá trị của length. Nếu length lớn hơn 1, sử dụng explosion.mid, ngược lại sử dụng explosion.end
        explosion.SetActiveRenderer(length > 1 ? explosion.mid : explosion.end);
        //đặt hướng cho explosion
        explosion.SetDirection(direction);
        // thời gian tồn tại của explosion
        explosion.DestroyAfter(explosionDuration);
        //gọi đệ quy để tạo hiệu ứng nổ tiếp theo với length giảm đi 1 và vị trí tiếp theo theo hướng nổ.
        Explode(position, direction, length - 1);
        // hieu ung am thanh
        audioManager.PlaySFX(audioManager.explosion);
    }

    // phá hủy các đối tượng có thể phá hủy
    private void DestroyDestructible(Vector2 position)
    {
        // Sử dụng phương thức WorldToCell để chuyển đổi vị trí thành vị trí của ô gạch trên tilemap . Kết quả được lưu trong biến cell.
        Vector3Int cell = destructibleTilemaps.WorldToCell(position);
        //Sử dụng GetTile để lấy đối tượng gạch tại vị trí cell trên tilemap. Kết quả được lưu trong biến tile.
        TileBase tile = destructibleTilemaps.GetTile(cell);
        // kiểm tra xem có ô gạch nào ở ô đó không
        if (tile != null)
        {
            //tạo một đối tượng mới bằng cách sao chép prefab DestructiblePrefab tại vị trí position
            Instantiate(DestructiblePrefab, position, Quaternion.identity);
            //sử dụng SetTile để cập nhật tilemap, đặt giá trị của ô gạch cell trên tilemap thành null
            destructibleTilemaps.SetTile(cell, null);
        }
    }

    public void AddBom()
    {
        bombAmount++;
        bombRemain++;
    }
}


