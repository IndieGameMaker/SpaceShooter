using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; // Unity UI에 접근하기 위한 네임스페이스

public class PlayerCtrl : MonoBehaviour
{
    private float h = 0.0f;
    private float v = 0.0f;
    private float r = 0.0f;

    [Range(5.0f, 20.0f)]
    public float moveSpeed = 8.0f;
    [Range(100.0f, 200.0f)]
    public float turnSpeed = 200.0f;
    private float _turnSpeed;

    // Animation 컴포넌트를 저장할 변수를 선언
    [HideInInspector]      // Unity Atrributes
    [System.NonSerialized] // C# Attributes
    public Animation anim;

    private float initHp = 100.0f;  // 주인공 캐릭터의 초기 생명치
    public float currHp = 100.0f;   // 현재의 생명치 currHp/initHp

    public delegate void PlayerDieHandler();  // 델리게이트는 함수를 저장할 수 있는 데이터 타입
    public static event PlayerDieHandler OnPlayerDie; // 이벤트를 정의

    // Start is called before the first frame update
    IEnumerator Start()
    {
        _turnSpeed = 0.0f;

        anim = GetComponent<Animation>(); //Generic Syntax
        anim.Play("Idle");

        yield return new WaitForSeconds(0.5f);
        _turnSpeed = turnSpeed;
    }

    // Update is called once per fram
    void Update()
    {
        h = Input.GetAxis("Horizontal"); // -1.0f ~ 0.0f ~ +1.0f
        v = Input.GetAxis("Vertical");   // -1.0f ~ 0.0f ~ +1.0f
        r = Input.GetAxis("Mouse X");    //

        Vector3 dir = (Vector3.forward * v) + (Vector3.right * h);

        transform.Translate(dir.normalized * Time.deltaTime * moveSpeed);
        transform.Rotate(Vector3.up * Time.deltaTime * _turnSpeed * r);

        PlayerAnim();
    }

    void PlayerAnim()
    {
        if (v >= 0.1f) // 전진
        {
            anim.CrossFade("RunF", 0.3f);
        }
        else if (v <= -0.1f) // 후진
        {
            anim.CrossFade("RunB", 0.3f);
        }
        else if (h >= 0.1f) // 오른쪽 이동
        {
            anim.CrossFade("RunR", 0.3f);
        }
        else if (h <= -0.1f) // 왼쪽으로 이동
        {
            anim.CrossFade("RunL", 0.3f);
        }
        else
        {
            anim.CrossFade("Idle", 0.3f);
        }
    }


    /*
        OnCollision~ (Enter, Stay, Exit) : Collider IsTrigger X
        OnTrigger~ (Enter, Stay, Exit) : Collider IsTrigger O
    */

    void OnTriggerEnter(Collider coll)
    {
        if (currHp > 0.0f && coll.CompareTag("PUNCH"))
        {
            currHp -= 10.0f;
            if (currHp <= 0.0f)
            {
                PlayerDie();
            }
        }
    }

    void PlayerDie()
    {
        Debug.Log("주인공 사망 !!!");

        // 이벤트 발생(Raise)
        OnPlayerDie();

        // // MONSTER 태그로 설정된 몬스터를 배열 저장
        // GameObject[] monsters = GameObject.FindGameObjectsWithTag("MONSTER");

        // foreach (GameObject monster in monsters)
        // {
        //     // monster.GetComponent<MonsterCtrl>().YouWin();
        //     monster.SendMessage("YouWin", SendMessageOptions.DontRequireReceiver);
        // }
    }
}
