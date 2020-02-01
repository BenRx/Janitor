using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightSanityUp : MonoBehaviour
{
    public float exteriorLightGainSanity;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other) {
        if (other.tag == "Player") {
            PlayerTestController.Instance.sanityGain = exteriorLightGainSanity;
            Debug.Log("J'entre");
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player") {
            PlayerTestController.Instance.sanityGain = 0;
            Debug.Log("J'sort");
        }
    }

    private void OnTriggerStay(Collider other) {
         if (other.tag == "Player") {
            PlayerTestController.Instance.sanityGain = exteriorLightGainSanity;
            Debug.Log("J'entre");
        }   
    }
}
