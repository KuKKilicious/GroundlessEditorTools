using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Base
{
    public enum ItemCategory
    {
        Offense, Defense, Fortune, Neon, Utility
    }

    public enum ItemRarity
    {
        Common, Uncommon, Rare, Epic, Legendary
    }
    public enum EffectTrigger
    {
        OnMeleeHit, OnMeleeHitChance, OnBeingHit, OnBeingHitChance, OnItemPickup, OnAbilityUse, OnCooldown
    }

    public enum EffectTarget
    {
        Self, OnGround, Forward, ClosestTarget, FurthestTarget, RandomTarget
    }
    public enum EffectSource
    {
        Self, Ground, ClosestTarget, FurthestTarget, RandomTarget
    }

    public enum StatType
    {
        MaxHP, CurrentHP, MaxShield, Shield, AttackSpeed, AttackDamage, CritDamage, CritPercentage, GoldGain, SplashDamage, RarityChance, AbilityAmp, LifeLeech
    }
}