using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Networking;

public static class DataTransfer
{
    private static string IslandData { get; set; }
    
    static string URL = "https://jobfair.nordeus.com/jf24-fullstack-challenge/test";
    public static IEnumerator GetDataCoroutine()
    {
        using UnityWebRequest request = UnityWebRequest.Get(URL);
        yield return request.SendWebRequest();

        if (request.result == UnityWebRequest.Result.ConnectionError ||
            request.result == UnityWebRequest.Result.ProtocolError)
        {
            Debug.LogError("Error: " + request.error);
        }
        else
        {
            string jsonData = request.downloadHandler.text;
            Debug.Log("Received data: " + jsonData);
            IslandData = jsonData;
        }
    }
    
    //Transforms json from url to int[,] array
    public static int[,] GetDataArray()
    {
        if (string.IsNullOrEmpty(IslandData))
        {
            Debug.Log("IslandData has not been set.");
            return new int[,]{};
        }
        
        string[] rows = IslandData.Trim().Split(new[] {'\n', '\r'}, StringSplitOptions.RemoveEmptyEntries);
        
        int rowCount = rows.Length;
        int colCount = rows[0].Split(new[] {' ', ','}, StringSplitOptions.RemoveEmptyEntries).Length;
        
        int[,] array2D = new int[rowCount, colCount];
        
        for (int i = 0; i < rowCount; i++)
        {
            int[] values = rows[i].Split(new[] {' ', ','}, StringSplitOptions.RemoveEmptyEntries)
                .Select(int.Parse)
                .ToArray();
            for (int j = 0; j < colCount; j++)
            {
                array2D[i, j] = values[j];
            }
        }
        return array2D;
    }
}