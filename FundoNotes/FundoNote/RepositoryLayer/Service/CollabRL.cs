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
    public class CollabRL : ICollabRL
    {
        private readonly FundoContext fundoContext;
        public CollabRL(FundoContext fundooContext)
        {
            this.fundoContext = fundooContext;
        }
        public CollabEntity AddCollab(long noteid, long userid, string email)
        {
            try
            {
                var noteResult = fundoContext.NotesTable.Where(x => x.NotesId == noteid).FirstOrDefault();
                var emailResult = fundoContext.UserTable.Where(x => x.Email == email).FirstOrDefault();
                if (noteResult != null && emailResult != null)
                {
                    CollabEntity collabEntity = new CollabEntity();
                    collabEntity.NotesId = noteResult.NotesId;
                    collabEntity.CollabeEmail = emailResult.Email;
                    collabEntity.UserId = emailResult.UserId;
                    fundoContext.Add(collabEntity);
                    fundoContext.SaveChanges();
                    return collabEntity;
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
        public string Remove(long noteid, string email)
        {
            try
            {
                var noteResult = fundoContext.CollabTable.Where(x => x.NotesId == noteid && x.CollabeEmail == email).FirstOrDefault();
                if (noteResult != null)

                {
                    fundoContext.CollabTable.Remove(noteResult);
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
        public CollabEntity ReadCollab(long colabId, long userId)
        {
            try
            {
                var UserId = this.fundoContext.UserTable.Where(e => e.UserId == userId);
                if (UserId != null)
                {
                    return this.fundoContext.CollabTable.FirstOrDefault(e => e.CollabId == colabId);
                }

                return null;
            }
            catch (Exception)
            {
                throw;
            }
        }

        
    }

}
