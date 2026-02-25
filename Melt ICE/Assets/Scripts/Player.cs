using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.Design;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEditor.Progress;

public class Player : MonoBehaviour
{
    // event delegates and events
    public delegate void BoolDelegate(bool b);
    public delegate void ObjectDelegate(GameObject o);
    public delegate void WalkDelegate(bool a);



    public event BoolDelegate LookingAtInteractable;
    public event ObjectDelegate Interacted;
    public event WalkDelegate Walked;


    [SerializeField] private float _moveSpeed = 3.0f;
    [SerializeField] private float _turnSpeed = 3.0f;
    [SerializeField] private float _mouseSensitivity;
    [SerializeField] private float _interactDistance = 3.0f;


    // camera member variables
    private Transform _cameraTrans;
    private float _rotationX;
    private float _rotationY;


    [SerializeField] private List<string> _inventory;

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
        if(Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.D))
        {
            Walked.Invoke(true);
        }
        else
        {
            Walked.Invoke(false);
        }

        // check if looking at an interactable
        Debug.DrawRay(_cameraTrans.position, _cameraTrans.forward * _interactDistance, Color.blue);
        GameObject inter = CheckIfFocused();

        if (inter != null && Input.GetKeyDown(KeyCode.E))
        {
            // if looking at something and pressed E, invoke event
            Interacted?.Invoke(inter);
            _inventory.Add(inter.GetComponent<Interactable>().GetName()); //add to inventory

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
                LookingAtInteractable.Invoke(true);
                return seen.collider.gameObject;
            }
            else
            {
                LookingAtInteractable.Invoke(false);
            }
        }
        else
        {
            LookingAtInteractable.Invoke(false);
        }

        return null;

    }
}