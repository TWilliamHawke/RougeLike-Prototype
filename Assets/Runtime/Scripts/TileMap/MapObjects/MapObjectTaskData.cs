namespace Map.Objects
{
	public struct MapObjectTaskData
	{
	    public string taskText;
		public bool objectIsLocked;

        public MapObjectTaskData(string taskText, bool objectIsLocked)
        {
            this.taskText = taskText;
            this.objectIsLocked = objectIsLocked;
        }
    }
}

