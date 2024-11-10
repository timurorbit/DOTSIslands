using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;

public class CanvasManager : MonoBehaviour
{
    public void GetData()
    {
        StartCoroutine(DataTransfer.GetDataCoroutine());
    }

    public void LoadScene(string scene)
    {
        SceneManager.LoadScene(scene);
    }
}