using UnityEngine;

public class Contador : MonoBehaviour
{
    float timer = 0f;
    float randomTime;
    int minTime = 2;
    int maxTime = 45;
    public bool hasTriggered = false;
    bool waitingForKey = false;
    float player1Difference = 0f;
    float player2Difference = 0f;
    bool player1Reacted = false;
    bool player2Reacted = false;

    void SetNewRandomTime()
    {
        randomTime = Random.Range(minTime, maxTime);
        Debug.Log("Nuevo tiempo aleatorio elegido: " + randomTime.ToString("F0") + " segundos");
    }
    void Start()
    {
        SetNewRandomTime();
    }

    void Update()
    {
        // Contador normal
        if (!hasTriggered || (hasTriggered && waitingForKey && (!player1Reacted || !player2Reacted)))
        {
            timer += Time.deltaTime;
            Debug.Log("Tiempo: " + timer.ToString("F0"));
        }

        if (!hasTriggered && timer >= randomTime)
        {
            hasTriggered = true;
            waitingForKey = true;
            player1Reacted = false;
            player2Reacted = false;
            Debug.Log("¡PULSA ESPACIO (Jugador 1) o HAZ CLIC (Jugador 2)!");
        }

        if (waitingForKey)
        {
            // Registrar la diferencia de tiempo para el Jugador 1
            if (!player1Reacted && Input.GetKeyDown(KeyCode.Space))
            {
                player1Difference = timer - randomTime;
                player1Reacted = true;
            }

            // Registrar la diferencia de tiempo para el Jugador 2
            if (!player2Reacted && Input.GetMouseButtonDown(0))
            {
                player2Difference = timer - randomTime;
                player2Reacted = true;
            }

            // Detener el contador inmediatamente cuando ambos jugadores hayan reaccionado
            if (player1Reacted && player2Reacted)
            {
                Debug.Log("Tiempo final: " + timer.ToString("F3") + " segundos");
                Debug.Log("JUGADOR 1 - Diferencia de tiempo: " + player1Difference.ToString("F3") + " segundos");
                Debug.Log("JUGADOR 2 - Diferencia de tiempo: " + player2Difference.ToString("F3") + " segundos");
                Debug.Log("Resumen de la ronda:\nJUGADOR 1 - Diferencia: " + player1Difference.ToString("F3") + "s\nJUGADOR 2 - Diferencia: " + player2Difference.ToString("F3") + "s");
                waitingForKey = false;
            }
        }
    }
}
