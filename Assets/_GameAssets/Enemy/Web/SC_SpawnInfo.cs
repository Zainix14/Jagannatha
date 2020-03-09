using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SC_SpawnInfo : MonoBehaviour
{
    #region Singleton

    private static SC_SpawnInfo _instance;
    public static SC_SpawnInfo Instance { get { return _instance; } }



    #endregion


    void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            _instance = this;
        }
    }
    // Start is called before the first frame update
    public BezierSolution.BezierSpline[] GetBezierSplines()
    {
        BezierSolution.BezierSpline[] spawnSplines = new BezierSolution.BezierSpline[gameObject.transform.childCount];

        for (int i =0; i<gameObject.transform.childCount;i++)
        {
            spawnSplines[i] = gameObject.transform.GetChild(i).GetComponent<BezierSolution.BezierSpline>();

        }

        return spawnSplines;

    }
}
