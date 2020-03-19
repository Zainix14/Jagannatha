using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class SC_SyncVar_vision_arrow : NetworkBehaviour
{

    #region Singleton

    private static SC_SyncVar_vision_arrow _instance;
    public static SC_SyncVar_vision_arrow Instance { get { return _instance; } }

    #endregion

    [SyncVar]
    public float rotCasque;


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

    // Start is called before the first frame update
    void Start()
    {
        


    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
