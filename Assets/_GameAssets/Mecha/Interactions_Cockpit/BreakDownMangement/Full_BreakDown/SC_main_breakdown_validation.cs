using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SC_main_breakdown_validation : MonoBehaviour
{
    #region Singleton

    private static SC_main_breakdown_validation _instance;
    public static SC_main_breakdown_validation Instance { get { return _instance; } }

    #endregion

    public bool isValidated = false;
    GameObject SFX_Validate;
    GameObject SFX_ValidateSound;


    private GameObject Mng_SyncVar;
    private SC_SyncVar_Main_Breakdown sc_syncvar;
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
        GetReferences();
    }

    void GetReferences()
    {

        if (Mng_SyncVar == null)
            Mng_SyncVar = GameObject.FindGameObjectWithTag("Mng_SyncVar");
        if (Mng_SyncVar != null && sc_syncvar == null)
            sc_syncvar = Mng_SyncVar.GetComponent<SC_SyncVar_Main_Breakdown>();
    }
    /*
    IEnumerator getTo(Vector3 destination)
    {

        float i = 0.0f;
        float rate = 1.0f / 2;

        while (i < 1.0)
        {

            i += Time.deltaTime * rate;

            transform.position = Vector3.Lerp(transform.position, destination, i);
            

            Debug.Log("je ne suis pas arrêté, fdp =3");
            yield return null;

        }
        
    }
    */
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.V))
        {
            Validate();
            

        }
        if (sc_syncvar == null || Mng_SyncVar == null)
            GetReferences();

        if (sc_syncvar != null)
        {

        }
            /*
            if (Input.GetKeyDown(KeyCode.F))
            {
                this.GetComponent<Animator>().SetBool("BOUGE", false);
            }
            */
        }

    public void bringDown()
    {
        this.GetComponent<Animator>().SetBool("BOUGE", true);
    }

    public void bringUp()
    {
        this.GetComponent<Animator>().SetBool("BOUGE", false);
        
    }

    public void textBlink()
    {
        this.GetComponent<Animator>().SetBool("B_Blink", true);
        sc_syncvar.checkReboot(true);

    }
    public void textStopBlink()
    {
        this.GetComponent<Animator>().SetBool("B_Blink", false);
        sc_syncvar.checkReboot(false);
    }

    public void Validate()
    {
        //On check pour savoir si tous les systemes sont déjà réparés en additionant leurs compteurs
        if(SC_BreakdownDisplayManager.Instance.CurNbOfBreakdown == 0)
        {
            isValidated = true;
            SFX_Validate = CustomSoundManager.Instance.PlaySound(gameObject, "SFX_p_voice_rebooting_system", false, 1f);
            SFX_ValidateSound = CustomSoundManager.Instance.PlaySound(gameObject, "SFX_p_reboot_button_validate", false, 0.1f);
        }
        else
        {
            SFX_Validate = CustomSoundManager.Instance.PlaySound(gameObject, "SFX_p_voice_system_failure", false, 0.5f);
            SFX_ValidateSound = CustomSoundManager.Instance.PlaySound(gameObject, "SFX_p_reboot_button_fail", false, 0.1f);
        }
        SC_MainBreakDownManager.Instance.CheckBreakdown();
        textStopBlink();
        //Désactive le timer
        SC_BreakdownOnBreakdownAlert.Instance.StopAllCoroutines();
    }


}
