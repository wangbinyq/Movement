using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class MovingSphere : MonoBehaviour
{
    [SerializeField, Range(0, 100)]
    float maxSpeed = 10, maxAcceleration = 10;

    Vector3 velocity;

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
        var desiredVelocity = new Vector3(playerInput.x, 0, playerInput.y) * maxSpeed;
        velocity = body.linearVelocity;
        var maxSpeedChange = maxAcceleration * Time.deltaTime;
        velocity.x = Mathf.MoveTowards(velocity.x, desiredVelocity.x, maxSpeedChange);
        velocity.z = Mathf.MoveTowards(velocity.z, desiredVelocity.z, maxSpeedChange);

        body.linearVelocity = velocity;
    }
}
