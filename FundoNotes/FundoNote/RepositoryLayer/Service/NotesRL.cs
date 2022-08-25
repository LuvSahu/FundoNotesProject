using CommonLayer.Model;
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
    public class NotesRL : INotesRL
    {
        private readonly IConfiguration _Appsettings;

        private readonly FundoContext fundoContext;

        public object UserId { get; private set; }

        public NotesRL(FundoContext fundoContext, IConfiguration _Appsettings)
        {
            this.fundoContext = fundoContext;
            this._Appsettings = _Appsettings;
        }
        public NotesEntity CreateNotes(NotesModel notesModel, long userId)
        {
            try
            {
                NotesEntity user = new NotesEntity();
                var result = fundoContext.NotesTable.FirstOrDefault(e => e.UserId == userId);
                user.Title = notesModel.Title;
                user.Description = notesModel.Description;
                user.Color = notesModel.Color;
                user.Image = notesModel.Image;
                user.Archive = notesModel.Archive;
                user.Pin = notesModel.Pin;
                user.Trash = notesModel.Trash;
                user.Reminder = notesModel.Reminder;
                user.CreateTable = notesModel.CreateTable;
                user.EditedTime = notesModel.EditedTime;
                user.UserId = userId;


                fundoContext.NotesTable.Add(user);
                int res = fundoContext.SaveChanges();
                if (res > 0)
                {
                    return user;
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
