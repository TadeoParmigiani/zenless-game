using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Inventario : MonoBehaviour
{
    public static Inventario Instance;
    public List<Item> Items = new List<Item>();

    public Transform ItemContent;
    public GameObject InventoryItem; 
    public GameObject canvas;

    public void Start()
    {
        canvas.SetActive(false);
        Debug.Log(!canvas.activeSelf);
    } 
    private void Awake()
    {
        Instance = this;
    }

     void Update()
    {
        // Verificar si se presiona la tecla "i"
        if (Input.GetKeyDown(KeyCode.I))
        {
            // Alternar la activación/desactivación del Canvas
            canvas.SetActive(!canvas.activeSelf);
        }
    }
    public void Add(Item item)
    {
        Items.Add(item);
    }
    public void Remove(Item item)
    {
        Items.Remove(item);
    }
    public void ListItems()
    {
        foreach(var item in Items)
        {
            
            GameObject obj = Instantiate(InventoryItem, ItemContent);
            Debug.Log(obj.name);
            //var itemName = obj.transform.Find("ItemName").GetComponent<Text>();
            //var itemIcon = obj.transform.Find("ItemIcon").GetComponent<Image>();
            //Debug.Log(itemName);
            //itemName.name = item.itemName;
            //itemIcon.prite = item.icon;
        }
    }
}
