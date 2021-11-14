using UnityEngine;

public class Spawner : MonoBehaviour
{
    public GameObject obj;
    public float interwal;
    public float zRotation;
    float timer;
    public Mesh cubeMesh;

    void Start()
    {
        timer = 0;
    }

    void Update()
    {
        timer -= Time.deltaTime;
        if (timer < 0)
        {
            timer = interwal;
            Instantiate(obj, transform.position, Quaternion.Euler(0, 0, zRotation), this.gameObject.transform);
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.black;
        Gizmos.DrawWireCube(transform.position, obj.GetComponent<SpriteRenderer>().size);
    }

}
