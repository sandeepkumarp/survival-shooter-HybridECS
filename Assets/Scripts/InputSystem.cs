using Unity.Entities;
using UnityEngine;

public class InputSystem : ComponentSystem
{
    private struct Data
    {
        //reserved name: number of entities unity find with this struct.
        //need to be readonly or else will result in memory leak.
        public readonly int Length;

        //Tells Unity to scan the scene for Entities that have an InputComponent
        public ComponentArray<InputComponent> InputComponent;
    }

    [Inject] private Data _data;

    protected override void OnUpdate()
    {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");
        for (int i = 0; i < _data.Length; i++)
        {
            _data.InputComponent[i].Horizontal = horizontal;
            _data.InputComponent[i].Vertical = vertical;
        }
    }
}