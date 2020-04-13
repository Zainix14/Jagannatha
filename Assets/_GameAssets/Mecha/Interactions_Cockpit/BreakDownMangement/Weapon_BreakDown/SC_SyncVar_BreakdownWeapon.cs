using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class SC_SyncVar_BreakdownWeapon : NetworkBehaviour
{

    #region Singleton

    private static SC_SyncVar_BreakdownWeapon _instance;
    public static SC_SyncVar_BreakdownWeapon Instance { get { return _instance; } }

    #endregion

    public int tourbilolNb = 2;

    public struct Tourbilol
    {
        public float value;
        public float valueWanted;
        public bool isEnPanne;

        public Tourbilol(float v1, float v2, bool v3) : this()
        {
            this.value = v1;
            this.valueWanted = v2;
            this.isEnPanne = v3;
        }

        public void setValue(float newValue)
        {
            this.value = newValue;
        }

        public void setValueWanted(float newValue)
        {
            this.valueWanted = newValue;
        }

        public void setIsEnPanne(bool newValue)
        {
            this.isEnPanne = newValue;
        }

    }

    public class SyncListTourbilols : SyncListStruct<Tourbilol>
    {

    }

    public SyncListTourbilols SL_Tourbilols = new SyncListTourbilols();

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
            //init des tourbilol

            for (int i = 0; i <tourbilolNb; i++)
            {
                SL_Tourbilols.Add(new Tourbilol(0f, 0f, false));
            }

        }
    }

    /// FONCTIONS DE SET dans la list de struc des slider, can we make it more compact?
    public void TourbilolChangeValue(int index, float newValue)
    {
        Tourbilol tourbilol = SL_Tourbilols[index];
        tourbilol.setValue(newValue);
        SL_Tourbilols.RemoveAt(index);
        SL_Tourbilols.Insert(index, tourbilol);

    }

    public void TourbilolChangeValueWanted(int index, float newValue)
    {
        Tourbilol slider = SL_Tourbilols[index];
        slider.setValueWanted(newValue);
        SL_Tourbilols.RemoveAt(index);
        SL_Tourbilols.Insert(index, slider);

    }

    public void TourbilolChangeIsPanne(int index, bool newValue)
    {
        Tourbilol slider = SL_Tourbilols[index];
        slider.setIsEnPanne(newValue);
        SL_Tourbilols.RemoveAt(index);
        SL_Tourbilols.Insert(index, slider);

    }

}
