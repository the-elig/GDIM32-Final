using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UIController : MonoBehaviour
{
    [SerializeField] private TMP_Text _interactText;
    [SerializeField] private TMP_Text _inventoryText;
    [SerializeField] private TMP_Text _notifText;


    // Start is called before the first frame update
    void Start()
    {
        Locator.Instance.Player.LookingAtInteractable += ShowInteract;

    }

    // Update is called once per frame
    void Update()
    {

    }

    private void ShowInteract(bool b)
    {
        _interactText.enabled = b;
    }
}
