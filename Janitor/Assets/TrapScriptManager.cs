using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrapScriptManager : MonoBehaviour
{
    private PlayerTestController player;

    void Start()
    {
        player = PlayerTestController.Instance;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other) {
        if (other.tag == "Player") {
            player.ResetPlayer();
        }
    }
}
