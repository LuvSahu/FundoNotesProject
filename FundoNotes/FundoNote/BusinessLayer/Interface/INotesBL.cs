﻿using CommonLayer.Model;
using RepositoryLayer.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLayer.Interface
{
    public interface INotesBL
    {
        public NotesEntity CreateNotes(NotesModel notesModel, long userId);

        public IEnumerable<NotesEntity> ReadNotes(long userId);


        public NotesEntity UpdateNotes(NotesModel noteModel, long noteid, long userId);

        public bool DeleteNote(long noteid);

        public NotesEntity PinnedORNot(long noteid, long userId);

        public bool Archive(long noteId, long userId);








    }
}
