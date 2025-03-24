
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Contador : MonoBehaviour
{
    float timer = 0f;
    float randomTime;
    int minTime = 2;
    int maxTime = 45;
    public bool hasTriggered = false;
    float reactionStartTime;
    bool waitingForKey = false;
    float player1RemainingTime = 0f;
    float player2RemainingTime = 0f;
    bool player1Reacted = false;
    bool player2Reacted = false;

    void SetNewRandomTime()
    {
        randomTime = Random.Range(minTime, maxTime);
        Debug.Log("Nuevo tiempo aleatorio elegido: " + randomTime.ToString("F2") + " segundos");
    }

    void Start()
    {
        SetNewRandomTime();
    }

    void Update()
    {
        // Contador normal
        if (!hasTriggered)
        {
            timer += Time.deltaTime;
            Debug.Log("Tiempo: " + timer.ToString("F0"));

            if (timer >= randomTime)
            {
                hasTriggered = true;
                timer = randomTime;
                reactionStartTime = Time.time;
                waitingForKey = true;
                player1Reacted = false;
                player2Reacted = false;
                Debug.Log("¡PULSA ESPACIO (Jugador 1) o HAZ CLIC (Jugador 2)!");
            }
        }
        else if (waitingForKey)
        {
            // Reacción del Jugador 1 con ESPACIO
            if (!player1Reacted && Input.GetKeyDown(KeyCode.Space))
            {
                player1RemainingTime = randomTime - timer;
                Debug.Log("JUGADOR 1 - Tiempo sobrante: " + player1RemainingTime.ToString("F3") + " segundos");
                player1Reacted = true;
            }

            // Reacción del Jugador 2 con CLIC
            if (!player2Reacted && Input.GetMouseButtonDown(0))
            {
                player2RemainingTime = randomTime - timer;
                Debug.Log("JUGADOR 2 - Tiempo sobrante: " + player2RemainingTime.ToString("F3") + " segundos");
                player2Reacted = true;
            }

            // Solo avanzar cuando ambos jugadores hayan reaccionado
            if (player1Reacted && player2Reacted)
            {
                Debug.Log("Resumen de la ronda: JUGADOR 1 - " + player1RemainingTime.ToString("F3") + "s, JUGADOR 2 - " + player2RemainingTime.ToString("F3") + "s");
                //    PrepareNextRound();
                //}
            }
        }

        //void PrepareNextRound()
        //{
        //    waitingForKey = false;
        //    hasTriggered = false;
        //    timer = 0f;
        //    player1RemainingTime = 0f;
        //    player2RemainingTime = 0f;
        //    SetNewRandomTime();
        //}

    
    }
}