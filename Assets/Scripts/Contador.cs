
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Contador : MonoBehaviour
{
    float timer = 0f;
    float randomTime;
    float minTime = 2f;
    float maxTime = 45f;
    public bool hasTriggered = false;
    float reactionStartTime;
    bool waitingForKey = false;

    void Start()
    {
        // Elegimos un número aleatorio entre 0 y 45 al iniciar
        randomTime = Random.Range(minTime, maxTime);
        Debug.Log("Tiempo aleatorio escogido: " + randomTime.ToString("F0") + " segundos");
    }

    void Update()
    {
        if (!hasTriggered)
        {
            timer += Time.deltaTime;
            Debug.Log("Tiempo: " + timer.ToString("F0"));

            if (timer >= randomTime)
            {
                hasTriggered = true;
                timer = randomTime; // Aseguramos que no siga subiendo
                reactionStartTime = Time.time; // Guardamos el momento en que se debe pulsar
                waitingForKey = true;
                Debug.Log("¡PULSA ESPACIO YA!");
            }
        }
        else if (waitingForKey)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                float reactionTime = Time.time - reactionStartTime;
                Debug.Log("¡Has pulsado ESPACIO!");
                Debug.Log("Tiempo de reacción: " + reactionTime.ToString("F3") + " segundos");
                waitingForKey = false;
            }
        }
    }
}
