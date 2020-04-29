using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IF_GetRightController
{
    GameObject getGameObject();
}

public class SC_GetRightController : MonoBehaviour, IF_GetRightController
{

    public GameObject getGameObject()
    {
        return gameObject;
    }

}
