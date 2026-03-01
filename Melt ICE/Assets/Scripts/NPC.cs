using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum NPCSpeech
{
    Idle, Aware, Talking

}

public abstract class NPC : Interactable
{
    // Start is called before the first frame update
    public abstract void Talk();
    public abstract void NPCState();
   
}
