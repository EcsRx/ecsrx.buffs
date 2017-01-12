# Components

As this is more of a basis for others to build upon it only targets the most common use-cases.

## EffectableComponent

This is the contract in the ECS world, it contains all active effects on a given entity, so if you think of your players or NPCs who may have 
multiple buffs applied to themselves, it would encapsulate that. It exposes an `ActiveEffects` property which should be updated by your 
own systems when someone casts a spell or uses and item to apply a buff to an entity.
