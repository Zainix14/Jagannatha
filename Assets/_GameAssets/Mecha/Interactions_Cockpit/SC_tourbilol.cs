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

    // Start is called before the first frame update
    void Start()
    {
        oldRot = this.transform.localEulerAngles.z;


        sc_syncvar = SC_SyncVar_BreakdownWeapon.Instance;


    }

    // Update is called once per frame
    void Update()
    {
        if (this.transform.localEulerAngles.z != oldRot)
        { 
            curRot = this.transform.localEulerAngles.z ;

            if(Mathf.Abs(oldRot - curRot)<260)
                 totalAngle += oldRot-curRot;
            //si on veut limiter à un tour
            /*
            if (totalAngle < -360)
                totalAngle += 360;
            else if (totalAngle > 360)
                totalAngle -= 360;
                */
            //Debug.Log(totalAngle);


            //FORMATAGE ET ENVOIE COTE OP
            //Ici on crante par 90°
            sendToSynchVar(Mathf.Floor(totalAngle/90));

            oldRot = curRot;

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
        while (good == false)
        {
            desiredValue = Random.Range(-4, 4) * 90;

            if (Mathf.Abs(desiredValue - totalAngle) > 89 )
            {
                good = true;
            }
        }


        SetIsEnPanne(true);


        sc_syncvar.TourbilolChangeValueWanted(index, desiredValue);
        sc_syncvar.TourbilolChangeIsPanne(index, true);


    }

    public void Repair()
    {

        desiredValue = totalAngle;

        SetIsEnPanne(false);


        sc_syncvar.TourbilolChangeValueWanted(index, desiredValue);
        sc_syncvar.TourbilolChangeIsPanne(index, false);


    }


    void SetIsEnPanne(bool value)
    {

        isEnPanne = value;
        SC_WeaponBreakdown.Instance.CheckBreakdown();

    }


}
