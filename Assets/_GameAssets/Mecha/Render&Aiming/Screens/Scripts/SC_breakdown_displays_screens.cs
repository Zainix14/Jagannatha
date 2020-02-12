using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// script sur le parent des ecrans de panne
/// </summary>

public class SC_breakdown_displays_screens : MonoBehaviour
{

    private int curNbPanne = 0;

    public Renderer[] tab_screens_renderers;

    // Start is called before the first frame update
    void Start()
    {
        tab_screens_renderers = new Renderer[gameObject.transform.childCount];

        for (int i = 0; i < gameObject.transform.childCount; i++)
        {
            tab_screens_renderers[i] = gameObject.transform.GetChild(i).GetComponent<Renderer>();
            tab_screens_renderers[i].enabled = false;
        }

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.J))
        {

                PutXenPanne(2);

        }

        if (Input.GetKeyDown(KeyCode.K))
        {

            RepairAll();

        }
    }

    void PutXenPanne(int x)
    {
        for (int i = 0; i < x; i++)
        {
            if (curNbPanne < tab_screens_renderers.Length)
            {
                int rand = Random.Range(0, tab_screens_renderers.Length);
                if (tab_screens_renderers[rand].enabled)
                {

                    i--;

                }
                else
                {
                    tab_screens_renderers[rand].enabled = true;
                    curNbPanne++;

                }
            }
                
        }


    }

    void RepairAll()
    {
        for (int i = 0; i < tab_screens_renderers.Length; i++)
        {
            tab_screens_renderers[i].enabled = false;
            curNbPanne = 0;
        }
    }
}
