using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Test : MonoBehaviour
{
    public UnityEvent evento;

    private void Start()
    {
        
    }

    private void Update()
    {
        PresionarTecla();
    }

    private void PresionarTecla()
    {
        if (Input.GetButtonDown("Jump"))
        {
            evento.Invoke();
        }
    }
}
