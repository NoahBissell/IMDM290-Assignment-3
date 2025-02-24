using System;
using TMPro;
using UnityEngine;

public class SpectrumVisualizer : MonoBehaviour
{
    public SpectrumTracker.FrequencyRange range;
    public SpectrumTracker spectrum;
    public TextMeshProUGUI text;

    private Transform[] cubes;

    public float width;

    public float scale;
    public float padding;
    
    private void Start()
    {
        cubes = new Transform[SpectrumTracker.FFTSIZE];
        
        for (int i = 0; i < cubes.Length; i++)
        {
            var cube = GameObject.CreatePrimitive(PrimitiveType.Cube);
            cube.transform.position =
                Vector3.Lerp(Vector3.left * (width - padding), Vector3.right * (width - padding), i / (float)(cubes.Length - 1));
            cube.transform.localScale = new Vector3(width / SpectrumTracker.FFTSIZE, 1, 1);
            
            cubes[i] = cube.transform;
        }
    }

    private void Update()
    {
        float ampInRange = 0;
        int indexStart = (int)(range.min * SpectrumTracker.invMaxFrequency * SpectrumTracker.FFTSIZE);
        int indexEnd = (int)(range.max * SpectrumTracker.invMaxFrequency * SpectrumTracker.FFTSIZE);
        for (int i = 0; i < SpectrumTracker.FFTSIZE; i++)
        {
            float sample = 0;
            Vector3 currentScale = cubes[i].localScale;
            if (i >= indexStart && i < indexEnd)
            {
                sample = spectrum.samples[i];
                ampInRange += sample;
            }
            cubes[i].localScale = new Vector3(currentScale.x, scale * sample, currentScale.z);
        }
        text.text = "Total amp in range: " + ampInRange;
    }
}
