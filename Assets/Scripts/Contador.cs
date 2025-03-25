using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Contador : MonoBehaviour
{
    float timer = 0f;
    float randomTime;
    int minTime = 2;
    int maxTime = 45;

    public bool hasTriggered = false;

    float reactionStartTime;
    float player1ReactionTime;
    float player2ReactionTime;

    bool player1Reacted = false;
    bool player2Reacted = false;

    bool player1Blocked = false;
    bool player2Blocked = false;

    float player1UnblockTime = 0f;
    float player2UnblockTime = 0f;

    float maxReactionTime = 1f;
    float penaltyTime = 0.5f;

    bool roundEnded = false;

    void Start()
    {
        SetNewRandomTime();
    }

    void Update()
    {
        // Si la ronda terminó, esperar que el jugador presione ENTER para cargar la tienda
        if (roundEnded && Input.GetKeyDown(KeyCode.Return))
        {
            LoadShopScene();
            return; // Salir del Update para evitar procesar más lógica
        }

        // Fase antes de la señal
        if (!hasTriggered)
        {
            timer += Time.deltaTime;
            Debug.Log("Tiempo: " + timer.ToString("F0"));

            // Desbloqueo si ya pasó la penalización
            if (player1Blocked && Time.time >= player1UnblockTime)
                player1Blocked = false;

            if (player2Blocked && Time.time >= player2UnblockTime)
                player2Blocked = false;

            // Reacción temprana - penalización
            if (!player1Blocked && Input.GetKeyDown(KeyCode.Space))
            {
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

            // Señal de reacción
            if (timer >= randomTime)
            {
                hasTriggered = true;
                reactionStartTime = Time.time;
                player1Reacted = false;
                player2Reacted = false;
                Debug.Log("¡PULSA ESPACIO (Jugador 1) o HAZ CLIC (Jugador 2)!");
            }
        }
        // Fase de reacción
        else
        {
            // Reacción jugador 1
            if (!player1Reacted && !player1Blocked && Input.GetKeyDown(KeyCode.Space))
            {
                player1ReactionTime = Time.time - reactionStartTime;
                player1Reacted = true;
                Debug.Log("JUGADOR 1 reaccionó en: " + player1ReactionTime.ToString("F3") + " segundos");
            }

            // Reacción jugador 2
            if (!player2Reacted && !player2Blocked && Input.GetMouseButtonDown(0))
            {
                player2ReactionTime = Time.time - reactionStartTime;
                player2Reacted = true;
                Debug.Log("JUGADOR 2 reaccionó en: " + player2ReactionTime.ToString("F3") + " segundos");
            }

            // Evaluar resultados
            if (player1Reacted || player2Reacted)
            {
                if (player1Reacted && !player2Reacted && Time.time - reactionStartTime >= maxReactionTime)
                {
                    Debug.Log("JUGADOR 2 no reaccionó a tiempo. JUGADOR 1 gana la ronda.");
                    EndRound();
                }
                else if (player2Reacted && !player1Reacted && Time.time - reactionStartTime >= maxReactionTime)
                {
                    Debug.Log("JUGADOR 1 no reaccionó a tiempo. JUGADOR 2 gana la ronda.");
                    EndRound();
                }
                else if (player1Reacted && player2Reacted)
                {
                    DetermineWinner();
                    EndRound();
                }
            }
            else if (Time.time - reactionStartTime >= maxReactionTime)
            {
                Debug.Log("Ningún jugador reaccionó a tiempo. Nueva ronda.");
                EndRound();
            }
        }
    }

    void DetermineWinner()
    {
        if (player1ReactionTime < player2ReactionTime)
            Debug.Log("JUGADOR 1 gana la ronda.");
        else if (player2ReactionTime < player1ReactionTime)
            Debug.Log("JUGADOR 2 gana la ronda.");
        else
            Debug.Log("¡Empate!");
    }

    void LoadShopScene()
    {
        Debug.Log("Cargando la tienda...");
        SceneManager.LoadScene("TIENDA");
    }

    void SetNewRandomTime()
    {
        randomTime = Random.Range(minTime, maxTime);
        Debug.Log("Nuevo tiempo aleatorio: " + randomTime.ToString("F0") + " segundos");
    }

    void EndRound()
    {
        if (!roundEnded)
        {
            roundEnded = true;
            Debug.Log("Ronda finalizada. Presiona ENTER para ir a la tienda.");
        }
    }
}
