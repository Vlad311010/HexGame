using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class CircleController : MonoBehaviour
{
    Rigidbody2D rigidbody;
    CircleCollider2D collider;

    public LayerMask groundLayerMask;

    public float speed;
    float midairSpeed;

    public GameObject splashEffect;
    float paintCD = 0.2f;
    float paintCDTimer;

    void Start()
    {
        paintCDTimer = paintCD;
        rigidbody = GetComponent<Rigidbody2D>();
        collider = GetComponent<CircleCollider2D>();
        midairSpeed = speed / 2.5f;
    }

    void FixedUpdate()
    {
        bool isGrounded = IsGrounded();
        float currentSpeed;

        if (isGrounded)
            currentSpeed = speed;
        else
            currentSpeed = midairSpeed;
        if (Input.GetKey(KeyCode.A))
        {
            rigidbody.AddForce(Vector3.left * currentSpeed * rigidbody.mass);
        }
        if (Input.GetKey(KeyCode.D))
        {
            rigidbody.AddForce(Vector3.right * currentSpeed * rigidbody.mass);
        }

    }

    public bool IsGrounded()
    {
        RaycastHit2D groundCheckHit = Physics2D.CircleCast(collider.bounds.center, collider.bounds.extents.x, Vector2.down, 0.2f, groundLayerMask);
        if (groundCheckHit.transform == null)
            return false;
        else return true;
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
