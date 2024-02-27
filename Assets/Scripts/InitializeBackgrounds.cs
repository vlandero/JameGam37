using UnityEngine;

public class InitializeBackgrounds : MonoBehaviour
{
    public GameObject movingBackground1;
    public GameObject movingBackground2;
    public Camera mainCamera;
    private float spriteWidth;
    private float cameraWidth;

    private void Awake()
    {
        cameraWidth = mainCamera.orthographicSize * 2 * mainCamera.aspect;
        spriteWidth = movingBackground1.GetComponent<SpriteRenderer>().bounds.size.x;

        movingBackground1.transform.position = new Vector3(mainCamera.transform.position.x, 0, 0);
        movingBackground2.transform.position = new Vector3(movingBackground1.transform.position.x + spriteWidth - 0.05f, 0, 0);
    }

    private void Update()
    {
        MoveBackground(movingBackground1);
        MoveBackground(movingBackground2);
    }

    private void MoveBackground(GameObject background)
    {
        background.transform.Translate(Vector2.left * Time.deltaTime * background.GetComponent<MovingBackground>().rollingSpeed);
        if (background.transform.position.x + spriteWidth <= mainCamera.transform.position.x - cameraWidth / 2)
        {
            GameObject otherBackground = (background == movingBackground1) ? movingBackground2 : movingBackground1;
            if (otherBackground.transform.position.x < mainCamera.transform.position.x + cameraWidth / 2)
            {
                float newPositionX = otherBackground.transform.position.x + spriteWidth - 0.05f;
                background.transform.position = new Vector3(newPositionX, background.transform.position.y, background.transform.position.z);
            }
        }
    }
}
