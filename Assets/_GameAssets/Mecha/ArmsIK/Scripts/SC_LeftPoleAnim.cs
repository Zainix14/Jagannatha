using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SC_LeftPoleAnim : MonoBehaviour
{
    public Transform HandTarget;
    public Transform HandPole;

    public float f_OffSetXFactor = 5;
    public float f_OffSetYFactor = 1;
    public float f_OffSetZFactor = 1;

    Vector3 HandTargetInitPos;
    Vector3 HandPoleInitPos;

    float f_HTOldPosX;
    float f_HTOldPosY;
    float f_HTOldPosZ;

    float f_HTNewPosX;
    float f_HTNewPosY;
    float f_HTNewPosZ;

    // Start is called before the first frame update
    void Start()
    {

        HandTargetInitPos = HandTarget.localPosition;
        HandPoleInitPos = HandPole.localPosition;

        f_HTOldPosX = HandTarget.localPosition.x;
        f_HTOldPosY = HandTarget.localPosition.y;
        f_HTOldPosZ = HandTarget.localPosition.z;

    }

    void LateUpdate()
    {
        UpdatePosX();
        UpdatePosY();
        UpdatePosZ();
    }

    void UpdatePosX()
    {

        f_HTNewPosX = HandTarget.localPosition.x;

        float f_CurOffSetX = f_HTNewPosX - f_HTOldPosX;

        if(f_HTNewPosX > HandTargetInitPos.x)
            HandPole.localPosition = new Vector3(HandPole.localPosition.x - (f_CurOffSetX * f_OffSetXFactor), HandPole.localPosition.y, HandPole.localPosition.z);      
        else if (f_HTNewPosX < HandTargetInitPos.x && HandPole.localPosition.x != HandPoleInitPos.x)
            HandPole.localPosition = new Vector3(HandPoleInitPos.x, HandPole.localPosition.y, HandPole.localPosition.z);

        f_HTOldPosX = f_HTNewPosX;

    }

    void UpdatePosY()
    {

        f_HTNewPosY = HandTarget.localPosition.y;

        float f_CurOffSetY = f_HTNewPosY - f_HTOldPosY;

        HandPole.localPosition = new Vector3(HandPole.localPosition.x, HandPole.localPosition.y - (f_CurOffSetY * f_OffSetYFactor), HandPole.localPosition.z);

        f_HTOldPosY = f_HTNewPosY;

    }

    void UpdatePosZ()
    {

        f_HTNewPosZ = HandTarget.localPosition.z;

        float f_CurOffSetZ = f_HTNewPosZ - f_HTOldPosZ;

        HandPole.localPosition = new Vector3(HandPole.localPosition.x, HandPole.localPosition.y, HandPole.localPosition.z + f_CurOffSetZ * f_OffSetZFactor);
        
        if (HandTarget.localPosition.z <= 0 && HandPole.localPosition.z < 0)
            HandPole.localPosition = new Vector3(HandPole.localPosition.x, HandPole.localPosition.y, HandPole.localPosition.z * -1);
        else if (HandTarget.localPosition.z >= 0 && HandPole.localPosition.z > 0)
            HandPole.localPosition = new Vector3(HandPole.localPosition.x, HandPole.localPosition.y, HandPole.localPosition.z * -1);
            

        f_HTOldPosZ = f_HTNewPosZ;

    }

}
