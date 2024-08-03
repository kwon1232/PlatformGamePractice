using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Player : MonoBehaviour
{
    [SerializeField]
    public float speed = 5f;
    [SerializeField]
    public float jump = 4f;

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
