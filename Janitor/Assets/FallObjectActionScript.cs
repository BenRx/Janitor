using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallObjectActionScript : ActionScript
{
    private Rigidbody rb;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    public override void makeAction() {
        rb.isKinematic = false;
    }
}
