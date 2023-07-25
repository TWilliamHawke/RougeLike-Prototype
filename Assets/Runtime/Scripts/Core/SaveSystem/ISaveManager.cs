namespace Core.SaveSystem
{
    public interface ILoadManager
    {
        bool GetSaveState<T>(string key, ref T data);
    }

    public interface ISaveManager
    {
        void AddSaveState<T>(string key, T data);
    }

}