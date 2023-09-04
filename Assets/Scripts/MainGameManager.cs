using System.Collections;
using System.Collections.Generic;
using UnityEngine;


// 목적1: SpawnPoint에 Player를 위치시킨다.
// 필요속성 : Spawnpoints배열
// 목적2: 특정 시간이 지나면 게임 시작 명령을 내린다
// 필요속성2: 특정시간, 현재시간, 게임시작명령 flag
public class MainGameManager : MonoBehaviour
{
    public static MainGameManager Instance;

    // 필요속성 : Spawnpoints배열
    public Transform[] spawnPoints;

    // 필요속성2: 특정시간, 현재시간, 게임시작명령 flag
    public float gameStartTime = 10f;
    float currentTime = 0f;
    public bool isGameStarted = false;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
    }

    void SetSpawnPoints()
    {
        Transform spawnPointParent = GameObject.Find("Grp_SpawnPoints").transform;

        spawnPoints = new Transform[spawnPointParent.childCount];
        
        for (int i =0; i < spawnPointParent.childCount; i++)
        {
            spawnPoints[i] = spawnPointParent.GetChild(i);
        }
    }

    // Update is called once per frame
    void Update()
    {
        currentTime += Time.deltaTime;

        if (currentTime > gameStartTime)
        {
            isGameStarted = true;
        }
    }

    // 시간을 기록해서 특정 시간이 지나면 게임 시작을 알린다.
    IEnumerator TimerCoroutine()
    {
        // 목적2: 특정 시간이 지나면 게임 시작 명령을 내린다
        yield return new WaitForSeconds(1);

        SetSpawnPoints();
        
        yield return new WaitForSeconds(gameStartTime);

        isGameStarted = true;
        print("게임이 시작되었습니다.");

    }

    // MainGameManager의 타이머를 시작하는 함수
    public void StartTimer()
    {
        SetSpawnPoints();

        StartCoroutine(TimerCoroutine());
    }
}
