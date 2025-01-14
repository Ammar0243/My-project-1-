using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

namespace ConsumableScript
{
    public class RandomSpawner2 : MonoBehaviour
    {
        public GameObject ItemPreFab;
        public float Radius = 1;
        public Vector3 SpawnCenter; // New variable for the spawn center
        public float spawnInterval = 10f; // Time interval for spawning
        public float despawnTime = 15f; //Time interval for despawn

        void Start()
        {
            // Start the coroutine to spawn objects at intervals
            StartCoroutine(SpawnObjectAtInterval());
        }

        void SpawnObjectAtRandom()
        {
            // Generate a random position within the circle and offset it by the SpawnCenter
            Vector2 randomPos2D = Random.insideUnitCircle * Radius;
            Vector3 randomPos = new Vector3(randomPos2D.x, 0, randomPos2D.y) + SpawnCenter;

            Instantiate(ItemPreFab, randomPos, Quaternion.identity);
        }

        IEnumerator SpawnObjectAtInterval()
        {
            // Loop to spawn objects every `spawnInterval` seconds
            while (true)
            {
                SpawnObjectAtRandom();
                yield return new WaitForSeconds(spawnInterval);
            }
        }

        IEnumerator DespawnObjectAfterDelay(GameObject obj)
        {
            // Wait for the specified despawn time
            yield return new WaitForSeconds(despawnTime);

            // Destroy the object if it still exists
            if (obj != null)
            {
                Destroy(obj);
            }
        }

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.green;
            Gizmos.DrawWireSphere(SpawnCenter, Radius); // Use SpawnCenter instead of this.transform.position
        }
    }
}