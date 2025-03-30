using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Documents;
using System.Windows.Media;

namespace NotesApp.Model
{
    public class Note
    {
        public int ID { get; }
        public string Name { get; }
        public string Text { get; set; }
        public Color Color { get; set; }
        public List<Tag> Tags { get; set; }
        public DateTime CreatedAtDate { get; }
        public DateTime LastUpdatedDate { get; set; }

        public Note(int id, DateTime createdAtDate)
        {
            ID = id;
            CreatedAtDate = createdAtDate;
            LastUpdatedDate = createdAtDate;
            Name = string.Empty;
            Text = string.Empty;
            Tags = [];
            Color = Colors.LightSkyBlue;
        }

        public bool HasTags(List<Tag> tags)
        {
            foreach (var tag in tags)
            {
                if (!Tags.Contains(tag))
                    return false;
            }

            return true;
        }

        public bool ContainsText(string? text)
        {
            if (string.IsNullOrEmpty(text))
                return true;
            FlowDocument flowDoc = new FlowDocument();
            TextRange range = new TextRange(flowDoc.ContentStart, flowDoc.ContentEnd);
            using (MemoryStream ms = new MemoryStream(Encoding.UTF8.GetBytes(Text)))
            {
                range.Load(ms, DataFormats.Rtf);
            }
            if (range.Text.Contains(text, StringComparison.CurrentCultureIgnoreCase))
                return true;
            return false;
        }
    }
}
