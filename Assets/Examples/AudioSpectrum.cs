// Unity Audio Spectrum data analysis
// IMDM Course Material 
// Author: Myungin Lee
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent (typeof(AudioSource))]

public class AudioSpectrum : MonoBehaviour
{
    AudioSource source;
    public static int FFTSIZE = 1024; // https://en.wikipedia.org/wiki/Fast_Fourier_transform
    public static float[] samples;
    public static float audioAmp = 0f;

    public static int maxFrequency;
    public static float invMaxFrequency;


    [System.Serializable]
    public struct FrequencyRange
    {
        public float min;
        public float max;
    }
    
    void Start()
    {
        samples = new float[FFTSIZE];
        source = GetComponent<AudioSource>();
        maxFrequency = AudioSettings.outputSampleRate / 2;
        invMaxFrequency = 1.0f / maxFrequency;
    }
    void Update()
    {
        // The source (time domain) transforms into samples in frequency domain 
        source.GetSpectrumData(samples, 0, FFTWindow.Hanning);
        // Empty first, and pull down the value.
        audioAmp = 0f;
        for (int i = 0; i < FFTSIZE; i++)
        {
            audioAmp += samples[i];
        }
    }

    public static float GetAmpInRange(FrequencyRange range)
    {
        if (range.max > maxFrequency || range.min < 0)
        {
            throw new System.Exception(maxFrequency + " frequency out of range");
        }
        
        float amp = 0;
        int indexStart = (int)(range.min * invMaxFrequency * FFTSIZE);
        int indexEnd = (int)(range.max * invMaxFrequency * FFTSIZE);
        for (int i = indexStart; i < indexEnd; i++)
        {
            amp += samples[i];
        }

        return amp;
    }
}
