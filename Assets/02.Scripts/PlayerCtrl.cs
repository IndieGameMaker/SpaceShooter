using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCtrl : MonoBehaviour
{
    private float h = 0.0f;
    private float v = 0.0f;
    public float moveSpeed = 8.0f;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per fram
    void Update()
    {
        h = Input.GetAxis("Horizontal"); // -1.0f ~ 0.0f ~ +1.0f
        v = Input.GetAxis("Vertical");   // -1.0f ~ 0.0f ~ +1.0f

        Vector3 dir = (Vector3.forward * v) + (Vector3.right * h);

        // Debug.Log("dir =" + dir.magnitude);
        // Debug.Log("dir.normalied = " + dir.normalized.magnitude);

        transform.Translate(dir.normalized * Time.deltaTime * moveSpeed);
    }


}
