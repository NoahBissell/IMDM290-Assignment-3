using UnityEngine;

public class TestAudio : MonoBehaviour
{
    public SpectrumTracker spectrum;
    public SpectrumTracker.FrequencyRange range;
    
    private GameObject ball;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        ball = GameObject.CreatePrimitive(PrimitiveType.Sphere);
    }

    // Update is called once per frame
    void Update()
    {
        float volume = spectrum.GetAmpInRange(range);

        ball.transform.localScale = Vector3.one * volume * 100;
    }
}
