using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Contador : MonoBehaviour
{
    public string nombreDeEscena = "TIENDA";
    private bool escenaListaParaCargar = false;
    private bool mensajeMostrado = false;

    float timer = 0f;
    float randomTime;
    int minTime = 2;
    int maxTime = 10;

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

    public GameObject player2Idle;
    public GameObject player2Shoot;
    public GameObject player2Death;

    public Transform objetoAMover;

    void Start()
    {
        SetNewRandomTime();
        SetIdleAnimation();
    }

    void Update()
    {
        if (escenaListaParaCargar && Input.GetKeyDown(KeyCode.Return))
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
                Debug.Log("JUGADOR 1 PRESION� ANTES DE TIEMPO. Penalizaci�n de 0.5 segundos.");
            }

            if (!player2Blocked && Input.GetMouseButtonDown(0))
            {
                player2Blocked = true;
                player2UnblockTime = Time.time + penaltyTime;
                Debug.Log("JUGADOR 2 PRESION� ANTES DE TIEMPO. Penalizaci�n de 0.5 segundos.");
            }

            if (timer >= randomTime)
            {
                hasTriggered = true;
                reactionStartTime = Time.time;
                player1Reacted = false;
                player2Reacted = false;

                if (objetoAMover != null)
                {
                    Vector3 nuevaPosicion = objetoAMover.position;
                    nuevaPosicion.z = -7;
                    objetoAMover.position = nuevaPosicion;
                }

                Debug.Log("�SE�AL MOSTRADA! Reacciona ahora.");
            }
        }
        else
        {
            if (!player1Reacted && !player1Blocked && Input.GetKeyDown(KeyCode.Space))
            {
                player1ReactionTime = Time.time - reactionStartTime;
                player1Reacted = true;
                ActivateShootAnimation(1);
            }

            if (!player2Reacted && !player2Blocked && Input.GetMouseButtonDown(0))
            {
                player2ReactionTime = Time.time - reactionStartTime;
                player2Reacted = true;
                ActivateShootAnimation(2);
            }

            if ((player1Reacted || player2Reacted) && !mensajeMostrado)
            {
                DetermineWinner();
            }
        }
    }

    void DetermineWinner()
    {
        if (mensajeMostrado) return;

        // Mensajes de tiempo de reacci�n de ambos jugadores
        Debug.Log("Tiempo de reacci�n del JUGADOR 1: " + player1ReactionTime.ToString("F3") + " segundos");
        Debug.Log("Tiempo de reacci�n del JUGADOR 2: " + player2ReactionTime.ToString("F3") + " segundos");

        // Determina el ganador y activa la animaci�n de muerte del perdedor
        if (player1ReactionTime < player2ReactionTime)
        {
            Debug.Log("JUGADOR 1 GANA LA RONDA");
            ActivateDeathAnimation(2); // Jugador 2 pierde
        }
        else if (player2ReactionTime < player1ReactionTime)
        {
            Debug.Log("JUGADOR 2 GANA LA RONDA");
            ActivateDeathAnimation(1); // Jugador 1 pierde
        }
        else
        {
            Debug.Log("�Empate! Nadie gana esta ronda.");
        }

        // Mensaje de instrucciones para continuar
        Debug.Log("Presiona ENTER para continuar...");

        EndRound();
        mensajeMostrado = true;
    }

    void ActivateDeathAnimation(int losingPlayer)
    {
        if (losingPlayer == 1)
        {
            Animator anim = player2Death.GetComponent<Animator>();
            if (anim != null)
            {
                anim.Play("DeathAnimation");
                Debug.Log("Animaci�n de muerte para JUGADOR 1");
            }
        }
        else if (losingPlayer == 2)
        {
            Animator anim = player2Death.GetComponent<Animator>();
            if (anim != null)
            {
                anim.Play("DeathAnimation");
                Debug.Log("Animaci�n de muerte para JUGADOR 2");
            }
        }

        escenaListaParaCargar = true;
    }

    void LoadShopScene()
    {
        Debug.Log("Cambiando a la tienda...");
        SceneManager.LoadScene(nombreDeEscena);
    }

    void SetNewRandomTime()
    {
        randomTime = Random.Range(minTime, maxTime);
    }

    void EndRound()
    {
        if (!roundEnded)
        {
            roundEnded = true;
            escenaListaParaCargar = true;
        }
    }

    void SetIdleAnimation()
    {
        SetActivePlayerState(1, true, false, false);
        SetActivePlayerState(2, true, false, false);
    }

    void ActivateShootAnimation(int player)
    {
        SetActivePlayerState(player, false, true, false);
    }

    void SetActivePlayerState(int player, bool idle, bool shoot, bool death)
    {
        if (player == 2)
        {
            player2Idle.SetActive(idle);
            player2Shoot.SetActive(shoot);
            player2Death.SetActive(death);

            Debug.Log($"Jugador 2 - Idle: {idle}, Shoot: {shoot}, Death: {death}");
        }
    }
}
