using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Player : MonoBehaviour
{
    [SerializeField]
    public float speed = 5f;
    [SerializeField]
    public float jump = 4f;
    [SerializeField]
    public double maxHp = 50;
    [SerializeField]
    public double curHp = 50;

    public PlayerInputSystem playerinputSystem;

    void Start()
    {
        playerinputSystem = GetComponent<PlayerInputSystem>();
    }

    public void Jump()
    {

    }
    void Update()
    {
        
    }
}
