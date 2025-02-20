using System;
using UnityEngine;

public class CameraShake : MonoBehaviour
{
    public float amplitude;
    public float frequency;
    public float duration;

    private float shakeTime;

    private Vector3 prevOffset;

    public void Shake()
    {
        shakeTime = duration;
    }

    private void Update()
    {
        if (shakeTime <= 0) return;

        Vector3 noise = new Vector3(Mathf.PerlinNoise1D(Time.time * frequency), 
            Mathf.PerlinNoise1D(Time.time * frequency + 100), 
            Mathf.PerlinNoise1D(Time.time * frequency + 200));

        float envelope = Mathf.Sin(shakeTime / duration * Mathf.PI);
        Vector3 offset = noise * envelope * amplitude;
        Vector3 delta = offset - prevOffset;

        transform.position += delta * Time.deltaTime;
        prevOffset = offset;
        shakeTime -= Time.deltaTime;
        
        
    }
}
