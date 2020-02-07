using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SC_JoystickMove : MonoBehaviour, IF_BreakdownSystem
{
    [HideInInspector]
    bool b_InBreakdown = false;
    bool b_BreakEngine = false;

    [SerializeField]
    float f_Speed = 10.0f;
    [SerializeField]
    float f_RotationSpeed = 1.0f;
    [SerializeField]
    float f_MaxSpeed;
    [SerializeField]
    float f_MaxRotSpeed;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (!b_InBreakdown && !b_BreakEngine)
            Move();
    }

    void Move()
    {

        //donne une impulsion en proportion à l'axe du joystick
        float f_ImpulseZ = Input.GetAxis("Vertical") * f_Speed;
        float f_ImpulseX = Input.GetAxis("Horizontal") * f_Speed;

        float f_Torque = Input.GetAxis("Rotation") * f_RotationSpeed;

        //Translation
        if (GetComponent<Rigidbody>().velocity.magnitude < f_MaxSpeed)
        {

            //Le joystick en diagonale => 1/1 à -1/-1 besoin de clamp l'addition des 2 vecteurs
            Vector3 V3_ClampedVector = Vector3.ClampMagnitude((transform.forward * f_ImpulseZ) + (transform.right * f_ImpulseX), f_Speed);

            GetComponent<Rigidbody>().AddForce(V3_ClampedVector, ForceMode.Impulse);

        }


        //Rotation
        if (GetComponent<Rigidbody>().angularVelocity.magnitude < f_MaxRotSpeed)
        {
            GetComponent<Rigidbody>().AddTorque(transform.up * f_RotationSpeed * f_Torque, ForceMode.Impulse);
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
