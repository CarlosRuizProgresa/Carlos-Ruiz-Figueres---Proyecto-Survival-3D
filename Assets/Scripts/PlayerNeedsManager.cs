using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerNeedsManager : MonoBehaviour
{
    // Variables necesarias:
    // 1. Neceistamos una variable para guardar el valor actual
    // -> Una variable distinta para cada barra
    // 2. Necesito especificar el valor de inicio
    // -> Una variable para cada barra
    // 3. Valor maximo:
    // -> Una por barra
    
    // Creamos los objetos de tipo Need
    public Need health;
    public Need hunger;
    public Need water;
    public Need sleep;
    
    // Variables especificas
    // Cantidad de vida que voy a perder cuando tenga sed y/o hambre
    public float hungerHealthDecay;
    public float waterHealthDecay;
    
    // Variable Gradiente para cambiar de color de la barra
    public Gradient colors;

    public DamageIndicatorController DamageIndicator;
    
    void Start()
    {
        // Establecemos los valores iniciales de cada Need
        health.curValue = health.startValue;
        hunger.curValue = hunger.startValue;
        water.curValue = water.startValue;
        sleep.curValue = sleep.startValue;
    }

    // Update is called once per frame
    void Update()
    {
        // Hambre, sed y sueño se desgasten por segundo
        hunger.Substract(hunger.decayRate * Time.deltaTime);
        water.Substract(water.decayRate * Time.deltaTime);
        sleep.Add(sleep.regenRate * Time.deltaTime);

        // Hambre y sed a cero
        if (hunger.curValue == 0.0f)
            health.Substract(hungerHealthDecay * Time.deltaTime);
        if (water.curValue == 0.0f)
            health.Substract(waterHealthDecay * Time.deltaTime);
        // Compruebo si estoy muerto
        if (health.curValue == 0.0f)
        {
            Die();
        }

        // Actualizar el tamaño de las barras
        health.UIBar.fillAmount = health.GetPercentage();
        hunger.UIBar.fillAmount = hunger.GetPercentage();
        hunger.UIBar.color = colors.Evaluate(hunger.GetPercentage());
        water.UIBar.fillAmount = water.GetPercentage();
        sleep.UIBar.fillAmount = sleep.GetPercentage();
    }
    
    // Acciones
    public void Heal(float amount)
    {
        health.Add(amount);
    }

    public void Eat(float amount)
    {
        hunger.Add(amount);
    }
    
    public void Drink(float amount)
    {
        water.Add(amount);
    }
    
    public void Sleep(float amount)
    {
        sleep.Substract(amount);
    }

    public void Die()
    {
        Debug.Log("Estoy muerto");
    }

    public void TakeDamage(float amount)
    {
        // Llamo al indicador de daño
        DamageIndicator.Fade();
        health.Substract(amount);
    }

}// Final de PlayerNeeds Manager


// Objeto Need
// Esto le dice a Unity entre otras cosas que pinte sus variables en el editor
[System.Serializable]
public class Need
{
    // Variables comunes
    // Valor actual de la barra
    [HideInInspector]
    public float curValue;
    // Valor máximo de la barra
    public float maxValue;
    // Valor inicial de la barra
    public float startValue;
    // Cantidad de regeneración por segundo
    public float regenRate;
    // Cantidad de desgaste por segundo
    public float decayRate;
    // Objeto imagen de la barra para poder hacer que suba o baje visualmente
    public Image UIBar;
    
    // Metodos comúnes o acesiones comúnes
    // Sumar
    public void Add(float amount)
    {
        curValue = Mathf.Min(curValue + amount, maxValue);
    }
    // Restar
    public void Substract(float amount)
    {
        curValue = Mathf.Max(curValue - amount, 0.0f);
    }
    // Teniendo en cuenta que nuestra barra funciona con porcentajes
    // Tenemos que pasar el valor actual a porcentaje
    public float GetPercentage()
    {
        return curValue / maxValue;
    }
}
