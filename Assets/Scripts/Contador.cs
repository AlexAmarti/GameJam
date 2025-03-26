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

    // Nuevas variables para las posiciones de los jugadores
    public Transform player1WinPosition;
    public Transform player2WinPosition;

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

        // Mensajes de tiempo de reacción de ambos jugadores
        Debug.Log("Tiempo de reacción del JUGADOR 1: " + player1ReactionTime.ToString("F3") + " segundos");
        Debug.Log("Tiempo de reacción del JUGADOR 2: " + player2ReactionTime.ToString("F3") + " segundos");

        // Determina el ganador y activa la animación de muerte del perdedor
        if (player1ReactionTime < player2ReactionTime)
        {
            Debug.Log("JUGADOR 1 GANA LA RONDA");
            ActivateDeathAnimation(2); // Jugador 2 pierde
            MoveWinningPlayer(1); // Cambia la posición z del Jugador 1
        }
        else if (player2ReactionTime < player1ReactionTime)
        {
            Debug.Log("JUGADOR 2 GANA LA RONDA");
            ActivateDeathAnimation(1); // Jugador 1 pierde
            MoveWinningPlayer(2); // Cambia la posición z del Jugador 2
        }
        else
        {
            Debug.Log("¡Empate! Nadie gana esta ronda.");
        }

        // Mensaje de instrucciones para continuar
        Debug.Log("Presiona ENTER para continuar...");

        EndRound();
        mensajeMostrado = true;
    }
    void MoveWinningPlayer(int winningPlayer)
    {
        if (objetoAMover != null)
        {
            Vector3 nuevaPosicion = objetoAMover.position;
            nuevaPosicion.z += 8; // Aumentar la posición en Z
            objetoAMover.position = nuevaPosicion;
            Debug.Log($"El objeto se ha movido a la nueva posición Z: {nuevaPosicion.z}");
        }

        if (winningPlayer == 1 && player1WinPosition != null)
        {
            Vector3 newPosition = player1WinPosition.position;
            newPosition.z = -1; // Cambia la posición Z cuando gana el Jugador 1
            player1WinPosition.position = newPosition;
        }
        else if (winningPlayer == 2 && player2WinPosition != null)
        {
            Vector3 newPosition = player2WinPosition.position;
            newPosition.z = -1; // Cambia la posición Z cuando gana el Jugador 2
            player2WinPosition.position = newPosition;
        }
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
    }
}
