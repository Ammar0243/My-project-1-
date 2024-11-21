using System.Collections;
using UnityEngine;

namespace ConsumableScript
{
    public enum PotionType
    {
        Health,
        Speed,
        Strength
    }

    public class PotionBehaviour2 : MonoBehaviour
    {
        public PotionType potionType; // Set the type of potion in the Inspector
        public float despawnTime = 15f; // Time interval for despawn

        private void Start()
        {
            StartCoroutine(DespawnObjectAfterDelay());
        }

        private IEnumerator DespawnObjectAfterDelay()
        {
            yield return new WaitForSeconds(despawnTime);
            Destroy(gameObject); // Destroy the potion after its despawn time
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.CompareTag("Player"))
            {
                ApplyEffect(other.gameObject);
                Destroy(gameObject); // Destroy the potion after applying the effect
            }
        }

        private void ApplyEffect(GameObject player)
        {
            PlayerStats playerStats = player.GetComponent<PlayerStats>();
            PlayerHealth playerHealth = player.GetComponent<PlayerHealth>();
            if (playerStats == null)
            {
                Debug.LogWarning("Player does not have a PlayerStats component!");
                return;
            }

            // Apply effect based on the potion type
            switch (potionType)
            {
                case PotionType.Health:

                    Debug.Log("Applying Health");
                    if (playerHealth.health == 100)
                    {
                        Debug.Log("No effect");
                        break;
                    }
                    else
                    {
                        playerHealth.health += 10; // Increase health
                        Debug.Log("Health increased by 10!");
                        Debug.Log(playerStats.health);
                        break;
                    }

                case PotionType.Speed:
                    Debug.Log("Applying speed boost...");
                    playerStats.StartCoroutine(playerStats.ApplySpeedBoost(playerStats, 1f, 5f)); // Start coroutine on PlayerStats
                    break;

                case PotionType.Strength:
                    playerStats.damage += 1; // Increase strength
                    Debug.Log("Damage increased by 1!");
                    Debug.Log(playerStats.damage);
                    break;

                default:
                    Debug.LogWarning("Unknown potion type!");
                    break;
            }

            Destroy(gameObject); // Destroy the potion
        }

        private IEnumerator ApplySpeedBoost(PlayerStats playerStats, float speedIncrease, float duration)
        {
            float originalSpeed = playerStats.speed; // Store the original speed
            playerStats.UpdateSpeed(playerStats.speed + speedIncrease); // Increase speed
            Debug.Log($"Speed increased by {speedIncrease}!");

            yield return new WaitForSeconds(duration); // Wait for the duration

            playerStats.UpdateSpeed(originalSpeed); // Reset speed to original
            Debug.Log("Speed has returned to normal.");
        }
    }
}
