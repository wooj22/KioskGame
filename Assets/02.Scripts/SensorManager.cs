using HKY;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SensorManager : MonoBehaviour
{
    [SerializeField][Header("��ġ ���� ��")]
    float Left, Top, Right, Bottom;                // Down�� ��ǥ��
    private OSCManager m_senserData;               // OSC �Ŵ��� ��ũ��Ʈ
    List<Vector3> vector3 = new List<Vector3>();   // ���� ��ǥ�� ���� ����Ʈ

    void Start()
    {
        m_senserData = GameObject.Find("OSCManager").GetComponent<OSCManager>();
        StartCoroutine(GetSendsorData());
    }

    // ���� ��ǥ�� �޾ƿ���
    IEnumerator GetSendsorData()
    {
        while (true)
        {
            yield return new WaitForFixedUpdate();
            vector3.Clear();

            for (int i = 0; i < m_senserData._position.Count; i++)
            {
                //bool check = false;
                vector3.Add(new Vector3(scale(-m_senserData.RectSize.x / 2, m_senserData.RectSize.x / 2, Left, Right, m_senserData._position[i].x),
                                        1,
                                        scale(-m_senserData.RectSize.y / 2, m_senserData.RectSize.y/2, Bottom, Top, m_senserData._position[i].y)));
            }
        }
    }

    // ���� ��ǥ�� �Ѱ��ֱ�
    public List<Vector3> getSensorVector()
    {
        return vector3;
    }

    private float scale(float OldMin, float OldMax, float NewMin, float NewMax, float OldValue)
    {
        float OldRange = (OldMax - OldMin);
        float NewRange = (NewMax - NewMin);
        float NewValue = (((OldValue - OldMin) * NewRange) / OldRange) + NewMin;

        return (NewValue);
    }
}
