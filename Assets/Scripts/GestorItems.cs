using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GestorItems : MonoBehaviour
{
    public static GestorItems instancia;

    public string itemJugador1 = null;
    public string itemJugador2 = null;

    private void Awake()
    {
        if (instancia == null)
        {
            instancia = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void AsignarItem(int jugador, string item)
    {
        if (jugador == 1)
            itemJugador1 = item;
        else if (jugador == 2)
            itemJugador2 = item;
    }
}