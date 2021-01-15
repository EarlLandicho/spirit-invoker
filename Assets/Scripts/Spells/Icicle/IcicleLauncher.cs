﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IcicleLauncher : MonoBehaviour
{
    [SerializeField] GameObject icicleProjetile;
    [SerializeField] float launchDelay;
    [SerializeField] int numberOfProjectiles;

    private float angle;

    void Awake()
    {
        if (Input.GetKey(KeyCode.W))
        {
            angle = 90;
        }
        else if (Input.GetKey(KeyCode.S))
        {
            angle = -90;
        }



        InvokeRepeating("Launch", 0f, launchDelay);
    }

    private void Launch()
    {
        numberOfProjectiles--;

        Instantiate(icicleProjetile, transform.position + new Vector3(Random.Range(-.1f, .1f) , Random.Range(-.1f, .1f)), transform.rotation).GetComponent<IcicleLaunch>().Launch(angle);

        if(numberOfProjectiles <= 0)
        {
            CancelInvoke();
            Destroy(gameObject);
        }
    }



}
