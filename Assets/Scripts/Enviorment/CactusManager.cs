using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CactusManager : MonoBehaviour
{
    // Daño
    public float damage;
    
    // Cada cuantos segundos va a pegar
    public float damageRate;
    
    // Lista de gente a la que tiene que pegar
    private List<GameObject> ThingsToDamage = new List<GameObject>();

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(DealDamage());
    }
    
    // Si alguien entra en colisión, lo meto en la lista
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            ThingsToDamage.Add(collision.gameObject);
        }
        else if (collision.gameObject.CompareTag("Enemy"))
        {
            ThingsToDamage.Add(collision.gameObject);
        }
    }

    private void OnCollisionExit(Collision other)
    {
        if (ThingsToDamage.Contains(other.gameObject))
        {
            ThingsToDamage.Remove(other.gameObject);
        }
    }
    
    // Rutina con bulce infinito que se va a ejecutar
    // cada "x" segundos
    IEnumerator DealDamage()
    {
        // Dentro de un bucle infinito
        // Vamos a hacer daño a todos los de la lista
        // cada 0.5 segundos
        while (true)
        {
            for (int i = 0; i < ThingsToDamage.Count; i++)
            {
                // OJO que podemos tener al "player" o "enemigos"
                switch (ThingsToDamage[i].tag)
                {
                    case "Player":
                        ThingsToDamage[i].GetComponent<PlayerNeedsManager>().TakeDamage(damage);
                        break;
                    
                    // TODO: Case de "Enemy"
                }
            }
            
            // Decirle a la corutina que pare 0.5 segundos
            yield return new WaitForSeconds(damageRate);
        }
    }
}
