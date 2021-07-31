using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
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
        // InvokeRepeating("CreateMonster", 2.0f, createTime);
        StartCoroutine(CreateMonster());
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
