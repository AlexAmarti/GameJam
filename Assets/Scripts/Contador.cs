using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement; // Importar SceneManagement para cambiar de escena

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

    void Start()
    {
        SetNewRandomTime();
    }

    void Update()
    {
        // Fase antes de la se�al
        if (!hasTriggered)
        {
            timer += Time.deltaTime;
            Debug.Log("Tiempo: " + timer.ToString("F0"));

            // Desbloqueo si ya pas� la penalizaci�n
            if (player1Blocked && Time.time >= player1UnblockTime)
                player1Blocked = false;

            if (player2Blocked && Time.time >= player2UnblockTime)
                player2Blocked = false;

            // Reacci�n temprana - penalizaci�n
            if (!player1Blocked && Input.GetKeyDown(KeyCode.Space))
            {
                player1Blocked = true;
                player1UnblockTime = Time.time + penaltyTime;
                Debug.Log("JUGADOR 1 PRESION� ANTES DE TIEMPO. Penalizaci�n de 0.5 segundos.");
            }

            if (!player2Blocked && Input.GetMouseButtonDown(0))
            {
                player2Blocked = true;
                player2UnblockTime = Time.time + penaltyTime;
                Debug.Log("JUGADOR 2 PRESION� ANTES DE TIEMPO. Penalizaci�n de 0.5 segundos.");
            }

            // Se�al de reacci�n
            if (timer >= randomTime)
            {
                hasTriggered = true;
                reactionStartTime = Time.time;
                player1Reacted = false;
                player2Reacted = false;
                Debug.Log("�PULSA ESPACIO (Jugador 1) o HAZ CLIC (Jugador 2)!");
            }
        }
        // Fase de reacci�n
        else
        {
            // Reacci�n jugador 1
            if (!player1Reacted && !player1Blocked && Input.GetKeyDown(KeyCode.Space))
            {
                player1ReactionTime = Time.time - reactionStartTime;
                player1Reacted = true;
                Debug.Log("JUGADOR 1 reaccion� en: " + player1ReactionTime.ToString("F3") + " segundos");
            }

            // Reacci�n jugador 2
            if (!player2Reacted && !player2Blocked && Input.GetMouseButtonDown(0))
            {
                player2ReactionTime = Time.time - reactionStartTime;
                player2Reacted = true;
                Debug.Log("JUGADOR 2 reaccion� en: " + player2ReactionTime.ToString("F3") + " segundos");
            }

            // Evaluar resultados
            if (player1Reacted || player2Reacted)
            {
                if (player1Reacted && !player2Reacted && Time.time - reactionStartTime >= maxReactionTime)
                {
                    Debug.Log("JUGADOR 2 no reaccion� a tiempo. JUGADOR 1 gana la ronda.");
                    LoadShopScene();
                }
                else if (player2Reacted && !player1Reacted && Time.time - reactionStartTime >= maxReactionTime)
                {
                    Debug.Log("JUGADOR 1 no reaccion� a tiempo. JUGADOR 2 gana la ronda.");
                    LoadShopScene();
                }
                else if (player1Reacted && player2Reacted)
                {
                    DetermineWinner();
                    LoadShopScene();
                }
            }
            else if (Time.time - reactionStartTime >= maxReactionTime)
            {
                Debug.Log("Ning�n jugador reaccion� a tiempo. Nueva ronda.");
                LoadShopScene();
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
            Debug.Log("�Empate!");
    }

    // Nueva funci�n que carga la escena "TIENDA"
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
}
