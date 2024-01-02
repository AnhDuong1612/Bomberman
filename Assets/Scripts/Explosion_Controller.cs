using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explosion_Controller : MonoBehaviour
{
    public Render_Sprites start;
    public Render_Sprites mid;
    public Render_Sprites end;
    public void SetActiveRenderer(Render_Sprites renderer)
    {
        start.enabled = renderer == start;
        mid.enabled = renderer == mid;
        end.enabled = renderer == end;
    }

    public void SetDirection(Vector2 direction)
    {
        //float angle = Mathf.Atan2(direction.x, direction.y);
        //transform.rotation = Quaternion.AngleAxis(angle * 8, Vector3.forward);
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0f, 0f, angle);
    }

    public void DestroyAfter(float seconds)
    {
        Destroy(gameObject, seconds);
    }
}
