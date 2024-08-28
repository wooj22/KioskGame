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
    [SerializeField] public int currentMoleLife;                // �Ϲݵδ��� ��ƾ��ϴ� Ƚ��
    [SerializeField] public int currentMoleCarrotEatingTime;    // �Ϲݵδ��� ��ٸԴ½ð�
    [SerializeField] public List<int> moleCarrotEatingTimeList;

    [Header("�ְ��δ��� ����")]
    [SerializeField] public int currentSMoleSpawnInterval;       // �ְ��δ��� �����ֱ�
    [SerializeField] public List<int> sMoleSpawnIntervalList;
    [SerializeField] public int currentMaxActiveSMoles;          // �ְ��δ��� ���û��� �ִ밳��(1)
    [SerializeField] public int currentSMoleLife;                // �ְ��δ��� ��ƾ��ϴ� Ƚ��(1)
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
        InitializeGame();
        StartGame();
    }

    /// ������Ʈ Ǯ��
    void InitializeGame()
    {
        CreateCarrots();
        CreateMoles();
        CreateSMoles();
    }

    /// ���ӽ���
    void StartGame()
    {
        SettingLevel(currentRoundLevel);
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
        currentMoleLife = level + 2;
        currentMoleCarrotEatingTime = moleCarrotEatingTimeList[level - 1];

        for (int i = 0; i < Moles.Count; i++)
        {
            Mole m = Moles[i].GetComponent<Mole>();
            m.moleCarrotEatingTime = currentMoleCarrotEatingTime;
            m.moleLife = currentMoleLife;
        }

        // �ְ��δ���
        currentSMoleSpawnInterval = sMoleSpawnIntervalList[level - 1];
    }

    /// ���� ����
    IEnumerator RoundStart()
    {
        Debug.Log(currentRoundLevel + "���� ����");

        // ����� Ȱ��ȭ
        ActivateCarrots();

        // �δ��� Ȱ��ȭ
        Coroutine mole = StartCoroutine(MolesCoroutine());
        Coroutine smole = StartCoroutine(SMolesCoroutine());

        // ���� ����
        yield return new WaitForSeconds(roundDuration);
        StopCoroutine(mole);
        StopCoroutine(smole);

        Debug.Log(currentRoundLevel + "���� ����");
    }

    IEnumerator MolesCoroutine()
    {
        while (true)
        {
            SpawnMoles();
            yield return new WaitForSeconds(currentMoleSpawnInterval);
        }
    }

    IEnumerator SMolesCoroutine()
    {
        while (true)
        {
            SpawnSMole();
            yield return new WaitForSeconds(currentSMoleSpawnInterval);
        }
    }

    /// �Ϲݵδ��� Ȱ��ȭ
    public void SpawnMoles()
    {
        for (int i = 0; i < currentMaxActiveMoles; i++)
        {
            GameObject mole = Moles[i];
            GameObject randomCarrot = Carrots[Random.Range(0, Carrots.Count)];
            while (!randomCarrot.activeSelf)
            {
                randomCarrot = Carrots[Random.Range(0, Carrots.Count)];
            }

            Vector3 position = randomCarrot.transform.position;
            position.y = 0;

            mole.transform.position = position;
            mole.SetActive(true);
        }
    }

    /// �ְ��δ��� Ȱ��ȭ
    public void SpawnSMole()
    {
        GameObject randomCarrot = Carrots[Random.Range(0, Carrots.Count)];
        while (!randomCarrot.activeSelf)
        {
            randomCarrot = Carrots[Random.Range(0, Carrots.Count)];
        }

        Vector3 position = randomCarrot.transform.position;
        position.y = 0;

        sMole.transform.position = position;
        sMole.SetActive(true);
    }


    /*---------- Ǯ�� ������ ----------*/
    /// ����� Ǯ��
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
    void ActivateCarrots()
    {
        for (int i = 0; i < Carrots.Count; i++)
        {
            Carrots[i].SetActive(true);
        }
    }

    /// �Ϲݵδ��� Ǯ��(10������ ��Ȱ��)
    void CreateMoles()
    {
        for (int i=0; i<10; i++) 
        {
            GameObject m = Instantiate(MolePrefab, startPosition, Quaternion.identity, MoleTransform);
            m.SetActive(false);
            Moles.Add(m);
        }
    }

    /// Ư���δ��� Ǯ��
    void CreateSMoles()
    {
        GameObject sm = Instantiate(sMolePrefab, startPosition, Quaternion.identity, sMoleTransform);
        sm.SetActive(false);
        sMole = sm;
    }
}
