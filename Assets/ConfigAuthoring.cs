using System.Collections;
using System.Collections.Generic;
using Unity.Entities;
using UnityEngine;

public class ConfigAuthoring : MonoBehaviour
{
    public GameObject cellPrefab;
    public int gridHeight = 30;
    public int gridWidth = 30;
    public int maxHeight = 1000;

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
                maxHeight = authoring.maxHeight
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
}