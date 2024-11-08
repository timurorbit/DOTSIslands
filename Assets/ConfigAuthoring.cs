using System.Collections;
using System.Collections.Generic;
using Unity.Entities;
using Unity.Mathematics;
using UnityEngine;

public class ConfigAuthoring : MonoBehaviour
{
    
    [Header("Prefabs")]
    public GameObject cellPrefab;
    
    [Header("World")]
    public int gridHeight = 30;
    public int gridWidth = 30;
    public int maxHeight = 1000;

    [Header("Color Depths")] 
    public Color waterColor;
    public Color sandColor;
    public Color groundColor;
    public Color highGroundColor;
    public Color mountainColor;
    public Color highMountainColor;

    [Header("Color Thresholds")] 
    public float sandThreshold;
    public float groundThreshold;
    public float highGroundThreshold;
    public float mountainThreshold;
    public float highMountainThreshold;

    class Baker : Baker<ConfigAuthoring>
    {
        public override void Bake(ConfigAuthoring authoring)
        {
            var entity = GetEntity(TransformUsageFlags.None);
            AddComponent<Config>(entity, new Config
            {
                cellPrefab = GetEntity(authoring.cellPrefab, TransformUsageFlags.Dynamic),
                gridHeight = authoring.gridHeight,
                gridWidth = authoring.gridWidth,
                maxHeight = authoring.maxHeight,
                
                waterColor = (Vector4) authoring.waterColor,
                sandColor = (Vector4) authoring.sandColor,
                groundColor = (Vector4) authoring.groundColor,
                highGroundColor = (Vector4) authoring.highGroundColor,
                mountainColor = (Vector4) authoring.mountainColor,
                highMountainColor = (Vector4) authoring.highMountainColor,
                
                sandThreshold = authoring.sandThreshold,
                groundThreshold = authoring.groundThreshold,
                highGroundThreshold = authoring.highGroundThreshold,
                mountainThreshold = authoring.mountainThreshold,
                highMountainThreshold = authoring.highMountainThreshold,
            });
        }
    }
}

public struct Config : IComponentData
{
    public Entity cellPrefab;
    public int gridHeight;
    public int gridWidth;
    public int maxHeight;
    
    public float4 waterColor;
    public float4 sandColor;
    public float4 groundColor;
    public float4 highGroundColor;
    public float4 mountainColor;
    public float4 highMountainColor;
    
    [Header("Color thresholds")] 
    public float sandThreshold;
    public float groundThreshold;
    public float highGroundThreshold;
    public float mountainThreshold;
    public float highMountainThreshold;
}