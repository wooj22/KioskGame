using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColliderManager : MonoBehaviour
{
    public SensorManager SensorManager;                      // 센서매니저 스크립트
    public GameObject PlayerPrefab;                          // Player 프리팹
    public Transform PlayersParent;                          // 부모 오브젝트

    List<Vector3> vector3;                                   // 센서 좌표값 리스트
    List<GameObject> PlayerList = new List<GameObject>();    // Player 리스트

    void Update()
    {
        // 센서 좌표값 리스트 get
        vector3 = SensorManager.getSensorVector();

        // 새로 인식된 센서가 있다면 Player 생성
        if (vector3.Count > PlayerList.Count)
        {
            for (int i = PlayerList.Count; i < vector3.Count; i++)
            {
                PlayerList.Add(Instantiate(PlayerPrefab, vector3[i], PlayerPrefab.transform.rotation, PlayersParent));
            }
        }

        // 인식이 사라진 센서가 있다면 player의 콜라이더 off
        if (vector3.Count < PlayerList.Count)
        {
            for (int i = vector3.Count; i < PlayerList.Count; i++)
            {
                PlayerList[i].GetComponent<ColliderFuntion>().offCollider();
            }
        }

        // Player위치 업데이트
        for (int i = 0; i < vector3.Count; i++)
        {
            // 콜라이더가 꺼져있는 상태라면 위치 이동, 켜져있는 상태라면 부드럽게 이동
            if (PlayerList[i].GetComponent<ColliderFuntion>().ColliderEnabled() == false)
            {
                PlayerList[i].transform.position = vector3[i];
            }
            PlayerList[i].GetComponent<ColliderFuntion>().onCollider();
            PlayerList[i].GetComponent<ColliderFuntion>().Movement(vector3[i]);
        }
    }
}
