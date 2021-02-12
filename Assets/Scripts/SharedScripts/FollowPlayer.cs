﻿using UnityEngine;

public class FollowPlayer : MonoBehaviour
{
    [SerializeField] private Vector2 offSet;
    [SerializeField] private float damping;

    private GameObject player;

    private Vector3 velocity = Vector3.zero;

    private void Awake()
    {
        player = GameObject.Find("Player");
    }

    private void Update()
    {
        transform.position = Vector3.SmoothDamp(transform.position + (Vector3)offSet, player.transform.position, ref velocity, damping);
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawSphere(offSet, .01f);
    }
}