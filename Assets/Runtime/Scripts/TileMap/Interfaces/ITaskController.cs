namespace Map
{
	public interface ITaskController
	{
		TaskData currentTask { get; }
		void HandleSpawnQueue(ISpawnQueue spawnQueue);
	}
}


