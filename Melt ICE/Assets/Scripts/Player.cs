using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private float _moveSpeed = 3.0f;
    [SerializeField] private float _turnSpeed = 3.0f;

    void Start()
    {
        
    }

    void Update()
    {
        // player movement
        float forwardbackwards = Input.GetAxis("Vertical") * _moveSpeed * Time.deltaTime;
        float leftright = Input.GetAxis("Horizontal") * _turnSpeed * Time.deltaTime;

        transform.Translate(0, 0, forwardbackwards);
        transform.Translate(leftright, 0, 0);

        // still need to do camera rotation (following mouse, which idk how to do yet lmao)
    }
}
