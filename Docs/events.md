# Events

These events will all be automatically raised by the built in systems to notify you of changes to active buffs as well as when a buff which ticks (i.e poison, regen) applies.

## EffectAddedEvent

This indicates an entity has had a new effect applied to it, you generally dont need to do anything here but it notifies you if you do want to do anything.

## EffectExpiredEvent

This indicates an entity has had an existing effect expire, you generally dont need to do anything here but it notifies you if you do want to do anything.

## EffectTickedEvent

This is probably the more important event which tells you that an active effect has ticked, that implies you need to do something with it.

So if you were to have a poison effect which had a frequency of 1000 milliseconds, then every second you would get an event telling you the entity
that has the effect applied, as well as the underlying effect and how many times it has ticked so far (a helper for if you want every N tick to give a bonus).

So with this information you would probably do the following:

- You would probably want to have your own system react to this event
- Work out what type of effect it is (in this scenario it would be some form of `HealthReduction` type as its poison)
- Get whatever component tracks the stats that need to be effected (in this scenario it would be the entities health)
- Calculate the impact of the effect in numeric terms (based on the effects potency, so a poison of 1.0 may take 5hp with 2.0 taking 1hp etc)
- Apply whatever numerical changes are needed to stats (reduce health by `x` for this tick)

You may also trigger other events in your game off the back of this, so for example if you take damage you may want to raise an event which would 
display a red effect on the screen, or a blood particle system play on the NPC/Player in the scene etc.

It is up to you how you interpret and action these events but it should give you an entry point to carry out any action on your entities.