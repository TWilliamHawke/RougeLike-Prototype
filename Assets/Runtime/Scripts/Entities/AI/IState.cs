namespace Entities.AI
{
    public interface IState
    {
        void StartTurn();
        void EndTurn();
        bool Condition();
        bool isDone { get; }
    }
}