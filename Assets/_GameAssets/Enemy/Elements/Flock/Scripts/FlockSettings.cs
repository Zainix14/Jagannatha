using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class FlockSettings : ScriptableObject
{
    [Header ("Attack")]
    public bool attackCity;
    public bool attackPlayer;

    
    
    public enum PathPreference
    {
        Line,
        Circle
    }

    public PathPreference pathPreference;

    [Tooltip("inSecond")]
    public int timeBeforeAttack;

    [Tooltip("inSecond")]
    public float attackDuration;

    [Header("Boids")]

    [Range(10,100)]
    public int boidSpawn;

    [Range(10,200)]
    public int maxBoid;

    [Tooltip("boids per 10sec")]
    public int regenerationRate;




}