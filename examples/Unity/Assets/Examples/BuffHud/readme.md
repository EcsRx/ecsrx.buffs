# Buff Hud Example

This example is here to show how you can build upon the basic infrastructure provided to create your own buff system and hud much like in MMOs.

There are a few main things to discuss in here which hopefully give you a basis to develop your own systems off but also provide some ideas on
how to use this library in your own projects.

---

## Buff Data Management

So in almost all games which have buffs/effects there is going to be some central point where they are managed in their raw form. So in this
example we are pretending the data comes from a database and we have an enum to represent the type of effects buffs may have. This is often 
handled differently based upon the tools, frameworks, developers, designers, project etc so its not like there is one size fits all but in almost 
all cases you have some form of data store which contains all the basic data for a buff be it in an actual database or scriptableobject/asset etc.

So just think of the below classes as the most basic examples trying to show intent rather than being the correct way to do this, for simple systems
this sort of approach should work fine but in the real world you wouldnt want to have a change to buff details to require a recompile of the games
or have your designers having to crack open source code to add new buffs.

### EffectDatabase

This is the pretend database, in the real world it would probably source the data from a database/file/web service etc, but to keep things simple
there is a list of all available buffs within the game. There are only a few in there just to make the example work but you could add as many 
different types as you want.

### EffectTypes

This is here to provide our games type of effects that could be applied, so in a fantasy setting you may want a buff that gives you more Armour,
but if it were a sci-fi setting you may have same sort of mechanics but it would be known as Shield instead of Armour. If you were making a FPS 
game you may want a buff that increases zoom stability, but if you were making a fantasy RPG that type of buff would be useless.

The point I am trying to get at here is that you would make your own types to fit your own game, so this is a basic attempt to show intent for 
this example but you would in the real world make your own types that match your scenario.

---

## The Hud

So the Hud is pretty simple at a high level, its basically just a UI panel which arranges everything automatically and displays the current
state of the buffs, in the real world this may be a side bar with icons and a name, it may be its own window with pure text, you can display
information however you want, but here we are modelling it like typical MMO buff bars to show a simple use case.

### BuffViewResolver

This is the most complex bit of it all, and that is down to the fact that we are wanting to have a single view per component HOWEVER that
view may contain multiple effects so we are basically creating a main view which has lots of sub views and logic within it. So this will
have to subscribe to effect changes within components and then update the UI accordingly, such as effects being added/removed and durations.

### LoggingSystem

This is quite simple, it just listens to ticks and buff changes and outputs it into a simple console, it subscribes to multiple events
and just spouts it out, recycling the log once it gets to a certain size. In the real world this would be far more complex but it should
hopefully show the sort of use case and how to achieve something basic in this area.

---

As mentioned above this is far from real world scenarios, but it should at least provide a basic use case for the underlying plugin as well 
as how to achieve something similar in your own games without too much effort.