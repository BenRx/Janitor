using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FearBarManager : MonoBehaviour
{
    private PlayerTestController player;
    private Slider fearBar;



    void Awake() {
        player = PlayerTestController.Instance;
        fearBar = GetComponent<Slider>();
    }
    void Start() {
        
    }

    void Update()
    {
        fearBar.value = player.fearLvl;
    }
}
