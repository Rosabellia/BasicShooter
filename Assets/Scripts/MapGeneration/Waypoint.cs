using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Waypoint : MonoBehaviour
{
    void Start()
    {
        GameManager.Instance.waypoints.Add(this);
    }

    private void OnDestroy()
    {
        GameManager.Instance.waypointsToAdd.Add(this);
        GameManager.Instance.waypoints.Remove(this);
    }
}
