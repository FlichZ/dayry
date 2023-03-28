using System;
using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;

namespace Notes
{
    internal class Serializer
    {
        public static void Serialize(List<Note> notes)
        {
            string pathJson = Environment.GetFolderPath(Environment.SpecialFolder.Desktop) + "\\notes.json";
            string json = JsonConvert.SerializeObject(notes);
            File.WriteAllText(pathJson, json);
        }
        public static List<Note> Deserialize()
        {
            try
            {
                string pathJson = Environment.GetFolderPath(Environment.SpecialFolder.Desktop) + "\\notes.json";
                string json = File.ReadAllText(pathJson);
                List<Note> notes = JsonConvert.DeserializeObject<List<Note>>(json);
                return notes;
            }
            catch
            {
                return new List<Note>();
            }
        }
    }
}
