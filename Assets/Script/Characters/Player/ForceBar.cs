using UnityEngine;
using UnityEngine.UI;
using System.Collections;
public class ForceBar: MonoBehaviour
{
    public Slider forceBar;
    private int maxForce = 100;
    private int currentForce;

    private WaitForSeconds regenTick = new WaitForSeconds(0.1f);
    private Coroutine regen;

    /*
    For Singleton purpose
     */ 
     
    public static ForceBar instance;
  

    private void Awake()
    {
        instance = this;
    }
    void Start()
    {
        currentForce = maxForce;
        forceBar.maxValue = maxForce;
        forceBar.value = currentForce;
      
    }

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
}
