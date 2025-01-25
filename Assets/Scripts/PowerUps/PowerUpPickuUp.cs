using System;
using UnityEditor;
using UnityEngine;

public class PowerUpPickuUp : MonoBehaviour
{
    public string powerUpName;
    Type powerUpType;
    bool toOpponent;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        powerUpType = Type.GetType(powerUpName);
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("PowerUp");
        if (other.CompareTag("Player"))
        {
            Debug.Log("Player");
            other.gameObject.AddComponent(powerUpType);
            Destroy(gameObject);
        }
    }
}

