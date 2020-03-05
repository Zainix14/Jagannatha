using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class SC_BreakdownOnBreakdownAlert : MonoBehaviour
{

    float timer = 0f;

    public GameObject go_timer;

    TextMeshPro textComponent;

    Coroutine coroutineTimer;




    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.B))
        {
            LaunchGlobalAlert();
        }
        if (Input.GetKeyDown(KeyCode.N))
        {
            StopGlobalAlert();
        }
    }

    IEnumerator Timer()
    {
        while (timer > 0)
        {
            timer -= Time.deltaTime;

            textComponent.text = "babar"/*((Mathf.Round(timer*100))/100).ToString()*/;

            yield return null;
        }

        Debug.Log("Damage/shake/FX");

    }

    public void LaunchGlobalAlert()

    {
        timer = 10f;
        coroutineTimer = StartCoroutine("Timer");


    }


    public void StopGlobalAlert()
    {
        StopCoroutine(coroutineTimer);

    }

}
