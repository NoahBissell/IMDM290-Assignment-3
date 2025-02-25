using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(SpectrumTracker))]
public class AudioThreshold : MonoBehaviour
{
    public List<ThresholdTrigger> triggers;
    SpectrumTracker spectrum;

    [System.Serializable]
    public class ThresholdTrigger
    {
        public SpectrumTracker.FrequencyRange frequencyRange;
        public float smoothing;
        public float threshold;
        public UnityEvent<float> OnThreshold;

        [HideInInspector] public float currentAmp;
        [HideInInspector] public float velocity;
    }

    private void Start()
    {
        spectrum = GetComponent<SpectrumTracker>();
    }

    // Update is called once per frame
    void Update()
    {
        for (int tIndex = 0; tIndex < triggers.Count; tIndex++)
        {
            ThresholdTrigger t = triggers[tIndex];

            float amp = spectrum.GetAmpInRangeSmooth(t.frequencyRange, t.currentAmp, ref t.velocity, t.smoothing);
            
            if (amp > t.threshold && t.currentAmp < t.threshold)
            {
                //print("?????:");
                t.OnThreshold?.Invoke(amp - t.threshold);
            }

            t.currentAmp = amp;
        }
        
    }
}
