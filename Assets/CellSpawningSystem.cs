using System.Collections;
using System.Collections.Generic;
using Unity.Burst;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Rendering;
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
                    Value = float4x4.Scale(1f, yScale, 1f)
                });

                var mappedColor = this.mapColor(randomYScale, config.maxHeight);
                state.EntityManager.SetComponentData(cellEntity, new URPMaterialPropertyBaseColor
                {
                    Value = mappedColor
                });
            }
        }
    }

    [BurstCompile]
    private float4 mapColor(float currentValue, float max)
    {
        float height = currentValue / max;
        var config = SystemAPI.GetSingleton<Config>();
        float4 answer = config.waterColor;
        
        if (height < config.sandThreshold)
        {
            answer = math.lerp(config.waterColor, config.sandColor, height / config.sandThreshold);
        } else if (height < config.groundThreshold)
        {
            answer = math.lerp(config.sandColor, config.groundColor, height / config.groundThreshold);
        }
        else if (height < config.highGroundThreshold)
        {
            answer = math.lerp(config.groundColor, config.highGroundColor, height / config.highGroundThreshold);
        }
        else if (height < config.mountainThreshold)
        {
            answer = math.lerp(config.highGroundColor, config.mountainColor, height / config.mountainThreshold);
        }
        else if (height < config.highMountainThreshold)
        {
            answer = math.lerp(config.mountainColor, config.highMountainColor, height / config.highMountainThreshold);
        }
        
        return answer;
    }
}