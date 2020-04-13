using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SC_WeaponLineState : MonoBehaviour
{

    [Header("References")]
    [SerializeField]
    RectTransform Container;
    LineRenderer line;

    [Header("Curves Parameters")]
    [SerializeField]
    AnimationCurve FunctionalState;
    [SerializeField]
    AnimationCurve BreakdownStateLvl01;
    [SerializeField]
    AnimationCurve BreakdownStateLvl02;
    [SerializeField]
    int n_CurveSize = 300;
    [SerializeField]
    float DrawTime = 1;
    float f_MaxX = 376;
    float f_MaxY = 170;
    Color32 curColor;

    [Header("Curves Infos")]
    [SerializeField]
    AnimationCurve CurCurve;

    Coroutine CurCoro;

    // Start is called before the first frame update
    void Start()
    {
        line = this.gameObject.GetComponent<LineRenderer>();
        CurCurve = FunctionalState;
        f_MaxX = Container.rect.width;
        f_MaxY = Container.rect.height;
        line.positionCount = n_CurveSize;

        curColor = new Color32();
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

        if (CurCoro != null)
            StopCoroutine(CurCoro);

        //DrawLine();
        CurCoro = StartCoroutine(DrawLineTime(DrawTime));

    }

    void DrawLine()
    {

        if (line.positionCount != n_CurveSize)
            line.positionCount = n_CurveSize; //Configuration du nombre 

        float f_Size = n_CurveSize;
        for (int i = 0; i < f_Size; i++)
        {         
            float x = (i * (f_MaxX / f_Size));
            float y = CurCurve.Evaluate((1 / f_Size) * i) * f_MaxY;
            Debug.Log((1 / f_Size) * i);
            line.SetPosition(i, new Vector3(x, y, 0f));           
        }

    }

    IEnumerator DrawLineTime(float Duration)
    {

        if (line.positionCount != n_CurveSize)
            line.positionCount = n_CurveSize; //Configuration du nombre 

        float f_Size = n_CurveSize;

        float time = 0;
        float rate = 1 / Duration;
        
        for (int i = 0; i < f_Size; i++)
        {

            float f_TargetI = Mathf.Round(time * f_Size);

            if (i < f_TargetI)
            {
                float x = (i * (f_MaxX / f_Size));
                float y = CurCurve.Evaluate((1 / f_Size) * i) * f_MaxY;
                line.SetPosition(i, new Vector3(x, y, 0f));
            }

            else
            {
                time += Time.deltaTime * rate;
                float x = (i * (f_MaxX / f_Size));
                float y = CurCurve.Evaluate((1 / f_Size) * i) * f_MaxY;
                line.SetPosition(i, new Vector3(x, y, 0f));
                yield return 0;
            }

        }

    }

}
