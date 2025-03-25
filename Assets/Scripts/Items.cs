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
        [TextArea] public string description; // Descripci�n del objeto
        public Sprite image; // Imagen del objeto
    }

    public Item[] items; // Lista de objetos a mostrar
    public GameObject itemPrefab; // Prefab para cada �tem (Panel con Imagen y Descripci�n)
    public GameObject buttonPrefab; // Prefab para cada bot�n
    public Transform parentContainer; // Contenedor de los �tems
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

            // Instancia el �tem (imagen + descripci�n)
            GameObject newItem = Instantiate(itemPrefab, parentContainer);
            newItem.transform.Find("ItemImage").GetComponent<Image>().sprite = items[i].image;
            newItem.transform.Find("ItemName").GetComponent<TMP_Text>().text = items[i].name;
            newItem.transform.Find("ItemDescription").GetComponent<TMP_Text>().text = items[i].description;

            // Instancia el bot�n
            GameObject newButton = Instantiate(buttonPrefab, buttonContainer);
            newButton.GetComponentInChildren<TMP_Text>().text = items[i].name;

            // Asigna evento al bot�n para mostrar el �tem al hacer clic
            newButton.GetComponent<Button>().onClick.AddListener(() => ShowItem(index));
        }
    }

    void ShowItem(int index)
    {
        Debug.Log("Seleccionaste: " + items[index].name);
    }
}
