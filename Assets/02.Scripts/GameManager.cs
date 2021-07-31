using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public List<GameObject> monsterPool = new List<GameObject>();
    public int maxPool = 10;

    public Transform[] points;
    public GameObject monsterPrefab;

    public float createTime = 3.0f;
    public bool isGameOver = false;

    private WaitForSeconds ws;

    void OnEnable()
    {
        PlayerCtrl.OnPlayerDie += GameOver;
    }

    void OnDisable()
    {
        PlayerCtrl.OnPlayerDie -= GameOver;
    }

    void GameOver()
    {
        isGameOver = true;
    }

    void Start()
    {
        ws = new WaitForSeconds(createTime);

        points = GameObject.Find("SpawnPointGroup")?.GetComponentsInChildren<Transform>();

        CreatePooling();
        // InvokeRepeating("CreateMonster", 2.0f, createTime);
        // StartCoroutine(CreateMonster());
    }

    void CreatePooling()
    {
        for (int i = 0; i < maxPool; i++)
        {
            // 몬스터 인스턴스 생성
            GameObject monster = Instantiate<GameObject>(monsterPrefab);
            monster.name = $"Monster_{i:00}";
            monster.SetActive(false);

            // 오브젝트 풀링에 추가
            monsterPool.Add(monster);
        }
    }

    IEnumerator CreateMonster()
    {
        yield return new WaitForSeconds(2.0f);

        while (!isGameOver)
        {
            int idx = Random.Range(1, points.Length);
            Instantiate(monsterPrefab, points[idx].position, Quaternion.identity);

            yield return ws;
        }
    }
}

/*
    오브젝트 풀링 (Object Pooling)

*/