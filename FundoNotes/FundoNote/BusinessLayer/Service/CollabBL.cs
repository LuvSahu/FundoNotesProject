using BusinessLayer.Interface;
using RepositoryLayer.Entity;
using RepositoryLayer.Interface;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLayer.Service
{
    public class CollabBL : ICollabBL
    {
        private readonly ICollabRL collabRL;
        public CollabBL(ICollabRL collabRL)
        {
            this.collabRL = collabRL;
        }
        public CollabEntity AddCollab(long noteid, long userid, string email)
        {
            try
            {
                return collabRL.AddCollab(noteid, userid, email);
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
                return collabRL.Remove(noteid, email);
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
                return collabRL.ReadCollab(colabId, userId);
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
