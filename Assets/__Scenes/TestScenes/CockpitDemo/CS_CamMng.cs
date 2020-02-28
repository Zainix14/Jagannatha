using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CS_CamMng : MonoBehaviour
{

    /// <summary>
    /// 
    /// cam pilot
    /// pos1: x:0 y:2.79 z:-6.6
    /// pos2: x:0 y:1.29 z:-0.36
    /// 
    /// cam h
    /// pos1: x:0 y:1.12 z:0.97
    /// pos2: x:0 y:5.24 z:0.97
    /// 
    /// cam scale
    /// pos1: x:-1.61 y:1.12 z:0.25
    /// pos2: x:-1.61 y:3.65 z:-5.54
    /// 
    /// cam voute
    /// pos1: x:0 y:2.66 z:0.66
    /// pos2 x:0 y:2.98 z:-3.31
    /// 
    /// </summary>

    [SerializeField]
    GameObject[] Tab_Cam;

    [SerializeField]
    float LerpTime;

    int n_Index = 0;

    // Start is called before the first frame update
    void Start()
    {
        SetCam();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void SetCam()
    {

        if (n_Index >= Tab_Cam.Length)
            n_Index = 0;

        for(int i=0 ; i<Tab_Cam.Length ; i++)
        {
            if (n_Index == i)
                Tab_Cam[i].SetActive(true);
            else
                Tab_Cam[i].SetActive(false);
        }

        GameObject curCam = Tab_Cam[n_Index];
        StartCoroutine( CamLerp(curCam, curCam.GetComponent<IF_CamCockpitDemo>().GetInitVt3(), curCam.GetComponent<IF_CamCockpitDemo>().GetTargetVt3(), curCam.GetComponent<IF_CamCockpitDemo>().GetLerpTime() ) );

    }

    IEnumerator CamLerp(GameObject CurCam, Vector3 InitPos, Vector3 targetpos, float time)
    {
        float elapsedTime = 0;
        float waitTime = time;
        CurCam.transform.position = InitPos;
        Vector3 currentPos = InitPos;

        while (elapsedTime < waitTime)
        {
            CurCam.transform.position = Vector3.Lerp(currentPos, targetpos, (elapsedTime / waitTime));
            elapsedTime += Time.deltaTime;

            // Yield here
            yield return null;
        }
        // Make sure we got there
        CurCam.transform.position = targetpos;
        n_Index++;
        SetCam();
        yield return null;
    }

}
