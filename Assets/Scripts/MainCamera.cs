using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainCamera : MonoBehaviour
{
    [HideInInspector] public float width;

    private void Awake()
    {
        width = Camera.main.orthographicSize * 2 * Camera.main.aspect;
    }
}
