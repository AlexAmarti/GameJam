using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

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

    public Animator animatorPlayer1; // Animator del Jugador 1
    public Animator animatorPlayer2; // Animator del Jugador 2
    public Image signalImage; // Imagen que se mostrará en el Canvas

    void Start()
    {
        SetNewRandomTime();

        // Ocultar la imagen al inicio
        if (signalImage != null)
            signalImage.gameObject.SetActive(false);
    }

    void Update()
    {
        if (roundEnded && Input.GetKeyDown(KeyCode.Return))
        {
            LoadShopScene();
            return;
        }

        if (!hasTriggered)
        {
            timer += Time.deltaTime;

            if (player1Blocked && Time.time >= player1UnblockTime)
                player1Blocked = false;

            if (player2Blocked && Time.time >= player2UnblockTime)
                player2Blocked = false;

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

            if (timer >= randomTime)
            {
                hasTriggered = true;
                reactionStartTime = Time.time;
                player1Reacted = false;
                player2Reacted = false;

                // Mostrar la imagen en pantalla
                if (signalImage != null)
                    signalImage.gameObject.SetActive(true);

                Debug.Log("¡SEÑAL MOSTRADA! Reacciona ahora.");
            }
        }
        else
        {
            if (!player1Reacted && !player1Blocked && Input.GetKeyDown(KeyCode.Space))
            {
                player1ReactionTime = Time.time - reactionStartTime;
                player1Reacted = true;

                // Activar animación de disparo del Jugador 1
                if (animatorPlayer1 != null)
                    animatorPlayer1.SetTrigger("Shoot");

                Debug.Log("JUGADOR 1 reaccionó en: " + player1ReactionTime.ToString("F3") + " segundos");
            }

            if (!player2Reacted && !player2Blocked && Input.GetMouseButtonDown(0))
            {
                player2ReactionTime = Time.time - reactionStartTime;
                player2Reacted = true;

                // Activar animación de disparo del Jugador 2
                if (animatorPlayer2 != null)
                    animatorPlayer2.SetTrigger("Shoot");

                Debug.Log("JUGADOR 2 reaccionó en: " + player2ReactionTime.ToString("F3") + " segundos");
            }

            if (player1Reacted || player2Reacted)
            {
                if (player1Reacted && !player2Reacted && Time.time - reactionStartTime >= maxReactionTime)
                {
                    Debug.Log("JUGADOR 2 no reaccionó a tiempo. JUGADOR 1 gana la ronda.");
                    ActivateDeathAnimation(2);
                    EndRound();
                }
                else if (player2Reacted && !player1Reacted && Time.time - reactionStartTime >= maxReactionTime)
                {
                    Debug.Log("JUGADOR 1 no reaccionó a tiempo. JUGADOR 2 gana la ronda.");
                    ActivateDeathAnimation(1);
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
        {
            Debug.Log("JUGADOR 1 gana la ronda.");
            ActivateDeathAnimation(2);
        }
        else if (player2ReactionTime < player1ReactionTime)
        {
            Debug.Log("JUGADOR 2 gana la ronda.");
            ActivateDeathAnimation(1);
        }
        else
        {
            Debug.Log("¡Empate!");
        }
    }

    void ActivateDeathAnimation(int losingPlayer)
    {
        if (losingPlayer == 1)
        {
            if (animatorPlayer1 != null)
                animatorPlayer1.SetTrigger("Death");

            Debug.Log("Activando animación de muerte para JUGADOR 1");
        }
        else if (losingPlayer == 2)
        {
            if (animatorPlayer2 != null)
                animatorPlayer2.SetTrigger("Death");

            Debug.Log("Activando animación de muerte para JUGADOR 2");
        }
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
