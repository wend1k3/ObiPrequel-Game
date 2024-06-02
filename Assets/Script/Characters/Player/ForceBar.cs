using UnityEngine;
using UnityEngine.UI;

public class ForceBar: MonoBehaviour
{
    public Slider forceBar;
    private static float maxForce = 100f;
    private float currentForce;

    
    public PlayerController obi;
    [Header("Force Regen Parameters")]
    [Range(0,50)][SerializeField] private float forceRegen = 10;
    [Range(0,50)][SerializeField] private float forceDrain= 10;



    /*
    For Singleton purpose
    private WaitForSeconds regenTick = new WaitForSeconds(0.1f);
    private Coroutine regen;
    public static ForceBar instance;
    private void Awake()
    {
        instance = this;
    }

     */






    void Start()
    {
        currentForce = maxForce;
        forceBar.maxValue = maxForce;
        forceBar.value = currentForce;
        obi = GetComponent<PlayerController>();
        //forceBar.transform.position = obi.transform.position;
      
    }
    private void Update()
    {
        //forceBar.transform.position = obi.transform.position;
        if (!obi.IsRButton)
        {
            if (currentForce<=maxForce-0.01)
            {
                currentForce +=  forceRegen * Time.deltaTime;
                UpdateForceBar();

                
                
            }
            
        }
        if (obi.IsBlocking) Blocking();
    }

    private bool check()
    {
        return currentForce > 0;
    }
    public void Blocking()
    {
        if (check())
        {
            obi.IsBlocking = true;
            currentForce -= forceDrain * Time.deltaTime;
            UpdateForceBar();

            if (currentForce<=0) obi.IsBlocking = false;
            

        }
    }

   

    private void UpdateForceBar()
    {
        forceBar.value = currentForce;
    }

    /*
    public bool UseForce(int amount)
    {
        if (currentForce - amount >= 0)
        {
            currentForce -= amount;
            forceBar.value = currentForce;
            
            return true;
        }else
        {
            return false;
        }
    }

    public void ReForce()
    {
        if (regen != null) StopCoroutine(regen);
        regen = StartCoroutine(RegenForce());
    }

    private IEnumerator RegenForce()
    {
        yield return new WaitForSeconds(1);

        while (currentForce < maxForce)
        {
            currentForce += maxForce / 100;
            forceBar.value = currentForce;
            yield return regenTick;
        }
        regen = null;
    }
    */
    
}
