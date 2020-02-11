using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class FlockSettings : ScriptableObject
{
    [Header ("Attack")]
    public bool attackCity;
    public bool attackPlayer;

    [Header("BoidSettings")]
    [Tooltip("Index 0 : Roam | Index 1 : Direction Joueur | Index 2 : Attaque autour du joueur")]
    public BoidSettings[] boidSettings;

    [Header("Spawn Position")]
    public Vector3 spawnPosition = new Vector3(0,0,0);


    [Header("Spline Settings")]
    public float speedOnSpline;

    [Header("Attack Settings")]
    [Range(0, 2)]
    public float speedToPlayer = 1;



    public enum PathPreference
    {
        Line,
        Circle
    }

    public PathPreference pathPreference;

    [Tooltip("in Second")]
    public int timeBeforeAttack;

    [Tooltip("in Second")]
    public float attackDuration;

    [Header("Boids")]

    [Range(10,200)]
    public int boidSpawn;

    [Range(10,200)]
    public int maxBoid;

    [Tooltip("boids per 10sec")]
    public int regenerationRate;




}