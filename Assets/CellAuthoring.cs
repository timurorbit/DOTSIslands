using System.Collections;
using System.Collections.Generic;
using Unity.Entities;
using Unity.Rendering;
using UnityEngine;

public class CellAuthoring : MonoBehaviour
{
    class Baker : Baker<CellAuthoring>
    {
        public override void Bake(CellAuthoring authoring)
        {
            var entity = GetEntity(TransformUsageFlags.Dynamic);

            AddComponent<Cell>(entity);
            AddComponent<URPMaterialPropertyBaseColor>(entity);
        }
    }
}

public struct Cell : IComponentData
{
    public int height;
}