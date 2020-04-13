using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SC_SnapCollider : MonoBehaviour
{

    Camera MechCam;

    [Header("Refrences")]
    [SerializeField]
    CapsuleCollider Collider;

    [Header("Infos")]
    [SerializeField]
    float DistFromMech;

    [Header("Height Parameters")]
    [SerializeField]
    float MinHeight = 1;
    [SerializeField]
    float MaxHeight = 50;
    [SerializeField]
    float MinDistHeight = 1;
    [SerializeField]
    float MaxDistHeight = 126;

    [Header("Radius Parameters")]
    [SerializeField]
    float MinRadius = 2;
    [SerializeField]
    float MaxRadius = 8;
    [SerializeField]
    float MinDistRadius = 20;
    [SerializeField]
    float MaxDistRadius = 126;

    // Start is called before the first frame update
    void Start()
    {
        GetReferences();
    }

    // Update is called once per frame
    void Update()
    {

        if (MechCam == null)
            GetReferences();

        if (MechCam != null)
        {
            LookAtMech();
            UpdateDimension();
        }   

    }

    void GetReferences()
    {
        if(MechCam == null)
            MechCam = SC_CheckList.Instance.Cam_Mecha;
    }

    void LookAtMech()
    {
        this.transform.LookAt(MechCam.transform);
    }

    void UpdateDimension()
    {

        Vector3 Dist = this.transform.position - MechCam.transform.position;
        DistFromMech = Dist.magnitude;

        if(DistFromMech < MinDistHeight)
            Collider.height = MinHeight;
        else if(DistFromMech > MaxDistHeight)
            Collider.height = MaxHeight;
        else
        {

            float HeightRange = MaxHeight - MinHeight;
            float DistRange = MaxDistHeight - MinDistHeight;
            float CurDistRange = DistFromMech - MinDistHeight;

            float CurDistRatio = CurDistRange * 100 / DistRange;
            float CurHeightRatio = CurDistRatio / 100 * HeightRange;
            float CurHeight = CurHeightRatio + MinHeight;

            Collider.height = CurHeight;

        }
        
        if (DistFromMech < MinDistRadius)
            Collider.radius = MinRadius;
        else if (DistFromMech > MaxDistRadius)
            Collider.radius = MaxRadius;
        else
        {

            float RadiusRange = MaxRadius - MinRadius;
            float DistRange = MaxDistRadius - MinDistRadius;
            float CurDistRange = DistFromMech - MinDistRadius;

            float CurDistRatio = CurDistRange * 100 / DistRange;
            float CurRadiusRatio = CurDistRatio / 100 * RadiusRange;
            float CurRadius = CurRadiusRatio + MinRadius;

            Collider.radius = CurRadius;

        }
        
    }

}
