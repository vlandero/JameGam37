using UnityEngine;

public class InitializeBackgrounds : MonoBehaviour
{
    public GameObject movingBackground1;
    public GameObject movingBackground2;
    public GameObject floor1;
    public GameObject floor2;
    public GameObject tire1;
    public GameObject tire2;
    public GameObject obstacleBulk;
    public float tireTranslate = 30;
    public float rollingSpeed = 0.5f;
    public float tireRollingSpeed = 0.5f;
    public float currentRollingSpeed;
    public float currentTireRollingSpeed;
    private float backgroundSpriteWidth;
    private float floorSpriteWidth;
    private float tireSpriteWidth;
    private MainCamera mainCamera;

    private void Awake()
    {
        mainCamera = Camera.main.GetComponent<MainCamera>();
        backgroundSpriteWidth = movingBackground1.GetComponent<SpriteRenderer>().bounds.size.x;
        floorSpriteWidth = floor1.GetComponent<SpriteRenderer>().bounds.size.x;
        tireSpriteWidth = tire1.GetComponent<SpriteRenderer>().bounds.size.x;

        movingBackground1.transform.position = new Vector3(mainCamera.transform.position.x - mainCamera.width / 2, 0, 0);
        movingBackground2.transform.position = movingBackground1.transform.position + new Vector3(backgroundSpriteWidth, 0, 0);

        floor1.transform.localPosition = new Vector3(mainCamera.transform.position.x - mainCamera.width / 2, -3.2f, 0);
        floor2.transform.localPosition = floor1.transform.localPosition + new Vector3(floorSpriteWidth, 0, 0);

        tire1.transform.localPosition = new Vector3(mainCamera.transform.position.x - mainCamera.width / 2, 0.15f, 0);
        tire2.transform.localPosition = tire1.transform.localPosition + new Vector3(tireTranslate, 0, 0);

        currentRollingSpeed = rollingSpeed;
        currentTireRollingSpeed = tireRollingSpeed;
    }

    private void Start()
    {

    }

    private void Update()
    {
        MoveBackgrounds();
        MoveFloors();
        MoveObstacles();
        MoveTires();
    }

    private void MoveBackgrounds()
    {
        Vector3 movement = Vector2.left * currentRollingSpeed * Time.deltaTime;
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
        Vector3 movement = Vector2.left * currentRollingSpeed * Time.deltaTime;
        floor1.transform.localPosition += movement;
        floor2.transform.localPosition += movement;

        if (floor1.transform.position.x + floorSpriteWidth < mainCamera.transform.position.x - mainCamera.width/ 10)
        {
            floor1.transform.localPosition += new Vector3(floorSpriteWidth * 2, 0, 0);
        }

        if (floor2.transform.position.x + floorSpriteWidth < mainCamera.transform.position.x - mainCamera.width / 10)
        {
            floor2.transform.localPosition += new Vector3(floorSpriteWidth * 2, 0, 0);
        }
    }

    private void MoveTires()
    {
        Vector3 movement = Vector2.left * currentTireRollingSpeed * Time.deltaTime;
        tire1.transform.position += movement;
        tire2.transform.position += movement;

        if (tire1.transform.position.x + tireSpriteWidth / 2 < mainCamera.transform.position.x - mainCamera.width / 2)
        {
            tire1.transform.localPosition += new Vector3(tireTranslate * 2, 0, 0);
        }
        if (tire2.transform.position.x + tireSpriteWidth / 2 < mainCamera.transform.position.x - mainCamera.width / 2)
        {
            tire2.transform.localPosition += new Vector3(tireTranslate * 2, 0, 0);
        }
    }

    private void MoveObstacles()
    {
        obstacleBulk.transform.position += Vector3.left * currentRollingSpeed * Time.deltaTime;
    }
}
