using UnityEngine;
using System.Collections;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;

public class ExampleClass : MonoBehaviour
{

    GameObject Mng_CheckList;
    GameObject Mng_Network;
    NetworkManager NetManager;

    bool b_IsCo = false;
    bool b_InRun = false;

    void Start()
    {
        GetRefs();
    }

    void GetRefs()
    {

        Mng_CheckList = GameObject.FindGameObjectWithTag("Mng_CheckList");

        if (Mng_CheckList != null)
            Mng_Network = Mng_CheckList.GetComponent<SC_CheckList>().GetNetworkManager();

        if (Mng_Network != null)
            NetManager = Mng_Network.GetComponent<NetworkManager>();

    }

    void Update()
    {

        if (Mng_CheckList == null || Mng_Network == null || Mng_Network == null)
            GetRefs();

        if (NetManager != null)
            CheckConnection();

        if (!b_IsCo)
            Connect();

        if (Input.GetKey(KeyCode.K))
        {
            Debug.Log("Connect");
            NetManager.StartClient();
        }

    }
    
    void CheckConnection()
    {
        if (b_IsCo && !NetManager.IsClientConnected())
        {
            b_IsCo = false;
            StopAllCoroutines();
        }        
        else if (!b_IsCo && NetManager.IsClientConnected())
            b_IsCo = true;
    }

    void Connect()
    {     
        int n_index = SceneManager.GetActiveScene().buildIndex;
        if ( n_index != 0 && n_index != 1 && n_index != 3 && !b_InRun)
        {
            b_InRun = true;
            //StartCoroutine(ResetII());
        }
    }

    IEnumerator Reset()
    {
        NetManager.StopClient();
        yield return new WaitForSeconds(1);

        //NetManager.networkAddress = "192.168.151.72";
        NetManager.networkAddress = "localhost";
        NetManager.StartClient();       
        yield return new WaitForSeconds(5);
        
        if (NetManager.IsClientConnected())
        {
            Debug.Log("Quit");
            yield break;
        }            
        else
        {
            Debug.Log("Restart");
            b_InRun = false;
            yield return 0;
        }                  
    }

    IEnumerator ResetII()
    {
        Debug.Log("Restart");
        NetManager.StopClient();
        yield return new WaitForSeconds(1);
        //NetManager.networkAddress = "192.168.151.72";
        NetManager.networkAddress = "localhost";
        b_InRun = false;
        NetManager.StartClient();
    }



}