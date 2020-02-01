using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pressure : MonoBehaviour
{

    public ActionScript action;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other) {
        if (other.tag == "Case") {
            Debug.Log("wut");
            action.makeAction();
        }
    }
}
