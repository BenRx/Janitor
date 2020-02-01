using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTestController : MonoBehaviour
{

    private int layer;
    public float distance = 10f;
    private static PlayerTestController _instance;
    public static PlayerTestController Instance { get {return _instance; } }
    public float sanityCoefDown = 0.3f;
    public float TimeBeforeFearMax = 60f;
    public float sanity = 100f;
    public float sanityMax = 200f;
    public float sanityGain = 0f;
    public float fearLvl = 0;
    public float fearCoef = 4;
    private Rigidbody rb;

    public float maxSpeed = 20f;
    public float speed = 10f;
    public float jumpSpeed = 100f;
    public GameObject GlowStick;
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
        TimeBeforeFearMax -= Time.deltaTime;
        if (TimeBeforeFearMax < 0.5)
            TimeBeforeFearMax = 0.5f;
        sanity -= sanityCoefDown + Time.deltaTime + (1/TimeBeforeFearMax);
        sanity += sanityGain;
        if (sanity > sanityMax) {
            sanity = sanityMax;
        } else if (sanity < 0) {
            // TODO : DEAD :D
            //return;
        }

        fearManagement();
    }

    void fearManagement() {
        if (fearLvl < 100) {
            fearLvl = Time.time / fearCoef;
        }
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
            if (Input.GetAxisRaw("Horizontal") > 0.5f && rb.velocity.x < maxSpeed) {
                rb.AddForce(Vector3.right * speed);
            } else if (Input.GetAxisRaw("Horizontal") < -0.5f && rb.velocity.x < maxSpeed) {
                rb.AddForce(Vector3.left * speed);
            }
        }
    }
}
