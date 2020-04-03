using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SC_Movement_Direction : MonoBehaviour
{

    public bool b_IsBreak = false;
    public bool b_InUse = false;

    public float speedRotateInit;
    public float speedRotateUsed;

    public float CurRotationSpeed;

    // Update is called once per frame
    void Update()
    {
        Rotate();
    }

    void Rotate()
    {

        if (!SC_SyncVar_MovementSystem.Instance.b_BreakEngine)
        {

            if (!b_IsBreak)
            {
                if (b_InUse)
                    this.transform.Rotate(Vector3.up * speedRotateUsed * Time.deltaTime, Space.World);
                else
                    this.transform.Rotate(Vector3.up * speedRotateInit * Time.deltaTime, Space.World);
            }

            else
            {

                switch (SC_SyncVar_MovementSystem.Instance.n_BreakDownLvl)
                {

                    case 0:
                        if (b_InUse)
                            this.transform.Rotate(Vector3.up * speedRotateUsed * Time.deltaTime, Space.World);
                        else
                            this.transform.Rotate(Vector3.up * speedRotateInit * Time.deltaTime, Space.World);
                        break;

                    case 1:
                        if (b_InUse)
                            this.transform.Rotate(Vector3.up * (speedRotateUsed / 2) * Time.deltaTime, Space.World);
                        else
                            this.transform.Rotate(Vector3.up * speedRotateInit * Time.deltaTime, Space.World);
                        break;

                    case 2:
                        if (b_InUse)
                        {
                            int n_Corofactor = 1;
                            if (SC_SyncVar_MovementSystem.Instance.CoroDir == SC_JoystickMove.Dir.None || SC_SyncVar_MovementSystem.Instance.CoroDir == SC_JoystickMove.Dir.Off)
                                n_Corofactor = 0;
                            this.transform.Rotate(Vector3.up * (speedRotateUsed / 2) * n_Corofactor * Time.deltaTime, Space.World);
                        }
                        else
                            this.transform.Rotate(Vector3.up * speedRotateInit * Time.deltaTime, Space.World);
                        break;

                    case 3:
                        this.transform.Rotate(Vector3.up * 0 * Time.deltaTime, Space.World);
                        break;

                }

            }

        }
        
    }

}
