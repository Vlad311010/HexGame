using Cinemachine;
using UnityEngine;


public class PlayerController : MonoBehaviour
{

    public GameObject square;
    public GameObject circle;
    public GameObject triangle;
    public GameObject deathParticlesSys;
    public CinemachineVirtualCamera camera;

    int currentFigureId;
    GameObject currentFigure;

    Vector3 startPos;

    private void Awake()
    {
        
        startPos = transform.position;
    }

    void Start()
    {
        ChangePlayer(2);
        
    }

    void Update()
    {
        
        Vector2 currentVelocity;

        if (Input.GetKeyDown(KeyCode.H))
        {
            transform.position = currentFigure.transform.position;
            currentVelocity = currentFigure.GetComponent<Rigidbody2D>().velocity;
            Destroy(currentFigure);
            ChangePlayer(1);
            currentFigure.GetComponent<Rigidbody2D>().velocity = currentVelocity;
        }
        if (Input.GetKeyDown(KeyCode.J))
        {
            transform.position = currentFigure.transform.position;
            currentVelocity = currentFigure.GetComponent<Rigidbody2D>().velocity;
            Destroy(currentFigure);
            ChangePlayer(2);
            currentFigure.GetComponent<Rigidbody2D>().velocity = currentVelocity;
        }
        if (Input.GetKeyDown(KeyCode.K))
        {
            transform.position = currentFigure.transform.position;
            currentVelocity = currentFigure.GetComponent<Rigidbody2D>().velocity;
            Destroy(currentFigure);
            ChangePlayer(3);
            currentFigure.GetComponent<Rigidbody2D>().velocity = currentVelocity;
        }
    }


    public void Death()
    {
        
        Destroy(currentFigure);
        transform.position = currentFigure.transform.position;
        Instantiate(deathParticlesSys, transform.position, Quaternion.identity);

        currentFigureId = 2;
        currentFigure = Instantiate<GameObject>(circle, startPos, Quaternion.identity, this.gameObject.transform);

    }

    void ChangePlayer(int figureId)
    {
        currentFigureId = figureId;
        if (figureId == 1)
            currentFigure = Instantiate<GameObject>(square, transform.position, Quaternion.identity, this.gameObject.transform);
        if (figureId == 2)
            currentFigure = Instantiate<GameObject>(circle, transform.position, Quaternion.identity, this.gameObject.transform);
        if (figureId == 3)
            currentFigure = Instantiate<GameObject>(triangle, transform.position, Quaternion.identity, this.gameObject.transform);
        if (camera != null)
        {
            camera.LookAt = currentFigure.transform;
            camera.Follow = currentFigure.transform;
        }
    }

}
