using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
    public float explosionDuration = 1f;
    public float explosionRadius = 1;
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
    private IEnumerator PlaceBomb()
    {
        Vector2 position = transform.position;
        position.x = Mathf.Round(position.x);
        position.y = Mathf.Round(position.y);

        GameObject bomb = Instantiate(bombPrefabs, position, Quaternion.identity);
        bombRemain--;

        yield return new WaitForSeconds(bombFuseTime);
        Destroy( bomb );
        bombRemain++;
    }
}
