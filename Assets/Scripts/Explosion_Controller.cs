using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explosion_Controller : MonoBehaviour
{
    // khai báo các đối tượng sprites start,mid,end 
    public Render_Sprites start;
    public Render_Sprites mid;
    public Render_Sprites end;
    // kích hoạt các sprite
    public void SetActiveRenderer(Render_Sprites renderer)
    {
        //Nếu renderer trùng khớp với start, thì start.enabled được đặt thành true, ngược lại là false. Tương tự cho mid và end
        start.enabled = renderer == start;
        mid.enabled = renderer == mid;
        end.enabled = renderer == end;
    }
    //đặt hướng của đối tượng nổ dựa trên hướng được truyền vào (direction)
    public void SetDirection(Vector2 direction)
    {
        //tính toán góc giữa hướng và trục x (hoặc y), sau đó chuyển đổi góc từ radian sang độ (Mathf.Rad2Deg)
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        //quay đối tượng nổ theo hướng mong muốn
        transform.rotation = Quaternion.Euler(0f, 0f, angle);
    }

    //phá hủy đối tượng nổ sau một khoảng thời gian nhất định (seconds).
    public void DestroyAfter(float seconds)
    {
        Destroy(gameObject, seconds);
    }
}
