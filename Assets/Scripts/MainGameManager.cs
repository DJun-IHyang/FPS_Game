using System.Collections;
using System.Collections.Generic;
using UnityEngine;


// ����1: SpawnPoint�� Player�� ��ġ��Ų��.
// �ʿ�Ӽ� : Spawnpoints�迭
// ����2: Ư�� �ð��� ������ ���� ���� ����� ������
// �ʿ�Ӽ�2: Ư���ð�, ����ð�, ���ӽ��۸�� flag
public class MainGameManager : MonoBehaviour
{
    public static MainGameManager Instance;

    // �ʿ�Ӽ� : Spawnpoints�迭
    public Transform[] spawnPoints;

    // �ʿ�Ӽ�2: Ư���ð�, ����ð�, ���ӽ��۸�� flag
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

    // �ð��� ����ؼ� Ư�� �ð��� ������ ���� ������ �˸���.
    IEnumerator TimerCoroutine()
    {
        // ����2: Ư�� �ð��� ������ ���� ���� ����� ������
        yield return new WaitForSeconds(1);

        SetSpawnPoints();
        
        yield return new WaitForSeconds(gameStartTime);

        isGameStarted = true;
        print("������ ���۵Ǿ����ϴ�.");

    }

    // MainGameManager�� Ÿ�̸Ӹ� �����ϴ� �Լ�
    public void StartTimer()
    {
        SetSpawnPoints();

        StartCoroutine(TimerCoroutine());
    }
}
