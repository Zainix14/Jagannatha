using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SC_Hitbox_mech : MonoBehaviour
{
    public GameObject Mng_Breakdown;
    public SC_MainBreakDownManager sc_main_breakdown_manager;

    // Start is called before the first frame update
    void Start()
    {
        sc_main_breakdown_manager = GameObject.FindGameObjectWithTag("Mng_BreakdownMain").GetComponent<SC_MainBreakDownManager>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }


}
