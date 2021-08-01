using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro; // TextMesh Pro 네임스페이스

public class GameManager : MonoBehaviour
{
    public List<GameObject> monsterPool = new List<GameObject>();
    public int maxPool = 10;

    public Transform[] points;
    public GameObject monsterPrefab;

    public float createTime = 3.0f;
    public bool isGameOver = false;

    public TMP_Text scoreText;

    public float score;

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
        StartCoroutine(CreateMonster());
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
            // 몬스터 풀링에서 검색해서 활성화 시키는 로직
            foreach (var monster in monsterPool)
            {
                if (monster.activeSelf == false)
                {
                    int idx = Random.Range(1, points.Length);
                    monster.transform.position = points[idx].position;
                    monster.transform.rotation = Quaternion.LookRotation(points[0].position - points[idx].position);
                    monster.SetActive(true);
                    break;
                }
            }

            yield return ws;
        }
    }
}

/*
    오브젝트 풀링 (Object Pooling)
    GI
*/