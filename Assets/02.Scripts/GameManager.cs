using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public Transform[] points;
    public GameObject monsterPrefab;

    public float createTime = 3.0f;
    public bool isGameOver = false;

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
        points = GameObject.Find("SpawnPointGroup")?.GetComponentsInChildren<Transform>();
    }

    // Update is called once per frame
    void Update()
    {

    }
}
