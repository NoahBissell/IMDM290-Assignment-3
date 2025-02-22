using UnityEngine;

public class Lightpulser : MonoBehaviour
{
    public Material material;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        material.color = Color.white * Mathf.Sin(Time.time);
    }
}
