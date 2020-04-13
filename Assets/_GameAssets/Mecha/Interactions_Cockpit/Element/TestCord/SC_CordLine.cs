using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SC_CordLine : MonoBehaviour
{

    [SerializeField]
    GameObject BaseHelper;
    [SerializeField]
    GameObject IntHelper;
    [SerializeField]
    LineRenderer LineRenderer;

    // Start is called before the first frame update
    void Start()
    {
        SetLineAnchor();
    }

    // Update is called once per frame
    void Update()
    {
        SetLineAnchor();
    }

    void SetLineAnchor()
    {
        LineRenderer.SetPosition(0, IntHelper.transform.parent.localPosition + (IntHelper.transform.position - IntHelper.transform.parent.position));
        LineRenderer.SetPosition(1, BaseHelper.transform.parent.localPosition + (BaseHelper.transform.position - BaseHelper.transform.parent.position));

    }

}
