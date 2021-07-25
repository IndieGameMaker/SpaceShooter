using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class BarrelCtrl : MonoBehaviour
{
    private int hitCount;
    public Texture[] textures;
    public new MeshRenderer renderer;
    public GameObject expEffect;

    /*
        난수 발생
        Random.Range(min, max)

        Random.Range(0, 10)  => 0,1,2,...,9
        Random.Range(0.0f, 10.0f)   => 0.0f, ~ , 10.0f
    */
    void Start()
    {
        int idx = Random.Range(0, textures.Length); // 0, 1, 2
        renderer = GetComponentInChildren<MeshRenderer>();
        renderer.material.mainTexture = textures[idx];
    }

    void OnCollisionEnter(Collision coll)
    {
        if (coll.collider.CompareTag("BULLET"))
        {
            if (++hitCount == 3)
            {
                ExpBarrel();
            }
        }
    }

    void ExpBarrel()
    {
        Rigidbody rb = this.gameObject.AddComponent<Rigidbody>();
        rb.AddForce(Vector3.up * 1500.0f);
        Destroy(this.gameObject, 2.0f);

        // 폭발효과 발생
        GameObject exp = Instantiate(expEffect, transform.position, Quaternion.identity);
        Destroy(exp, 5.0f);
    }
}
