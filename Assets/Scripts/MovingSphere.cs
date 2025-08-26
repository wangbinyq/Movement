using UnityEngine;

public class MovingSphere : MonoBehaviour
{
    [SerializeField, Range(0, 100)]
    float maxSpeed = 10, maxAcceleration = 10;
    [SerializeField]
    Rect allowedArea = new(-5, -5, 10, 10);

    Vector3 velocity;
    void Update()
    {
        var playerInput = Vector2.zero;
        playerInput.x = Input.GetAxis("Horizontal");
        playerInput.y = Input.GetAxis("Vertical");
        playerInput = Vector2.ClampMagnitude(playerInput, 1);
        var desiredVelocity = new Vector3(playerInput.x, 0, playerInput.y) * maxSpeed;
        var maxSpeedChange = maxAcceleration * Time.deltaTime;
        velocity.x = Mathf.MoveTowards(velocity.x, desiredVelocity.x, maxSpeedChange);
        velocity.z = Mathf.MoveTowards(velocity.z, desiredVelocity.z, maxSpeedChange);

        var displacement = Time.deltaTime * velocity;
        var newPosition = transform.localPosition + displacement;
        if (!allowedArea.Contains(new Vector2(newPosition.x, newPosition.z)))
        {
            newPosition = transform.localPosition;
        }
        transform.localPosition = newPosition;
    }
}
