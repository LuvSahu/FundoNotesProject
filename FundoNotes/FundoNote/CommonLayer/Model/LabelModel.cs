using System;
using System.Collections.Generic;
using System.Text;

namespace CommonLayer.Model
{
    public class LabelModel
    {
        public long labelId { get; set; }

        public string labelName { get; set; }

        public long NotesId { get; set; }

        public long UserId { get; set; }
    }
}
