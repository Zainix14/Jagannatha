﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class WaveSettings : ScriptableObject
{

    [Header("Inital Spawn")]
    public FlockSettings[] initialSpawnFlock;
    public int[] initialSpawnPosition;
    public float timeBetweenSpawnInitial;

    [Header("Backup")]
    public bool backup;
    public FlockSettings[] backupSpawnFlock;
    public int[] backupSpawnPosition;

    [Tooltip("-1 if no timer condition wanted")]
    public float timeBeforeBackup;
    [Tooltip("-1 if no dead condition wanted")]
    public float flockLeftBeforeBackup;

    public float timeBetweenSpawnBackup;





}