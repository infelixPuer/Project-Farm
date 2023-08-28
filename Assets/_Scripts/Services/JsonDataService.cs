using System;
using System.IO;
using Newtonsoft.Json;
using UnityEngine;

namespace _Scripts.Services
{
    public class JsonDataService : IDataService
    {
        public bool SaveData<T>(T data, string relativePath)
        {
            var path = Application.persistentDataPath + relativePath;
            
            try
            {
                if (File.Exists(path))
                    File.Delete(path);

                using FileStream stream = File.Create(path);
                stream.Close();
                File.WriteAllText(path, JsonConvert.SerializeObject(data, Formatting.Indented));
                return true;
            }
            catch (Exception e)
            {
                Console.WriteLine($"Unable to save file due to: {e.Message} {e.StackTrace}");
                return false;
            }
        }

        public T LoadData<T>(string relativePath)
        {
            var path = Application.persistentDataPath + relativePath;

            if (!File.Exists(path))
            {
                Debug.LogError($"Unable to load file because {path} does not exist!");
                throw new FileNotFoundException($"File at {path} does not exist!");
            }

            try
            {
                T data = JsonConvert.DeserializeObject<T>(path);
                return data;
            }
            catch (Exception e)
            {
                Console.WriteLine($"Unable to load data due to: {e.Message}, {e.StackTrace}");
                throw;
            }
        }
    }
}