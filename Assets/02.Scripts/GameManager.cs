using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [Header("���� ����")]
    [SerializeField] public int currentRoundLevel;              // ���� ����
    [SerializeField] public List<int> roundLevelList;

    [Header("�Ϲݵδ��� ����")]
    [SerializeField] public int currentMoleSpawnInterval;       // �Ϲݵδ��� �����ֱ�
    [SerializeField] public List<int> moleSpawnIntervalList;
    [SerializeField] public int currentMaxActiveMoles;          // �Ϲݵδ��� ���û��� �ִ밳��
    [SerializeField] public List<int> maxActiveMolesList;
    [SerializeField] public int currentMoleHitCount;            // �Ϲݵδ��� ��ƾ��ϴ� Ƚ��
    [SerializeField] public int currentMoleCarrotEatingTime;    // �Ϲݵδ��� ��ٸԴ½ð�
    [SerializeField] public List<int> moleCarrotEatingTimeList;

    [Header("�ְ��δ��� ����")]
    [SerializeField] public int currentSMoleSpawnInterval;       // �ְ��δ��� �����ֱ�
    [SerializeField] public List<int> sMoleSpawnIntervalList;
    [SerializeField] public int currentMaxActiveSMoles;          // �ְ��δ��� ���û��� �ִ밳��(1)
    [SerializeField] public int currentSMoleHitCount;            // �Ϲݵδ��� ��ƾ��ϴ� Ƚ��(1)
    [SerializeField] public int currentSMoleCarrotEatingTime;    // �Ϲݵδ��� ��� �Դ� �ð�(3)


    [Header("�� ũ��")]
    [SerializeField] public Vector3 startPosition;
    [SerializeField] public Vector3 endPosition;
    [SerializeField] public float interval;

    [Header("���")]
    [SerializeField] public GameObject CarrotPrefab;
    [SerializeField] public Transform CarrotTransform;
    [SerializeField] public float yPosition;
    [SerializeField] public List<GameObject> Carrots;

    [Header("�Ϲݵδ���")]
    [SerializeField] public GameObject MolePrefab;
    [SerializeField] public Transform MoleTransform;
    [SerializeField] public List<GameObject> Moles;

    [Header("Ư���δ���")]
    [SerializeField] public GameObject sMolePrefab;
    [SerializeField] public Transform sMoleTransform;


    void Start()
    {
        currentRoundLevel = roundLevelList[0];
        CreateCarrots();
        CreateMoles();
    }

    void CreateCarrots()
    {
        for (float x = startPosition.x; x <= endPosition.x; x += 2)
        {
            for (float z = startPosition.z; z >= endPosition.z; z -= 2)
            {
                Vector3 position = new Vector3(x, yPosition, z);
                Carrots.Add(Instantiate(CarrotPrefab, position, Quaternion.identity, CarrotTransform));
            }
        }
    }

    void CreateMoles()
    {
        for (float x = startPosition.x; x <= endPosition.x; x += 2)
        {
            for (float z = 9; z >= -9; z -= 2)
            {
                Vector3 position = new Vector3(x, 0, z);
                Moles.Add(Instantiate(MolePrefab, position, Quaternion.identity, MoleTransform));
            }
        }
    }

    void Update()
    {
        
    }
}
