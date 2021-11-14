using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DontDestoyOnLoad : MonoBehaviour
{
    void Start()
    {
        DontDestroyOnLoad(this.gameObject);
        
    }
}
