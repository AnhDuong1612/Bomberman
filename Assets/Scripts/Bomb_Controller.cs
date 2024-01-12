using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Bomb_Controller : MonoBehaviour
{
    [Header("Bomb")]
    public GameObject bombPrefabs;
    public KeyCode inputKey = KeyCode.Space;
    public float bombFuseTime = 3.0f;
    public int bombAmount = 1;
    private int bombRemain;

    [Header("Explosion")]
    public Explosion_Controller explosionPrefab;
    public LayerMask explosionLayerMask;
    public float explosionDuration = 1f;
    public int explosionRadius = 1;

    [Header("Destructible")]
    public Tilemap destructibleTilemaps;
    public Destructible DestructiblePrefab;
    private void OnEnable()
    {
        bombRemain = bombAmount;
    }
    private void Update()
    {
        if ( bombRemain > 0 && Input.GetKeyDown(inputKey))
        {
            StartCoroutine(PlaceBomb());
        }
    }
    public IEnumerator PlaceBomb()
    {
        Vector2 position = transform.position;
        position.x = Mathf.Round(position.x);
        position.y = Mathf.Round(position.y);
        GameObject bomb = Instantiate(bombPrefabs, position, Quaternion.identity);
        bombRemain--;

        yield return new WaitForSeconds(bombFuseTime);
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

        Destroy( bomb );
        bombRemain++;
    }
    private void Explode(Vector2 position, Vector2 direction, int length)
    {
        if (length <= 0)
        {
            return;
        }
        position += direction;
        if(Physics2D.OverlapBox(position, Vector2.one/2f, 0f, explosionLayerMask))
        {
            DestroyDestructible(position);
            return;
        }

        Explosion_Controller explosion = Instantiate(explosionPrefab, position, Quaternion.identity);
        explosion.SetActiveRenderer(length > 1 ? explosion.mid : explosion.end);
        explosion.SetDirection(direction);
        explosion.DestroyAfter(explosionDuration);
        Explode(position, direction, length - 1);

    }

    private void DestroyDestructible(Vector2 position)
    {
        Vector3Int cell = destructibleTilemaps.WorldToCell(position);
        TileBase tile = destructibleTilemaps.GetTile(cell);
        if (tile != null)
        {
            Instantiate(DestructiblePrefab, position, Quaternion.identity);
            destructibleTilemaps.SetTile(cell, null);
        }
    }
}


