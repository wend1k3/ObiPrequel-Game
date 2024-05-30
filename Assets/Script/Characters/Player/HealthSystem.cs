using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthSystem : MonoBehaviour
{
    // Start is called before the first frame update
    private int maxHealthBar = 10;

    [SerializeField]
    private int startHealthBar = 3;

    [SerializeField]
    private int curHealth;

    [SerializeField]
    private int maxHealth;

    private int healthPerBar = 10;

    Damagable damagable;

    public Slider[] healthSliders;
    void Start()
    {
        damagable = GetComponent<Damagable>();
        curHealth = startHealthBar * healthPerBar;
        damagable.Health = curHealth;
        maxHealth = maxHealthBar * healthPerBar;
        damagable.MaxHealth = maxHealth;    
        checkHealthAmount();
  
        
    }

    void checkHealthAmount()
    {
        for (int i=0; i<maxHealthBar; i++)
        {
            
            if (startHealthBar <= i) healthSliders[i].gameObject.SetActive(false);
            else healthSliders[i].gameObject.SetActive(true);
        }
        Debug.LogWarning(curHealth);
        updateHealth(curHealth);
    }

    public void updateHealth(int curHealth)
    {
        
        bool empty = false;
        int i = 0;
        foreach (Slider slider in healthSliders)
        {
            if (empty) slider.value = 0;
            else
            {
                i++;
                curHealth = damagable.Health;
               
                if (curHealth >= healthPerBar * i) slider.value = healthPerBar;
                else
                {
                    int tempHealth = curHealth%healthPerBar;
                    float valuePerBar = (float) tempHealth/ healthPerBar;
                    slider.value = valuePerBar;
                    empty = true;

                }

            }
            
           
        }
        
    }

    public void addHealthBar()
    {
        startHealthBar = startHealthBar+1;
        startHealthBar = Mathf.Clamp(startHealthBar, 0, maxHealthBar);
        curHealth = startHealthBar * healthPerBar;
        maxHealth = maxHealthBar * healthPerBar;
        damagable.Health = curHealth;
        damagable.MaxHealth = maxHealth;
        checkHealthAmount();
       
    }


}
