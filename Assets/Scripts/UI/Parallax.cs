using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Parallax : MonoBehaviour
{
    [SerializeField]
    private float speed;

    private float basicSpeed;

    float textureWidth;
    // Start is called before the first frame update
    void Start()
    {
        basicSpeed = speed;
        Sprite sprite = GetComponent<SpriteRenderer>().sprite;
        textureWidth = sprite.texture.width * transform.localScale.x/ sprite.pixelsPerUnit;
        ButtonHighlight.highlightOn += () => this.speed = 0;
        ButtonHighlight.highlightOff += () => this.speed = basicSpeed;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += new Vector3(speed * Time.deltaTime, 0f, 0f);
        if ((Mathf.Abs(transform.position.x) - textureWidth) > 0)
            transform.position = new Vector3(0.0f, transform.position.y, transform.position.z);
    }
}
