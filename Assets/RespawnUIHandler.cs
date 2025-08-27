using System;
using UnityEngine;
using UnityEngine.LightTransport;
using UnityEngine.UI;
using random = UnityEngine.Random;

public class RespawnUIHandler : MonoBehaviour
{
    public PlayerMovement player;
    public Text PlayerHealthText;
    public Transform worldFloor;
    public Button respawnButton;
    public SavingSystem savingSystem;

    void Start()
    {
        respawnButton.onClick.AddListener(OnRespawnButtonPressed);
        gameObject.SetActive(false);
    }


    private void OnRespawnButtonPressed()
    {
        player.isPlayerAlive = true;

        HealthComponent playerHealth = player.GetComponent<HealthComponent>();
        playerHealth.currentHealth = playerHealth.maxHealth;
        PlayerHealthText.text = (playerHealth.currentHealth).ToString() + "/" + (playerHealth.currentHealth).ToString();

        SpawnPlayerAtRandomLocation();

        SavingModel emptyModel = new SavingModel();
        savingSystem.CreateEmptyInventory(emptyModel);
        gameObject.SetActive(false);
    }


    private void SpawnPlayerAtRandomLocation()
    {
        print("Spawned");
        float randomXPos = random.Range(worldFloor.position.x - 5f, worldFloor.position.x + 5f);
        float randomZPos = random.Range(worldFloor.position.z - 5f, worldFloor.position.z + 5f);

        player.gameObject.transform.position = new Vector3(randomXPos, worldFloor.position.y + player.playerHeight / 2, randomZPos);
    }
}
