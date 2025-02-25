using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioLine : MonoBehaviour
{
    GameObject[] spheres;
    static int numSphere = 200; 
    float time = 0f;
    Vector3[] initPos;
    Vector3[] startPosition;
    Vector3[] endPositionU, endPositionD;
    float lerpFraction; // Lerp point between 0~1
    float t;
    GameObject mother;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        // Assign proper types and sizes to the variables.
        spheres = new GameObject[numSphere];
        initPos = new Vector3[numSphere]; // Start positions
        startPosition = new Vector3[numSphere]; 
        endPositionU = new Vector3[numSphere];
        endPositionD = new Vector3[numSphere];
        mother = GameObject.Find("leaf");
        for (int i =0; i < numSphere; i++){
            // Random start positions
            float r = 10f;
            //startPositionU[i] = new Vector3(r *1f , r * 1f, r * 1f);        
            startPosition[i] = new Vector3(i, 0, 0);   

            //Up and Down end positions
            endPositionU[i] = new Vector3(i,5,0);
            endPositionD[i] = new Vector3(i,-5,0);
        }
        // Let there be spheres..
        for (int i =0; i < numSphere; i++){
            // Draw primitive elements:
            // https://docs.unity3d.com/6000.0/Documentation/ScriptReference/GameObject.CreatePrimitive.html
            //spheres[i] = GameObject.CreatePrimitive(PrimitiveType.Sphere);
            spheres[i] = Instantiate(mother);

            // Position
            initPos[i] = startPosition[i];
            spheres[i].transform.position = initPos[i];
            spheres[i].transform.localRotation = Quaternion.EulerAngles(Random.Range(-180f, 180f), Random.Range(-180f, 180f), Random.Range(-180f, 180f));
            spheres[i].transform.localScale = new Vector3(Random.Range(0.3f, 0.5f), Random.Range(0.3f, 0.5f), Random.Range(0.3f, 0.5f));
            // Color
            // Get the renderer of the spheres and assign colors.
            Renderer sphereRenderer = spheres[i].GetComponent<Renderer>();
            // HSV color space: https://en.wikipedia.org/wiki/HSL_and_HSV
            float hue = (float)i / numSphere; // Hue cycles through 0 to 1
            Color color = Color.HSVToRGB(hue, 1f, 1f); // Full saturation and brightness
            sphereRenderer.material.color = color;
        }
    }
//                    GameObject Sphere = GameObject.CreatePrimitive(PrimitiveType.Sphere);
                    
  //                  Spheres.Add(Sphere); //add spheres to the list
    //                Sphere.GetComponent<MeshRenderer>().material = Colors[z];
    // Update is called once per frame
    void Update()
    {
        
    }
}
