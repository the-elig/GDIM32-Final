using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlamethrowerLogic : MonoBehaviour
{
    [SerializeField] private GameController _gc;

    private int _meltingDelay = 0;

    private void Start()
    {
        _gc = FindObjectOfType<GameController>().GetComponent<GameController>();

    }

    
    private void OnTriggerStay(Collider other)
    {
        Debug.Log("ontrigger called");

        if (other.gameObject.CompareTag("Ice"))
        {
            _meltingDelay++; 
            Debug.Log("melting ice: " + _meltingDelay);

            if (_meltingDelay > 75) //must be colliding for 75 frames 
                _gc.MeltIce(other.gameObject);
        }
    }
    
}
