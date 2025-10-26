using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float walkSpeed = 4f;
    [SerializeField] private float runSpeed = 7f;
    [SerializeField] private float diagonalSpeed = 1.4f;
    [SerializeField] private float smoothTime = 0.1f;
    private float speed;
    [HideInInspector]
    public float speedDebuff;
    [Header("Abilities")]
    [SerializeField] private float dashForce = 100f;
    [SerializeField] private float dashCooldown = 1f;
    private bool canDash = true;

    private Rigidbody2D rb;
    private Vector2 movement;
    private Vector2 velocity = Vector2.zero;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        // input keys stored in movement vector
        movement = new Vector2();
        if (Input.GetKey(KeyCode.W)) movement.y += 1;
        if (Input.GetKey(KeyCode.S)) movement.y -= 1;
        if (Input.GetKey(KeyCode.A)) movement.x -= 1;
        if (Input.GetKey(KeyCode.D)) movement.x += 1;

        // make the vector normalized (only direction, value=1) to prevent faster diagonal movement
        if (movement.magnitude > 1)
        {
            movement = movement.normalized * diagonalSpeed;
        }
        if(Input.GetKeyUp(KeyCode.Space) && canDash)
        {
            Dash();
        }
    }

    // fix update for physics calculations :tuzar:
    void FixedUpdate()
    {
        // if sprinting, use run speed, else use walk speed, apply speed debuff
        speed = Input.GetKey(KeyCode.LeftShift) ? runSpeed / (1 + speedDebuff) : walkSpeed / (1 + speedDebuff);

        // smooth movement
        rb.velocity = Vector2.SmoothDamp(rb.velocity, movement * speed, ref velocity, smoothTime);
    }

    void Dash()
    {
        rb.AddForce(movement.normalized* speed * dashForce, ForceMode2D.Impulse);
        StartCoroutine(DashCooldown());
    }

    IEnumerator DashCooldown()
    {
        canDash = false;
        //float originalSmoothTime = smoothTime;
        //smoothTime = 0.05f;
        yield return new WaitForSeconds(dashCooldown);
        //smoothTime = originalSmoothTime;
        canDash = true;
    }


}
