using UnityEngine;

public class MovingSphere : MonoBehaviour
{
    [SerializeField]
    float maxSpeed = 10;

    Vector3 velocity;
    void Update()
    {
        var playerInput = Vector2.zero;
        playerInput.x = Input.GetAxis("Horizontal");
        playerInput.y = Input.GetAxis("Vertical");
        playerInput = Vector2.ClampMagnitude(playerInput, 1);
        var acceleration = new Vector3(playerInput.x, 0, playerInput.y) * maxSpeed;
        velocity += acceleration * Time.deltaTime;
        var displacement = Time.deltaTime * velocity;
        transform.localPosition += displacement;
    }
}
