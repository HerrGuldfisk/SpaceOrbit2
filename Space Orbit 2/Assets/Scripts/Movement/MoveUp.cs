//place on a gameobject with a rigidbody2D to make to move upwards at a constant speed

using UnityEngine;

public class MoveUp : MonoBehaviour
{
    [SerializeField] float speed = 1.0f; // Speed at which the object moves forward
    private Rigidbody2D rb; // Reference to the Rigidbody2D component

    // Start is called before the first frame update
    void Start()
    {
        // Get the Rigidbody2D component attached to the GameObject
        rb = GetComponent<Rigidbody2D>();

        if (rb == null)
        {
            Debug.LogError("Rigidbody2D component not found on the GameObject");
        }
    }

    // FixedUpdate is called once per fixed timestep
    void FixedUpdate()
    {
        if (rb != null)
        {
            // Move the object forward
            Vector2 newPosition = rb.position + Vector2.up * speed * Time.fixedDeltaTime;
            rb.MovePosition(newPosition);
        }
    }
}
