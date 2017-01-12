# Systems

There are not many systems in play, there is one to track effects that are added and one that tracks the timings for durations/ticks.

## EffectAddedSystem

This just listends to an effect being added and then raises an event for it happening, this may be useful for updating your HUD or
having some sort of visual effect on the character.

## EffectTimingSystem

This monitors how long effects have been active and if an effect with a frequency should tick, it then raises events off the back of 
these checks, such as effects expiring events and effects ticking.