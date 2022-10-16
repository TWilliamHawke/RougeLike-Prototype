namespace Core.SaveSystem
{
    public interface ILoadManager
    {
        void GetSaveState<T>(string key, ref T data);
    }

    public interface ISaveManager
    {
        void AddSaveState<T>(string key, T data);
    }

}