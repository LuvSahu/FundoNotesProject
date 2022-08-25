﻿using CommonLayer.Model;
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
                if(result != null)
                {
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
                    fundoContext.SaveChanges();
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

        public IEnumerable<NotesEntity> ReadNotes(long noteid)
        {
            try
            {
                var result = this.fundoContext.NotesTable.Where(x => x.NotesId == noteid);
                return result;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public NotesEntity UpdateNotes(NotesModel notes, long noteid,long userId)
        {
            try
            {
                NotesEntity result = fundoContext.NotesTable.Where(e => e.NotesId == noteid && e.UserId == userId).FirstOrDefault();
                if (result != null)
                {
                    result.Title = notes.Title;
                    result.Description = notes.Description;
                    result.Color = notes.Color;
                    result.Image = notes.Image;
                    result.Archive = notes.Archive;
                    result.Pin = notes.Pin;
                    fundoContext.NotesTable.Update(result);
                    fundoContext.SaveChanges();
                    return result;
                }
                return null;
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
                var result = this.fundoContext.NotesTable.FirstOrDefault(x => x.NotesId == noteid);
                fundoContext.Remove(result);
                int deletednote = this.fundoContext.SaveChanges();
                if (deletednote > 0)
                {
                    return true;
                }
                return false;
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
                NotesEntity result = this.fundoContext.NotesTable.FirstOrDefault(x => x.NotesId == noteid && x.UserId == userId);
                if (result.Pin == true)
                {
                    result.Pin = false;
                    this.fundoContext.SaveChanges();
                    return result;
                }
                result.Pin = true;
                this.fundoContext.SaveChanges();
                return null;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public bool Archive(long noteId, long userId)
        {
            try
            {
                var result = fundoContext.NotesTable.Where(x => x.UserId == userId && x.NotesId == noteId).FirstOrDefault();

                if (result.Archive == false)
                {
                    result.Archive = true;
                    fundoContext.SaveChanges();
                    return true;
                }
                else
                {
                    result.Archive = false;
                    fundoContext.SaveChanges();
                    return false;
                }
            }
            catch (Exception)
            {
                throw;
            }
        }


    }
}
