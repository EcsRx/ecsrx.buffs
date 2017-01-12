# Models/Pocos

The models are not part of the ECS world and are just data containers for your raw data, although they will be used via composition
within the ECS world on the components they should generally be seen as a contract with your database/flat file/web service etc which
stores and provides you the underlying data for use in your game.

## Effect

This is a Model/Poco which represents the basic buff structure, it contains information on the name, description, potency, duration etc and
should be used as a contract with the system. In the example you will see how this structure is used to create buffs for your games then you
can apply them to entities by the components.

## ActiveEffects

Where the `Effect` class is more a data model to represent how your buff data would look in your database/file/service etc, the `ActiveEffect`
is more the in-game counterpart which contains a reference to the underlying effect but also tracks how long it has been active, when it 
last ticked etc. So this is used for effects that are currently active and applied to something.