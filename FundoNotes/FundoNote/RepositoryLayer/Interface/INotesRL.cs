using CommonLayer.Model;
using RepositoryLayer.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace RepositoryLayer.Interface
{
    public interface INotesRL
    {
        public NotesEntity CreateNotes(NotesModel notesModel, long userId);

        public IEnumerable<NotesEntity> ReadNotes(long noteid);

        public NotesEntity UpdateNotes(NotesModel notes, long noteid, long userId);

        public bool DeleteNote(long noteid);

        public NotesEntity PinnedORNot(long noteid, long userId);

        public NotesEntity Archive(long noteid, long userid);



    }
}
