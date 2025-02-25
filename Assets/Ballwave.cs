using System.Collections.Generic;
using UnityEngine;

public class Ballwave : MonoBehaviour
{
    public SpectrumTracker spectrum;
    public SpectrumTracker.FrequencyRange sirenRange;
    public float delay = 0.25f; //faked data
    public float timer = 0;
    public float waveSpeed = 0.1f;
    public List<GameObject> Spheres = new List<GameObject>();
    public List<Material> Colors = new List<Material>();

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        for(int z = 0; z < 20; z++)

        {
            for (int x = 0; x < 30; x++)
                {
                    GameObject Sphere = GameObject.CreatePrimitive(PrimitiveType.Sphere);
                    Sphere.transform.position = new Vector3(x, 0, z);
                    Spheres.Add(Sphere); //add spheres to the list
                    Sphere.GetComponent<MeshRenderer>().material = Colors[z];
                }
            }
       

    }

    // Update is called once per frame
    void Update()
    {
        float volume = spectrum.GetAmpInRange(sirenRange);
        //if(Time.time > timer)
        {
            timer = Time.time + delay; //changing the delay changes the beat
            for(int i = 0; i < 50; i++)
            {
                Vector3 pos = Spheres[i].transform.position;
                // pos.y = Mathf.Sin(i * volume * waveSpeed);
                pos.y = Mathf.Sin(Time.time - (i * volume));
                //pos.y = Mathf.Sin(volume);
                Spheres[i].transform.position = pos;
            }
        }
    }
}
