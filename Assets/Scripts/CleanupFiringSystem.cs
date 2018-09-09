using Unity.Collections;
using Unity.Entities;
using Unity.Jobs;
using UnityEngine;

public class CleanupFiringSystem : JobComponentSystem
{
    private struct CleanupFiringJob : IJobParallelFor
    {
        [ReadOnly] public EntityArray Entities;
        public EntityCommandBuffer.Concurrent EntityCommandBuffer;
        public float CurrentTime;
        public ComponentDataArray<Firing> Firing;

        public void Execute(int index)
        {
            if (CurrentTime - Firing[index].FiredAt < 0.5f)
            {
                return;
            }

            EntityCommandBuffer.RemoveComponent<Firing>(Entities[index]);
        }
    }

    private struct Data
    {
        public readonly int Length;
        public EntityArray Entities;
        public ComponentDataArray<Firing> Firing;
    }
    [Inject] private Data _data;
    [Inject] private CleanupFiringBarrier _Barrier;

    protected override JobHandle OnUpdate(JobHandle inputDeps)
    {
        return new CleanupFiringJob
        {
            Entities = _data.Entities,
            EntityCommandBuffer = _Barrier.CreateCommandBuffer(),
            CurrentTime = Time.time,
            Firing = _data.Firing
        }.Schedule(_data.Length, 64, inputDeps);
        //return base.OnUpdate(inputDeps);
    }
}

public class CleanupFiringBarrier : BarrierSystem { }