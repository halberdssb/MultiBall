using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * Handles ball spawning in game space
 *
 * Jeff Stevenson
 * 4.14.26
 */
public class BallSpawner : MonoBehaviour
{
    [SerializeField]
    private GameObject ballPrefab;

    [SerializeField] private float defaultBallSpeed;

    public void SpawnBall()
    {
        SpawnBall(CollisionBounds.CenterOfBounds, defaultBallSpeed);
    }

    public void SpawnBall(Vector3 spawnPos, float spawnSpeed)
    {
        ballPrefab = Instantiate(ballPrefab, spawnPos, Quaternion.identity);
        Ball spawnedBall = ballPrefab.GetComponent<Ball>();
        spawnedBall.Speed = spawnSpeed;
    }

    private void Awake()
    {
        // spawn one ball at start
        SpawnBall();
    }
}
