using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActionFallFirstScene : ActionScript
{
    private Rigidbody[] rigidbodies;
    // Start is called before the first frame update
    void Start()
    {
        rigidbodies = GetComponentsInChildren<Rigidbody>();        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public override void makeAction() {
        for (int i = 0; i < rigidbodies.Length; i++) {
            rigidbodies[i].isKinematic = false;
        }
    }
}
