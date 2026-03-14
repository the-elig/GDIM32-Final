using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlamethrowerLogic : MonoBehaviour
{
    [SerializeField] private Collider _flame;

    private void OnTriggerEnter(Collider other)
    {
        if (gameObject.GetComponent<Collider>() == _flame && other.CompareTag("Ice"))
        {

        }
    }
}
