using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.Design;
using Unity.VisualScripting;
using UnityEngine;

public class Player : MonoBehaviour
{
    // event delegates and events
    public delegate void InterDelegate(Interactable i);
    public delegate void ObjectDelegate(GameObject o);
    public event ObjectDelegate Interacted;


    [SerializeField] private float _moveSpeed = 3.0f;
    [SerializeField] private float _turnSpeed = 3.0f;
    [SerializeField] private float _mouseSensitivity;
    [SerializeField] private float _interactDistance = 3.0f;

    [SerializeField] private GameObject TEMP_UI;


    // camera member variables
    private Transform _cameraTrans;
    private float _rotationX;
    private float _rotationY;
    

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        _cameraTrans = Camera.main.transform; // grabs Camera game object
    }

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


        // check if looking at an interactable
        Debug.DrawRay(_cameraTrans.position, _cameraTrans.forward * _interactDistance, Color.blue);
        GameObject inter = CheckIfFocused();

        if (inter != null && Input.GetKeyDown(KeyCode.E))
        {
            // if looking at something and pressed E, invoke event
            Interacted?.Invoke(inter);
        }
    }


    //This is to check if the raycast attached to the player cursor actually hit something
    private GameObject CheckIfFocused()
    {
        RaycastHit seen;
        if (Physics.Raycast(_cameraTrans.position, _cameraTrans.forward, out seen, _interactDistance))
        {
            if (seen.collider.gameObject.CompareTag("Interactable"))
            {
                TEMP_UI.SetActive(true);
                return seen.collider.gameObject;
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

        return null;

    }
}