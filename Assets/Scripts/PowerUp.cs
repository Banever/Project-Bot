using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PowerUp : MonoBehaviour
{
    public Basemovement player;

    private void Awake()
    {

        player = FindObjectOfType<Basemovement>();
    }

    public void OnCollisionEnter2D(Collision2D Col)
    {
        player.Boots = true;
        Destroy(gameObject);
        
    }
}
