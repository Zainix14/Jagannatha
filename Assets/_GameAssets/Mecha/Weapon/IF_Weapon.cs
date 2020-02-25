using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IF_Weapon
{
    Vector3Int GetWeaponSensitivity();
    void Trigger();

    void ReleaseTrigger();

    void SetSensitivity(int index,int value);

}

