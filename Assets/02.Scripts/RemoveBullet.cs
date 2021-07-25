using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RemoveBullet : MonoBehaviour
{
    public GameObject sparkEffect;

    // 충돌 콜백함수(Call Back Function), 이벤트 함수(Event Function)
    void OnCollisionEnter(Collision coll)
    {
        if (coll.collider.tag == "BULLET")
        {
            // 총알을 삭제
            Destroy(coll.gameObject);

            // 스파크 이펙트 , 위치(충돌위치), 법선 벡터
            Vector3 pos = coll.GetContact(0).point;
            Vector3 normal = -coll.GetContact(0).normal;

            // 벡터가 바라보는 방향의 각도를 산출
            Quaternion rot = Quaternion.LookRotation(normal);
            GameObject spark = Instantiate(sparkEffect, pos, rot);
            Destroy(spark, 0.5f);
        }
    }
}
