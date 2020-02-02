using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FearBarManager : MonoBehaviour
{
    private PlayerTestController player;
    private Slider fearBar;
    void Awake() {
        fearBar = GetComponent<Slider>();
    }
    void Start() {
        player = PlayerTestController.Instance;
    }

    void Update()
    {
        fearBar.value = player.fearLvl;
        Debug.Log(player.fearLvl);
    }
}
