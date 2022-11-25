using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemObject : MonoBehaviour, IInteractuable
{
    public ItemsSO item;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public string GetMensajeEnPantalla()
    {
        // Devolver el mensaje que tiene que pintar el canvas
        // cuando apuntemos a un objeto interactuable
        // (Recoger "NombreDelObjeto")
        return "Recoger " + item.nombre;
    }

    public void AlInteractuar()
    {
        Destroy(gameObject);
    }
}
