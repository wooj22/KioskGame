using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class GameManager : MonoBehaviour
{
    [Header("라운드 정보")]
    [SerializeField] public int currentRoundLevel;              // 라운드 레벨
    [SerializeField] public List<int> roundLevelList;
    [SerializeField] public int roundLifeSpan;                  // 라운드 수명(=라운드 레벨)
    [SerializeField] public float roundDuration;                // 라운드 진행 시간

    [Header("일반두더지 레벨")]
    [SerializeField] public int currentMoleSpawnInterval;       // 일반두더지 생성주기
    [SerializeField] public List<int> moleSpawnIntervalList;
    [SerializeField] public int currentMaxActiveMoles;          // 일반두더지 동시생성 최대개수
    [SerializeField] public List<int> maxActiveMolesList;
    [SerializeField] public int currentMoleLife;                // 일반두더지 밟아야하는 횟수
    [SerializeField] public int currentMoleCarrotEatingTime;    // 일반두더지 당근먹는시간
    [SerializeField] public List<int> moleCarrotEatingTimeList;

    [Header("최강두더지 레벨")]
    [SerializeField] public int currentSMoleSpawnInterval;       // 최강두더지 생성주기
    [SerializeField] public List<int> sMoleSpawnIntervalList;
    [SerializeField] public int currentMaxActiveSMoles;          // 최강두더지 동시생성 최대개수(1)
    [SerializeField] public int currentSMoleLife;                // 최강두더지 밟아야하는 횟수(1)
    [SerializeField] public int currentSMoleCarrotEatingTime;    // 최강두더지 당근 먹는 시간(3)

    [Header("판 크기")]
    [SerializeField] public Vector3 startPosition;
    [SerializeField] public Vector3 endPosition;
    [SerializeField] public float interval;

    [Header("당근")]
    [SerializeField] public GameObject CarrotPrefab;
    [SerializeField] public Transform CarrotTransform;
    [SerializeField] public float yPosition;
    [SerializeField] public List<GameObject> Carrots;

    [Header("일반두더지")]
    [SerializeField] public GameObject MolePrefab;
    [SerializeField] public Transform MoleTransform;
    [SerializeField] public List<GameObject> Moles;

    [Header("최강두더지")]
    [SerializeField] public GameObject sMolePrefab;
    [SerializeField] public Transform sMoleTransform;
    [SerializeField] public GameObject sMole;

    void Start()
    {
        InitializeGame();
        StartGame();
    }

    /// 오브젝트 풀링
    void InitializeGame()
    {
        CreateCarrots();
        CreateMoles();
        CreateSMoles();
    }

    /// 게임시작
    void StartGame()
    {
        SettingLevel(currentRoundLevel);
        StartCoroutine(RoundStart());
    }

    /// 레벨 셋팅
    void SettingLevel(int level)
    {
        // 수명
        roundLifeSpan = level;

        // 일반두더지
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

        // 최강두더지
        currentSMoleSpawnInterval = sMoleSpawnIntervalList[level - 1];
    }

    /// 라운드 진행
    IEnumerator RoundStart()
    {
        Debug.Log(currentRoundLevel + "라운드 시작");

        // 당근판 활성화
        ActivateCarrots();

        // 두더지 활성화
        Coroutine mole = StartCoroutine(MolesCoroutine());
        Coroutine smole = StartCoroutine(SMolesCoroutine());

        // 라운드 종료
        yield return new WaitForSeconds(roundDuration);
        StopCoroutine(mole);
        StopCoroutine(smole);

        Debug.Log(currentRoundLevel + "라운드 종료");
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

    /// 일반두더지 활성화
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

    /// 최강두더지 활성화
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


    /*---------- 풀링 디자인 ----------*/
    /// 당근판 풀링
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

    /// 당근판 초기화
    void ActivateCarrots()
    {
        for (int i = 0; i < Carrots.Count; i++)
        {
            Carrots[i].SetActive(true);
        }
    }

    /// 일반두더지 풀링(10마리로 재활용)
    void CreateMoles()
    {
        for (int i=0; i<10; i++) 
        {
            GameObject m = Instantiate(MolePrefab, startPosition, Quaternion.identity, MoleTransform);
            m.SetActive(false);
            Moles.Add(m);
        }
    }

    /// 특수두더지 풀링
    void CreateSMoles()
    {
        GameObject sm = Instantiate(sMolePrefab, startPosition, Quaternion.identity, sMoleTransform);
        sm.SetActive(false);
        sMole = sm;
    }
}
