using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UIController : MonoBehaviour
{
    [SerializeField] private TMP_Text _interactText;
    [SerializeField] public TMP_Text _inventoryText;
    [SerializeField] private GameObject _inventorybox;
    [SerializeField] private TMP_Text _notifText;
    private bool _boxActive = false;

    private void Awake()
    {
        DontDestroyOnLoad(this);
    }

    // Start is called before the first frame update
    void Start()
    {
        Locator.Instance.Player.LookingAtInteractable += ShowInteract;

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.I) && _boxActive == false)
        {
            _inventorybox.SetActive(true);
            _boxActive = true;
        }
        else if (Input.GetKeyDown(KeyCode.I) && _boxActive == true)
        {
            _inventorybox.SetActive(false);
            _boxActive = false;
        }
    }

    private void ShowInteract(bool b)
    {
        _interactText.enabled = b;
    }
}
