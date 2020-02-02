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
    public GameObject GlowStick;
    private Rigidbody rb;
    private Vector3 initialPosition;
    private float initialTime;
    private float currentTime;

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
        if (Input.GetButtonDown("Jump")) {
            rb.AddForce(Vector3.up * jumpSpeed);
        } 
        if (Input.GetButtonDown("Submit") || Input.GetKeyDown(KeyCode.R)) {
            ResetPlayer();
        }
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
        }
    }
}
