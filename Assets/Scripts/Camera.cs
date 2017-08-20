using UnityEngine;

public class Camera : MonoBehaviour
{
    private Transform player;

    void Start()
    {
        player = GameObject.Find("Player").transform;
    }

    void Update()
    {
        Vector3 playerPosition = player.position;
        playerPosition.z = transform.position.z;
        transform.position = playerPosition;
    }
}