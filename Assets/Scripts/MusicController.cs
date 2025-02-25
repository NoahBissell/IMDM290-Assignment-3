using System;
using UnityEditor.SceneManagement;
using UnityEngine;
using Random = UnityEngine.Random;

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
        65,
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
    
    private void FixedUpdate()
    {
        MusicTime += Time.fixedDeltaTime;
        
        MusicStage lastStage = Stage;
        Stage = CalculateStage();
        bool justTransitioned = lastStage != Stage;

        if (Stage == MusicStage.Exposition)
        {
            
        }
        else if (Stage == MusicStage.Development)
        {
            if (justTransitioned)
            {
                particles.DeleteParticles();
                particles.transform.position = Vector3.zero;
                particles.Spawn(3000, 10f, 15, 
                    (radius) =>
                    {
                        Vector2 r = Random.insideUnitCircle * radius;
                        return new Vector3(r.x, 0, r.y);
                    }, 
                    (pos) => pos.normalized);
            }
        }
        else if (Stage == MusicStage.Climax)
        {
            if (justTransitioned)
            {
                particles.DeleteParticles();
                particles.transform.position = Vector3.zero;
                particles.Spawn(3000, 10f, 15, 
                    (radius) =>
                    {
                        Vector2 r = Random.insideUnitCircle * radius;
                        return new Vector3(r.x, 0, r.y);
                    }, 
                    (pos) => pos.normalized);
            }
        }
        else if(Stage == MusicStage.Resolution)
        {
            if (justTransitioned)
            {
                particles.DeleteParticles();
            }
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
