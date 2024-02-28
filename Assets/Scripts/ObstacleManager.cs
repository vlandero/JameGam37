using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleManager : MonoBehaviour
{
    [HideInInspector] public Obstacle[] obstacles;
    void Start()
    {
        obstacles = FindObjectsOfType<Obstacle>();
        Debug.Log(obstacles.Length);
    }

    public void SwitchReality()
    {
        foreach(Obstacle obstacle in obstacles)
        {
            obstacle.SwitchReality();
        }
    }
}
