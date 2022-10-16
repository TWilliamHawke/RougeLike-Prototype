namespace Core.SaveSystem
{
    public interface IHaveSaveState
    {
        public void Save(ISaveManager saveManager);
        public void Load(ILoadManager loadManager);
    }
}