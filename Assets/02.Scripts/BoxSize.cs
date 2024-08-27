using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxSize : MonoBehaviour
{
    Vector2 p1, p2, p3, p4;     // 좌측하단, 우측하단, 좌측상단, 우측상단

    private void Start()
    {
        p1 = new Vector2(transform.position.x - transform.localScale.x / 2, transform.position.z + transform.localScale.z / 2);
        p2 = new Vector2(transform.position.x + transform.localScale.x / 2, transform.position.z + transform.localScale.z / 2);
        p3 = new Vector2(transform.position.x - transform.localScale.x / 2, transform.position.z - transform.localScale.z / 2);
        p4 = new Vector2(transform.position.x + transform.localScale.x / 2, transform.position.z - transform.localScale.z / 2);
    }


    /// vector2가 Box 영역내에 있는지 Check
    public bool CheckPoint(Vector2 vector2)
    {
        if (p1.x < vector2.x && p2.x > vector2.x &&
            p1.y > vector2.y && p3.y < vector2.y)
            return true;
        return false;
    }
}
