using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI; // Navigation을 사용하기위해서 추가해야하는 네임스페이스

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
    private NavMeshAgent agent;
    private Animator anim;

    public float attackDist = 2.0f; // 공격 사정거리
    public float traceDist = 10.0f; // 추적 사정거리

    public bool isDie = false;

    // 파라메터의 해시값을 미리 추출
    private int hashTrace = Animator.StringToHash("IsTrace");
    private int hashAttack = Animator.StringToHash("IsAttack");

    void Start()
    {
        playerTr = GameObject.FindGameObjectWithTag("PLAYER")?.GetComponent<Transform>();
        monsterTr = GetComponent<Transform>();  //  monsterTr = transform;
        agent = GetComponent<NavMeshAgent>();
        anim = GetComponent<Animator>();

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

    // 몬스터의 상태에 따라서 행동을 분기시키는 로직
    IEnumerator MonsterAction()
    {
        while (!isDie)
        {
            switch (state)
            {
                case State.IDLE:
                    // 정지
                    agent.isStopped = true;
                    // Idle 애니메이션으로 변경 : Walk --> Idle
                    anim.SetBool(hashTrace, false);
                    break;

                case State.TRACE:
                    // 추적로직을 구동
                    agent.isStopped = false;
                    agent.SetDestination(playerTr.position);
                    // Walk 애니메이션으로 변경 : Idle --> Walk
                    anim.SetBool(hashTrace, true);
                    anim.SetBool(hashAttack, false);
                    break;

                case State.ATTACK:
                    // 추적로직을 정지
                    agent.isStopped = true;
                    // Attack 애니메이션으로 변경 : Walk --> Attack
                    anim.SetBool(hashAttack, true);
                    break;

                case State.DIE:
                    break;
            }

            yield return new WaitForSeconds(0.3f);
        }
    }

}
