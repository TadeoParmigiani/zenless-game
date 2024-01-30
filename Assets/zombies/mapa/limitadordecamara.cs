using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class limitadordecamara : MonoBehaviour
{
    public GameObject Player;

    void Update()
    {
        transform.position = new Vector3(Player.transform.position.x, 40, Player.transform.position.z);
    }
}
