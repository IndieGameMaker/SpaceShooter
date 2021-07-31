using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class FireCtrl : MonoBehaviour
{
    public GameObject bulletPrefab;
    public Transform firePos;
    public AudioClip fireSfx;

    private new AudioSource audio;
    public MeshRenderer muzzleFlash;

    private RaycastHit hit;

    void Start()
    {
        audio = GetComponent<AudioSource>();
        muzzleFlash = firePos.GetComponentInChildren<MeshRenderer>();
        muzzleFlash.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        Debug.DrawRay(firePos.position, firePos.forward * 10.0f, Color.green);

        if (Input.GetMouseButtonDown(0))
        {
            Fire();

            if (Physics.Raycast(firePos.position, firePos.forward, out hit, 10.0f))
            {
                Debug.Log($"hit={hit.collider.name}");
            }
        }
    }

    void Fire()
    {
        // Bullet 동적으로 생성 : Instantiate(프리팹, 위치, 각도)
        // Instantiate(bulletPrefab, firePos.position, firePos.rotation);
        audio.PlayOneShot(fireSfx, 0.8f);
        StartCoroutine(ShowMuzzleFlash());
    }

    // 코루틴(Co-routine)
    IEnumerator ShowMuzzleFlash()
    {
        // Texture Offset 변경
        Vector2 offset = new Vector2(Random.Range(0, 2), Random.Range(0, 2)) * 0.5f;
        muzzleFlash.material.mainTextureOffset = offset;

        // Texture Scale 변경
        float scale = Random.Range(1.0f, 2.5f);
        muzzleFlash.transform.localScale = Vector3.one * scale;

        // Muzzle Flash Z축으로 불규칙하게 회전
        float angle = Random.Range(0, 360); // 오일러 회전각
        muzzleFlash.transform.localRotation = Quaternion.Euler(Vector3.forward * angle);

        // MuzzleFlash 활성화
        muzzleFlash.enabled = true;

        // Waitting.. Sleep....
        yield return new WaitForSeconds(0.3f);

        // MuzzleFlash 비활성화
        muzzleFlash.enabled = false;
    }
}

/*
    Quaternion 쿼터니언 (사원수 x, y, z, w) : 복소수 4차원 벡터

    x->y->z 오일러 회전  => 짐벌락(Gimbal Lock)
*/

