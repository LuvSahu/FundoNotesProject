using RepositoryLayer.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLayer.Interface
{
    public interface ICollabBL
    {
        public CollabEntity AddCollab(long noteid, long userid, string email);

        public string Remove(long noteid, string email);

        public CollabEntity ReadCollab(long colabId, long userId);


    }
}
