using System.Collections;

using UnityEngine;
using UnityEngine.Rendering;

public class SquareController : MonoBehaviour
{
    Rigidbody2D rigidbody;
    AudioSource audio;

    public float speed;
    public float jumpHeight;

    float rotationChangeHight = 5f;
    bool isGrounded;
    bool movementAllowed = true;


    //Refs
    public BoxCollider2D boxCollider;
    public LayerMask groundLayerMask;
    public SpriteRenderer sprite;
   
    
    public GameObject splashEffect;
    //float splashTimer = 1f;
    float paintCD = 0.25f;
    float paintCDTimer;

    void Start()
    {
        paintCDTimer = paintCD;
        rigidbody = GetComponent<Rigidbody2D>();
        audio = GetComponent<AudioSource>();
    }

    void Update()
    {
        isGrounded = IsGrounded();

        //rotation
        Quaternion newRotation = GetPlatformRotation();
        if (newRotation != transform.rotation)
            transform.rotation = Quaternion.RotateTowards(transform.rotation, newRotation, 0.45f);


        if (isGrounded)
        {
            rigidbody.drag = 1;
            movementAllowed = true;
        }
        else { rigidbody.drag = 0; StartCoroutine(BlockMovement()); }


        if (Input.GetKey(KeyCode.A) && movementAllowed)
        {
            rigidbody.AddForce(Vector3.left * speed * rigidbody.mass);
        }
        if (Input.GetKey(KeyCode.D) && movementAllowed)
        {
            rigidbody.AddForce(Vector3.right * speed * rigidbody.mass);
        }

        if (Input.GetKeyDown(KeyCode.Space) && isGrounded) // if not grounded linear drag = 0;
        {
            audio.Play();
            rigidbody.AddForce(new Vector2(0, Mathf.Sqrt(-2.0f * Physics2D.gravity.y * jumpHeight * rigidbody.mass)), ForceMode2D.Impulse);
        }

    }

    public bool IsGrounded()
    {
        RaycastHit2D groundCheckHit = Physics2D.BoxCast( new Vector2(boxCollider.bounds.center.x, boxCollider.bounds.min.y), new Vector2(boxCollider.bounds.size.x, 0.1f), 0, Vector2.down, 0.23f, groundLayerMask);
        if (groundCheckHit.transform == null)
            return false;
        else return true;
    }

    IEnumerator BlockMovement()
    {
        yield return new WaitForSeconds(0.6f);
        movementAllowed = false;
        StopCoroutine(BlockMovement());
    }

    public void CreateSplash()
    {
        Instantiate(splashEffect, new Vector3(boxCollider.bounds.center.x, boxCollider.bounds.min.y - 1), transform.rotation);
    }

    public void CreateSplash(Quaternion rotation, Vector3 dir, Vector3 contactPoint)
    {
        Instantiate(splashEffect, contactPoint + dir * splashEffect.transform.localScale.x, rotation);
    }

    IEnumerator PaintCDTImer()
    {
        yield return new WaitForEndOfFrame();
        paintCDTimer -= 0.1f;
    }

    //Returns platform's z-axis euler rotation
    public Quaternion GetPlatformRotation()
    {
        RaycastHit2D groundCheckHit = Physics2D.BoxCast(new Vector2(boxCollider.bounds.center.x, boxCollider.bounds.min.y), new Vector2(boxCollider.bounds.size.x, 0.1f), 0, Vector2.down, rotationChangeHight, groundLayerMask);
        if (groundCheckHit.transform != null)
            return groundCheckHit.transform.rotation;
        else return Quaternion.identity;
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        StartCoroutine(PaintCDTImer());
        if (collision.gameObject.layer.Equals(LayerMask.NameToLayer("Ground")) && paintCDTimer <= 0)
        {
            Vector3 normal = -collision.contacts[0].normal;
            Vector3 averageContactPoint = AverageContactPoint(collision.contacts);
            float zRotationObj = collision.gameObject.transform.rotation.eulerAngles.z;
            float zRotation = zRotationObj;
            if (normal.x + normal.y > 0)
            {
                if (normal.x > normal.y)
                    zRotation += 90f;
                else
                    zRotation += 180f;
            }
            else
            {
                if (normal.x < normal.y)
                    zRotation += 270f;
            }
            paintCDTimer = paintCD;

            CreateSplash(Quaternion.Euler(0, 0, zRotation), normal, averageContactPoint);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
       
        if (collision.gameObject.layer.Equals(LayerMask.NameToLayer("Ground")))
        {
            if (collision.relativeVelocity.magnitude > 15f)
                audio.volume = 0.05f * collision.relativeVelocity.magnitude;
                audio.Play();

            Vector3 normal = -collision.contacts[0].normal;
            Vector3 averageContactPoint = AverageContactPoint(collision.contacts);
            float zRotationObj = collision.gameObject.transform.rotation.eulerAngles.z;
            float zRotation = zRotationObj;
            if (normal.x + normal.y > 0)
            {
                if (normal.x > normal.y)
                    //Debug.Log("RIGHT");
                    zRotation += 90f;
                else
                    //Debug.Log("UP");
                    zRotation += 180f;
            }
            else
            {
                if (normal.x < normal.y)
                    zRotation += 270f;
                //else
                //{ Debug.Log("DOWN"); }
            }
            
            CreateSplash(Quaternion.Euler(0, 0, zRotation), normal, averageContactPoint);
        }
    }

    Vector3 AverageContactPoint(ContactPoint2D[] points)
    {
        float x = 0;
        float y = 0;
        foreach (ContactPoint2D i in points)
        {
            x += i.point.x;
            y += i.point.y;
        }
        return new Vector3(x, y, 0) / points.Length;
    }

}
