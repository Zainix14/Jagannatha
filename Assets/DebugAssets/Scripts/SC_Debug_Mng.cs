using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SC_Debug_Mng : MonoBehaviour
{
    #region Singleton

    private static SC_Debug_Mng _instance;
    public static SC_Debug_Mng Instance { get { return _instance; } }

    #endregion

    public bool b_weapon_Cheatcode = false;

    [SerializeField]
    [Range(0, 100)]
    public int powerPerCent;

    private void Awake()
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
        if(Input.GetKeyDown(KeyCode.Comma))
        {

            if (b_weapon_Cheatcode)
            {
                b_weapon_Cheatcode = false;
            }

            else
            {
                if (!b_weapon_Cheatcode) b_weapon_Cheatcode = true;
            }
        }
        
    }
}
