using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public enum NPCSpeech
{
    Idle, Aware, Talking
}

public class NPC : Interactable
{
    public NPCSpeech _npcReaction;
    public bool _playerHasKeyItem;


    // dialogue member variables
    [SerializeField] private DialogueController _dControl;
    [SerializeField] private string _keyItem; // assigned individually to NPC
    public DialogueNode[] _dialogueStartingNodes; // list of starting dialogue depending on _hasKeyItem 
    

    // animation member variables
    [SerializeField] private float _awareDistance = 6.0f;
    [SerializeField] private float _playerDistance;
    [SerializeField] private Animator _animator;
    [SerializeField] private string _SceneName;

    private void Start()
    {
        _dControl = FindObjectOfType<DialogueController>().GetComponent<DialogueController>();
    }

    void Update()
    {
        _playerHasKeyItem = Player.Instance._inventoryString.Contains(_keyItem); //updates _hasKeyItem
        _playerDistance = Vector3.Distance(Player.Instance.transform.position, transform.position);
        
        RunState(_animator);
        NPCState();

        if (_npcReaction == NPCSpeech.Talking // if talking and get continue input
            && (Input.GetKeyDown(KeyCode.E) || Input.GetMouseButtonDown(0)))
        {
            _dControl.AdvanceDialogue();
        }
        string currentSceneName = SceneManager.GetActiveScene().name;
        if (_SceneName != currentSceneName)
        {
            gameObject.SetActive(false);
        }
    }



    private void NPCState()
    {
        if (_npcReaction == NPCSpeech.Talking)
        {
            // if it's talking, don't change state
        }
        else if (_playerDistance <= _awareDistance) // if in interact distance
        {
            _npcReaction = NPCSpeech.Aware;
            transform.LookAt(Player.Instance.transform);
            transform.eulerAngles = new Vector3(0, transform.eulerAngles.y, 0);
        }
        else // not in interact distance
        {
            _npcReaction = NPCSpeech.Idle;
        }
    }

    public NPC getNPC()
    {
        return this;
    }

    private void RunState(Animator _animator)
    {
        switch (_npcReaction)
        {
            case NPCSpeech.Idle:
                PlayIdleAni(_animator);
                break;
            case NPCSpeech.Aware:
                PlayAwareAni(_animator);
                break;
            case NPCSpeech.Talking:
                PlayTalkAni(_animator);
                break;
        }
    }

    private void PlayTalkAni(Animator _animator)
    {
        _animator.SetBool("Talk", true);
        _animator.SetBool("Aware", false);

    }
    private void PlayIdleAni(Animator _animator)
    {
        _animator.SetBool("Talk", false);
        _animator.SetBool("Aware", false);
    }
    private void PlayAwareAni(Animator _animator)
    {
        _animator.SetBool("Talk", false);
        _animator.SetBool("Aware", true);
    }
}
