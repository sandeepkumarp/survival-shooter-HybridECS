using Unity.Entities;
using UnityEngine;

public class PlayerRotationSystem : ComponentSystem
{
    private struct Filter
    {
        public Transform Transform;
        public RotationComponent RotationComponent;
    }
    protected override void OnUpdate()
    {
        Vector3 mousePosition = Input.mousePosition;
        Ray cameraRay = Camera.main.ScreenPointToRay(mousePosition);
        int layerMask = LayerMask.GetMask("Floor");

        if (Physics.Raycast(cameraRay, out RaycastHit hit, 100, layerMask))
        {
            foreach (Filter entity in GetEntities<Filter>())
            {
                Vector3 forward = hit.point - entity.Transform.position;
                Quaternion rotation = Quaternion.LookRotation(forward);

                entity.RotationComponent.Value = new Quaternion(0, rotation.y, 0, rotation.w).normalized;
            }
        }
    }
}