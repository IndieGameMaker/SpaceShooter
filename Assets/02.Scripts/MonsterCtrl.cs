using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterCtrl : MonoBehaviour
{
    public enum State
    {
        IDLE, TRACE, ATTACK, DIE
    }

    // 몬스터의 상태값을 저장하기 위한 변수
    public State state = State.IDLE;

    private Transform playerTr; //주인공 캐릭터의 Transform 컴포넌트를 저장할 변수
    private Transform monsterTr;

    void Start()
    {
        // GameObject playerObj = GameObject.FindGameObjectWithTag("PLAYER");
        // if (playerObj != null)
        // {
        //     playerTr = playerObj.GetComponent<Transform>();
        // }

        playerTr = GameObject.FindGameObjectWithTag("PLAYER")?.GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {

    }
}
