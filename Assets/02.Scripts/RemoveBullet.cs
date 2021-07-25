using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RemoveBullet : MonoBehaviour
{

    // 충돌 콜백함수(Call Back Function), 이벤트 함수(Event Function)
    void OnCollisionEnter(Collision coll)
    {
        if (coll.collider.tag == "BULLET")
        {
            // 총알을 삭제
            Destroy(coll.gameObject);
        }
    }
}
