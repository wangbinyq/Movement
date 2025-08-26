using UnityEngine;

public class MovingSphere : MonoBehaviour
{
    void Update()
    {
        var playerInput = Vector2.zero;
        playerInput.x = Input.GetAxis("Horizontal");
        playerInput.y = Input.GetAxis("Vertical");
        playerInput.Normalize();
        transform.localPosition = new Vector3(playerInput.x, 0.5f, playerInput.y);
    }
}
