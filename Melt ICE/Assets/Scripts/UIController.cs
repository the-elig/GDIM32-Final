using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using static UnityEditor.Progress;

public class UIController : MonoBehaviour
{
    // basic UI
    [SerializeField] private TMP_Text _interactText;
    [SerializeField] public TMP_Text _inventoryText;
    [SerializeField] private GameObject _inventorybox;
    [SerializeField] public GameObject _notifText;
    private bool _boxActive = false;

    // dialogue UI
    [SerializeField] private GameObject _dialogueBox;
    [SerializeField] private TMP_Text _npcText;
    [SerializeField] private GameObject _playerOptions;
    [SerializeField] private TMP_Text _option1;
    [SerializeField] private TMP_Text _option2;
    [SerializeField] private TMP_Text _option3;

    private void Awake()
    {
        DontDestroyOnLoad(this);
    }

    // Start is called before the first frame update
    void Start()
    {
        Player.Instance.LookingAtInteractable += ShowInteract;

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


    // dialogue logic
    public void ShowDialogue(string dialogue)
    {
        _dialogueBox.SetActive(true);

        _npcText.enabled = true;
        _playerOptions.SetActive(false);

        _npcText.text = dialogue;
    }

    // note: this only works for up to 3 dialogue options at a time currently
    // if you want to make more possible, you may have to get crafty with the UI... :)
    public void ShowPlayerOptions(string[] options)
    {
        _dialogueBox.SetActive(true);

        _npcText.enabled = false;
        _playerOptions.SetActive(true);

        _option1.text = options[0];

        if (options.Length >= 2)
        {
            _option2.transform.parent.gameObject.SetActive(true);
            _option2.text = options[1];
        }
        else
        {
            _option2.transform.parent.gameObject.SetActive(false);
        }

        if (options.Length >= 3)
        {
            _option3.transform.parent.gameObject.SetActive(true);
            _option3.text = options[2];
        }
        else
        {
            _option3.transform.parent.gameObject.SetActive(false);
        }
    }

    public void HideDialogue()
    {
        _dialogueBox.SetActive(false);
        _playerOptions.SetActive(false);
        _npcText.enabled = false;

        Player.Instance.SetCanMoveCamera(true);
    }
}
