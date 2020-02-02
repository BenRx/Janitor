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
    public bool isInSafeZone = true;
    public float maxSpeed = 20f;
    public float speed = 10f;
    public float jumpSpeed = 100f;
    public GameObject GlowStick;
    private Rigidbody rb;
    private Vector3 initialPosition;

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
        initialPosition = transform.prosition;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetAxisRaw("Horizontal") > 0.5f && rb.velocity.x < maxSpeed) {
            rb.AddForce(Vector3.right * speed);
        } else if (Input.GetAxisRaw("Horizontal") < -0.5f && rb.velocity.x < maxSpeed) {
            rb.AddForce(Vector3.left * speed);
        }
        if (Input.GetButtonDown("Jump")) {
            rb.AddForce(Vector3.up * jumpSpeed);
        }
        ManageSanity();
        ManageFear();
    }

    void ManageFear() {
        if (fearLvl < 100) {
            fearLvl = Time.time / fearCoef;
        }
    }

    void ManageSanity() {
        if (!isInSafeZone && sanity > 0) {
            sanity -= fearLvl / 2;
        }
    }

    void ResetPlayer() {
        transform.prosition = initialPosition;
        sanity = 100f;
        fearLvl = 0;
    }

    public bool PickMe(GameObject gm) {
        if (Input.GetButtonDown("Fire1")) {
            gm.transform.position = new Vector3(GlowStick.transform.position.x + 0.3f, GlowStick.transform.position.y, GlowStick.transform.position.z);
            gm.transform.parent = GlowStick.transform;
            return true;
        } else if (Input.GetButtonDown("Submit")) {
            ResetPlayer();
        }
        return false;
    }

    private void OnCollisionStay(Collision other) {
        if (other.gameObject.tag == "Case") {
            Debug.Log("wut");
            if (Input.GetAxisRaw("Horizontal") > 0.5f ) {
                Debug.Log("ayaya");
                rb.AddForce(Vector3.right * speed * 2f);
            } else if (Input.GetAxisRaw("Horizontal") < -0.5f ) {
                rb.AddForce(Vector3.left * speed * 2f);
            }
        }
    }
}
