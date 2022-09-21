using Entities.AI;

namespace Entities
{
	public interface IEntityWithAI
	{
	    StateMachine stateMachine { get; }
	}
}

