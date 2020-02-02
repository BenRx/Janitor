﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTestController : MonoBehaviour
{

    private int layer;
    public float distance = 10f;
    private static PlayerTestController _instance;
    public static PlayerTestController Instance { get {return _instance; } }
    public float sanity = 100f;
    public float fearLvl = 0;
    public float fearCoef = 4;
    public bool isInSafeZone = false;
    public float maxSpeed = 20f;
    public float speed = 10f;
    public float jumpSpeed = 100f;
    public GameObject GlowStick;
    private Rigidbody rb;
    private Vector3 initialPosition;
    private float initialTime;
    private float currentTime;
    private Animator animator;
    private bool isPush = false;
    void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(this.gameObject);
        } else {
            _instance = this;
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        layer = LayerMask.GetMask("PickUp");
        layer = ~layer;
        rb = GetComponent<Rigidbody>();
        Physics.IgnoreLayerCollision(0, 8);
        initialPosition = transform.position;
        initialTime = Time.time;
        animator = GetComponentInChildren<Animator>();
    }

    private bool IsGrounded() {
       return Physics.Raycast(transform.position, -Vector3.up, 1.0f + 0.1f);
    }


    // Update is called once per frame
    void Update()
    {
        currentTime = Time.time - initialTime;
        if (Input.GetAxisRaw("Horizontal") > 0.5f && rb.velocity.x < maxSpeed) {
            rb.AddForce(Vector3.right * speed);
        } else if (Input.GetAxisRaw("Horizontal") < -0.5f && rb.velocity.x < maxSpeed) {
            rb.AddForce(Vector3.left * speed);
        }
        if (Input.GetButtonDown("Jump") && IsGrounded()) {
            rb.AddForce(Vector3.up * jumpSpeed);
        } 
        if (Input.GetButtonDown("Submit") || Input.GetKeyDown(KeyCode.R)) {
            ResetPlayer();
        }
        animator.SetFloat("Speed", rb.velocity.x);
        animator.SetBool("isGrounded", IsGrounded());
        animator.SetBool("IsPush", isPush);
        ManageSanity();
        ManageFear();
    }

 
    void ManageFear() {
        if (fearLvl < 100) {
            fearLvl = currentTime / fearCoef;
        }
    }

    void ManageSanity() {
        if (!isInSafeZone && sanity > 0) {
            sanity -= fearLvl / 2;
        } else if (isInSafeZone && sanity < 100) {
            sanity += 2;
        }
    }

    void ResetPlayer() {
        transform.position = initialPosition;
        sanity = 100f;
        fearLvl = 0;
        initialTime = Time.time;
    }

    public bool PickMe(GameObject gm) {
        if (Input.GetButtonDown("Fire1")) {
            gm.transform.position = new Vector3(GlowStick.transform.position.x + 0.3f, GlowStick.transform.position.y, GlowStick.transform.position.z);
            gm.transform.parent = GlowStick.transform;
            return true;
        }
        return false;
    }

    private void OnCollisionStay(Collision other) {
        if (other.gameObject.tag == "Case") {
            if (Input.GetAxisRaw("Horizontal") > 0.5f ) {
                rb.AddForce(Vector3.right * speed * 2f);
            } else if (Input.GetAxisRaw("Horizontal") < -0.5f ) {
                rb.AddForce(Vector3.left * speed * 2f);
            }
        // Debug-draw all contact points and normals
            foreach (ContactPoint contact in other.contacts)
            {
                Debug.Log(contact.normal);
                Debug.Log(contact.normal.y > -1.5f && contact.normal.y < 1.5f);
                if (contact.normal.y > -1.5f  && contact.normal.y < -0.5 || contact.normal.y > 0.5 && contact.normal.y < 1.5f) {
                    isPush = false;
                    return;
                }
                else{
                    Debug.Log("NIOIIIIQUE");
                    isPush = true;
                    return;
                }
            }
        }
        else {
            isPush = false;
        }
    }
}
