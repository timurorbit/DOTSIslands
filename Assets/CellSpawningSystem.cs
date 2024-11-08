using System.Collections;
using System.Collections.Generic;
using Unity.Burst;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;
using UnityEngine;
using Random = Unity.Mathematics.Random;

partial struct CellSpawningSystem : ISystem
{
    [BurstCompile]
    public void OnCreate(ref SystemState state)
    {
        state.RequireForUpdate<Config>();
    }

    [BurstCompile]
    public void OnDestroy(ref SystemState state)
    {
        
    }
    
    [BurstCompile]
    public void OnUpdate(ref SystemState state)
    {
        state.Enabled = false;

        var config = SystemAPI.GetSingleton<Config>();
        var random = new Random(123);
        //spawn for height and weight
        for (int i = 0; i < config.gridHeight; i++)
        {
            for (int j = 0; j < config.gridWidth; j++)
            {
                var cellEntity = state.EntityManager.Instantiate(config.cellPrefab);
                state.EntityManager.SetComponentData(cellEntity, new LocalTransform
                {
                    Position = new float3
                    {
                        x = i,
                        y = 0,
                        z = j
                    },
                    Scale = 1,
                    Rotation = quaternion.identity
                });

                var randomYScale = random.NextFloat(0, config.maxHeight + 1);
                var yScale = 1 + ((randomYScale / 1000) * (10 - 1));
                state.EntityManager.AddComponent<PostTransformMatrix>(cellEntity);
                state.EntityManager.SetComponentData(cellEntity, new PostTransformMatrix
                {
                    Value = float4x4.Scale(1f,yScale,1f)
                });
            }
        }
    }
}
