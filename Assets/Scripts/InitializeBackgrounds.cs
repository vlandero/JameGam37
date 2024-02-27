using UnityEngine;

public class InitializeBackgrounds : MonoBehaviour
{
    public GameObject movingBackground1;
    public GameObject movingBackground2;
    public GameObject floor1;
    public GameObject floor2;
    public GameObject obstacleBulk;
    public float rollingSpeed = 0.5f;
    private float backgroundSpriteWidth;
    private float floorSpriteWidth;
    private MainCamera mainCamera;

    private void Awake()
    {
        mainCamera = Camera.main.GetComponent<MainCamera>();
        backgroundSpriteWidth = movingBackground1.GetComponent<SpriteRenderer>().bounds.size.x;
        floorSpriteWidth = floor1.GetComponent<SpriteRenderer>().bounds.size.x;

        movingBackground1.transform.position = new Vector3(mainCamera.transform.position.x - mainCamera.width / 2, 0, 0);
        movingBackground2.transform.position = movingBackground1.transform.position + new Vector3(backgroundSpriteWidth, 0, 0);

        floor1.transform.localPosition = new Vector3(mainCamera.transform.position.x - mainCamera.width / 2, -2.8f, 0);
        floor2.transform.localPosition = floor1.transform.localPosition + new Vector3(floorSpriteWidth, 0, 0);
    }

    private void Start()
    {

    }

    private void Update()
    {
        MoveBackgrounds();
        MoveFloors();
        MoveObstacles();
    }

    private void MoveBackgrounds()
    {
        Vector3 movement = Vector2.left * rollingSpeed * Time.deltaTime;
        movingBackground1.transform.position += movement;
        movingBackground2.transform.position += movement;

        if (movingBackground1.transform.position.x + backgroundSpriteWidth < mainCamera.transform.position.x - mainCamera.width / 3)
        {
            movingBackground1.transform.position += new Vector3(backgroundSpriteWidth * 2, 0, 0);
        }

        if (movingBackground2.transform.position.x + backgroundSpriteWidth < mainCamera.transform.position.x - mainCamera.width / 3)
        {
            movingBackground2.transform.position += new Vector3(backgroundSpriteWidth * 2, 0, 0);
        }
    }

    private void MoveFloors()
    {
        Vector3 movement = Vector2.left * rollingSpeed * Time.deltaTime;
        floor1.transform.localPosition += movement;
        floor2.transform.localPosition += movement;

        if (floor1.transform.position.x + floorSpriteWidth < mainCamera.transform.position.x - mainCamera.width / 5)
        {
            floor1.transform.localPosition += new Vector3(floorSpriteWidth * 2, 0, 0);
        }

        if (floor2.transform.position.x + floorSpriteWidth < mainCamera.transform.position.x - mainCamera.width / 5)
        {
            floor2.transform.localPosition += new Vector3(floorSpriteWidth * 2, 0, 0);
        }
    }

    private void MoveObstacles()
    {
        obstacleBulk.transform.position += Vector3.left * rollingSpeed * Time.deltaTime;
    }
}
