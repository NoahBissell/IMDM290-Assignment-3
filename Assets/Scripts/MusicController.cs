using System;
using UnityEditor.SceneManagement;
using UnityEngine;

public class MusicController : MonoBehaviour
{
    public static MusicController Instance { get; private set; }

    public float startTime;
    
    public static float MusicTime;
    public static MusicStage Stage;
    public GravityParticles particles;

    public float[] StageStartTimes = new float[]
    {
        0,
        64,
        84,
        133
    };

    
    public enum MusicStage
    {
        Exposition,
        Development,
        Climax,
        Resolution
    }

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this);
            return;
        }

        Instance = this;
    }

    private void Start()
    {
        MusicTime = startTime;
    }
    
    private void Update()
    {
        MusicTime += Time.deltaTime;
        
        MusicStage lastStage = Stage;
        bool justTransitioned = lastStage != Stage;
        Stage = CalculateStage();
        

        if (Stage == MusicStage.Exposition)
        {
            
        }
        else if (Stage == MusicStage.Development)
        {
            if (justTransitioned)
            {
                
            }
        }
        else if (Stage == MusicStage.Climax)
        {
            
        }
        else if(Stage == MusicStage.Resolution)
        {
            
        }
    }

    MusicStage CalculateStage()
    {
        for (int i = 3; i >= 0; i--)
        {
            if (MusicTime >= StageStartTimes[i])
            {
                return (MusicStage)i;
            }
        }

        return MusicStage.Exposition;
    }
}
