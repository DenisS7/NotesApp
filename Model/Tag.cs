using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NotesApp.Model
{
    public class Tag
    {
        public string Name { get; set; }
        public Color TagColor { get; set; }

        public Tag(string name, Color tagColor)
        {
            Name = name;
            TagColor = tagColor;
        }

        public bool Conflicts(Tag tag)
        {
            return !Name.Equals(tag.Name);
        }
    }
}
