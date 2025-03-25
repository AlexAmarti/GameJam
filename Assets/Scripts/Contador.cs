using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Contador : MonoBehaviour
{
    public string nombreDeEscena = "TIENDA"; // Puedes cambiar esto en el Inspector
    private bool escenaListaParaCargar = false; // Indica si la ronda terminó


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


    //public GameObject player1Idle;
    //public GameObject player1Shoot;
    //public GameObject player1Death;

    public GameObject player2Idle;
    public GameObject player2Shoot;
    public GameObject player2Death;

    public Transform objetoAMover;  // Asigna este objeto en el Inspector


    void Start()
    {
        SetNewRandomTime();

        SetIdleAnimation();
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

                // Cambiar la posición Z del objeto
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
                Debug.Log("JUGADOR 1 reaccionó en: " + player1ReactionTime.ToString("F3") + " segundos");
            }

            if (!player2Reacted && !player2Blocked && Input.GetMouseButtonDown(0))
            {
                player2ReactionTime = Time.time - reactionStartTime;
                player2Reacted = true;
                ActivateShootAnimation(2);
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
                DetermineWinner();
                EndRound();
            }
        }
        if (escenaListaParaCargar && Input.GetKeyDown(KeyCode.Return))
        {
            Debug.Log("Cargando la escena: " + nombreDeEscena);
            SceneManager.LoadScene(nombreDeEscena); // Cambia de escena cuando se presiona "Enter"
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

        EndRound(); // Marcar la ronda como finalizada
    }

    void ActivateDeathAnimation(int losingPlayer)
    {
        if (losingPlayer == 1)
        {
            SetActivePlayerState(1, false, false, true);
            Debug.Log("Activando animación de muerte para JUGADOR 1");
        }
        else if (losingPlayer == 2)
        {
            SetActivePlayerState(2, false, false, true);
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
            escenaListaParaCargar = true; // Habilita el cambio de escena con "Enter"
            Debug.Log("Ronda finalizada. Presiona ENTER para continuar...");
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
        if (player == 1)
        {
            //player1Idle.SetActive(idle);
            //player1Shoot.SetActive(shoot);
            //player1Death.SetActive(death);
        }
        else if (player == 2)
        {
            player2Idle.SetActive(idle);
            player2Shoot.SetActive(shoot);
            player2Death.SetActive(death);
        }
    }
}
