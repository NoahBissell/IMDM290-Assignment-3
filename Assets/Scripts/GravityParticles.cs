using System;
using UnityEngine;
using Random = UnityEngine.Random;

public class GravityParticles : MonoBehaviour
{
    public bool defaultSpawnStart;
    public int numParticles;
    public float initialSpeed;
    public Vector3 initialDir;
    public float gravityConstant;
    
    public float spawnRadius;

    private GameObject[] particles;
    private Vector3[] velocities;

    public float spawnSphereRadius;
    
    public GravityBody[] gravityBodies;
    private float[] bodyNormalizedMasses;

    public GameObject prefab;


    public delegate Vector3 PositionFunction(float spawnRadius);

    public delegate Vector3 InitialVelocityFunction(Vector3 pos);

    private void Start()
    {
        if (defaultSpawnStart)
        {
            Spawn(numParticles, spawnRadius, initialSpeed, 
                (rad) => Random.insideUnitSphere * spawnSphereRadius, 
                (pos) => initialDir);
        }

        CalculateNormalizedMasses();
    }

    public void CalculateNormalizedMasses()
    {
        float maxMass = float.NegativeInfinity;
        bodyNormalizedMasses = new float[gravityBodies.Length];
        for (int i = 0; i < gravityBodies.Length; i++)
        {
            if (gravityBodies[i].mass > maxMass)
            {
                maxMass = gravityBodies[i].mass;
            }
        }

        for (int i = 0; i < gravityBodies.Length; i++)
        {
            bodyNormalizedMasses[i] = gravityBodies[i].mass / maxMass;
        }
    }

    public void SetSpawnerPosition(Vector3 p)
    {
        transform.position = p;
    }

    public void DeleteParticles()
    {
        for (int i = 0; i < particles.Length; i++)
        {
            Destroy(particles[i].gameObject);
        }

        numParticles = 0;
        particles = Array.Empty<GameObject>();
        velocities = Array.Empty<Vector3>();
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public void Spawn(int numParticles, float spawnRadius, float speed, PositionFunction posFunc, InitialVelocityFunction velFunc)
    {
        this.numParticles = numParticles;
        
        particles = new GameObject[numParticles];
        velocities = new Vector3[numParticles];
        for (int i = 0; i < numParticles; i++)
        {
            particles[i] = Instantiate(prefab);
            particles[i].transform.SetParent(this.transform);
            Vector3 offsetPos = posFunc(spawnRadius);
            particles[i].transform.localPosition = offsetPos;
            
            velocities[i] = velFunc(offsetPos) * speed;
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        
        
        for (int i = 0; i < numParticles; i++)
        {
            for (int j = 0; j < gravityBodies.Length; j++)
            {
                Vector3 offset = gravityBodies[j].transform.position - particles[i].transform.position;
                
                float distance = Vector3.Magnitude(offset);
                
                Vector3 acceleration = offset / distance * (gravityConstant * bodyNormalizedMasses[j]) / Mathf.Pow(distance + 1, 2);

                velocities[i] += acceleration * Time.fixedDeltaTime;
                

                particles[i].transform.position += velocities[i] * Time.fixedDeltaTime;
            }
            
        }
    }
}
