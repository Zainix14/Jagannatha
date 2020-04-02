using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SC_Weapon_MechState : MonoBehaviour
{
    #region Singleton

    private static SC_Weapon_MechState _instance;
    public static SC_Weapon_MechState Instance { get { return _instance; } }

    #endregion

    [SerializeField]
    SC_UI_SystmShield _SystmShield;
    [SerializeField]
    SC_UI_SystmShield _WeaponEnergyLevel;

    [SerializeField]
    Image _Amplitude;
    [SerializeField]
    Image _Frequence;
    [SerializeField]
    Image _Phase;

    [SerializeField]
    Text _curTarget;
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
        updateValue();
    }

    void updateValue()
    {
        _WeaponEnergyLevel.simpleValue = SC_SyncVar_WeaponSystem.Instance.f_curEnergyLevel;
        _SystmShield.simpleValue = SC_SyncVar_WeaponSystem.Instance.f_WeaponLife;
        _Amplitude.fillAmount = SC_SyncVar_WeaponSystem.Instance.f_AmplitudeCalib;
        _Frequence.fillAmount = SC_SyncVar_WeaponSystem.Instance.f_FrequenceCalib;
        _Phase.fillAmount = SC_SyncVar_WeaponSystem.Instance.f_PhaseCalib;
        _curTarget.text = SC_SyncVar_WeaponSystem.Instance.s_KoaID;
    }
}
