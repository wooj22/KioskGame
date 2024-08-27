using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColliderManager : MonoBehaviour
{
    public SensorManager SensorManager;                      // �����Ŵ��� ��ũ��Ʈ
    public GameObject PlayerPrefab;                          // Player ������
    public Transform PlayersParent;                          // �θ� ������Ʈ

    List<Vector3> vector3;                                   // ���� ��ǥ�� ����Ʈ
    List<GameObject> PlayerList = new List<GameObject>();    // Player ����Ʈ

    void Update()
    {
        // ���� ��ǥ�� ����Ʈ get
        vector3 = SensorManager.getSensorVector();

        // ���� �νĵ� ������ �ִٸ� Player ����
        if (vector3.Count > PlayerList.Count)
        {
            for (int i = PlayerList.Count; i < vector3.Count; i++)
            {
                PlayerList.Add(Instantiate(PlayerPrefab, vector3[i], PlayerPrefab.transform.rotation, PlayersParent));
            }
        }

        // �ν��� ����� ������ �ִٸ� player�� �ݶ��̴� off
        if (vector3.Count < PlayerList.Count)
        {
            for (int i = vector3.Count; i < PlayerList.Count; i++)
            {
                PlayerList[i].GetComponent<ColliderFuntion>().offCollider();
            }
        }

        // Player��ġ ������Ʈ
        for (int i = 0; i < vector3.Count; i++)
        {
            // �ݶ��̴��� �����ִ� ���¶�� ��ġ �̵�, �����ִ� ���¶�� �ε巴�� �̵�
            if (PlayerList[i].GetComponent<ColliderFuntion>().ColliderEnabled() == false)
            {
                PlayerList[i].transform.position = vector3[i];
            }
            PlayerList[i].GetComponent<ColliderFuntion>().onCollider();
            PlayerList[i].GetComponent<ColliderFuntion>().Movement(vector3[i]);
        }
    }
}
