/*
fajdkfas


using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;

public class Sister : NPC
{
    // animation member variables
    [SerializeField] private float _awareDistance = 6.0f;
    [SerializeField] private float _playerDistance;
    [SerializeField] private Animator _animator;
    [SerializeField] private Player _player;
    private NPCSpeech _npcReaction;
   

    void Update()
    {
        _playerDistance = Vector3.Distance(_player.transform.position, transform.position);
        RunState(_animator);
        NPCState();
    }

    public void NPCState()
    {
        if(_playerDistance <= _awareDistance)
        {
            _npcReaction = NPCSpeech.Aware;
        }
        
        //else if(you press E and are talking to this dude)
        //{
        //    _npcReaction = NPCSpeech.Talking;
        //}
        
        else
        {
            _npcReaction = NPCSpeech.Idle;
        }
    }
    

    public void Talk()
    {
        throw new System.NotImplementedException();
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
*/
