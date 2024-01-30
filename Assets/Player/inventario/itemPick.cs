using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class itemPick : MonoBehaviour
{
    public Item Item; 
    // Start is called before the first frame update
    void Start()
    {
        
    }
    void PickUp()
    {
        Inventario.Instance.Add(Item);
        Destroy(gameObject);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            PickUp();
        }
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
