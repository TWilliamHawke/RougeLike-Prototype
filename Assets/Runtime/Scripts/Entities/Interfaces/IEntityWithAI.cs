using Entities.AI;
using Entities.Stats;
using UnityEngine;
using UnityEngine.Events;

namespace Entities
{
	public interface IEntityWithAI
	{
	    StateMachine stateMachine { get; }
	}

    public interface IEntityWithComponents
    {
        public U GetEntityComponent<U>() where U : MonoBehaviour, IEntityComponent;
        event UnityAction<IStatsController> OnStatsInit;
    }

    public interface IEntityComponent
    {

    }
}

