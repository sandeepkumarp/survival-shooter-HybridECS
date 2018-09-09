using Unity.Entities;
using UnityEngine;

public class RotationSystem : ComponentSystem
{
    private struct Data
    {
        public readonly int Length;
        public ComponentArray<RotationComponent> RotationComonents;
        public ComponentArray<Rigidbody> Rigidbody;
    }
    [Inject] private Data _data;

    protected override void OnUpdate()
    {
        for (int i = 0; i < _data.Length; i++)
        {
            Quaternion rotation = _data.RotationComonents[i].Value;
            _data.Rigidbody[i].MoveRotation(rotation);
        }
    }
}