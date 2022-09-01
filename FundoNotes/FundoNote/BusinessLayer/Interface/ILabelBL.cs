using RepositoryLayer.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLayer.Interface
{
    public interface ILabelBL
    {
        public LabelEntity AddLabel(long noteid, long userid, string labelname);

        public LabelEntity ReadLabel(long lableid, long userid);

        public LabelEntity UpdateLabel(long labelid, string labelname);

        public string DeleteLabel(long labelid);




    }
}
