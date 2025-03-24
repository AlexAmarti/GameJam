
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Contador : MonoBehaviour
{
    float timer = 0f;
    float randomTime;
    float minTime = 2f;
    float maxTime = 45f;
    bool hasTriggered = false;

    void Start()
    {
        // Elegimos un número aleatorio entre 0 y 45 al iniciar
        randomTime = Random.Range(minTime, maxTime);
        Debug.Log("Tiempo aleatorio escogido: " + randomTime.ToString("F2") + " segundos");
    }

    void Update()
    {
        timer += Time.deltaTime;

        Debug.Log("Tiempo: " + timer.ToString("F2"));

        // Cuando el tiempo alcanza el valor aleatorio y no se ha activado aún
        if (!hasTriggered && timer >= randomTime)
        {
            hasTriggered = true;
            Debug.Log("¡Evento activado en el segundo " + timer.ToString("F2") + "!");
            // Aquí puedes poner lo que necesites que pase
        }

        // Si ya pasaron 45 segundos, puedes detener el contador o reiniciar
        if (timer >= 45f)
        {
            Debug.Log("¡Se acabaron los 45 segundos!");
            enabled = false; // Desactiva este script si ya no necesitas Update
        }
    }
}
