using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.Experimental.GraphView.GraphView;

public enum NPCSpeech
{
    Idle, Aware, Talking

}

public class NPC : Interactable
{
    public NPCSpeech _npcReaction;

    // dialogue member variables
    [SerializeField] private UIController _dialogue;
    public DialogueNode _dialogueStartNode;
    private DialogueNode _currentNode;
    private int _currentLine = 0;
    private bool _waitingForPlayerResponse;

    // animation member variables
    [SerializeField] private float _awareDistance = 6.0f;
    [SerializeField] private float _playerDistance;
    [SerializeField] private Animator _animator;
    [SerializeField] private Player _player;

    private void Start()
    {
        _currentNode = _dialogueStartNode;
    }

    void Update()
    {
        _playerDistance = Vector3.Distance(_player.transform.position, transform.position);
        RunState(_animator);
        NPCState();

        if (_npcReaction == NPCSpeech.Talking && Input.GetKeyDown(KeyCode.E))
        {
            AdvanceDialogue();
        }
    }

    public void SetToTalking() // called by gamecontroller when interacts with npc
    {
        Debug.Log("set to talking");

        _npcReaction = NPCSpeech.Talking;
        _dialogue.ShowDialogue(_currentNode._lines[_currentLine]);
    }

    private void AdvanceDialogue()
    {
        Debug.Log("advanced dialogue");
        if (_currentLine < _currentNode._lines.Length)
        {
            // if we still have NPC lines left, keep playing NPC lines
            _dialogue.ShowDialogue(_currentNode._lines[_currentLine]);
            _currentLine++;
        }
        else if (_currentNode._playerReplyOptions != null && _currentNode._playerReplyOptions.Length > 0)
        {
            // show player dialogue options, if there are any
            _waitingForPlayerResponse = true;
            _dialogue.ShowPlayerOptions(_currentNode._playerReplyOptions);
        }
        else
        {
            // if there are no NPC or player lines left, close dialogue UI
            EndDialogue();
        }
    }

    public void SelectedOption(int option)
    {
        _currentLine = 0;
        _waitingForPlayerResponse = false;

        _currentNode = _currentNode._npcReplies[option];
        AdvanceDialogue();
    }

    private void EndDialogue()
    {
        Debug.Log("ended dialogue");

        _npcReaction = NPCSpeech.Idle; // put state off talking
        _waitingForPlayerResponse = false;
        _currentNode = _dialogueStartNode;
        _currentLine = 0;

        _dialogue.HideDialogue();
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
        }
        else // not in interact distance
        {
            _npcReaction = NPCSpeech.Idle;
        }
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
