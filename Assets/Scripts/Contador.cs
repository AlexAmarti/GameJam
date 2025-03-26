using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

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

                if (objetoAMover != null)
                {
                    Vector3 nuevaPosicion = objetoAMover.position;
                    nuevaPosicion.z = -7;
                    objetoAMover.position = nuevaPosicion;
                }

                Debug.Log("¡SEÑAL MOSTRADA! Reacciona ahora.");
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

        // 1. Mensaje de diferencia de reacción del Jugador 1
        Debug.Log("Tiempo de reacción del JUGADOR 1: " + player1ReactionTime.ToString("F3") + " segundos");

        // 2. Mensaje de diferencia de reacción del Jugador 2
        Debug.Log("Tiempo de reacción del JUGADOR 2: " + player2ReactionTime.ToString("F3") + " segundos");

        // Determina el ganador y activa la animación de muerte del perdedor
        if (player1ReactionTime < player2ReactionTime)
        {
            // 3. Mensaje del ganador
            Debug.Log("JUGADOR 1 GANA LA RONDA");

            // 4. Mensaje de animación de muerte del perdedor (Jugador 2)
            ActivateDeathAnimation(2);
        }
        else if (player2ReactionTime < player1ReactionTime)
        {
            Debug.Log("JUGADOR 2 GANA LA RONDA");
            ActivateDeathAnimation(1);
        }
        else
        {
            Debug.Log("¡Empate! Nadie gana esta ronda.");
        }

        // 5. Mensaje de pulsar Enter para continuar
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
                Debug.Log("Animación de muerte para JUGADOR 1");
            }
        }
        else if (losingPlayer == 2)
        {
            Animator anim = player2Death.GetComponent<Animator>();
            if (anim != null)
            {
                anim.Play("DeathAnimation");
                Debug.Log("Animación de muerte para JUGADOR 2");
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
        //if (player == 1)
        //{
        //    //player1Idle.SetActive(idle);
        //    //player1Shoot.SetActive(shoot);
        //    //player1Death.SetActive(death);
        //}
        //else if (player == 2)
        //{
        //    player2Idle.SetActive(idle);
        //    player2Shoot.SetActive(shoot);
        //    player2Death.SetActive(death);
        //}
    }
}
