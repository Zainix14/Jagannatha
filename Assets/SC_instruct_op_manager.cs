using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SC_instruct_op_manager : MonoBehaviour
{

    #region Singleton

    private static SC_instruct_op_manager _instance;
    public static SC_instruct_op_manager Instance { get { return _instance; } }

    #endregion


    public GameObject[] instruc_Go;


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
        
    }

    public void Activate(int index)
    {
        if (index < instruc_Go.Length)
        {

            instruc_Go[index].SetActive(true);

        }


    }

    public void Deactivate(int index)
    {
        if (index < instruc_Go.Length)
        {

            instruc_Go[index].SetActive(false);

        }


    }



}
