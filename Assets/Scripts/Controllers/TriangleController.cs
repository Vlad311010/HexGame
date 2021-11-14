using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriangleController : MonoBehaviour
{
    Rigidbody2D rigidbody;
    EdgeCollider2D collider;

    public LayerMask groundLayerMask;
    public LayerMask collectableLayerMask;

    public float normalSpeed;
    public float dashPower;
    float midairSpeed = 5;

    bool moveRight = true;

    float speed;
    bool isGrounded;
    bool canDash = true;
    float dashCD = 1f;

    public GameObject splashEffect;
    float paintCD = 0.29f;
    float paintCDTimer;

    void Start()
    {
        paintCDTimer = paintCD;
        rigidbody = GetComponent<Rigidbody2D>();
        collider = GetComponent<EdgeCollider2D>();
    }


    void FixedUpdate()
    {
        isGrounded = IsGrounded();


        if (isGrounded)
        { speed = normalSpeed; rigidbody.drag = 2; }
        else { speed = midairSpeed; rigidbody.drag = 0.8f; }


        if (Input.GetKey(KeyCode.A))
        {
            rigidbody.AddForce(Vector3.left * speed * rigidbody.mass);
            moveRight = false;
        }
        else if (Input.GetKey(KeyCode.D))
        {
            rigidbody.AddForce(Vector3.right * speed * rigidbody.mass);
            moveRight = true;
        }

        if (Input.GetKeyDown(KeyCode.Space) && canDash)
        {
            Dash(moveRight);
        }

    }


    bool IsGrounded()
    {
        RaycastHit2D groundCheckHit = Physics2D.BoxCast(new Vector2(collider.bounds.center.x, collider.bounds.min.y), new Vector2(collider.bounds.size.x, 0.1f), 0, Vector2.down, 0.3f, groundLayerMask);

        if (groundCheckHit.transform == null)
            return false;
        else return true;
    }

    void Dash(bool right)
    {
        Vector3 dir = right ? Vector3.right : Vector3.left;
        rigidbody.AddForce(dir * dashPower * rigidbody.mass + Vector3.up * 50, ForceMode2D.Impulse);
        canDash = false;
        StartCoroutine(DashCD());
    }

    IEnumerator DashCD()
    {
        yield return new WaitForSeconds(dashCD);
        canDash = true;

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
