using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SC_WeaponBreakdown : MonoBehaviour, IF_BreakdownManager
{
    #region Singleton

    private static SC_WeaponBreakdown _instance;
    public static SC_WeaponBreakdown Instance { get { return _instance; } }

    #endregion

    public float frequency;
    int offPercentage;
    int onPercentage;

    float curTimer;
    float offTime;
    float onTime;

    bool bCanFire;

    // Start is called before the first frame update
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
        offPercentage = 0;
        bCanFire = true;
    }

    // Update is called once per frame
    void Update()
    {
      
        if(offPercentage > 0)
        {
            offTime = offPercentage / frequency;
            onPercentage = 100 - offPercentage;
            onTime = onPercentage / frequency;
            curTimer += Time.deltaTime;

            if(curTimer >= onTime && bCanFire == true)
            {
                curTimer = 0;
                bCanFire = false;
            }

            if(curTimer >= offTime && bCanFire == false)
            {
                curTimer = 0;
                bCanFire = true;
            }


        }
    }

    public void SetNewBreakdown(int percent, float frequency = 10)
    {
        offPercentage += percent;
    }

    public void EndBreakdown()
    {
        offPercentage = 0;
    }

    public bool CanFire()
    {
        return bCanFire;
    }

    public void CheckBreakdown()
    {

    }
}
