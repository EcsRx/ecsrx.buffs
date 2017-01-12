﻿using EcsRx.Events;

namespace EcsRx.Groups
{
    public class DefaultGroupAccessorFactory : IGroupAccessorFactory
    {
        private readonly IEventSystem _eventSystem;

        public DefaultGroupAccessorFactory(IEventSystem eventSystem)
        {
            _eventSystem = eventSystem;
        }

        public IGroupAccessor Create(GroupAccessorConfiguration arg)
        {
            return new CacheableGroupAccessor(arg.GroupAccessorToken, arg.InitialEntities, _eventSystem);
        }
    }
}