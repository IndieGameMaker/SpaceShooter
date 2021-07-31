using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
        if (coll.CompareTag("PUNCH"))
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

    }
}
