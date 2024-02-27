using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Obstacle : MonoBehaviour
{
    protected MainCamera mainCamera;
    protected bool isActive;
    protected bool isOnScreen;

    protected void Start()
    {
        mainCamera = Camera.main.GetComponent<MainCamera>();
        isActive = false;
        isOnScreen = false;
    }

    protected void Update()
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

    public void SetActive(bool active)
    {
        isActive = active;
        if(isActive)
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
        Debug.Log("EnteredScreenArea");
    }

    virtual protected void Deactivate()
    { 
    
    }

    virtual protected void Activate()
    {

    }
}
