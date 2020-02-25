using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SC_KoaID_operator_display : MonoBehaviour
{

    SC_KoaSettingsOP sc_koa;

    TextMesh txt;

    // Start is called before the first frame update
    void Start()
    {
        sc_koa = transform.parent.GetComponent<SC_KoaSettingsOP>();
        txt = this.GetComponent<TextMesh>();
    }

    // Update is called once per frame
    void Update()
    {
        txt.text = sc_koa.GetKoaID();
    }
}
