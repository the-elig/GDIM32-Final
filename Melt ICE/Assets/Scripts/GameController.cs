using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public enum _objective
{
    empty, cup, key, flamethrower
}
public class GameController : MonoBehaviour
{
    public static GameController Instance { get; private set; }
    public Player Player { get; private set; }


    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this);
            return;
        }

        Instance = this;
        GameObject _player = GameObject.FindWithTag("Player");
        Player = _player.GetComponent<Player>();
    }


    public void Start()
    {
        Player.Interacted += PlayerInteracted;
    }


    public void PlayerInteracted(Interactable inter)
    {
        Debug.Log("interacted");
    }
}