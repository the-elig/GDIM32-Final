using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.Design;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

public class Player : MonoBehaviour //THIS IS OUR SINGLETON
{
    // event delegates and events
    public delegate void BoolDelegate(bool b);
    public delegate void ObjectDelegate(GameObject o);

    public event BoolDelegate LookingAtInteractable;
    public event ObjectDelegate Interacted;
    public event BoolDelegate Walked;


    // member variables
    [SerializeField] private float _moveSpeed = 3.0f;
    [SerializeField] private float _turnSpeed = 3.0f;
    [SerializeField] private float _mouseSensitivity;
    [SerializeField] private float _interactDistance = 3.0f;
    [SerializeField] private GameObject _interactText;
    [SerializeField] private UIController UI;
    [SerializeField] private float _spawnUp;
    [SerializeField] private float _spawnRight;
    private GameObject _itemOne;
    private GameObject _itemTwo;
    private GameObject _itemThree;
    public bool _itemOut = false;

    private bool _canMoveCamera = true;



    // camera member variables
    private Transform _cameraTrans;
    private float _rotationX;
    private float _rotationY;


    public List<GameObject> _inventory;
    public List<string> _inventoryString;


    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        _cameraTrans = Camera.main.transform; // grabs Camera game object
    }

    void Update()
    {
        // camera follows mouse
        if (_canMoveCamera)
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;

            float mouseY = Input.GetAxis("Mouse Y");
            _rotationY += mouseY * _mouseSensitivity;
            _rotationY = Mathf.Clamp(_rotationY, -60.0f, 60.0f);

            float mouseX = Input.GetAxis("Mouse X");
            _rotationX += mouseX * _mouseSensitivity;

            _cameraTrans.localEulerAngles = new Vector3(-_rotationY, 0, 0);
            transform.localEulerAngles = new Vector3(0, _rotationX, 0);
        }
        else
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }

        // player movement
        float forwardbackwards = Input.GetAxis("Vertical") * _moveSpeed * Time.deltaTime;
        float leftright = Input.GetAxis("Horizontal") * _turnSpeed * Time.deltaTime;

        transform.Translate(0, 0, forwardbackwards);
        transform.Translate(leftright, 0, 0);

        // walk audio
        if(Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.D))
        {
            Walked?.Invoke(true);
        }
        else
        {
            Walked?.Invoke(false);
        }

        // check if looking at an interactable
        Debug.DrawRay(_cameraTrans.position, _cameraTrans.forward * _interactDistance, Color.blue);
        GameObject inter = CheckIfFocused();

        if (inter != null && Input.GetKeyDown(KeyCode.E))
        {
            // if looking at something and pressed E, invoke event
            Interacted?.Invoke(inter);
        }

        EquippedItem();
    }

    private void EquippedItem()
    {
        // item visual position/rotation
        Vector3 myPosition = new Vector3(0.4f, -0.4f, 0.7f);
        Quaternion myRotation = new Quaternion(0, 0, 0, 1);


        // Item one spawn
        if (Input.GetKeyDown(KeyCode.Alpha1) && _inventory.Count >= 1 && _itemOut == false)
        {
            GameObject _itemOne = Instantiate(_inventory[0], _cameraTrans);
            _itemOne.SetActive(true);
            _itemOut = true;

            // if flamethrower, set different rotation
            if (_itemOne.GetComponent<Item>().GetName() == "Flamethrower")
            {
                myPosition = new Vector3(0.657000005f, -0.4f, 0.690999985f);
                myRotation = new Quaternion(-0.52504611f, -0.519083917f, -0.470709532f, 0.483022809f);

                _itemOne.GetComponent<Collider>().enabled = false;
            }

            _itemOne.transform.SetLocalPositionAndRotation(myPosition, myRotation);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha1) && _itemOut == true)
        {
            Destroy(GetComponent<Transform>().GetChild(0).gameObject.GetComponent<Transform>().GetChild(0).gameObject);
            _itemOut = false;
        }

        // Item two spawn
        if (Input.GetKeyDown(KeyCode.Alpha2) && _inventory.Count >= 2 && _itemOut == false)
        {
            GameObject _itemTwo = Instantiate(_inventory[1], _cameraTrans);
            _itemTwo.SetActive(true);
            _itemOut = true;

            // if flamethrower, set different rotation
            if (_itemTwo.GetComponent<Item>().GetName() == "Flamethrower")
            {
                myPosition = new Vector3(0.657000005f, -0.4f, 0.690999985f);
                myRotation = new Quaternion(-0.52504611f, -0.519083917f, -0.470709532f, 0.483022809f);

                _itemTwo.GetComponent<Collider>().enabled = false;
            }

            _itemTwo.transform.SetLocalPositionAndRotation(myPosition, myRotation);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2) && _itemOut == true)
        {
            Destroy(GetComponent<Transform>().GetChild(0).gameObject.GetComponent<Transform>().GetChild(0).gameObject);
            _itemOut = false;
        }

        // Item three spawn
        if (Input.GetKeyDown(KeyCode.Alpha3) && _inventory.Count >= 3 && _itemOut == false)
        {
            GameObject _itemThree = Instantiate(_inventory[2], _cameraTrans);
            _itemThree.SetActive(true);
            _itemOut = true;

            // if flamethrower, set different position and turn off collider
            if (_itemThree.GetComponent<Item>().GetName() == "Flamethrower")
            {
                myPosition = new Vector3(0.657000005f, -0.4f, 0.690999985f);
                myRotation = new Quaternion(-0.52504611f, -0.519083917f, -0.470709532f, 0.483022809f);

                _itemThree.GetComponent<Collider>().enabled = false;
            }

            _itemThree.transform.SetLocalPositionAndRotation(myPosition, myRotation);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha3) && _itemOut == true)
        {
            Destroy(GetComponent<Transform>().GetChild(0).gameObject.GetComponent<Transform>().GetChild(0).gameObject);
            _itemOut = false;
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
                _interactText.SetActive(true);
                return seen.collider.gameObject;
            }
            else
            {
                _interactText.SetActive(false);
                LookingAtInteractable.Invoke(false);
            }
        }
        else
        {
            LookingAtInteractable.Invoke(false);
        }

        return null;

    }

    public void SetCanMoveCamera(bool b)
    {
        _canMoveCamera = b;
    }

    
    
    //singleton stuff
    public static Player Instance { get; private set; }
    public Player _player { get; private set; }
    public ScreamingChildren _childInstance {get; private set; }
    public ScreamingChildren _screamingChildren {get; private set; }
    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this);
            return;
        }


        Instance = this;

        DontDestroyOnLoad(this);
    }
}