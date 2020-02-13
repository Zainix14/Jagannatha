using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IInteractible
{
    void ChangeDesired();
    void Repair();
    bool isBreakdown();

}
