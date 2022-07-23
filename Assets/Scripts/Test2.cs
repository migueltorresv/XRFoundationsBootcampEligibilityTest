using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test2 : MonoBehaviour
{
    private Test _test;

    private void Start()
    {
        _test = GetComponent<Test>();
        _test.evento.AddListener(Accion);
    }

    private void Accion()
    {
        Debug.Log("mensaje Test2");
    }
}
