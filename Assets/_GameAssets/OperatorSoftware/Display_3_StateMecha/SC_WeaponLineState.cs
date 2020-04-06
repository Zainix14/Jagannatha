using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SC_WeaponLineState : MonoBehaviour
{

    [SerializeField]
    int n_CurveSize = 300;

    [SerializeField]
    RectTransform Container;

    [SerializeField]
    AnimationCurve FunctionalState;
    [SerializeField]
    AnimationCurve BreakdownStateLvl01;
    [SerializeField]
    AnimationCurve BreakdownStateLvl02;

    [SerializeField]
    AnimationCurve CurCurve;

    LineRenderer line;
    Color32 curColor;

    float f_MaxX = 376;
    float f_MaxY = 170;

    // Start is called before the first frame update
    void Start()
    {
        line = this.gameObject.GetComponent<LineRenderer>();
        curColor = new Color32();
        CurCurve = FunctionalState;
        f_MaxX = Container.rect.width;
        f_MaxY = Container.rect.height;
    }

    public void UpdateLine()
    {

        switch (SC_SyncVar_WeaponSystem.Instance.f_CurNbOfBd)
        {

            case 0:
                CurCurve = FunctionalState;
                break;

            case 1:
                CurCurve = BreakdownStateLvl01;
                break;

            case 2:
                CurCurve = BreakdownStateLvl02;
                break;

        }

        line.positionCount = n_CurveSize; //Configuration du nombre 
        float n_TempCount = line.positionCount;

        for (int i = 0; i < n_TempCount; i++)
        {
            float x = (i * (f_MaxX / n_TempCount));
            float y = CurCurve.Evaluate((1 / n_TempCount) * i) * f_MaxY;
            //float y = CurCurve.Evaluate(x);
            line.SetPosition(i, new Vector3(x, y, 0f));
        }

    }

}
