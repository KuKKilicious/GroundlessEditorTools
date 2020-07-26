using UnityEngine;
// ReSharper disable PublicField
namespace Game.Base
{
    [System.Serializable]
    public abstract class ItemEffect : ScriptableObject
    {
        public EffectSource Source;
        public EffectTarget Target;
        public EffectTrigger Trigger;
    
    }

//  TODO: Check if this is the way to apply oessary
//     public interface IItemEffect
//     {
//         void Apply(ISource source, ITarget target, IItemEffectParameter itemEffectParameter);
//     }
// 
//     public interface IItemEffectParameter { }
// 
//     public interface ITarget { }
// 
//     public interface ISource { }


}