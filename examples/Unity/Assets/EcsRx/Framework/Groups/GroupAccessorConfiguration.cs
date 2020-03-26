﻿using System.Collections.Generic;
using EcsRx.Entities;
using EcsRx.Pools;

namespace EcsRx.Groups
{
    public class GroupAccessorConfiguration
    {
        public GroupAccessorToken GroupAccessorToken { get; set; }
        public IEnumerable<IEntity> InitialEntities { get; set; }
    }
}