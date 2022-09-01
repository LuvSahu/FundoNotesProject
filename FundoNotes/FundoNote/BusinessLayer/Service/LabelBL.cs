using BusinessLayer.Interface;
using RepositoryLayer.Entity;
using RepositoryLayer.Interface;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLayer.Service
{
    public class LabelBL : ILabelBL
    {
        private readonly ILabelRL labelRL;
        public LabelBL(ILabelRL labelRL)
        {
            this.labelRL = labelRL;
        }   
        public LabelEntity AddLabel(long noteid, long userid, string labelname)
        {
            try
            {
                return labelRL.AddLabel(noteid, userid, labelname);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public LabelEntity ReadLabel(long lableid, long userid)
        {
            try
            {
                return labelRL.ReadLabel(lableid, userid);
            }
            catch (Exception)
            {
                throw;
            }

        }

        public LabelEntity UpdateLabel(long labelid, string labelname)
        {
            try
            {
                return labelRL.UpdateLabel(labelid, labelname);
            }
            catch (Exception)
            {
                throw;
            }

        }

        public string DeleteLabel(long labelid)
        {
            try
            {

                return labelRL.DeleteLabel(labelid);

            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
