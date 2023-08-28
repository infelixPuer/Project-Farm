namespace _Scripts.Services
{
    public interface IDataService
    {
        public bool SaveData<T>(T data, string relativePath);
        public T LoadData<T>(string relativePath);
    }
}