using System.Collections;
using System.Collections.Generic;
using UnityEngine;
// 1. Necesito decirle a Unity donde aparecerá dentro del menú de "creación de assets"
[CreateAssetMenu(fileName = "Objeto", menuName = "NuevoObjeto")]

public class ItemsSO : ScriptableObject
{
    // Esto es una plantilla para crear assets de tipo item
    [Header("Info")] 
    public string nombre;
    
    public string descripción;

    public TipoItem tipo;
    // Sprite que va a tener el objeto cuando esté en el inventario
    public Sprite icono;
    // Prefab para cuando lo soltemos del inventario
    public GameObject dropPrefab;

    [Header("Stackeo")] public bool puedeStakear;
    public int maxCantidadStack;
}

public enum TipoItem
{
    Recurso,
    Equipable,
    Consumible
}