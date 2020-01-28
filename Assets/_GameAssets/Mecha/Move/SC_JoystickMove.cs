using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Mouvement du player pilote en fonction du joystick    
/// GameObject Scene : Mecha
/// Auteur : Cyrille
/// </summary>

public class SC_JoystickMove : MonoBehaviour
{
    [HideInInspector]
    public bool b_MoveBreakdown = false;

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

        //donne une impulsion en proportion à l'axe du joystick
        float f_ImpulseZ = Input.GetAxis("Vertical") * f_Speed;
        float f_ImpulseX = Input.GetAxis("Horizontal") * f_Speed;

        float f_Torque = Input.GetAxis("Rotation") * f_RotationSpeed;

        //Translation
        if (GetComponent<Rigidbody>().velocity.magnitude < f_MaxSpeed && !b_MoveBreakdown)
        {

            //Le joystick en diagonale => 1/1 à -1/-1 besoin de clamp l'addition des 2 vecteurs
            Vector3 V3_ClampedVector = Vector3.ClampMagnitude((transform.forward * f_ImpulseZ) + (transform.right * f_ImpulseX), f_Speed);

            GetComponent<Rigidbody>().AddForce(V3_ClampedVector, ForceMode.Impulse);

        }


        //Rotation
        if (GetComponent<Rigidbody>().angularVelocity.magnitude < f_MaxRotSpeed && !b_MoveBreakdown)
        {
            GetComponent<Rigidbody>().AddTorque(transform.up * f_RotationSpeed * f_Torque, ForceMode.Impulse);
        }

    }

}
