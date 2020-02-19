using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SC_UI_Display_MapInfos_animNeutral : MonoBehaviour
{
    [SerializeField]
    Text neutralText;
    bool onUp = true;
    float compt;
    float delay = 1 ;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        animText(neutralText);

    }

    void animText(Text textToAnim)
    {
        if (onUp)
        {
            compt += Time.deltaTime;
            if (compt < delay)
            {
                textToAnim.transform.localScale += new Vector3(0.01f, 0.01f, 0.01f);
            }
            else if (compt >= delay)
            {
                onUp = false;
            }
        }
        else
        {
            compt -= Time.deltaTime;
            if(compt > 0)
            {
                textToAnim.transform.localScale -= new Vector3(0.01f, 0.01f, 0.01f);
            }
            else if(compt <= 0)
            {
                onUp = true;
            }
        }
        
        
    }
}
