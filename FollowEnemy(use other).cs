using UnityEngine;

public class EnemyFollow : MonoBehaviour
{
    public string playerTag = "Player";
    public float moveSpeed = 3f;

    private Transform playerTransform;

    void Start()
    {
        // Find the player GameObject using the tag
        GameObject playerObject = GameObject.FindGameObjectWithTag(playerTag);

        if (playerObject != null)
        {
            // Get the player's Transform component
            playerTransform = playerObject.transform;
        }
        else
        {
            Debug.LogError("Player not found. Make sure the player has the tag: " + playerTag);
        }
    }

    void Update()
    {
        if (playerTransform != null)
        {
            // Calculate the direction to the player
            Vector3 direction = playerTransform.position - transform.position;
            direction.Normalize();

            // Move towards the player
            transform.Translate(direction * moveSpeed * Time.deltaTime);
        }
    }
}
