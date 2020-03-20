using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class SC_SyncVar_BreakdownMovement : NetworkBehaviour, IF_SyncVar_Sliders
{

    #region Singleton

    private static SC_SyncVar_BreakdownMovement _instance;
    public static SC_SyncVar_BreakdownMovement Instance { get { return _instance; } }

    #endregion




    /// ////////////////////////////////////////////////////////////////////// /// //////////////////////////////////////////////////////////////////////
    /// ////////////////////////////////////////////////////////////////////// /// //////////////////////////////////////////////////////////////////////


    /// <summary>
    /// //////////////////////////////////List struct des switches
    /// </summary>

    //nombre de switches pour l'init
    public int switchesNb = 3;

    public struct Switch
    {
        public bool value;
        public bool valueWanted;
        public bool isEnPanne;


        public Switch(bool v1, bool v2, bool v3) : this()
        {
            this.value = v1;
            this.valueWanted = v2;
            this.isEnPanne = v3;
        }

        public void setValue(bool newValue)
        {
            this.value = newValue;
        }

        public void setValueWanted(bool newValue)
        {
            this.valueWanted = newValue;
        }

        public void setIsEnPanne(bool newValue)
        {
            this.isEnPanne = newValue;
        }

    }

    public class SyncListSwitches : SyncListStruct<Switch>
    {

    }

    public SyncListSwitches SL_switches = new SyncListSwitches();



    /// ////////////////////////////////////////////////////////////////////// /// //////////////////////////////////////////////////////////////////////
    /// ////////////////////////////////////////////////////////////////////// /// //////////////////////////////////////////////////////////////////////



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


    void Start()
    {

        
        if (isServer)
        {

            //init des Switches

            for (int i = 0; i < switchesNb; i++)
            {
                SL_switches.Add(new Switch(false, false, false));
            }
        }

        



    }

    // Update is called once per frame
    void Update()
    {

    }



    /// FONCTIONS DE SET dans la list de struc des switches, can we make it more compact?
    public void SwitchChangeValue(int index, bool newValue)
    {
        Switch aSwitch = SL_switches[index];
        aSwitch.setValue(newValue);
        SL_switches.RemoveAt(index);
        SL_switches.Insert(index, aSwitch);

    }
    public void SwitchChangeValueWanted(int index, bool newValue)
    {
        Switch aSwitch = SL_switches[index];
        aSwitch.setValueWanted(newValue);
        SL_switches.RemoveAt(index);
        SL_switches.Insert(index, aSwitch);

    }
    public void SwitchChangeIsPanne(int index, bool newValue)
    {
        Switch aSwitch = SL_switches[index];
        aSwitch.setIsEnPanne(newValue);
        SL_switches.RemoveAt(index);
        SL_switches.Insert(index, aSwitch);

    }

    


}
