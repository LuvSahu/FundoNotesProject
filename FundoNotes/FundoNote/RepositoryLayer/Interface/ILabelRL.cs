using RepositoryLayer.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace RepositoryLayer.Interface
{
    public interface ILabelRL
    {
        public LabelEntity AddLabel(long noteid, long userid, string labelname);

        public LabelEntity ReadLabel(long lableid, long userid);

        public string DeleteLabel(long labelid);

        public LabelEntity UpdateLabel(long labelid, string labelname);


    }
}
