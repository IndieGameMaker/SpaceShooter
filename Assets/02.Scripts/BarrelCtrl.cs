using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BarrelCtrl : MonoBehaviour
{
    private int hitCount;
    public Texture[] textures;
    public new MeshRenderer renderer;

    // Start is called before the first frame update
    void Start()
    {
        renderer = GetComponentInChildren<MeshRenderer>();
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
    }
}
