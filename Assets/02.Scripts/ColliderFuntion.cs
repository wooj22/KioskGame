using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class ColliderFuntion : MonoBehaviour
{
    SphereCollider Collider;
    Vector3 vel = Vector3.zero;

    private void Start()
    {
        Collider = GetComponent<SphereCollider>();
    }

    /// Player 이동
    public void Movement(Vector3 vector)
    {
        transform.position = Vector3.SmoothDamp(gameObject.transform.position, vector, ref vel, 0.3f);
    }

    /// Player 콜라이더 Off
    public void offCollider()
    {
        Collider.enabled = false;
    }

    /// Player 콜라이더 On
    public void onCollider()
    {
        Collider.enabled = true;
    }

    /// Player 콜라이더 상태 반환
    public bool ColliderEnabled()
    {
        return Collider.enabled;
    }
}