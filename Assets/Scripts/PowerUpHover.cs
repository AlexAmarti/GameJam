using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class PowerUpHover : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    private Animator animator;
    private UnityEngine.UI.Image image;

    public Color hoverColor = Color.white; // Color normal al hacer hover
    public Color defaultColor = Color.gray; // Color gris por defecto

    void Start()
    {
        animator = GetComponent<Animator>(); // Obtiene el Animator del power-up
        image = GetComponent<UnityEngine.UI.Image>(); // Obtiene la imagen
        image.color = defaultColor; // Inicia en gris
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        image.color = hoverColor; // Cambia el color a normal
        if (animator != null)
        {
            animator.SetBool("Hover", true); // Activa la animación
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        image.color = defaultColor; // Regresa a gris cuando se sale
        if (animator != null)
        {
            animator.SetBool("Hover", false); // Detiene la animación
        }
    }
}