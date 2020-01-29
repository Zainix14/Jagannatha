using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// Script gerant l'enchainement des Waves dans une phase
///  | Sur le prefab EnemyManager(à instantié une fois)
///  | Auteur : Zainix
/// </summary>
public class SC_PhaseManager : MonoBehaviour
{
    PhaseSettings curPhaseSettings;
    SC_WaveManager waveManager;
    WaveSettings[] waves;


    int curWaveIndex;
    // Start is called before the first frame update
    void Start()
    {
        waveManager = GetComponent<SC_WaveManager>();
        resetVariables();

    }

    public void Initialize(PhaseSettings newPhaseSettigns)
    {

        curPhaseSettings = newPhaseSettigns;
        resetVariables();
        waves = newPhaseSettigns.waves;
        waveManager.InitializeWave(waves[curWaveIndex]);
    }

    // Update is called once per frame
    void Update()
    {
    
    }
           
    public void EndWave()
    {
        curWaveIndex++;
        if(curWaveIndex<waves.Length)
        {
            waveManager.InitializeWave(waves[curWaveIndex]);
        }
    }

    void resetVariables()
    {
        curWaveIndex = 0;
    }
}
