using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SC_HitDisplay : MonoBehaviour
{
    #region Singleton

    private static SC_HitDisplay _instance;
    public static SC_HitDisplay Instance { get { return _instance; } }

    #endregion


    [SerializeField]
    GameObject pivot;
    [SerializeField]
    GameObject rotor;

    [SerializeField]
    float rotorDistance;


    void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            _instance = this;
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Hit(Vector3 hitPos)
    {
        Vector3 Direction = hitPos.normalized;
        rotor.transform.position = pivot.transform.position;
        rotor.transform.position += new Vector3(Direction.x * rotorDistance,0, Direction.z * rotorDistance);
    }
}
