using UnityEngine;

public class HealthComponent : MonoBehaviour
{

    public float currentHealth;
    public float maxHealth;
    public bool isAlive;

    void Start()
    {
        currentHealth = maxHealth;
        isAlive = true;
    }

    void Update()
    {
        
    }


    public void DamageObject(float damageTaken)
    {
        currentHealth -= damageTaken;
        print(currentHealth);

        if (currentHealth <= 0)
        {
            isAlive = false;
            Destroy(gameObject);
        }
    }
}
