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
    bool waitingForKey = false;
    bool waitingForRestart = false;
    float reactionStartTime;
    float player1ReactionTime;
    float player2ReactionTime;
    bool player1Reacted = false;
    bool player2Reacted = false;
<<<<<<< Updated upstream
<<<<<<< Updated upstream
    float maxReactionTime = 1f;
=======
=======
>>>>>>> Stashed changes
    bool player1Blocked = false;
    bool player2Blocked = false;
    float player1UnblockTime = 0f;
    float player2UnblockTime = 0f;
    float maxReactionTime = 1f;
    float penaltyTime = 0.5f; // Tiempo de penalización (0.5 segundos)
<<<<<<< Updated upstream
>>>>>>> Stashed changes
=======
>>>>>>> Stashed changes

    void Start()
    {
        SetNewRandomTime();
    }

    void Update()
    {
        if (waitingForRestart)
        {
            if (Input.GetKeyDown(KeyCode.Q))
            {
                Debug.Log("Nueva ronda iniciada.");
                StartNewRound();
            }
            return;
        }

        if (!hasTriggered)
        {
            timer += Time.deltaTime;
            Debug.Log("Tiempo: " + timer.ToString("F0"));

<<<<<<< Updated upstream
<<<<<<< Updated upstream
            if (timer >= randomTime)
            {
                hasTriggered = true;
                waitingForKey = true;
                reactionStartTime = Time.time;
                player1Reacted = false;
                player2Reacted = false;
                Debug.Log("¡PULSA ESPACIO (Jugador 1) o HAZ CLIC (Jugador 2)!");
            }
        }
        else if (waitingForKey)
        {
            if (!player1Reacted && Input.GetKeyDown(KeyCode.Space))
            {
=======
            // Verifica si los jugadores pueden ser desbloqueados
            if (player1Blocked && Time.time >= player1UnblockTime)
                player1Blocked = false;
            if (player2Blocked && Time.time >= player2UnblockTime)
                player2Blocked = false;

            if (!player1Blocked && Input.GetKeyDown(KeyCode.Space))
            {
=======
            // Verifica si los jugadores pueden ser desbloqueados
            if (player1Blocked && Time.time >= player1UnblockTime)
                player1Blocked = false;
            if (player2Blocked && Time.time >= player2UnblockTime)
                player2Blocked = false;

            if (!player1Blocked && Input.GetKeyDown(KeyCode.Space))
            {
>>>>>>> Stashed changes
                player1Blocked = true;
                player1UnblockTime = Time.time + penaltyTime;
                Debug.Log("JUGADOR 1 PRESIONÓ ANTES DE TIEMPO. Penalización de 0.5 segundos.");
            }

            if (!player2Blocked && Input.GetMouseButtonDown(0))
            {
                player2Blocked = true;
                player2UnblockTime = Time.time + penaltyTime;
                Debug.Log("JUGADOR 2 PRESIONÓ ANTES DE TIEMPO. Penalización de 0.5 segundos.");
            }

            if (timer >= randomTime)
            {
                hasTriggered = true;
                waitingForKey = true;
                reactionStartTime = Time.time;
                player1Reacted = false;
                player2Reacted = false;
                Debug.Log("¡PULSA ESPACIO (Jugador 1) o HAZ CLIC (Jugador 2)!");
            }
        }
        else if (waitingForKey)
        {
            if (!player1Reacted && !player1Blocked && Input.GetKeyDown(KeyCode.Space))
            {
<<<<<<< Updated upstream
>>>>>>> Stashed changes
=======
>>>>>>> Stashed changes
                player1ReactionTime = Time.time - reactionStartTime;
                player1Reacted = true;
                Debug.Log("JUGADOR 1 reaccionó en: " + player1ReactionTime.ToString("F3") + " segundos");
            }

<<<<<<< Updated upstream
<<<<<<< Updated upstream
            if (!player2Reacted && Input.GetMouseButtonDown(0))
=======
            if (!player2Reacted && !player2Blocked && Input.GetMouseButtonDown(0))
>>>>>>> Stashed changes
=======
            if (!player2Reacted && !player2Blocked && Input.GetMouseButtonDown(0))
>>>>>>> Stashed changes
            {
                player2ReactionTime = Time.time - reactionStartTime;
                player2Reacted = true;
                Debug.Log("JUGADOR 2 reaccionó en: " + player2ReactionTime.ToString("F3") + " segundos");
            }

            if (player1Reacted || player2Reacted)
            {
                if (player1Reacted && !player2Reacted && Time.time - reactionStartTime >= maxReactionTime)
                {
                    Debug.Log("JUGADOR 2 no reaccionó a tiempo. JUGADOR 1 gana la ronda.");
                    WaitForNextRound();
                }
                else if (player2Reacted && !player1Reacted && Time.time - reactionStartTime >= maxReactionTime)
                {
                    Debug.Log("JUGADOR 1 no reaccionó a tiempo. JUGADOR 2 gana la ronda.");
                    WaitForNextRound();
                }
                else if (player1Reacted && player2Reacted)
                {
                    DetermineWinner();
                    WaitForNextRound();
                }
            }
            else if (Time.time - reactionStartTime >= maxReactionTime)
            {
                Debug.Log("Ningún jugador reaccionó a tiempo. Nueva ronda.");
                WaitForNextRound();
            }
        }
    }

    void DetermineWinner()
    {
        if (player1ReactionTime < player2ReactionTime)
            Debug.Log("JUGADOR 1 gana la ronda.");
        else
            Debug.Log("JUGADOR 2 gana la ronda.");
    }

    void WaitForNextRound()
    {
        waitingForKey = false;
        waitingForRestart = true;
        Debug.Log("Presiona 'Q' para iniciar una nueva ronda.");
    }

    void StartNewRound()
    {
        waitingForRestart = false;
        hasTriggered = false;
        timer = 0f;
<<<<<<< Updated upstream
<<<<<<< Updated upstream
=======
        player1Blocked = false;
        player2Blocked = false;
>>>>>>> Stashed changes
=======
        player1Blocked = false;
        player2Blocked = false;
>>>>>>> Stashed changes
        SetNewRandomTime();
    }

    void SetNewRandomTime()
    {
        randomTime = Random.Range(minTime, maxTime);
        Debug.Log("Nuevo tiempo aleatorio: " + randomTime.ToString("F2") + " segundos");
    }
<<<<<<< Updated upstream
<<<<<<< Updated upstream
}
=======
}
>>>>>>> Stashed changes
=======
}
>>>>>>> Stashed changes
