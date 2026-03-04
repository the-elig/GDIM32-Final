using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public abstract class Interactable : MonoBehaviour
{
    [SerializeField] private string Scene;
    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }

    private void Update()
    {
        string _sceneName = SceneManager.GetActiveScene().name;
        Debug.Log(_sceneName);
        if (_sceneName != Scene)
        {
            gameObject.SetActive(false);
        }
    }
}

