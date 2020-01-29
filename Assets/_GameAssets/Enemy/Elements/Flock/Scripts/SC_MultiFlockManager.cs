using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SC_MultiFlockManager : MonoBehaviour
{
    List<GameObject> _flockList;
    List<Transform> _flockGuideList;
    List<Transform> _guideList;

    public Transform _guidePrefab;
    int _SpreadFactor = 4;
    Transform _mainGuide;

    public void Initialize(List<GameObject> newFlocks)
    {
        
        _mainGuide = transform;
        _flockList = newFlocks;
        _flockGuideList = new List<Transform>();
        _guideList = new List<Transform>();
        Debug.Log(newFlocks);
        foreach (GameObject flock in _flockList)
        {
            _flockGuideList.Add(flock.transform);
            flock.GetComponent<SC_FlockManager>()._merged = true;
            Transform newGuide = Instantiate(_guidePrefab, _mainGuide);
            newGuide.parent = _mainGuide;
            _guideList.Add(newGuide);
        }
    }

    void MoveAround()
    {
        for(int i =0; i<_guideList.Count;i++)
        {
            Vector3 rand =new Vector3(Random.Range(-1, 1)* _SpreadFactor, Random.Range(-1, 1) * _SpreadFactor, Random.Range(-1, 1) * _SpreadFactor);
            _guideList[i].localPosition += rand;

            //ACHANGER
            _flockGuideList[i].position = _guideList[i].position;


        }
    }
    


    // Update is called once per frame
    void Update()
    {
           MoveAround();    
    }


}
