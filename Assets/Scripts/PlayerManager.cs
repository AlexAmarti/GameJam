using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    public Animator player1Animator;
    public Animator player2Animator;

    public bool player1Dead = false;
    public bool player2Dead = false;

    void Update()
    {
        CheckGameState();
    }

    void CheckGameState()
    {
        if (player1Dead)
        {
            player1Animator.SetTrigger("Death");  // Activar animación de muerte en el jugador 1
        }
        else if (player2Dead)
        {
            player2Animator.SetTrigger("Death");  // Activar animación de muerte en el jugador 2
        }
    }
}