using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

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

    public float attackDist = 2.0f; // 공격 사정거리
    public float traceDist = 10.0f; // 추적 사정거리

    public bool isDie = false;

    void Start()
    {
        // GameObject playerObj = GameObject.FindGameObjectWithTag("PLAYER");
        // if (playerObj != null)
        // {
        //     playerTr = playerObj.GetComponent<Transform>();
        // }

        playerTr = GameObject.FindGameObjectWithTag("PLAYER")?.GetComponent<Transform>();
        monsterTr = GetComponent<Transform>();  //  monsterTr = transform;

        StartCoroutine(CheckMonsterState());
        StartCoroutine(MonsterAction());
    }

    IEnumerator CheckMonsterState()
    {
        while (!isDie) // while (isDie == false)
        {
            float distance = Vector3.Distance(playerTr.position, monsterTr.position);

            if (distance <= attackDist) // 공격사정거리 이내일 경우
            {
                state = State.ATTACK;
            }
            else if (distance <= traceDist) // 추적사정거리
            {
                state = State.TRACE;
            }
            else
            {
                state = State.IDLE;
            }

            yield return new WaitForSeconds(0.3f);
        }
    }

    IEnumerator MonsterAction()
    {
        while (!isDie)
        {
            switch (state)
            {
                case State.IDLE:
                    // 로직
                    break;

                case State.TRACE:
                    break;

                case State.ATTACK:
                    break;

                case State.DIE:
                    break;
            }

            yield return new WaitForSeconds(0.3f);
        }
    }

}
