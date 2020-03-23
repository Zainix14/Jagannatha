using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SC_tourbilol : MonoBehaviour, IInteractible
{
    float oldRot;
    float curRot;

    float totalAngle = 0;

    public int index = 0;

    public bool isEnPanne = false;

    private float desiredValue = 0f;

    private SC_SyncVar_BreakdownWeapon sc_syncvar;

    [SerializeField]
    tourbiType type;

    enum tourbiType
    {
        tourbiFirst,
        tourbiSecond
    }

    // Start is called before the first frame update
    void Start()
    {
        oldRot = this.transform.localEulerAngles.z;


        sc_syncvar = SC_SyncVar_BreakdownWeapon.Instance;


    }

    //Il faut faire 1/4 de tour de valve pour changer d'un cran la valeur signifiante
    // Update is called once per frame
    void Update()
    {
        if (this.transform.localEulerAngles.z != oldRot)
        { 
            curRot = this.transform.localEulerAngles.z ;

            if(Mathf.Abs(oldRot - curRot)<260)
                 totalAngle += oldRot-curRot;
            //si on veut limiter à un tour
            
            if (totalAngle < -360)
                totalAngle = 359;
            else if (totalAngle > 360)
                totalAngle = -360;
                
            //Debug.Log(totalAngle);


            //FORMATAGE ET ENVOIE COTE OP
            //Ici on crante par 90°

            if (oldRot != curRot)
            {
                sendToSynchVar(Mathf.Floor(totalAngle / 90));
                IsValueOk();
            }
            

            oldRot = curRot;

        }


        if (Input.GetKeyDown(KeyCode.Y) && index == 0)
        {
            Debug.Log("total : " + totalAngle);
            Debug.Log("desired : " + desiredValue);



        }
        
    }


    public void IsValueOk()
    {

        if (/*Mathf.Abs(totalAngle - desiredValue) < 89*/ Mathf.Floor(totalAngle / 90) == Mathf.Floor(desiredValue / 90))
        {

            if (isEnPanne)
            {
                SetIsEnPanne(false);

                sc_syncvar.TourbilolChangeIsPanne(index, false);

            }
        }
        else if(!isEnPanne)
        {

            SetIsEnPanne(true);
            sc_syncvar.TourbilolChangeIsPanne(index, true);

        }

    }
    


    public bool isBreakdown()
    {
        return isEnPanne;
    }


    void sendToSynchVar(float value)
    {

            sc_syncvar.TourbilolChangeValue(index, value);

    }

    public void ChangeDesired()
    {

        // pour l'instant je pars sur 2 tours pour obtenir la totalité des positions possibles avec un changement d'état tous les 90°

        bool good = false;

        int count = 0;
        while (good == false)
        {
            if(type == tourbiType.tourbiFirst)
            {
                int rand = Random.Range(0,2);

                if (rand == 0)
                {

                    desiredValue = 3 * 90;
                }
                else
                    desiredValue = -90;

            }
            else if (type == tourbiType.tourbiSecond)
            {
                int rand = Random.Range(0, 3);

                switch(rand)
                {
                    case 0:

                        desiredValue = -4 * 90;

                        break;
                    case 1:

                        desiredValue = -2 * 90;

                        break;
                    case 2:

                        desiredValue = 1 * 90;

                        break;
                }
            }
            else
            {
                desiredValue = Random.Range(-4, 3) * 90;
            }
                

            if (Mathf.Abs(desiredValue - totalAngle) > 90 )
            {
                good = true;
            }

            count++;

            if (count > 10)
            {
                Debug.LogError("La boucle elle est FUCKEE");
                break;
                
            }
                

        }


        SetIsEnPanne(true);


        sc_syncvar.TourbilolChangeValueWanted(index, desiredValue/90);
        sc_syncvar.TourbilolChangeIsPanne(index, true);


    }

    public void Repair()
    {

        desiredValue = totalAngle;

        SetIsEnPanne(false);


        sc_syncvar.TourbilolChangeValueWanted(index, Mathf.Floor(desiredValue/90));
        sc_syncvar.TourbilolChangeIsPanne(index, false);


    }


    void SetIsEnPanne(bool value)
    {

        isEnPanne = value;
        SC_WeaponBreakdown.Instance.CheckBreakdown();

    }

    public bool testAgainstOdds()
    {
        return true;
    }
}
