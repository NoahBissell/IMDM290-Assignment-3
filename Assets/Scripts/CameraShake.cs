using System;
using UnityEngine;

public class CameraShake : MonoBehaviour
{
    public float amplitude;
    public float frequency;
    public float duration;

    public float falloff;
    
    private float shakeTime;

    private Vector3 prevOffset;

    
    public void Shake()
    {
        shakeTime = duration;
    }

    private void Update()
    {
        if (shakeTime <= 0) return;
        
        Vector3 noise = new Vector3(Mathf.PerlinNoise1D(Time.time * frequency) * 2 - 1, 
            Mathf.PerlinNoise1D(Time.time * frequency + 1000) * 2 - 1, 
            Mathf.PerlinNoise1D(Time.time * frequency + 2000) * 2 - 1);

        float t = 1 - shakeTime / duration;
        //float envelope = Mathf.Sin(t * Mathf.PI);
        float envelope = Mathf.Exp(-t * falloff) * (1 - t);
        Vector3 offset = noise * (envelope * amplitude);
        Vector3 delta = offset - prevOffset;

        transform.position += delta * Time.deltaTime;
        prevOffset = offset;
        shakeTime -= Time.deltaTime;
        
        
    }
}
