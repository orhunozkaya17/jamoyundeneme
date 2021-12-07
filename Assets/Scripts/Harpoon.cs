using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Harpoon : MonoBehaviour
{
    //variables
    private Rigidbody rb;
    private Gun gunScript;
    private bool hasHit;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        gunScript = GameObject.FindWithTag("Gun").GetComponent<Gun>();
    }

    // Update is called once per frame
    void Update()
    {
        if (hasHit == false)
        {
            float angle = Mathf.Atan2(rb.velocity.y, rb.velocity.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        hasHit = true;
        rb.velocity = Vector3.zero;
        rb.isKinematic = true;
        gunScript.prepareHarpoon();
    }
}
