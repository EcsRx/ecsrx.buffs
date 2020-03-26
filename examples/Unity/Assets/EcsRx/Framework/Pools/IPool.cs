﻿using System.Collections.Generic;
using EcsRx.Blueprints;
using EcsRx.Entities;

namespace EcsRx.Pools
{
    public interface IPool
    {
        string Name { get; }

        IEnumerable<IEntity> Entities { get; }

        IEntity CreateEntity(IBlueprint blueprint = null);
        void RemoveEntity(IEntity entity);
    }
}
