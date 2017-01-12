# EcsRx.Buffs - Plugin

This is a simple plugin for the EcsRx framework that provides infrastructure/scaffolding for basic buff/effect systems.

(Throughout all the docs I will often use the term *buff* and *effect* interchangeably, and although almost all classes are
using the term *effect* it is the same notion as a buff just slightly more generic, I didn't want to call this EcsRx.Effects
as people may get it confused with visual effects.)

---

## What it does?

So it provides the most simple generic infrastructure to handle the notion of buffs. In almost all games that would require buffs
you would need mechanisms for:

- Tracking active buffs
- Automatically expiring buffs
- Finding out when a buff ticks (for things like poison, regen)
- Finding out how potent a buff is (to work out the effect of it)
- Managing the type of effect a buff would apply (to work out how to apply the buff)

There is not much to the plugin really, a simple model to represent the basic buff structure as well as some events and systems
to notify you when changes occur so you can react to them.

Realistically you would probably build upon this or augment it for your needs, so you may have buffs which proc under certain 
conditions rather than arbitrary ticks, or you may want to have a buff which has multiple effects. However for a basis to build 
upon or for simple games which do not have any complex stat/buff requirements it should just do the main work for you out of the box.

It is not a complete system, but should be seen as a basis for creating other plugins that build upon it (unless your scenario
is quite simple). It provides the important bits such as a contract for designers to create effects within your game, systems
to keep track of what buffs are active and notify you when something happens via events so you can apply whatever logic to your
entities.

(Just to be clear this PURELY deals with the aspect of buffs in a game, it is not a complete magic, stat or weapon system in any way
it just provides a mechanism for you to apply buffs to an entity and be notified when something related to those buffs happens. You 
would still need to make your own item/spell system to actually apply the buffs to entities.)

---

## How it works?

You register the plugin in your EcsRx application and it will automatically look for any entities with `EffectableComponent` and will
automatically raise events when buffs are added/expired and tick. This way you can just add this component to your existing entities
and then interact with the components `ActiveEffects` property and the systems the plugin registers will provide the events for free.

There are also some helper methods which allow you to find out the total potency of active buffs for a type, so for example if you had
multiple types of poison being applied, you may want to total the total potency of active health reducing buffs.

---

## How to use it

All you need to do from a basic perspective is just register the plugin in your application, apply the `EffectableComponent` to your 
entities, then subscribe to any of the events that are raised that you care about, most commonly `EffectTickedEvent` to then carry out
your logic based upon the type of buff, its potency, any other related buffs that are applied that may alter the effect etc.

For more documentation look in the docs folder which documents the important bits in further detail, there is also an example use case
for the system within the project with its own documentation which may help.