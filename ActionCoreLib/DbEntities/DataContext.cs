using ActionCoreLib.DbEntities.EntitiesConfig;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ActionCoreLib.DbEntities
{
    /* <summary> 
     * Class for entities, add config, connect to DB.
     * File use public
    */

    public class DataContext : DbContext
    {
        private readonly string _dbPath;
        public DbSet<Models.DiaryModel> Diary { get; set; }
        public DbSet<Models.UserAuthenModel> UserAuthen { get; set; }

        // constructor
        public DataContext(string DbPath)
        {
            _dbPath = DbPath;
            Database.EnsureCreated();
        }

        // event on create database
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite("Data Source=" + _dbPath);                                                    
        }

        // event on create table
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // TODO: add config for other table

            modelBuilder.ApplyConfiguration(new DiaryEntitiesConfig());
            modelBuilder.ApplyConfiguration(new UserAuthenEntitiesConfig());
            base.OnModelCreating(modelBuilder);
        }

        // Dont usse this
        // add new diary
        public void AddDiary(Models.DiaryModel diary)
        {
            Diary.Add(diary);
            SaveChanges();
        }

        // update diary
        public void UpdateDiary(Models.DiaryModel diary)
        {
            Diary.Update(diary);
            SaveChanges();
        }

        // delete diary
        public void DeleteDiary(Models.DiaryModel diary)
        {
            Diary.Remove(diary);
            SaveChanges();
        }

        // get all diary
        public List<Models.DiaryModel> GetDiary()
        {
            return Diary.ToList();
        }

        // get all ImageEntryTF limit
        public List<Models.DiaryModel> GetImageEntryTF(int limit = 50)
        {
            return Diary.OrderByDescending(x => x.Id).Take(limit).ToList();
        }



    }
}
