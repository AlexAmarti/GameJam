using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Items : MonoBehaviour
{
    [Header("Referencia a Componentes UI")]
    public Text nombreText;  // Campo de texto para el nombre
    public Text descripcionText;  // Campo de texto para la descripci�n
    public Image iconoImage;  // Imagen del �tem

    [Header("Datos del �tem")]
    public string nombre;
    [TextArea] public string descripcion;
    public Sprite icono;

    void Start()
    {
        // Actualiza los valores en la UI al iniciar
        ActualizarUI();
    }

    public void ActualizarUI()
    {
        if (nombreText != null)
            nombreText.text = nombre;

        if (descripcionText != null)
            descripcionText.text = descripcion;

        if (iconoImage != null)
            iconoImage.sprite = icono;
    }

    // M�todo para cambiar los valores din�micamente desde otro script
    public void ConfigurarItem(string nuevoNombre, string nuevaDescripcion, Sprite nuevoIcono)
    {
        nombre = nuevoNombre;
        descripcion = nuevaDescripcion;
        icono = nuevoIcono;

        ActualizarUI();
    }
}
