using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioController : MonoBehaviour
{
    [SerializeField] private AudioSource _walk;
    [SerializeField] private AudioSource _collected;

    
    // Start is called before the first frame update
    
    void Start()
    {
        Locator.Instance.Player.Walked += Walk;
        //Locator.Instance.GameController.PickedUp += Collected;

    }

    // Update is called once per frame

    
    private void Walk(bool a)
    {
        if(a == true)
        {
            _walk.enabled = true;

        }
        else
        {
            _walk.enabled = false;
        }
    }
    /*
    private void Collected()
    {
        
    
        _collected.enabled = true; 

        
    }

    private void Equipped(bool c)
    {
        if(c == true)
        {
            _walk.enabled = true;

        }
        else
        {
            _walk.enabled = false;
        }
    }
    */
}
