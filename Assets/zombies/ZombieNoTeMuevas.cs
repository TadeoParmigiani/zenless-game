﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieNoTeMuevas : MonoBehaviour
{
    public GameObject zom;
    // Start is called before the first frame update
    void Start()
    {
        transform.position = new Vector3(zom.transform.position.x, zom.transform.position.y, zom.transform.position.z);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
