using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

public class Item : Interactable
{
    [SerializeField] private GameObject _prefab;
   public GameObject GetPrefab()
    {
        return _prefab;
    }
}
