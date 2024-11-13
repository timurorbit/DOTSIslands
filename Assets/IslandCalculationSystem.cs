using System.Collections.Generic;
using Unity.Burst;
using Unity.Collections;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;
using UnityEngine;

[UpdateAfter(typeof(CellSpawningSystem))]
public partial struct IslandCalculationSystem : ISystem
{
    NativeHashSet<float3> usedTransforms;

    private NativeHashMap<float3, int> map;

    private int tempSum;
    private int tempQuantity;

    public void OnCreate(ref SystemState state)
    {
        state.RequireForUpdate<Config>();
    }

    public void OnDestroy(ref SystemState state)
    {
    }


    public void OnUpdate(ref SystemState state)
    {
        state.Enabled = false;

        usedTransforms = new NativeHashSet<float3>(1000, Allocator.Persistent);
        map = new NativeHashMap<float3, int>(1000, Allocator.Persistent);

        foreach (var (cell, transform) in SystemAPI.Query<Cell, LocalTransform>())
        {
            if (cell.height > 0)
            {
                map.Add(transform.Position, cell.height);
            }
        }


        foreach (var pair in map)
        {
            if (!usedTransforms.Contains(pair.Key))
            {
                tempSum = 0;
                tempQuantity = 0;
                islandCalculation(pair.Key);
                Debug.Log(tempSum);
                Debug.Log("average = " + (tempSum / tempQuantity));
            }
        }
    }

    private void islandCalculation(float3 current)
    {
        tempSum += map[current];
        tempQuantity++;
        usedTransforms.Add(current);
        foreach (var position in map.GetKeyArray(Allocator.Temp))
        {
            if (math.distance(current, position) <= 1f && !usedTransforms.Contains(position))
            {
                islandCalculation(position);
            }
        }
    }
}