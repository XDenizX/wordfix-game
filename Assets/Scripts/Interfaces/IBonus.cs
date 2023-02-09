public interface IBonus
{
    public bool IsAvailable(BonusContext context);

    public BonusInfo GetBonus(BonusContext context);
}