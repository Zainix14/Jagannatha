using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SC_KoaCollider : MonoBehaviour
{
    SC_KoaManager manager;
    // Start is called before the first frame update
    public void Initialize(SC_KoaManager manager)
    {
        this.manager = manager;
    }

    // Update is called once per frame
    public void GetHit()
    {
        manager.GetHit();
    }
}
