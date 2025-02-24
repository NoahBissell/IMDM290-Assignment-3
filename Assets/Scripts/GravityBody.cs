using System;
using UnityEngine;

public class GravityBody : MonoBehaviour
{
    
    
    private GravityBody[] otherBodies;


    public Vector3 initialVelocity;
    
    public float mass;
    private Vector3 velocity;

    public void Initialize(GravityBody[] others)
    {
        otherBodies = new GravityBody[others.Length - 1];
        int otherBodyIndex = 0;
        for (int i = 0; i < others.Length; i++)
        {
            if (others[i] == this)
            {
                continue;
            }

            otherBodies[otherBodyIndex] = others[i];
            otherBodyIndex++;
        }
    }

    private void Start()
    {
        velocity = initialVelocity;
    }

    private void FixedUpdate()
    {
        for (int i = 0; i < otherBodies.Length; i++)
        {
            GravityBody other = otherBodies[i];

            Vector3 offset = other.transform.position - transform.position;
            float distance = Vector3.Magnitude(offset);
            Vector3 acceleration = BodyManager.GravityConstant * other.mass / Mathf.Pow(distance + 1, 2) * offset;

            velocity += acceleration * Time.fixedDeltaTime;
            transform.position += velocity * Time.fixedDeltaTime;
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawLine(transform.position, transform.position + initialVelocity);
    }
}
