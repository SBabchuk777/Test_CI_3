namespace Tools.MaxCore.Scripts.Services.DataHubService.SaveLoadService
{
    public interface ISaveLoadService<TDataKey>
    {
        public void Save<TData>(TDataKey dataKey, TData data);
        public TData Load<TData>(TDataKey dataKey);
        public TData Load<TData>(TDataKey dataKey, TData defaultValue);
    }
}