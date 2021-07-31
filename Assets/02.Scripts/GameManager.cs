using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public Transform[] points;

    void Start()
    {
        points = GameObject.Find("SpawnPointGroup")?.GetComponentsInChildren<Transform>();
    }

    // Update is called once per frame
    void Update()
    {

    }
}
