using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    // Properties
    [SerializeField] float movementSpeed, acceleration, maxDash, dashCD, dashFalloff;

    private float _dashCD = 0, dash;

    private Rigidbody2D rb;
    private bool _awake = true;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if(_awake)
        {
            Movement();
            rb.velocity = Vector3.Lerp(rb.velocity, targetVelocity, Time.fixedDeltaTime * acceleration);
        }
        else
        {
            rb.velocity = Vector3.Lerp(rb.velocity, Vector3.zero, Time.fixedDeltaTime);
        }
    }

    private void Update()
    {
        // Dash
        if (Input.GetKeyDown(KeyCode.LeftShift) || Input.GetKeyDown(KeyCode.L))
        {
            if (_dashCD <= 0)
            {
                dash = maxDash;
                _dashCD = dashCD;

                Movement();

                rb.velocity = targetVelocity;
            }
        }

        if (dash > 0)
        {
            
            dash = Mathf.Lerp(dash, 0, Time.deltaTime * dashFalloff);
        }
        else
        {
            
            dash = 0;
        }
        _dashCD -= Time.fixedDeltaTime;
    }

    private Vector3 targetVelocity;
    void Movement()
    {
        Vector3 direction = new Vector3(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"), 0).normalized;

        targetVelocity.x = direction.x * (movementSpeed * (1 + dash));
        targetVelocity.y = direction.y * (movementSpeed * (1 + dash));

        
    }
}
