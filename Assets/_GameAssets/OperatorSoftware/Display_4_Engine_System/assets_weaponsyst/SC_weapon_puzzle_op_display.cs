using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SC_weapon_puzzle_op_display : MonoBehaviour
{
    GameObject[] tableau_barres;
    float[] tableau_init_rot_z;

    float init_rot_cylindre;

    private void Awake()
    {
        tableau_barres = new GameObject[gameObject.transform.childCount];

        tableau_init_rot_z = new float[gameObject.transform.childCount];

        for (int i = 0; i < gameObject.transform.childCount; i++)
        {
            tableau_barres[i] = gameObject.transform.GetChild(i).gameObject;
            tableau_init_rot_z[i] = gameObject.transform.GetChild(i).localEulerAngles.y;


        }

        init_rot_cylindre = gameObject.transform.eulerAngles.y;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        /*
        foreach(GameObject GO in tableau_barres)
        {
            GO.transform.rotation = Quaternion.Euler(-90, 0, SC_SyncVar_BreakdownWeapon.Instance.SL_Tourbilols[0].value * 22.5f);
        }
        */
        for (int i =0; i< tableau_barres.Length; i++)
        {
            tableau_barres[i].transform.localRotation = Quaternion.Euler(-90, 0, tableau_init_rot_z[i] +SC_SyncVar_BreakdownWeapon.Instance.SL_Tourbilols[0].value * 22.5f+22.5f);

        }

        //this.transform.rotation = Quaternion.Euler(0, SC_SyncVar_BreakdownWeapon.Instance.SL_Tourbilols[1].value * 22.5f + init_rot_cylindre, 0);


        
        switch(SC_SyncVar_BreakdownWeapon.Instance.SL_Tourbilols[1].value)
        {
            //solution1
            case -4:

                this.transform.rotation = Quaternion.Euler(0, init_rot_cylindre , 0);

                break;
                
            case -3:

                this.transform.rotation = Quaternion.Euler(0, 35 , 0);

                break;
                
                //solution2
            case -2:

                this.transform.rotation = Quaternion.Euler(0, 70, 0);

                break;

            case -1:

                this.transform.rotation = Quaternion.Euler(0,  100, 0);

                break;
                
            case 0:

                this.transform.rotation = Quaternion.Euler(0, 140, 0);

                break;
               //solution3 
            case 1:

                this.transform.rotation = Quaternion.Euler(0, 178, 0);

                break;
                
            case 2:

                this.transform.rotation = Quaternion.Euler(0, 220, 0);

                break;
                
            case 3:

                this.transform.rotation = Quaternion.Euler(0,  300, 0);

                break;
                

        }
        


    }
}
