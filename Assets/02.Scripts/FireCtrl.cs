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

    void Start()
    {
        audio = GetComponent<AudioSource>();
        muzzleFlash = firePos.GetComponentInChildren<MeshRenderer>();
        muzzleFlash.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Fire();
        }
    }

    void Fire()
    {
        // Bullet 동적으로 생성 : Instantiate(프리팹, 위치, 각도)
        Instantiate(bulletPrefab, firePos.position, firePos.rotation);
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
        muzzleFlash.material.mainTextureScale = Vector3.one * scale;
        //muzzleFlash.material.mainTextureScale = new Vector3(scale, scale, scale);

        // MuzzleFlash 활성화
        muzzleFlash.enabled = true;

        // Waitting.. Sleep....
        yield return new WaitForSeconds(0.2f);

        // MuzzleFlash 비활성화
        muzzleFlash.enabled = false;
    }
}

/*
    총구화염 (Muzzle Flash)
*/

