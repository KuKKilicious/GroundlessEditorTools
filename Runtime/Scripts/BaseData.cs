using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Base
{
    public enum ItemCategory
    {
        Any,Offense, Defense, Fortune, Neon, Utility
    }

    public enum ItemRarity
    {
        Any,Common, Uncommon, Rare, Mythic
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
        Self, Sword, Ground, ClosestTarget, FurthestTarget, RandomTarget
    }

    public enum StatType
    {
        MaxHP, CurrentHP, MaxShield, Shield, AttackSpeed, AttackDamage, CritDamage, CritPercentage, GoldGain, SplashDamage, RarityChance, AbilityAmp, LifeLeech
    }

    public enum EffectExplanation
    {
        Stun, Root, NeonOrb, NeonSpear, NeonBomb,NeonTrail, NeonSpirit,NeonBeam,Charm, NeonWave
    }
}