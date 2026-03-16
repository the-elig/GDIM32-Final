using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public abstract class Interactable : MonoBehaviour
{
    [SerializeField] public string _name;
    public bool _hasInteracted = false;

    private void Update()
    {
        if (_hasInteracted == true && gameObject.activeSelf == true)
        {
            Destroy(gameObject);
        }
    }

    public string GetName()
    {
        return _name;
    }

}

