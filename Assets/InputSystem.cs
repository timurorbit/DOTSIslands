using Unity.Burst;
using Unity.Entities;
using UnityEngine;

public partial struct InputSystem : ISystem
{
    public void OnCreate(ref SystemState state)
    {
        state.RequireForUpdate<Hit>();
        state.RequireForUpdate<Config>();
    }

    public void OnUpdate(ref SystemState state)
    {
        var hit = SystemAPI.GetSingleton<Hit>();
        hit.HitChanged = false;

        if (Camera.main == null || !Input.GetMouseButtonDown(0))
        {
            return;
        }

        var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (new Plane(Vector3.up, 0f).Raycast(ray, out var dist))
        {
            hit.Value = ray.GetPoint(dist);
            Debug.Log(hit.Value);
            hit.HitChanged = true;
        }
    }
}