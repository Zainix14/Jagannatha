using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Le Koa Manager gère tout les Boids et le Koa, exécute les ordres du Flock Manager 
///  | Sur le prefab Flock
///  | Auteur : Zainix
/// </summary>
public class SC_BoidCompute : MonoBehaviour
{

    //Tkt ca marche
    const int threadGroupSize = 3;
    ComputeBuffer boidBuffer;
    BoidData[] boidData;

    [SerializeField]
    ComputeShader compute;
  
    List<Boid> _boidsList; //Tableau contenant les boids
    List<BoidSettings> _settingList; //Tableau contenant les boids
    Coroutine m_boidCor = null;
    

    void Start()
    {
        boidData = new BoidData[100];

        _boidsList = new List<Boid>();
        _settingList = new List<BoidSettings>();
    }

    public void AddNewBoids(List<Boid> newBoids, BoidSettings newSettings)
    {
        foreach(Boid b in newBoids)
        {
            _boidsList.Add(b);
            _settingList.Add(newSettings);
        }


        //Création du buffer | paramètre 1 count : nombre d'éléments | paramètre 2 stride (type de data en bit) : typage du data de BoidData
        boidBuffer = new ComputeBuffer(_boidsList.Count, BoidData.Size);
        m_boidCor = StartCoroutine(BoidTest());
 

    }

    public void ChangeSettings(List<Boid> boids, BoidSettings newSettings)
    {

    }

    public void RemoveBoid(Boid boid)
    {

    }

    IEnumerator BoidTest()
    {
        //Sécurité
        if (_boidsList != null)
        {

            while (true)
            {

                //Ajoute le Koa a la nuée avant les calcules de Flock 
                //_boidsList.Add(_koa);


                int numBoids = _boidsList.Count; //Conversion en int du nombres selon le nombre de boid

                Array.Clear(boidData, 0, boidData.Length); //Burst
                //boidData = new BoidData[numBoids]; //Création d'un variable (Type BoidData) contenant un tableau avec le nombre d'éléments actuels


                for (int i = 0; i < _boidsList.Count; i++)
                {
                    boidData[i].position = _boidsList[i].position; //Chaque élément déjà positionné est stocké
                    boidData[i].direction = _boidsList[i].forward; //Stockage direction
                }

                boidBuffer.SetData(boidData);//Dans boidBuffer (type ComputeBuffer), stocakge data du tableau actuel

                //ComputeShader compute est stockage du Shader

                //Buffer Configuration | 
                //para1 : kernelIndex (ordre de la fonction dans ComputeShader)
                //para2 : name ID (cf ComputeShader ligne 14)
                //para3 : buffer 
                compute.SetBuffer(0, "boids", boidBuffer);

                //Configuration des variables du ComputeShader
                compute.SetInt("numBoids", _boidsList.Count);
                compute.SetFloat("viewRadius", _settingList[0].perceptionRadius);
                compute.SetFloat("avoidRadius", _settingList[0].avoidanceRadius);


                int threadGroups = Mathf.CeilToInt(numBoids / (float)threadGroupSize); //Nombre de groupe = nombre éléments / nombre tkt
                compute.Dispatch(0, threadGroups, 1, 1); //Execute le Shader

                yield return 0;



                boidBuffer.GetData(boidData); //Récupère le résultat du Shader

                //Cf script Boid
                for (int i = 0; i < _boidsList.Count; i++)
                {
                    _boidsList[i].avgFlockHeading = boidData[i].flockHeading; //Stockage pour chaque boid : moyenne devant lui
                    _boidsList[i].centreOfFlockmates = boidData[i].flockCentre; //Stockage pour chaque boid : moyenne à côté
                    _boidsList[i].avgAvoidanceHeading = boidData[i].avoidanceHeading; //Stockage pour chaque boid : moyenne des éléments à éviter
                    _boidsList[i].numPerceivedFlockmates = boidData[i].numFlockmates; //Stockage pour chaque boid : nombre de mate autour

                    _boidsList[i].UpdateBoid(); //Update les boids
                }

                yield return 0;

            }
        }
    }

    /// <summary>
    /// Structure envoyée dans le ComputeShader 
    /// </summary>
    public struct BoidData
    {
        public Vector3 position;
        public Vector3 direction;

        public Vector3 flockHeading;
        public Vector3 flockCentre;
        public Vector3 avoidanceHeading;

        public int numFlockmates;

        //sizeof => retourne la mémoire en bit, pour un type de variable
        //Ici float* 3(Vector3) * 5(nombre de valeurs) + int * 1
        public static int Size
        {
            get
            {
                return sizeof(float) * 3 * 5 + sizeof(int);
            }
        }
    }
}