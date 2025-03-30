using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using NotesApp.Converters;
using NotesApp.Model;

namespace NotesApp.Saving
{
    class NoteListSavingUtility
    {
        public static void SaveNoteList(NoteList noteList)
        {
            string exeFolder = AppDomain.CurrentDomain.BaseDirectory;
            string dataFolder = Path.Combine(exeFolder, "Data");

            if (!Directory.Exists(dataFolder))
            {
                Directory.CreateDirectory(dataFolder);
            }

            string filePath = Path.Combine(dataFolder, "NoteList.json");

            JsonSerializerSettings settings = new JsonSerializerSettings
            {
                Formatting = Formatting.Indented
            };
            settings.Converters.Add(new ColorJsonConverter());

            string json = JsonConvert.SerializeObject(noteList, settings);
            File.WriteAllText(filePath, json);
        }

        public static NoteList LoadNoteList()
        {
            string exeFolder = AppDomain.CurrentDomain.BaseDirectory;
            string dataFolder = Path.Combine(exeFolder, "Data");
            string filePath = Path.Combine(dataFolder, "NoteList.json");

            if (!File.Exists(filePath))
            {
                return new NoteList();
            }

            JsonSerializerSettings settings = new JsonSerializerSettings();
            settings.Converters.Add(new ColorJsonConverter());

            string json = File.ReadAllText(filePath);
            NoteList? noteList = JsonConvert.DeserializeObject<NoteList>(json, settings);

            if (noteList is null)
            {
                return new NoteList();
            }

            return noteList;
        }
    }
}
