using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// /!\ Debug Scripts | 
/// Deplacements Generiques a mettre sur un objet. | 
/// Script by Leni |
/// </summary>
public class SC_DebugMove : MonoBehaviour, IF_BreakdownSystem
{

    bool b_InBreakdown = false;
    bool b_BreakEngine = false;

    public float f_Speed = 10f;
    public float f_RotFactor = 1f;
    Rigidbody rb;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (!b_InBreakdown && !b_BreakEngine)
            Move();
    }

    void Move()
    {
        if (Input.GetKey(KeyCode.Z))
        {
            GetComponent<Rigidbody>().AddForce(transform.forward * f_Speed, ForceMode.Impulse);
        }
        else if (Input.GetKey(KeyCode.S))
        {
            GetComponent<Rigidbody>().AddForce(transform.forward * (f_Speed * -1), ForceMode.Impulse);
        }

        if (Input.GetKey(KeyCode.E))
        {
            GetComponent<Rigidbody>().AddForce(transform.right * f_Speed, ForceMode.Impulse);
        }
        else if (Input.GetKey(KeyCode.A))
        {
            GetComponent<Rigidbody>().AddForce(transform.right * (f_Speed * -1), ForceMode.Impulse);
        }

        if (Input.GetKey(KeyCode.Q))
        {
            rb.AddTorque(transform.up * (f_Speed * f_RotFactor * -1), ForceMode.Force);
        }
        else if (Input.GetKey(KeyCode.D))
        {
            rb.AddTorque(transform.up * f_Speed * f_RotFactor, ForceMode.Force);
        }
    }

    public void SetBreakdownState(bool State)
    {
        b_InBreakdown = State;
    }

    public void SetEngineBreakdownState(bool State)
    {
        b_BreakEngine = State;
    }

}
