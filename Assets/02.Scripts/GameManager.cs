using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [Header("라운드 정보")]
    [SerializeField] public int currentRoundLevel;              // 라운드 레벨
    [SerializeField] public List<int> roundLevelList;

    [Header("일반두더지 레벨")]
    [SerializeField] public int currentMoleSpawnInterval;       // 일반두더지 생성주기
    [SerializeField] public List<int> moleSpawnIntervalList;
    [SerializeField] public int currentMaxActiveMoles;          // 일반두더지 동시생성 최대개수
    [SerializeField] public List<int> maxActiveMolesList;
    [SerializeField] public int currentMoleHitCount;            // 일반두더지 밟아야하는 횟수
    [SerializeField] public int currentMoleCarrotEatingTime;    // 일반두더지 당근먹는시간
    [SerializeField] public List<int> moleCarrotEatingTimeList;

    [Header("최강두더지 레벨")]
    [SerializeField] public int currentSMoleSpawnInterval;       // 최강두더지 생성주기
    [SerializeField] public List<int> sMoleSpawnIntervalList;
    [SerializeField] public int currentMaxActiveSMoles;          // 최강두더지 동시생성 최대개수(1)
    [SerializeField] public int currentSMoleHitCount;            // 일반두더지 밟아야하는 횟수(1)
    [SerializeField] public int currentSMoleCarrotEatingTime;    // 일반두더지 당근 먹는 시간(3)


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

    [Header("특수두더지")]
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
