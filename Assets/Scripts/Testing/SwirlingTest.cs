using UnityEngine;

public class SwirlingTest : MonoBehaviour
{
    public int numParticles;
    public float initialSpeed;
    
    public float spawnRadius;

    private GameObject[] particles;
    private Vector3[] velocities;

    public GravityBody[] gravityBodies;

    public GameObject prefab;

    [System.Serializable]
    public class GravityBody
    {
        public Transform transform;
        public float mass;
    }
    
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        particles = new GameObject[numParticles];
        velocities = new Vector3[numParticles];
        for (int i = 0; i < numParticles; i++)
        {
            particles[i] = Instantiate(prefab);
            particles[i].transform.position = Random.insideUnitCircle.normalized * spawnRadius;
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
                Vector3 offset = gravityBodies[j].transform.position - particles[i].transform.position;
                float sqrDistance = Vector3.SqrMagnitude(offset);
                
                Vector3 acceleration = offset.normalized * gravityBodies[j].mass / sqrDistance;
                
                velocities[i] += acceleration;
            
                particles[i].transform.position += velocities[i] * Time.deltaTime;
            }
            
        }
    }
}
