using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PickableObject : MonoBehaviour
{

    private Renderer rend;
    private bool FadeIn = false;
    private float width = 1.0f;
    public float speed = 2f;
    private bool FadeOut = false;
    private bool imPick;
    public bool imLight = false;
    public GameObject light;
    // Start is called before the first frame update
    void Start()
    {
        rend = GetComponent<Renderer>();
    }

    // Update is called once per frame
    void Update()
    {
        if (FadeIn) {
            FadeInPickable();
        } else if (FadeOut) {
            FadeOutPickable();
        }
    }
    private void FadeInPickable() {
        if (width >= 1.2f) {
            width = 1.2f;
            FadeIn = false;
        }
        rend.material.SetFloat("_OutlineWidth", width);
        width += Time.deltaTime * speed;
    }

    private void FadeOutPickable() {
        if (width <= 1.0f) {
            FadeOut = false;
            width = 1.0f;
        }
        rend.material.SetFloat("_OutlineWidth", width);
        width -= Time.deltaTime;
    }
    private void OnTriggerEnter(Collider other) {
        if (other.tag == "Player") {
            FadeOut = false;
            FadeIn = true;
        }
    }

    private void OnTriggerExit(Collider other) {
        if (other.tag == "Player") {
            FadeIn = false;
            FadeOut = true;
        }
    }

    private void OnTriggerStay(Collider other) {
        if (other.tag == "Player") {
            FadeIn = true;
        }
        if (other.tag == "PickUpZone") {
            imPick = PlayerTestController.Instance.PickMe(gameObject);
            if (imLight && imPick) {
                light.SetActive(false);
            }
        }

        if (imPick && imLight && other.tag == "PickDownLight") {
            gameObject.transform.position = other.gameObject.transform.position;
            gameObject.transform.parent = other.gameObject.transform;
            gameObject.transform.rotation = Quaternion.identity;
    //        Debug.Log(other.gameObject.transform.rotation);
  //          gameObject.transform.rotation = new Quaternion(-other.gameObject.transform.rotation.x, other.gameObject.transform.rotation.y, other.gameObject.transform.rotation.z, other.gameObject.transform.rotation.w);
            light.SetActive(true);
        }
        if (imPick && !imLight && other.tag == "PickDownInterupt") {
            gameObject.transform.position = other.gameObject.transform.position;
            gameObject.transform.parent = other.gameObject.transform;
            SceneManager.LoadScene("End", LoadSceneMode.Single);   
        }
    }
}
