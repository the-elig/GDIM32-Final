using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DontDestroy : MonoBehaviour
{
    // Start is called before the first frame update
    void Awake()
    {
        for (int i = 0; i < Object.FindObjectsOfType<DontDestroy>().Length; i++)
        {
            if (Object.FindObjectsOfType<DontDestroy>()[i] != this)
            {
                if (Object.FindObjectsOfType<DontDestroy>()[i].name == gameObject.name)
                {
                    Destroy(gameObject);
                }
            }
        }
        DontDestroyOnLoad(gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        if (gameObject.activeSelf == false)
        {
            for (int i = 0; i < Object.FindObjectsOfType<DontDestroy>().Length; i++)
            {
                if (Object.FindObjectsOfType<DontDestroy>()[i] != this)
                {
                    if (Object.FindObjectsOfType<DontDestroy>()[i].name == gameObject.name)
                    {
                        Destroy(gameObject);
                        Debug.Log("So this is happening");
                    }
                }
            }
        }
    }
}
