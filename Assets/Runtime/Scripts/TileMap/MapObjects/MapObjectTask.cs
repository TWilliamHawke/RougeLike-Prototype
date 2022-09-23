namespace Map.Objects
{
	public struct MapObjectTask
	{
	    public string taskText;
		public bool objectIsLocked;

        public MapObjectTask(string taskText, bool objectIsLocked)
        {
            this.taskText = taskText;
            this.objectIsLocked = objectIsLocked;
        }
    }
}

