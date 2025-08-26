using UnityEngine;

public class MovingSphere : MonoBehaviour
{
    [SerializeField, Range(0, 100)]
    float maxSpeed = 10, maxAcceleration = 10;
    [SerializeField]
    Rect allowedArea = new(-4.5f, -4.5f, 9, 9);
    [SerializeField, Range(0, 1)]
    float bounceness = 0.5f;

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
        if (newPosition.x < allowedArea.xMin || newPosition.x > allowedArea.xMax)
        {
            newPosition.x = Mathf.Clamp(newPosition.x, allowedArea.xMin, allowedArea.xMax);
            velocity.x = -velocity.x * bounceness;

        }
        if (newPosition.z < allowedArea.yMin || newPosition.z > allowedArea.yMax)
        {
            newPosition.z = Mathf.Clamp(newPosition.z, allowedArea.yMin, allowedArea.yMax);
            velocity.z = -velocity.z * bounceness;
        }
        transform.localPosition = newPosition;
    }
}
