﻿using System;

namespace Coree.VisualStudio.DotnetToolbar.ExtensionMethods
{
    public static class JsonHelper
    {
        public static T ReadFromFile<T>(string file)
        {
            var contents = System.IO.File.ReadAllText(file);
            T result = System.Text.Json.JsonSerializer.Deserialize<T>(contents);
            return result;
        }

        public static T TryReadFromFile<T>(string file)
        {
            string contents;
            T result;
            try
            {
                contents = System.IO.File.ReadAllText(file);
                result = System.Text.Json.JsonSerializer.Deserialize<T>(contents);
            }
            catch (Exception)
            {
                if (System.IO.File.Exists(file))
                {
                    System.IO.File.Move(file, $@"{file}.backup");
                }
                T instance = (T)Activator.CreateInstance(typeof(T));
                var JsonString = System.Text.Json.JsonSerializer.Serialize<T>(instance);
                System.IO.File.WriteAllText(file, JsonString);
                contents = System.IO.File.ReadAllText(file);
                result = System.Text.Json.JsonSerializer.Deserialize<T>(contents);
            }
            return result;
        }

        public static bool CreateDefault<T>(string file)
        {
            if (!System.IO.File.Exists(file))
            {
                T instance = (T)Activator.CreateInstance(typeof(T));
                var JsonString = System.Text.Json.JsonSerializer.Serialize<T>(instance);
                System.IO.File.WriteAllText(file, JsonString);
                return true;
            }
            return false;
        }

        public static void WriteToFile<T>(T value, string file)
        {
            var JsonString = System.Text.Json.JsonSerializer.Serialize<T>(value, new System.Text.Json.JsonSerializerOptions() { WriteIndented = true });
            System.IO.File.WriteAllText(file, JsonString);
        }
    }
}