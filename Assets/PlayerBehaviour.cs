using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBehaviour : MonoBehaviour
{
    [SerializeField] public LayerMask platformLayerMask;
    public float speed;
    private Rigidbody2D rb;
    private CircleCollider2D ourCollider;
    // Start is called before the first frame update
    void Start()
    {
        speed = 10f;
        rb = GetComponent<Rigidbody2D>();
        ourCollider = GetComponent<CircleCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {

        if (Input.GetAxis("Horizontal") != 0)
        {
            float sidemove = Input.GetAxis("Horizontal");

            rb.velocity += new Vector2(sidemove, 0.0f);
            // Max speed
            float speeding = speed - Mathf.Abs(rb.velocity.x);
            if (speeding < 0)
            {
                Vector2 vel = rb.velocity;
                vel[0] = speed;
                if (rb.velocity.x < 0) {
                    vel[0] *= -1;
                }
                rb.velocity = vel;
            }
        }

        if ((Input.GetKeyDown(KeyCode.Space) || Input.GetButtonDown("Jump")) && IsGrounded()) {
            Vector2 jumpvel = new Vector2(0.0f, 10.0f);
            rb.velocity += jumpvel;
        }
    }

    private bool IsGrounded()
    {
        float extraHeightTest = 0.01f;
        RaycastHit2D rayHit = Physics2D.BoxCast(ourCollider.bounds.center, ourCollider.bounds.size, 0f, Vector2.down, extraHeightTest, platformLayerMask);
        Color rayColor;
        if (rayHit.collider != null)
        {
            rayColor = Color.green;
        } else
        {
            rayColor = Color.red;
        }
        Debug.DrawRay(ourCollider.bounds.center, Vector2.down * (ourCollider.bounds.extents.y + extraHeightTest), rayColor);
        Debug.Log("Grounded?" + (rayHit.collider != null));
        return rayHit.collider != null;
    }
}
