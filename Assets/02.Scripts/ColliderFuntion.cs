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

    /// Player �̵�
    public void Movement(Vector3 vector)
    {
        transform.position = Vector3.SmoothDamp(gameObject.transform.position, vector, ref vel, 0.3f);
    }

    /// Player �ݶ��̴� Off
    public void offCollider()
    {
        Collider.enabled = false;
    }

    /// Player �ݶ��̴� On
    public void onCollider()
    {
        Collider.enabled = true;
    }

    /// Player �ݶ��̴� ���� ��ȯ
    public bool ColliderEnabled()
    {
        return Collider.enabled;
    }
}