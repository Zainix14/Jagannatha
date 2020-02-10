using UnityEngine;
using System.Collections;

public class ViveGripExample_Lever : MonoBehaviour
{
    private ViveGrip_ControllerHandler controller;
    private float oldXRotation;
    private int VIBRATION_DURATION_IN_MILLISECONDS = 50;
    private float MAX_VIBRATION_STRENGTH = 0.7f;
    private float MAX_VIBRATION_ANGLE = 35f;

    private Rigidbody leverRigidBody;

    public GameObject TopCollider;
    public GameObject BotCollider;
    private BoxCollider TopBox;
    private BoxCollider BotBox;



    void Start()
    {
        oldXRotation = transform.eulerAngles.x;

        leverRigidBody = gameObject.GetComponent<Rigidbody>();
        TopBox = TopCollider.GetComponent<BoxCollider>();
        BotBox = BotCollider.GetComponent<BoxCollider>();
    }

    void ViveGripGrabStart(ViveGrip_GripPoint gripPoint)
    {
        controller = gripPoint.controller;
        leverRigidBody.isKinematic = false;
    }

    void ViveGripGrabStop()
    {
        controller = null;
        leverRigidBody.isKinematic = true;
    }


    void Update()
    {
        float newXRotation = transform.eulerAngles.x;
        if (controller != null)
        {
            float distance = Mathf.Min(Mathf.Abs(newXRotation - oldXRotation), MAX_VIBRATION_ANGLE);
            float vibrationStrength = (distance / MAX_VIBRATION_ANGLE) * MAX_VIBRATION_STRENGTH;
            controller.Vibrate(VIBRATION_DURATION_IN_MILLISECONDS, vibrationStrength);
        }
        oldXRotation = newXRotation;



    }
    private void OnTriggerEnter(Collider other)
    {
        if (other == TopBox)
        {
            Debug.Log("Top");

        }
        if (other == BotBox)
        {
            Debug.Log("Bottom");

        }
    }

}
