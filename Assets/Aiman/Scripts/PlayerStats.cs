using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;

public class PlayerStats : MonoBehaviour
{
    public float health;
    public float speed;
    public float damage;

    private PlayerController playerController;

    private void Awake()
    {
        playerController = GetComponent<PlayerController>();
        if (playerController == null)
        {
            Debug.LogError("PlayerController component not found on the Player!");
        }
    }

    public void UpdateSpeed(float newSpeed)
    {
        Debug.Log($"Updating speed from {speed} to {newSpeed}");
        speed = newSpeed;

        if (playerController != null)
        {
            playerController.SetMoveSpeed(speed);
            Debug.Log($"PlayerController moveSpeed updated to: {speed}");
        }
    }


    public IEnumerator ApplySpeedBoost(PlayerStats playerStats, float speedIncrease, float duration)
    {
        float originalSpeed = playerStats.speed; // Store the original speed
        playerStats.UpdateSpeed(playerStats.speed + speedIncrease); // Increase speed
        Debug.Log($"Speed increased to: {playerStats.speed}");

        float elapsedTime = 0f;
        while (elapsedTime < duration)
        {
            elapsedTime += Time.deltaTime;
            Debug.Log($"Elapsed Time: {elapsedTime}");
            yield return null;
        }

        Debug.Log("Speed boost duration ended. Resetting speed...");
        playerStats.UpdateSpeed(originalSpeed); // Reset speed to original
        Debug.Log($"Speed reset to: {playerStats.speed}");
    }
}
