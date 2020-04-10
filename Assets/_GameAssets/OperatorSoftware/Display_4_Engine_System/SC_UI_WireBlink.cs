using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SC_UI_WireBlink : MonoBehaviour
{
    [SerializeField]
    Image[] img_ToBreakDown;

    Material[] wireSafe;

    [SerializeField]
    Material wireBreakdown;


    bool isActive;

    // Start is called before the first frame update
    void Start()
    {
        wireSafe = new Material[img_ToBreakDown.Length];

        for (int i = 0; i < wireSafe.Length; i++)
        {
            wireSafe[i] = img_ToBreakDown[i].material;
        }

    }


    public void SetBreakDown(bool b)
    {
        if(b && !isActive)
        {
            StartCoroutine(RedWireCoro());
            isActive = true;
        }
        else if(!b)
        {
            isActive = false;
            EndCoroutine();
        }
    }


    void EndCoroutine()
    {
        StopAllCoroutines();
        for (int i = 0; i < img_ToBreakDown.Length; i++)
        {
            img_ToBreakDown[i].material = wireSafe[i];
            img_ToBreakDown[i].color = Color.white;
        }

    }


    IEnumerator RedWireCoro()
    {
        float animTime = 0.5f;
        float maxOpacity = 1;
        float minOpacity = 0f;
        float ratePerSec = (maxOpacity - minOpacity / animTime) * 2;
        float curOpacity;
        bool Add = true;
        float t = 0;

        for (int i = 0; i < img_ToBreakDown.Length; i++)
        {
            img_ToBreakDown[i].material = wireBreakdown;
        }

        Vector4 ColorTampon = Color.white;
        curOpacity = minOpacity;

        while (true)
        {
            if (t < animTime)
            {
                t += Time.deltaTime;
                if (Add)
                {

                    if (curOpacity < maxOpacity)
                        curOpacity = Mathf.Lerp(curOpacity, maxOpacity, ratePerSec * Time.deltaTime);
                }
                else
                {

                    if (curOpacity > minOpacity)
                        curOpacity = Mathf.Lerp(curOpacity, minOpacity, ratePerSec * Time.deltaTime);

                }

                for (int i = 0; i < img_ToBreakDown.Length; i++)
                {
                    img_ToBreakDown[i].color = new Vector4(ColorTampon.x, ColorTampon.y, ColorTampon.z, curOpacity);
                }

            }
            else
            {
                Add = !Add;
                t = 0;
            }
            yield return 0;
        }

    }

}
