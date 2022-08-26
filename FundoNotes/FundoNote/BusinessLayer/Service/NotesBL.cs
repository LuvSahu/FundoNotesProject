using BusinessLayer.Interface;
using CommonLayer.Model;
using RepositoryLayer.Entity;
using RepositoryLayer.Interface;
using RepositoryLayer.Service;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLayer.Service
{
    public class NotesBL : INotesBL
    {
        private readonly INotesRL notesRL;

        public NotesBL(INotesRL notesRL)
        {
            this.notesRL = notesRL;
        }

        public NotesEntity CreateNotes(NotesModel notesModel, long userId)
        {
            try
            {
                return notesRL.CreateNotes(notesModel,userId);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public IEnumerable<NotesEntity> ReadNotes(long noteid)
        {
            try
            {
                return notesRL.ReadNotes(noteid);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public NotesEntity UpdateNotes(NotesModel noteModel, long noteid, long userId)
        {
            try
            {
                return this.notesRL.UpdateNotes(noteModel, noteid, userId);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public bool DeleteNote(long noteid)
        {
            try
            {
                return this.notesRL.DeleteNote(noteid);
            }
            catch (Exception)
            {
                throw;
            }
        }

        

        public NotesEntity PinnedORNot(long noteid, long userId)
        {
            try
            {
                return this.notesRL.PinnedORNot(noteid, userId);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public NotesEntity Archive(long noteid, long userid)
        {
            try
            {
                return this.notesRL.Archive(noteid,userid);
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
