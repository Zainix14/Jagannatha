using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class WaveSettings : ScriptableObject
{

    [Header("Inital Spawn")]
    public FlockSettings[] initialSpawnFlockType;
    public int[] initialSpawnFlockQuantity;
    public float timeBetweenSpawnInitial;


    public float getInitialFlockNumber()
    {
        int curNumber = 0;
        for(int i =0; i<initialSpawnFlockType.Length; i++)
        {
            for(int j =0; j<initialSpawnFlockQuantity[i]; j++)
            {
                curNumber++;
            }
        }
        return curNumber;
    }


    [Header("Backup")]
    public bool backup;
    public float timeBetweenSpawnBackup;

    public FlockSettings[] backupSpawnFlockType;
    public int[] backupSpawnFlockQuantity;


    [Tooltip("-1 if no timer condition wanted")]
    public float timeBeforeBackup;
    [Tooltip("-1 if no dead condition wanted")]
    public float flockLeftBeforeBackup;

    public float getBackupFlockNumber()
    {
        int curNumber = 0;
        for (int i = 0; i < backupSpawnFlockType.Length; i++)
        {
            for (int j = 0; j < backupSpawnFlockQuantity[i]; j++)
            {
                curNumber++;
            }
        }
        return curNumber;
    }



}