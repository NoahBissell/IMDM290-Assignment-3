using UnityEngine;

public class SwirlingTest : MonoBehaviour
{
    public int numParticles;
    public float initialSpeed;
    public float gravityStrength;
    public float spawnRadius;

    private GameObject[] particles;
    private Vector3[] velocities;

    public Transform[] gravityBodies;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        particles = new GameObject[numParticles];
        velocities = new Vector3[numParticles];
        for (int i = 0; i < numParticles; i++)
        {
            particles[i] = GameObject.CreatePrimitive(PrimitiveType.Sphere);
            particles[i].transform.position = Random.insideUnitSphere * spawnRadius;
            velocities[i] = Vector3.Cross(Vector3.up, particles[i].transform.position.normalized) * initialSpeed;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
        
        for (int i = 0; i < numParticles; i++)
        {
            for (int j = 0; j < gravityBodies.Length; j++)
            {
                Vector3 acceleration = (gravityBodies[j].position - particles[i].transform.position).normalized * gravityStrength;

                velocities[i] += acceleration;
            
                particles[i].transform.position += velocities[i] * Time.deltaTime;
            }
            
        }
    }
}
