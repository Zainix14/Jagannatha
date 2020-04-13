using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SC_UI_Update_Float : MonoBehaviour
{

    [SerializeField]
    Text text_component_desired;

    [SerializeField]
    Text text_component_value;

    public int index = 0;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

        if (SC_SyncVar_BreakdownWeapon.Instance != null)
        {
            text_component_desired.text = SC_SyncVar_BreakdownWeapon.Instance.SL_Tourbilols[index].valueWanted.ToString();
            text_component_value.text = SC_SyncVar_BreakdownWeapon.Instance.SL_Tourbilols[index].value.ToString();

        }
            


    }
}
