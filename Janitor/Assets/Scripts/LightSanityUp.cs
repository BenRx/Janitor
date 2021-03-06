﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightSanityUp : MonoBehaviour
{
    private void OnTriggerEnter(Collider other) {
        if (other.tag == "Player") {
            PlayerTestController.Instance.isInSafeZone = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player") {
            PlayerTestController.Instance.isInSafeZone = false;
        }
    }
}
