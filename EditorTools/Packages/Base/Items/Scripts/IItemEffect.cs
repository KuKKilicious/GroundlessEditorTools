using UnityEngine;
// ReSharper disable PublicField
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
    public abstract class ItemEffect : ScriptableObject
    {
        public EffectSource Source;
        public EffectTarget Target;
        public EffectTrigger Trigger;

    }

    public class SpawnEffect : ItemEffect
    {
        public GameObject ObjectToSpawn;
    }


}