using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteracionManager : MonoBehaviour
{
    // Variables
    public float checkRate = 0.05f;
    private float lastCheckTime;
    public float distancia;
    public LayerMask LayerMask;

}

public interface IInteractuable
{
    // Nombramos dos métodos
    string GetMensajeEnPantalla();
    void AlInteractuar();
}