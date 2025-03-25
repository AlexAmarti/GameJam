using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Items : MonoBehaviour
{
    [System.Serializable]
    public class Item
    {
        public string name; // Nombre del objeto
        [TextArea] public string description; // Descripción del objeto
        public Sprite image; // Imagen del objeto
    }

    public Item[] items; // Lista de objetos a mostrar
    public GameObject itemPrefab; // Prefab para cada ítem (Panel con Imagen y Descripción)
    public GameObject buttonPrefab; // Prefab para cada botón
    public Transform parentContainer; // Contenedor de los ítems
    public Transform buttonContainer; // Contenedor de los botones

    void Start()
    {
        GenerateItems();
    }

    void GenerateItems()
    {
        for (int i = 0; i < items.Length; i++)
        {
            int index = i; // Necesario para evitar problemas de referencia en lambdas

            // Instancia el ítem (imagen + descripción)
            GameObject newItem = Instantiate(itemPrefab, parentContainer);
            newItem.transform.Find("ItemImage").GetComponent<Image>().sprite = items[i].image;
            newItem.transform.Find("ItemName").GetComponent<TMP_Text>().text = items[i].name;
            newItem.transform.Find("ItemDescription").GetComponent<TMP_Text>().text = items[i].description;

            // Instancia el botón
            GameObject newButton = Instantiate(buttonPrefab, buttonContainer);
            newButton.GetComponentInChildren<TMP_Text>().text = items[i].name;

            // Asigna evento al botón para mostrar el ítem al hacer clic
            newButton.GetComponent<Button>().onClick.AddListener(() => ShowItem(index));
        }
    }

    void ShowItem(int index)
    {
        Debug.Log("Seleccionaste: " + items[index].name);
    }
}
