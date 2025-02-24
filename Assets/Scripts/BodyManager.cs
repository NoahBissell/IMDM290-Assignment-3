using System;
using UnityEngine;

public class BodyManager : MonoBehaviour
{
    public static float GravityConstant = 1;
    private GravityBody[] activeBodies;
    
    private void Start()
    {
        activeBodies = FindObjectsByType<GravityBody>(FindObjectsInactive.Exclude, FindObjectsSortMode.None);

        foreach (var body in activeBodies)
        {
            body.Initialize(activeBodies);
        }
    }

    public GravityBody[] GetBodiesInScene()
    {
        return activeBodies;
    }
    
}
