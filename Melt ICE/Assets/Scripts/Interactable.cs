using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Interactable : MonoBehaviour
{
    [SerializeField] public string _name;
    [SerializeField] public GameObject _prefab;

    public string GetName()
    { 
        return _name; 
    }

    public GameObject GetPrefab() 
    {  
        return _prefab; 
    }
}

