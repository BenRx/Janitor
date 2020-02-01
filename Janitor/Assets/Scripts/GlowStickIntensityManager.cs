using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlowStickIntensityManager : MonoBehaviour
{
    PlayerTestController player;
    Light pointLight;
    Renderer  renderer;

    void Awake() {
        pointLight = GetComponentInChildren<Light>();
        renderer = GetComponent<Renderer>();
    }
    
    void Start() {
        player = PlayerTestController.Instance;
    }

    void Update()
    {
        pointLight.intensity = player.sanity / 100;
    }
}
