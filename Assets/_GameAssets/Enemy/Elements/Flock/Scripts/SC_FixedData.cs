using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SC_FixedData : MonoBehaviour
{
    #region Singleton

    private static SC_FixedData _instance;
    public static SC_FixedData Instance { get { return _instance; } }

    #endregion

    [SerializeField]
    BoidSettings[] boidSettings;

    void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(gameObject);
        }

        else
        {
            _instance = this;
        }
    }

    public int GetBoidIndex(BoidSettings curboidSettings)
    {
        int index = 0;
        for(int i =0; i<boidSettings.Length;i++)
        {
            if (curboidSettings == boidSettings[i])
                index = i;
        }

        return index;
    } 
    
    public BoidSettings GetBoidSettings(int index)
    {
        return boidSettings[index];
    }

}
