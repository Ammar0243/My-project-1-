using UnityEngine;

public class PlayerSortingLayerController : MonoBehaviour
{
    [SerializeField] private Transform prefabTransform; // Reference to the prefab's transform
    private SpriteRenderer playerRenderer; // Player's SpriteRenderer
    private SpriteRenderer prefabRenderer; // Prefab's SpriteRenderer

    private void Start()
    {
        // Get the player's SpriteRenderer
        playerRenderer = GetComponent<SpriteRenderer>();

        // Get the prefab's SpriteRenderer
        if (prefabTransform != null)
        {
            prefabRenderer = prefabTransform.GetComponent<SpriteRenderer>();
        }

        // Error handling
        if (playerRenderer == null)
        {
            Debug.LogError("Player SpriteRenderer is missing!");
        }

        if (prefabTransform == null)
        {
            Debug.LogError("Prefab Transform is not assigned!");
        }
        else if (prefabRenderer == null)
        {
            Debug.LogError("Prefab SpriteRenderer is missing!");
        }
    }

    private void Update()
    {
        if (prefabTransform != null && playerRenderer != null && prefabRenderer != null)
        {
            // Compare Y positions
            if (transform.position.y > prefabTransform.position.y)
            {
                // Player is above the prefab, render below it
                playerRenderer.sortingOrder = prefabRenderer.sortingOrder - 1;
            }
            else
            {
                // Player is below the prefab, render above it
                playerRenderer.sortingOrder = prefabRenderer.sortingOrder + 1;
            }
        }
    }
}
