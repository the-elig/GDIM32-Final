using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public enum _objective
{
    empty, cup, key, flamethrower
}

public class GameController : MonoBehaviour
{
    [SerializeField] private Item[] _items;
    [SerializeField] private NPC[] _NPCs;


    public void Start()
    {
        Locator.Instance.Player.Interacted += PlayerInteracted;
        Debug.Log("GameControl start");
    }


    private void PlayerInteracted(Interactable inter)
    {
        Debug.Log("interacted");
    }
}