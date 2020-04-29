using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SC_Cord : MonoBehaviour
{
    [Header("References")]
    [SerializeField]
    GameObject Base;
    [SerializeField]
    GameObject Hand;
    [SerializeField]
    MeshRenderer Renderer;
    [SerializeField]
    Material[] tab_Materials;

    [Header("Parameters")]
    [SerializeField]
    int n_Index = 0;
    [SerializeField, Range(0, 1)]
    float ConstraintRange = 0.7f;
    [SerializeField, Range(0, 0.5f)]
    float DeadZone = 0.15f;
    [SerializeField, Range(0, 0.5f)]
    float AddMaxRange = 0.3f;

    [Header("Infos")]
    [SerializeField]
    float f_CurDistance;
    [SerializeField]
    bool b_InRange;
    [SerializeField]
    bool b_Enable = false;
    [SerializeField]
    bool b_Grabbing = false;

    //Non Public Refs
    Rigidbody HandRb;

    // Start is called before the first frame update
    void Start()
    {
        HandRb = Hand.GetComponent<Rigidbody>();
        SetMaterial();
    }

    // Update is called once per frame
    void Update()
    {

        CalculateDistance();

        ObjectStatus();

        RangeEffect();

        if (!b_Grabbing)
            FollowByHand();

    }

    void ObjectStatus()
    {

        #if UNITY_EDITOR

        if (UnityEditor.Selection.activeObject == Hand.gameObject && !b_Grabbing)
            b_Grabbing = true;

        else if (UnityEditor.Selection.activeObject != Hand.gameObject && b_Grabbing)
            b_Grabbing = false;

        #endif

    }

    void FollowByHand()
    {
        Hand.transform.position = this.transform.position;
    }

    void CalculateDistance()
    {
        Vector3 Distance = Base.transform.position - this.transform.position;
        f_CurDistance = Distance.magnitude;
    }

    void ReleaseObject()
    {

        #if UNITY_EDITOR

        UnityEditor.Selection.SetActiveObjectWithContext(null, null);

        #endif

    }

    void RangeEffect()
    {

        if (f_CurDistance < ConstraintRange)
            b_InRange = true;

        if (f_CurDistance > ConstraintRange + DeadZone && b_InRange)
        {
            b_Enable = !b_Enable;
            b_InRange = false;
            SetMaterial();
            SC_MovementBreakdown.Instance.AddToPilotSeq(n_Index);
        }

        if (f_CurDistance > ConstraintRange + AddMaxRange)
            ReleaseObject();

    }

    void SetMaterial()
    {
        if (!b_Enable)
            Renderer.material = tab_Materials[0];
        if (b_Enable)
            Renderer.material = tab_Materials[1];
    }

    public void HandKinematic(bool state)
    {
        Debug.Log("HandKinematic - " + state);
        HandRb.isKinematic = state;
    }

}

