using UnityEngine;
using UnityEngine.Events;

public class AudioThreshold : MonoBehaviour
{
    public ThresholdTrigger[] triggers;

    [System.Serializable]
    public class ThresholdTrigger
    {
        public AudioSpectrum.FrequencyRange frequencyRange;
        public float threshold;
        public UnityEvent<float> OnThreshold;
    }
    
    
    // Update is called once per frame
    void Update()
    {
        for (int tIndex = 0; tIndex < triggers.Length; tIndex++)
        {
            ThresholdTrigger t = triggers[tIndex];

            float amp = AudioSpectrum.GetAmpInRange(t.frequencyRange);
            
            if (amp > t.threshold)
            {
                print("jfkdls;a");
                t.OnThreshold?.Invoke(t.threshold);
            }
        }
        
    }
}
