using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro; // TextMesh Pro 네임스페이스

/*
    Singleton 싱글턴(싱글톤)
*/

public class GameManager : MonoBehaviour
{
    public static GameManager instance = null;

    public List<GameObject> monsterPool = new List<GameObject>();
    public int maxPool = 10;

    public Transform[] points;
    public GameObject monsterPrefab;

    public float createTime = 3.0f;
    public bool isGameOver = false;

    public TMP_Text scoreText;

    private float score; // 멤버 변수

    // 스코어 프로퍼티를 선언 (public 타입 프로퍼티명)
    public float Score
    {
        get { return score; }
        set
        {
            score += value;
            scoreText.text = $"<color=#00ff00>Score :</color> <color=#ff0000>{score:00000}</color>";
            PlayerPrefs.SetFloat("SCORE", score);
        }
    }

    private WaitForSeconds ws;

    void Awake()
    {
        //instance = this;

        if (instance == null)   // 처음 실행여부를 판단
        {
            instance = this;
        }
        else if (instance != this)
        {
            Destroy(this.gameObject);
        }
        DontDestroyOnLoad(this.gameObject);
    }

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
        // 기존에 저장된 데이터를 로드
        this.Score = PlayerPrefs.GetFloat("SCORE", 0.0f);

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