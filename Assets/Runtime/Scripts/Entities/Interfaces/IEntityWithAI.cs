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
        public U GetEntityComponent<U>() where U : IEntityComponent;
        event UnityAction<IStatsController> OnStatsInit;
    }

    public interface IEntityComponent
    {

    }

    public interface IEntityWithTemplate
    {
        event UnityAction<ITemplateWithBaseStats> OnTemplateApplied;
    }
}

