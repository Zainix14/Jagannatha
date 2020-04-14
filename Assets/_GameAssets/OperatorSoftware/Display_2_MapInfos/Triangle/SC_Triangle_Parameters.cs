using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SC_Triangle_Parameters : MonoBehaviour
{
    GameObject Mng_SyncVar = null;
    SC_SyncVar_calibr sc_syncvar;
    [SerializeField]
    Material triangleMat;


    Mesh triangleMesh;
    MeshRenderer msh_Rend;
    Vector3[] vertex;

    public float amplitudeValue;

    public float frequenceValue;

    public float phaseValue;

    [SerializeField]
    float speed = 5;
    int[] triangles;

    Vector3[] vertexPos;

    public bool b_Init = false;
    // Start is called before the first frame update
    void Start()
    {
        Mng_SyncVar = GameObject.FindGameObjectWithTag("Mng_SyncVar");
        GetReferences();

        gameObject.AddComponent<MeshFilter>();
        msh_Rend =  gameObject.AddComponent<MeshRenderer>();

        msh_Rend.material = triangleMat;

        triangleMesh = new Mesh();
        GetComponent<MeshFilter>().mesh = triangleMesh;

        vertex = new[]
        {
            new Vector3(0,0,0),
            new Vector3(0,1,0),
            new Vector3(1,0,0),
        };
        vertexPos = new Vector3[3];
        triangleMesh.vertices = vertex;

        triangles = new[] { 0, 1, 2 };

        triangleMesh.triangles = triangles;
        for (int i = 0; i < vertexPos.Length; i++)
        {
            vertexPos[i] = vertex[i];
        }
       
    }

    void GetReferences()
    {
        if (Mng_SyncVar == null)
            Mng_SyncVar = GameObject.FindGameObjectWithTag("Mng_SyncVar");
        if (Mng_SyncVar != null && sc_syncvar == null)
            sc_syncvar = Mng_SyncVar.GetComponent<SC_SyncVar_calibr>();
    }

    // Update is called once per frame
    void Update()
    {
            if (sc_syncvar == null || Mng_SyncVar == null)
        {
            GetReferences();

        }
        if (sc_syncvar != null)
        {
            if(b_Init)
            {
                initTriangle();
            }
            else
            {
                animTriangle();
            }
        }
    }

    void initTriangle()
    {
        
        for (int i = 0; i < vertexPos.Length; i++)
        {
            Debug.Log("Come here");
            vertex[i] = Vector3.zero;
            vertexPos[i] = Vector3.zero;
        }
        triangleMesh.vertices = vertex;
        
    }

    void animTriangle()
    {
            vertexPos[0] = new Vector3(-frequenceValue * 0.86f, -frequenceValue / 2, 0);
            vertexPos[1] = new Vector3(0, amplitudeValue, 0);
            vertexPos[2] = new Vector3(phaseValue * 0.86f, -phaseValue / 2, 0);


            for (int i = 0; i < vertexPos.Length; i++)
            {
                vertex[i] = Vector3.Lerp(vertex[i], vertexPos[i], Time.deltaTime * speed);
            }


            triangleMesh.vertices = vertex;
            triangleMesh.triangles = triangles;
        
    }
}   
