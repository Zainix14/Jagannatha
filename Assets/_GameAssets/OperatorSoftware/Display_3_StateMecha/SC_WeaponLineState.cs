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

        //CurCoro = StartCoroutine(DrawLine());
        CurCoro = StartCoroutine(DrawLineII(1));

    }

    IEnumerator DrawLine()
    {

        if (line.positionCount != n_CurveSize)
            line.positionCount = n_CurveSize; //Configuration du nombre 

        for (int i = 0; i < n_CurveSize; i++)
        {
            float x = (i * (f_MaxX / n_CurveSize));
            float y = CurCurve.Evaluate((1 / n_CurveSize) * i) * f_MaxY;
            line.SetPosition(i, new Vector3(x, y, 0f));
            yield return 0;
        }

    }

    IEnumerator DrawLineII(float Duration)
    {

        float time = 0;
        float rate = 1 / Duration;
        float f_TimePerUnit = Duration / n_CurveSize;

        if (line.positionCount != n_CurveSize)
            line.positionCount = n_CurveSize; //Configuration du nombre 

        for (int i = 0; i < n_CurveSize; i++)
        {

            if(time < f_TimePerUnit*i)
            {
                time += Time.deltaTime * rate;
                float x = (i * (f_MaxX / n_CurveSize));
                float y = CurCurve.Evaluate((1 / n_CurveSize) * i) * f_MaxY;
                line.SetPosition(i, new Vector3(x, y, 0f));
            }

            else
            {
                yield return 0;
            }

        }

    }

}
