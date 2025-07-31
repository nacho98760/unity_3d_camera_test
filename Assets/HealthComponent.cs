using UnityEngine;

public class HealthComponent : MonoBehaviour
{

    public int currentHealth;
    public int maxHealth;
    public bool isAlive;

    void Start()
    {
        currentHealth = maxHealth;
        isAlive = true;
    }

    void Update()
    {
        
    }


    public void DamageObject(int damageTaken)
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
