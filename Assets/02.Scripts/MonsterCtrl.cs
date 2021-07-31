#pragma warning disable IDE0051

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
    private readonly int hashTrace = Animator.StringToHash("IsTrace");
    private readonly int hashAttack = Animator.StringToHash("IsAttack");
    private readonly int hashHit = Animator.StringToHash("Hit");
    private readonly int hashDie = Animator.StringToHash("Die");
    private readonly int hashPlayerDie = Animator.StringToHash("PlayerDie");
    private readonly int hashAniSpeed = Animator.StringToHash("AniSpeed");

    private GameObject bloodEffect;
    private float hp = 100.0f;

    void OnEnable()
    {
        // 특정 이벤트를 연결할 때 사용
        PlayerCtrl.OnPlayerDie += this.YouWin;

        StartCoroutine(CheckMonsterState());
        StartCoroutine(MonsterAction());
    }

    void OnDisable()
    {
        PlayerCtrl.OnPlayerDie -= this.YouWin;
    }

    void Awake()
    {
        playerTr = GameObject.FindGameObjectWithTag("PLAYER")?.GetComponent<Transform>();
        monsterTr = GetComponent<Transform>();  //  monsterTr = transform;
        agent = GetComponent<NavMeshAgent>();
        anim = GetComponent<Animator>();

        // 혈흔효과 프리팹을 로딩
        bloodEffect = Resources.Load<GameObject>("BloodEffect");
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

    void OnCollisionEnter(Collision coll)
    {
        if (coll.collider.CompareTag("BULLET"))
        {
            Destroy(coll.gameObject);
            anim.SetTrigger(hashHit);
            // 총알의 충돌 좌표
            Vector3 pos = coll.GetContact(0).point;
            // 법선벡터를 Quaternion 타입으로 변환
            Quaternion rot = Quaternion.LookRotation(-coll.GetContact(0).normal);
            // 혈흔 효과 생성
            GameObject blood = Instantiate(bloodEffect, pos, rot);
            Destroy(blood, 0.5f);

            // HP 차감
            hp -= 25.0f;
            if (hp <= 0.0f)
            {
                MonsterDie();
            }
        }
    }

    void MonsterDie()
    {
        Debug.Log("Monster Die!");
        // 내비게이션 정지
        agent.isStopped = true;
        // Die 애니메이션 실행
        anim.SetTrigger(hashDie);

        GetComponent<CapsuleCollider>().enabled = false;

        // 코루틴 강제 종료
        StopAllCoroutines();

        // 오브젝트 풀링으로 되돌리는 함수호출
        Invoke("ReturnPooling", 5.0f);
    }

    void ReturnPooling()
    {
        this.gameObject.SetActive(false);

        isDie = false;
        hp = 100.0f;
        GetComponent<CapsuleCollider>().enabled = true;
        state = State.IDLE;
    }

    void OnTriggerEnter(Collider coll)
    {
        Debug.Log(coll.gameObject.name);
    }

    public void YouWin()
    {
        agent.isStopped = true;
        StopAllCoroutines();
        anim.SetFloat(hashAniSpeed, Random.Range(0.8f, 1.2f));
        anim.SetTrigger(hashPlayerDie);
    }

}
