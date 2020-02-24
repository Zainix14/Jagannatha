using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SC_HitMarker : MonoBehaviour
{
    
    #region Singleton

    private static SC_HitMarker _instance;
    public static SC_HitMarker Instance { get { return _instance; } }

    #endregion

    public enum HitType { Normal, Critical };

    public void Awake()
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

    public void HitMark(HitType Type)
    {
        switch (Type)
        {

            case HitType.Normal :

                break;

            case HitType.Critical:

                break;

        }
    }

}
