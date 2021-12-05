namespace Entities.AI
{
    public interface IState
    {
        void StartTurn();
        bool Condition();
    }
}