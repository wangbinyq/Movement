using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class MovingSphere : MonoBehaviour
{
    [SerializeField, Range(0, 100)]
    float maxSpeed = 10, maxAcceleration = 10, jumpHeight = 2, maxAirAcceleration = 1f;
    [SerializeField, Range(0, 5)]
    int maxAirJumps = 0;


    Vector3 velocity, desiredVelocity;
    bool desiredJump;
    bool onGround;
    int jumpPhase;

    Rigidbody body;
    void Awake()
    {
        body = GetComponent<Rigidbody>();
    }

    void Update()
    {
        var playerInput = Vector2.zero;
        playerInput.x = Input.GetAxis("Horizontal");
        playerInput.y = Input.GetAxis("Vertical");
        playerInput = Vector2.ClampMagnitude(playerInput, 1);
        desiredVelocity = new Vector3(playerInput.x, 0, playerInput.y) * maxSpeed;

        desiredJump |= Input.GetButtonDown("Jump");
    }

    void FixedUpdate()
    {
        UpdateState();
        float acceleration = onGround ? maxAcceleration : maxAirAcceleration;
        var maxSpeedChange = acceleration * Time.fixedDeltaTime;
        velocity.x = Mathf.MoveTowards(velocity.x, desiredVelocity.x, maxSpeedChange);
        velocity.z = Mathf.MoveTowards(velocity.z, desiredVelocity.z, maxSpeedChange);

        if (desiredJump)
        {
            desiredJump = false;
            Jump();
        }

        body.linearVelocity = velocity;
        onGround = false;
    }

    void UpdateState()
    {
        velocity = body.linearVelocity;
        if (onGround)
        {
            jumpPhase = 0;
        }
    }

    void Jump()
    {
        if (onGround || jumpPhase < maxAirJumps)
        {
            jumpPhase += 1;
            float jumpSpeed = Mathf.Sqrt(-2f * Physics.gravity.y * jumpHeight);
            if (velocity.y > 0f)
            {
                jumpSpeed = Mathf.Max(jumpSpeed - velocity.y, 0f);
            }
            velocity.y += jumpSpeed;
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        EvaluateCollision(collision);
    }

    void OnCollisionStay(Collision collision)
    {
        EvaluateCollision(collision);

    }

    void EvaluateCollision(Collision collision)
    {
        for (var i = 0; i < collision.contactCount; i++)
        {
            var normal = collision.GetContact(i).normal;
            onGround |= normal.y >= 0.9f;
        }
    }
}
