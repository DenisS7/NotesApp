using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NotesApp.Model
{
    public class Note
    {
        public int ID { get; }
        public string Name { get; }
        public string Text { get; }
        public List<Tag> Tags { get; set; }
        public DateTime CreatedAtDate { get; }
        public DateTime LastUpdatedDate { get; set; }

        public Note(int id, DateTime createdAtDate)
        {
            ID = id;
            CreatedAtDate = createdAtDate;
            Name = string.Empty;
            Text = string.Empty;
            Tags = [];
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
    }
}
