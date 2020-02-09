using System.Collections;
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
    public int sanityCoef = 6;
    public GameObject GlowStick;
    private Rigidbody rb;
    private Vector3 initialPosition;
    private float initialTime;
    private float currentTime;
    private Animator animator;
    private bool isPush = false;
    private bool jump = false;
    private bool forceAdded = false;
    private float timeBeforeJump = 0f;
    public GameObject playerSkin;
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
       return Physics.Raycast(transform.position, -Vector3.up, 0.7f);
    }


    // Update is called once per frame
    void Update()
    {
        currentTime = Time.time - initialTime;
        if (Input.GetAxisRaw("Horizontal") > 0.5f && rb.velocity.x < maxSpeed) {
            if (playerSkin.transform.localScale.z < 0)
                playerSkin.transform.localScale = new Vector3(1f, 1f, 1f);
            rb.AddForce(Vector3.right * speed);
        } else if (Input.GetAxisRaw("Horizontal") < -0.5f && rb.velocity.x > -maxSpeed) {
            if (playerSkin.transform.localScale.z > 0)
                playerSkin.transform.localScale = new Vector3(1f, 1f, -1f);
            rb.AddForce(Vector3.left * speed);
        }
        if (Input.GetButtonDown("Jump") && IsGrounded()) {
            jump = true;
        } 
        if (Input.GetButtonDown("Submit") || Input.GetKeyDown(KeyCode.R)) {
            ResetPlayer();
        }
        if (jump) {
            timeBeforeJump += Time.deltaTime;
            if (timeBeforeJump > 0.12f) {
                if (!forceAdded) {
                    rb.AddForce(Vector3.up * jumpSpeed);
                    forceAdded = true;
                }
                if (IsGrounded() && timeBeforeJump > 0.15f) {
                    timeBeforeJump = 0f;
                    jump = false;
                    forceAdded = false;
                }
            }
        }
        animator.SetFloat("Speed", Mathf.Abs(rb.velocity.x));
        animator.SetBool("isGrounded", !jump);
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
        if (sanity < 1) {
            ResetPlayer();
        }
        if (!isInSafeZone && sanity > 0) {
            sanity -= fearLvl / sanityCoef;
        } else if (isInSafeZone && sanity < 100) {
            sanity += 2;
        }
    }

    public void ResetPlayer() {
        Application.LoadLevel (Application.loadedLevel);
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

                if (contact.normal.y > -1.5f  && contact.normal.y < -0.5 || contact.normal.y > 0.5 && contact.normal.y < 1.5f) {
                    isPush = false;
                    return;
                }
                else {
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
