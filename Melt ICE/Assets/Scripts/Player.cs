using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.Design;
using Unity.VisualScripting;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private float _moveSpeed = 3.0f;
    [SerializeField] private float _turnSpeed = 3.0f;
    [SerializeField] private float _mouseSensitivity;
    [SerializeField] private float _interactDistance = 3.0f;

    [SerializeField] private GameObject TEMP_UI;

    //the singleton
    public static Player Instance {get; private set;}


    // camera member variables
    private Transform _cameraTrans;
    private float _rotationX;
    private float _rotationY;
    

    void Awake()
    {
        if(Instance != null && Instance != this)
        {
            Destroy(this);
            return;
        }
        Instance = this;

        GameObject thePlayer = GameObject.FindWithTag("Player");
    }
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        _cameraTrans = Camera.main.transform; // grabs Camera game object
    }

    // create an event delegate saying you are looking at an interactable object

    void Update()
    {
        // camera follows mouse
        

        float mouseY = Input.GetAxis("Mouse Y");
        _rotationY += mouseY * _mouseSensitivity;
        _rotationY = Mathf.Clamp(_rotationY, -60.0f, 60.0f);

        float mouseX = Input.GetAxis("Mouse X");
        _rotationX += mouseX * _mouseSensitivity;

        _cameraTrans.localEulerAngles = new Vector3(-_rotationY, 0, 0);
        transform.localEulerAngles = new Vector3(0, _rotationX, 0);



        // player movement
        float forwardbackwards = Input.GetAxis("Vertical") * _moveSpeed * Time.deltaTime;
        float leftright = Input.GetAxis("Horizontal") * _turnSpeed * Time.deltaTime;

        transform.Translate(0, 0, forwardbackwards);
        transform.Translate(leftright, 0, 0);

        Debug.DrawRay(_cameraTrans.position, _cameraTrans.forward * _interactDistance, Color.blue);

        CheckIfLookingLine();
        CheckIfFocused();



        
    }

    private void CheckIfLookingLine()
    {
        // raycast interact distance
        RaycastHit hit;
      
        if (Physics.Raycast(transform.position, transform.forward, out hit, _interactDistance))
            {
                if (hit.collider.gameObject.CompareTag("Interactable"))
                {
                    TEMP_UI.SetActive(true);
                }
                else
                {
                    TEMP_UI.SetActive(false);
                }
        }
        else
        {
            TEMP_UI.SetActive(false);
        }  
    }

    //This is to check if the raycast attached to the player cursor actually hit something
    private void CheckIfFocused()
    {
        RaycastHit seen;
        if(Physics.Raycast(_cameraTrans.position, _cameraTrans.forward, out seen, _interactDistance))
        {
            if (seen.collider.gameObject.CompareTag("Interactable"))
                {
                    TEMP_UI.SetActive(true);
                }
                else
                {
                    TEMP_UI.SetActive(false);
                }
        }
        else
        {
            TEMP_UI.SetActive(false);
        } 
        
    }


    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawRay(transform.position, transform.forward * _interactDistance);
        //Gizmos.color = Color.blue;
        //Gizmos.DrawRay(_cameraTrans.position, _cameraTrans.forward * _interactDistance);

    }
}
