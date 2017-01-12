﻿using System;
using System.Collections.Generic;
using System.Linq;
using EcsRx.Entities;
using EcsRx.Events;
using EcsRx.Extensions;
using EcsRx.Groups;

namespace EcsRx.Pools
{
    public class PoolManager : IPoolManager, IDisposable
    {
        public const string DefaultPoolName = "default";
        
        private readonly IDictionary<GroupAccessorToken, IGroupAccessor> _groupAccessors;
        private readonly IDictionary<string, IPool> _pools;

        public IEventSystem EventSystem { get; private set; }
        public IEnumerable<IPool> Pools { get { return _pools.Values; } }
        public IPoolFactory PoolFactory { get; private set; }
        public IGroupAccessorFactory GroupAccessorFactory { get; private set; }

        public PoolManager(IEventSystem eventSystem, IPoolFactory poolFactory, IGroupAccessorFactory groupAccessorFactory)
        {
            EventSystem = eventSystem;
            PoolFactory = poolFactory;
            GroupAccessorFactory = groupAccessorFactory;

            _groupAccessors = new Dictionary<GroupAccessorToken, IGroupAccessor>();
            _pools = new Dictionary<string, IPool>();

            CreatePool(DefaultPoolName);
        }
        
        public IPool CreatePool(string name)
        {
            var pool = PoolFactory.Create(name);
            _pools.Add(name, pool);

            EventSystem.Publish(new PoolAddedEvent(pool));

            return pool;
        }

        public IPool GetPool(string name = null)
        { return _pools[name ?? DefaultPoolName]; }

        public void RemovePool(string name)
        {
            if(!_pools.ContainsKey(name)) { return; }

            var pool = _pools[name];
            _pools.Remove(name);

            EventSystem.Publish(new PoolRemovedEvent(pool));
        }
        
        public IEnumerable<IEntity> GetEntitiesFor(IGroup group, string poolName = null)
        {
            if(group is EmptyGroup)
            { return new IEntity[0]; }

            if (poolName != null)
            { return _pools[poolName].Entities.MatchingGroup(group); }

            return Pools.GetAllEntities().MatchingGroup(group);
        }

        public IGroupAccessor CreateGroupAccessor(IGroup group, string poolName = null)
        {
            var groupAccessorToken = new GroupAccessorToken(group.TargettedComponents.ToArray(), poolName);
            if (_groupAccessors.ContainsKey(groupAccessorToken)) { return _groupAccessors[groupAccessorToken]; }

            var entityMatches = GetEntitiesFor(@group, poolName);
            var groupAccessor = new CacheableGroupAccessor(groupAccessorToken, entityMatches, EventSystem);
            groupAccessor.MonitorEntityChanges();
            _groupAccessors.Add(groupAccessorToken, groupAccessor);

            return _groupAccessors[groupAccessorToken];
        }

        public void Dispose()
        {
            _groupAccessors.Values.ForEachRun(x =>
            {
                if (x is IDisposable)
                {
                    (x as IDisposable).Dispose();
                }
            });
        }
    }
}