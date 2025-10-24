using System;
using UnityEngine;

public class HealthComponent : MonoBehaviour
{
    public event Action<float, float> OnObjectHit;
    public float currentHealth;
    public float maxHealth;
    public bool isAlive = false;

    void Start()
    {
        currentHealth = maxHealth;
        isAlive = true;
    }


    public void DamageObject(float damageTaken)
    {
        currentHealth -= damageTaken;

        OnObjectHit?.Invoke(currentHealth, maxHealth);

        if (currentHealth <= 0)
        {
            isAlive = false;
        }
    }
}
