namespace Assets.EcsRx.Examples.Types
{
    public enum EffectTypes : short
    {
        Unknown = 0,

        // Health
        AddHealth = 10,
        ReduceHealth,

        // Armour
        AddArmour = 20,
        ReduceArmour,

        // Utility
        AddViewDistance = 30,
        ReduceViewDistance,
        AddMovementSpeed,
        ReduceMovementSpeed
    }
}