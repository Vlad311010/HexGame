using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class BreakGlass : MonoBehaviour
{
    BoxCollider2D collider;
    public GameObject glassShard;
    public bool isKinematic;

    Rigidbody2D rigidbody;
    public Transform SpawnPos;
    float forceToBreake = 33f;

    float shardW;
    float shardH;

    int partsW;
    int partsH;

    Vector2 startSpawnPos;
    Vector2 dir;
    Vector2 perpen;

    void Start()
    {
        collider = GetComponent<BoxCollider2D>();
        rigidbody = GetComponent<Rigidbody2D>();

        shardW = glassShard.transform.localScale.x;
        shardH = glassShard.transform.localScale.y;

        partsW = (int)(transform.localScale.x / shardW);
        partsH = (int)(transform.localScale.y / shardH);

        startSpawnPos = SpawnPos.position;
        dir = (Quaternion.AngleAxis(transform.eulerAngles.z - 90f, Vector3.forward) * Vector3.right).normalized;
        perpen = Vector2.Perpendicular(dir);


    }

    void Update()
    {

    }

    public void Break()
    {
        for (int h = 0; h < partsH; h++)
        {
            InstantiateRow(h);
        }

        Destroy(this.gameObject);

        //Debug.Log("SHARD SIZES H|W:" + shardH + "|" + shardW);
        //Debug.Log("SHARD Amount H|W:" + partsH + "|" + partsW);
    }

    void InstantiateRow(int rowId)
    {
        for (int w = 0; w < partsW + 1; w++)
        {
            Instantiate(glassShard, (startSpawnPos + new Vector2(shardW, shardH) * dir) + dir * rowId * shardH * 2.4f - perpen * w * shardW * 2.4f, transform.rotation);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {

        //Debug.Log(collision.relativeVelocity.magnitude);
        if (collision.gameObject.CompareTag("Triangle") && collision.relativeVelocity.magnitude > forceToBreake)
        {
            Break();
        }
    }
}
