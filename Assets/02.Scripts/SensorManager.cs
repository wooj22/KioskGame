using HKY;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SensorManager : MonoBehaviour
{
    [SerializeField][Header("위치 적용 값")]
    float Left, Top, Right, Bottom;                // Down의 좌표값
    private OSCManager m_senserData;               // OSC 매니저 스크립트
    List<Vector3> vector3 = new List<Vector3>();   // 센서 좌표값 저장 리스트

    void Start()
    {
        m_senserData = GameObject.Find("OSCManager").GetComponent<OSCManager>();
        StartCoroutine(GetSendsorData());
    }

    // 센서 좌표값 받아오기
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

    // 센서 좌표값 넘겨주기
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
