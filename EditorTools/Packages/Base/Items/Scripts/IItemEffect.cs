using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Base
{
    public interface IItemEffect
    {
        void Apply(ISource source, ITarget target, IItemEffectParameter itemEffectParameter);
    }

    public interface IItemEffectParameter { }

    public interface ITarget { }

    public interface ISource { }

    [System.Serializable]
    public abstract class ItemEffectssssss : IItemEffect
    {
        public void Apply(ISource source, ITarget target, IItemEffectParameter itemEffectParameter)
        {
            throw new System.NotImplementedException();
        }
    }
    [System.Serializable]
    public abstract class ItemEffect : ScriptableObject
    {
        public EffectSource source;
        public EffectTarget target;
        public EffectTrigger trigger;

    }
}