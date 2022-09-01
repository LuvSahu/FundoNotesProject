using Microsoft.Extensions.Configuration;
using RepositoryLayer.Context;
using RepositoryLayer.Entity;
using RepositoryLayer.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RepositoryLayer.Service
{
    public class LabelRL : ILabelRL
    {
        private readonly FundoContext fundoContext;

        public LabelRL(FundoContext fundoContext)
        {
            this.fundoContext = fundoContext;
        }

        public LabelEntity AddLabel(long noteid, long userid, string labelname)
        {
            try
            {
                var result = fundoContext.NotesTable.Where(x => x.NotesId == noteid).FirstOrDefault();
                if (result != null) 
                { 
                LabelEntity Entity = new LabelEntity();
                Entity.labelName = labelname;
                Entity.UserId = result.UserId;
                Entity.NotesId = result.NotesId;
                fundoContext.LabelTable.Add(Entity);
                fundoContext.SaveChanges();
                return Entity;
                }
                else
                {
                    return null;
                }
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
                var UserId = this.fundoContext.UserTable.Where(e => e.UserId == userid);
                if (UserId != null)
                {
                    return this.fundoContext.LabelTable.FirstOrDefault(e => e.labelId == lableid);
                }

                return null;
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

                var label = fundoContext.LabelTable.Where(x => x.labelId == labelid).FirstOrDefault();
                if (label != null)
                {

                    label.labelName = labelname;
                    this.fundoContext.SaveChanges();
                    return label;
                }
                else
                {
                    return null;
                }
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
                var noteResult = fundoContext.LabelTable.Where(x => x.labelId == labelid).FirstOrDefault();
                if (noteResult != null)

                {
                    fundoContext.LabelTable.Remove(noteResult);
                    this.fundoContext.SaveChanges();
                    return "Delete successfully";
                }
                else
                {
                    return null;
                }

            }
            catch (Exception)
            {
                throw;
            }
        }



        

    }
}
