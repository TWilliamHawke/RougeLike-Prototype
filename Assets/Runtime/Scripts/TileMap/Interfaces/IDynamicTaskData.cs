namespace Map
{
    public interface IDynamicTaskData
    {
        void TriggerTaskChangeEvent();
        string CreateTaskText(int targetCount);
    }
}


