using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

public class Item : Interactable
{
   public GameObject GetPrefab()
    {
        return _prefab;
    }
}
