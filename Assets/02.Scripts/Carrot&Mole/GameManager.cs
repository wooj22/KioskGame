using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class GameManager : MonoBehaviour
{
    [Header("���� ����")]
    [SerializeField] public int currentRoundLevel;              // ���� ����
    [SerializeField] public List<int> roundLevelList;
    [SerializeField] public int roundLifeSpan;                  // ���� ����(=���� ����)
    [SerializeField] public float roundDuration;                // ���� ���� �ð�

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
    [SerializeField] public int currentSMoleHitCount;            // �ְ��δ��� ��ƾ��ϴ� Ƚ��(1)
    [SerializeField] public int currentSMoleCarrotEatingTime;    // �ְ��δ��� ��� �Դ� �ð�(3)

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

    [Header("�ְ��δ���")]
    [SerializeField] public GameObject sMolePrefab;
    [SerializeField] public Transform sMoleTransform;
    [SerializeField] public GameObject sMole;


    void Start()
    {
        // ������Ʈ Ǯ��
        CreateCarrots();
        CreateMoles();
        CreateSMoles();

        // 1������ ����
        currentRoundLevel = roundLevelList[0];

        // ���� ����
        SettingLevel(currentRoundLevel);

        // ���� ����
        StartCoroutine(RoundStart());
    }

    /// ���� ����
    void SettingLevel(int level)
    {
        // ����
        roundLifeSpan = level;

        // �Ϲݵδ���
        currentMoleSpawnInterval = moleSpawnIntervalList[level-1];
        currentMaxActiveMoles = maxActiveMolesList[level - 1];
        currentMoleHitCount = level + 2;
        currentMoleCarrotEatingTime = moleCarrotEatingTimeList[level - 1];

        // �ְ��δ���
        currentSMoleSpawnInterval = sMoleSpawnIntervalList[level - 1];
    }

    /// ���� ����
    IEnumerator RoundStart()
    {
        // ����� Ȱ��ȭ
        ActiveCarrots();

        yield return 0f;
    }

    /// ����� ����
    void CreateCarrots()
    {
        for (float x = startPosition.x; x <= endPosition.x; x += 2)
        {
            for (float z = startPosition.z; z >= endPosition.z; z -= 2)
            {
                Vector3 position = new Vector3(x, yPosition, z);
                GameObject c = Instantiate(CarrotPrefab, position, Quaternion.identity, CarrotTransform);
                c.SetActive(false);
                Carrots.Add(c);
            }
        }
    }

    /// ����� �ʱ�ȭ
    void ActiveCarrots()
    {
        for (int i = 0; i < Carrots.Count; i++)
        {
            Carrots[i].SetActive(true);
        }
    }

    /// �Ϲݵδ��� ����(10������ ��Ȱ��)
    void CreateMoles()
    {
        for (int i=0; i<10; i++) 
        {
            GameObject m = Instantiate(MolePrefab, startPosition, Quaternion.identity, MoleTransform);
            m.SetActive(false);
            Moles.Add(m);
        }
    }

    /// Ư���δ��� ����
    void CreateSMoles()
    {
        GameObject sm = Instantiate(sMolePrefab, startPosition, Quaternion.identity, sMoleTransform);
        sm.SetActive(false);
        sMole = sm;
    }
}
