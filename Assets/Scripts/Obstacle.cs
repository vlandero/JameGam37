using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Obstacle : MonoBehaviour
{
    [SerializeField] protected bool isActive;

    protected MainCamera mainCamera;
    protected bool isOnScreen;

    protected BoxCollider2D boxCollider;
    protected SpriteRenderer spriteRenderer;

    protected virtual void Start()
    {
        mainCamera = Camera.main.GetComponent<MainCamera>();
        boxCollider = GetComponent<BoxCollider2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        isOnScreen = false;
        Deactivate();
        // we don't need to activate here because at the first update frame it will enter the screen area and it will activate if necessary
    }

    protected virtual void Update()
    {
        if(!isOnScreen && transform.position.x < mainCamera.transform.position.x + mainCamera.width / 2)
        {
            EnteredScreenArea();
        }
    }

    public bool IsActive()
    {
        return isActive;
    }

    public void SwitchReality()
    {
        SetActive(!isActive);
    }

    public void SetActive(bool active)
    {
        isActive = active;
        if(isActive && isOnScreen)
        {
            Activate();
        }
        else
        {
            Deactivate();
        }
    }

    protected void EnteredScreenArea()
    {
        isOnScreen = true;
        if(isActive)
        {
            Activate();
        }
    }

    virtual public void Deactivate()
    {
        var color = spriteRenderer.color;
        spriteRenderer.color = new Color(color.r, color.g, color.b, .25f);
        boxCollider.enabled = false;
    }

    virtual public void Activate()
    {
        var color = spriteRenderer.color;
        spriteRenderer.color = new Color(color.r, color.g, color.b, 1);
        boxCollider.enabled = true;
    }
}
