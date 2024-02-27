using UnityEngine;

public class InitializeBackgrounds : MonoBehaviour
{
    public GameObject movingBackground1;
    public GameObject movingBackground2;
    public GameObject floor1;
    public GameObject floor2;
    public float rollingSpeed = 0.5f;
    private float backgroundSpriteWidth;
    private float floorSpriteWidth;
    private Camera mainCamera;
    private float cameraWidth;

    private void Awake()
    {
        mainCamera = Camera.main;
        cameraWidth = mainCamera.orthographicSize * 2 * mainCamera.aspect;
        backgroundSpriteWidth = movingBackground1.GetComponent<SpriteRenderer>().bounds.size.x;
        floorSpriteWidth = floor1.GetComponent<SpriteRenderer>().bounds.size.x;

        movingBackground1.transform.position = new Vector3(mainCamera.transform.position.x - cameraWidth / 2, 0, 0);
        movingBackground2.transform.position = movingBackground1.transform.position + new Vector3(backgroundSpriteWidth, 0, 0);

        floor1.transform.position = new Vector3(mainCamera.transform.position.x - cameraWidth / 2, -2.8f, 0);
        floor2.transform.position = floor1.transform.position + new Vector3(floorSpriteWidth, 0, 0);
    }

    private void Start()
    {
    }

    private void Update()
    {
        MoveBackgrounds();
        MoveFloors();
    }

    private void MoveBackgrounds()
    {
        Vector3 movement = Vector2.left * rollingSpeed * Time.deltaTime;
        movingBackground1.transform.position += movement;
        movingBackground2.transform.position += movement;

        if (movingBackground1.transform.position.x + backgroundSpriteWidth < mainCamera.transform.position.x - cameraWidth / 3)
        {
            movingBackground1.transform.position += new Vector3(backgroundSpriteWidth * 2, 0, 0);
        }

        if (movingBackground2.transform.position.x + backgroundSpriteWidth < mainCamera.transform.position.x - cameraWidth / 3)
        {
            movingBackground2.transform.position += new Vector3(backgroundSpriteWidth * 2, 0, 0);
        }
    }

    private void MoveFloors()
    {
        Vector3 movement = Vector2.left * rollingSpeed * Time.deltaTime;
        floor1.transform.position += movement;
        floor2.transform.position += movement;

        if (floor1.transform.position.x + floorSpriteWidth < mainCamera.transform.position.x - cameraWidth / 5)
        {
            floor1.transform.position += new Vector3(floorSpriteWidth * 2, 0, 0);
        }

        if (floor2.transform.position.x + floorSpriteWidth < mainCamera.transform.position.x - cameraWidth / 5)
        {
            floor2.transform.position += new Vector3(floorSpriteWidth * 2, 0, 0);
        }
    }
}
