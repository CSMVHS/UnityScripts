using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFollow : MonoBehaviour
{
    CharacterController _controller;
    Transform target;
    GameObject player;

    [SerializeField]
    float _moveSpeed = 5.0f;
    float followRange = 10.0f;

    void Start()
    {
        player = GameObject.FindWithTag("Player"); 
        target = player.transform;
        _controller = GetComponent<CharacterController>();
    }

    void Update()
    {
        // Calculate the distance between enemy and player
        float distanceToPlayer = Vector3.Distance(transform.position, target.position);

        // Check if the player is within the follow range
        if (distanceToPlayer <= followRange)
        {
            // Calculate the direction to move towards the player
            Vector3 direction = target.position - transform.position;
            direction = direction.normalized;

            // Move towards the player
            Vector3 velocity = direction * _moveSpeed;
            _controller.Move(velocity * Time.deltaTime);
        }
    }
}
