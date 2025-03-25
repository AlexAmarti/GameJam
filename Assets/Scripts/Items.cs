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
    public GameObject itemPrefab; // Prefab para mostrar cada ítem
    public Transform parentContainer; // Contenedor donde se instanciarán los ítems

    void Start()
    {
        GenerateItems();
    }

    void GenerateItems()
    {
        foreach (Item item in items)
        {
            GameObject newItem = Instantiate(itemPrefab, parentContainer); // Instancia el prefab
            newItem.transform.Find("ItemImage").GetComponent<Image>().sprite = item.image;
            newItem.transform.Find("ItemName").GetComponent<TMP_Text>().text = item.name;
            newItem.transform.Find("ItemDescription").GetComponent<TMP_Text>().text = item.description;
        }
    }
}
