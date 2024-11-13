using Unity.Burst;
using Unity.Collections;
using Unity.Entities;
using Unity.Transforms;
using UnityEngine;

[UpdateAfter(typeof(CellSpawningSystem))]
public partial struct IslandCalculationSystem : ISystem
{
    
    public void OnCreate(ref SystemState state)
    {
        state.RequireForUpdate<Config>();
    }

    public void OnDestroy(ref SystemState state)
    {
        
    }

    
    
    public void OnUpdate(ref SystemState state)
    {
    
        
    }
}