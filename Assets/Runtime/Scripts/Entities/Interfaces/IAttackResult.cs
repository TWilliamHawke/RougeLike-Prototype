namespace Entities.Combat
{
    public interface IAttackResult
    {
        int CalculateProbability(IDamageSource damageSource, IAttackTarget target);
        void DoHit(IDamageSource damageSource, IAttackTarget target);
    }
}
