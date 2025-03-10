using UnityEngine;

public class Lightpulser : MonoBehaviour
{
    public Material material;
    public SpectrumTracker spectrum;
    public SpectrumTracker.FrequencyRange dodoRange;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        float volume = spectrum.GetAmpInRange(dodoRange);
        material.color = Color.white * Mathf.Sin(100 * volume);
    }
}
