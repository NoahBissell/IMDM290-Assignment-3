using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using Random = UnityEngine.Random;

public class MusicController : MonoBehaviour
{
    public static MusicController Instance { get; private set; }

    public float startTime;

    public float explodeSpeed;
    
    public static float MusicTime;
    public static MusicStage Stage;
    public static float StagePercent;
    public GravityParticles particles1;
    public GravityParticles particles2;
    

    public AudioSource[] audioSources;

    private bool readyToExplode = false;

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
        CalculateStage();

        for (int i = 0; i < audioSources.Length; i++)
        {
            audioSources[i].time = startTime;
        }
        
    }
    
    private void FixedUpdate()
    {
        MusicTime += Time.fixedDeltaTime;
        
        MusicStage lastStage = Stage;
        Stage = CalculateStage();
        
        bool justTransitioned = lastStage != Stage;

        if (Stage == MusicStage.Exposition)
        {
            BodyManager.GravityConstant += .001f;
        }
        else if (Stage == MusicStage.Development)
        {
            if (justTransitioned)
            {
                StartCoroutine(ShrinkAnimation(particles1.transform, .7f));
                
                readyToExplode = true;
            }
        }
        else if (Stage == MusicStage.Climax)
        {
            if (justTransitioned)
            {
                
                readyToExplode = true;
            }
        }
        else if(Stage == MusicStage.Resolution)
        {
            if (justTransitioned)
            {
                StartCoroutine(ShrinkAnimation(particles1.transform, 1f));
                StartCoroutine(ShrinkAnimation(particles2.transform, 1f));

            }
        }
    }

    public void Explosion()
    {
        if (!readyToExplode) return;
        particles1.transform.localScale = Vector3.one;
        particles1.DeleteParticles();
        particles1.transform.position = Vector3.zero;
        particles1.Spawn(2000, 5f, explodeSpeed, 
            (radius) =>
            {
                Vector2 r = Random.insideUnitCircle * radius;
                return new Vector3(r.x, r.y, 0);
            }, 
            (pos) => Vector3.Cross(pos.normalized, Vector3.forward));

        readyToExplode = false;
    }

    IEnumerator ShrinkAnimation(Transform trans, float t)
    {
        Vector3 initialPosition = trans.position;
        Vector3 initialScale = trans.localScale;
        while (t >= 0)
        {
            trans.position = initialPosition * t;
            trans.localScale = initialScale * t;
            
            t-= Time.deltaTime;
            yield return null;
        }

        trans.localScale = Vector3.zero;
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
