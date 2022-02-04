using System.Collections;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Manages the three resources, Hydration, Relief, and Warmth, their associated sliders,
/// and killing the player when one runs out
/// </summary>
public class StatusManager : MonoBehaviour
{
    [SerializeField] private Slider HydrationSlider;
    [SerializeField] private Slider ReliefSlider;
    [SerializeField] private Slider WarmthSlider;
    [SerializeField] private DeathDisplay deathDisplay;
    [SerializeField] private Image WaterFlash;
    [SerializeField] private Image ReliefFlash;
    [SerializeField] private Image WarmthFlash;
    [SerializeField] private Color refillColor;
    [SerializeField] private Color dangerColor;

    public float Hydration;
    public float Relief;
    public float Warmth;

    [Header("Time (in seconds) to deplete the entire resource")]
    [SerializeField] private float HydrationDepletionTime = 180f;
    [SerializeField] private float ReliefDepletionTime = 240f;
    [SerializeField] private float WarmthDepletionTime = 300f;

    [Header("Loss is applied once every second")]
    [Min(0)] public float HydrationLossRate;
    [Min(0)] public float ReliefLossRate;
    [Min(0)] public float WarmthLossRate;
    
    [SerializeField] private bool DegradeHydration = true;
    [SerializeField] private bool DegradeRelief = true;
    [SerializeField] private bool DegradeWarmth = true; 
    
    private bool hydrationChanged;
    private bool reliefChanged;
    private bool warmthChanged;
    
    private float HydrationMax = 100f;
    private float ReliefMax = 100f;
    private float WarmthMax = 100f;
    
    private new bool enabled = true;
    private bool alive;
    private const float DEGRADETIME = 1f;

    private Color HydrationBar;
    private Color ReliefBar;
    private Color WarmthBar;

    private LogToServer logger;

    private void Start()
    {
        
        Hydration = HydrationMax;
        Relief = ReliefMax;
        Warmth = WarmthMax;

        HydrationSlider.maxValue = HydrationMax;
        ReliefSlider.maxValue = ReliefMax;
        WarmthSlider.maxValue = WarmthMax;

        HydrationLossRate = HydrationMax / HydrationDepletionTime;
        ReliefLossRate = ReliefMax / ReliefDepletionTime;
        WarmthLossRate = WarmthMax / WarmthDepletionTime;

        logger = GameObject.Find("Logger").GetComponent<LogToServer>();

        StartCoroutine(nameof(DegradeStatus),DegradeStatus());

        HydrationBar = HydrationSlider.image.color;
        ReliefBar = ReliefSlider.image.color;
        WarmthBar = WarmthSlider.image.color;

        alive = true;
    }

    // Update is called once per frame
    private void Update()
    {
        if (!enabled) return;

        if (Hydration <= 0)
        {
            PlayerDeath("Dehydration", "Dehydration Death :(");
        }
        else if (Relief <= 0)
        {
            PlayerDeath("Lack of relief", "Due to lack of a proper toilet, you were forced to defecate without proper " +
                                      "sanitation. You caught a disease and died.");
        }
        else if (Warmth <= 0)
        {
            PlayerDeath("Hypothermia", "Hypothermia Death :(");
        }

        if (hydrationChanged)
        {
            hydrationChanged = false;
            HydrationSlider.value = Hydration;
        }
        if (reliefChanged)
        {
            reliefChanged = false;
            ReliefSlider.value = Relief;
        }
        if (warmthChanged)
        {
            warmthChanged = false;
            WarmthSlider.value = Warmth;
        }

        RefillFlash();
        LowLevelFlash();
    }

    public void AffectHydration(float deltaH)
    {
        Hydration += deltaH;
        if (Hydration > HydrationMax) Hydration = HydrationMax;
        hydrationChanged = true;
        logger.sendToLog("Drank water: Hydration changed to " + Hydration,"STATUS");
    }
    public void AffectRelief(float deltaR)
    {
        Relief += deltaR;
        if (Relief > ReliefMax) Relief = ReliefMax;
        reliefChanged = true;
        logger.sendToLog("Relieved themselves: Relief changed to " + Relief,"STATUS");
    }
    public void AffectWarmth(float deltaW)
    {
        Warmth += deltaW;
        if (Warmth > WarmthMax) Warmth = WarmthMax;
        warmthChanged = true;
        logger.sendToLog("Warmth changed to " + Warmth,"STATUS");
    }

    public float GetHydration()
    {
        return Hydration;
    }
    
    public float GetRelief()
    {
        return Relief;
    }
    
    public float GetWarmth()
    {
        return Warmth;
    }
    
    public void PlayerDeath(string causeOfDeath, string textOnDeath)
    {
        if (!alive) return;
        alive = false;
        //Logger.Instance.Log("Player killed by: "+textOnDeath);
        logger.sendToLog("Death: "+ causeOfDeath, "DEATH");
        deathDisplay.Activate(textOnDeath);
    }
    
    public bool Paused => !enabled;


    public void Pause()
    {
        if(enabled) StopCoroutine(nameof(DegradeStatus));
        //StopCoroutine("QuakeCountdown");
        enabled = false;
    }
    

    public void UnPause()
    {
        var c = enabled;
        enabled = true;
        if(!c) StartCoroutine(nameof(DegradeStatus), DegradeStatus());
    }

    private IEnumerator DegradeStatus()
    {
        if (!enabled) yield break;
        if (DegradeHydration && HydrationLossRate > 0)
        {
            Hydration -= HydrationLossRate;
            hydrationChanged = true;
        }

        if (DegradeRelief && ReliefLossRate > 0)
        {
            Relief -= ReliefLossRate;
            reliefChanged = true;
        }

        if (DegradeWarmth && WarmthLossRate > 0)
        {
            Warmth -= WarmthLossRate;
            warmthChanged = true;
        }

        yield return new WaitForSeconds(DEGRADETIME);
        StartCoroutine(DegradeStatus());
    }

    public void SpeedUpWarmthLoss()
    {
        WarmthLossRate = WarmthLossRate * 2;
    }

    public void SlowDownWarmthLoss()
    {
        WarmthLossRate = WarmthLossRate * 0.5f;
    }

    public void SlowDownReliefLoss()
    {
        ReliefLossRate = ReliefLossRate * 0.2f;
    }

    public void SlowDownHydrationLoss()
    {
        HydrationLossRate = HydrationLossRate * 0.5f;
    }

    public void SlowDownWarmthLossMore()
    {
        WarmthLossRate = WarmthLossRate * 0.2f;
    }



    public void RefillFlash()
    {

        if (Relief == 100)
        {
            ReliefFlash.color = refillColor;
        }
        else if (Warmth == 100)
        {
            WarmthFlash.color = refillColor;
        }
        else if (Hydration == 100)
        {
            WaterFlash.color = refillColor;
        }

        else
        {
            WaterFlash.color = Color.Lerp(WaterFlash.color, Color.clear, Time.time);
            ReliefFlash.color = Color.Lerp(ReliefFlash.color, Color.clear, Time.time);
            WarmthFlash.color = Color.Lerp(WarmthFlash.color, Color.clear, Time.time);
        }
    }
    public void LowLevelFlash()
    {
        if (Hydration <= 25)
        {
            WaterFlash.color = Color.Lerp(dangerColor, Color.clear, Mathf.PingPong(Time.time, .5f));
        }
        if (Relief <= 25)
        {
            ReliefFlash.color = Color.Lerp(dangerColor, Color.clear, Mathf.PingPong(Time.time, .5f));
        }
        if (Warmth <= 25)
        {
            WarmthFlash.color = Color.Lerp(dangerColor, Color.clear, Mathf.PingPong(Time.time, .5f));
        }

    }

}
