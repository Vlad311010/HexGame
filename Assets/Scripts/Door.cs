using UnityEngine;

public class Door : MonoBehaviour
{
    public bool active { private get;  set; }


    float startHight;
    float minHight;
    float offset = 0;
    float maxOffset;
    public float speed;
    public float offsetMultiplier = 1;

    private void Start()
    {
        active = false;
        startHight = transform.position.y;
        minHight = transform.position.y - transform.localScale.y*2.4f;
        maxOffset = (-transform.localScale.y * 2.4f) * offsetMultiplier;
    }


    void Update()
    {
        if (active)
        {
            if (offset >= maxOffset)
            {
                transform.position += transform.up * -speed * Time.deltaTime;
                offset -= speed * Time.deltaTime;
            }
        }
        else
        {
            if (offset <= 0)
            {
                transform.position += transform.up * speed * Time.deltaTime;
                offset += speed * Time.deltaTime;

            }
        }
    }
}
