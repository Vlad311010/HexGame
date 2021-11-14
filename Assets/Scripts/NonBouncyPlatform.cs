using UnityEngine;

public class NonBouncyPlatform : MonoBehaviour
{
    public bool horizontal;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (horizontal)
            collision.rigidbody.velocity = new Vector3(collision.rigidbody.velocity.x, 0, 0);
        else 
            collision.rigidbody.velocity = new Vector3(0, collision.rigidbody.velocity.y, 0);
    }
}
