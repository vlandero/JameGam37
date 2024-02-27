using UnityEngine;

public class InitializeBackgrounds : MonoBehaviour
{
    public GameObject movingBackground1;
    public GameObject movingBackground2;
    public Camera mainCamera;
    public float rollingSpeed = 0.5f;
    private float spriteWidth;
    private float cameraWidth;

    private void Awake()
    {
        cameraWidth = mainCamera.orthographicSize * 2 * mainCamera.aspect;
        spriteWidth = movingBackground1.GetComponent<SpriteRenderer>().bounds.size.x;

        movingBackground1.transform.position = new Vector3(mainCamera.transform.position.x - cameraWidth / 2, 0, 0);
        movingBackground2.transform.position = movingBackground1.transform.position + new Vector3(spriteWidth, 0, 0);
    }

    private void Update()
    {
        MoveBackgrounds();
    }

    private void MoveBackgrounds()
    {
        Vector3 movement = Vector2.left * rollingSpeed * Time.deltaTime;
        movingBackground1.transform.position += movement;
        movingBackground2.transform.position += movement;

        if (movingBackground1.transform.position.x + spriteWidth < mainCamera.transform.position.x - cameraWidth / 3)
        {
            movingBackground1.transform.position += new Vector3(spriteWidth * 2, 0, 0);
        }

        if (movingBackground2.transform.position.x + spriteWidth < mainCamera.transform.position.x - cameraWidth / 3)
        {
            movingBackground2.transform.position += new Vector3(spriteWidth * 2, 0, 0);
        }
    }
}
