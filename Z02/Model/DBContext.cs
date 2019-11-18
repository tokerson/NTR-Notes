using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Z02.Model
{
    public class DBContext : DbContext
    {
        public DbSet<Note> Notes { get; set; }
        public DbSet<Category> Categories { get; set; }

        public DbSet<NoteCategory> NoteCategories { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder options)
            => options.UseSqlServer(@"Server=localhost,8200;Database=NTR2019Z;User Id=tokarzewski;Password=283778;");

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<NoteCategory>().HasKey(nc => new { nc.NoteID, nc.CategoryID });
            modelBuilder.Entity<NoteCategory>()
                .HasOne(nc => nc.Note)
                .WithMany(n => n.NoteCategories)
                .HasForeignKey(n => n.NoteID);
            modelBuilder.Entity<NoteCategory>()
                .HasOne(bc => bc.Category)
                .WithMany(c => c.NoteCategories)
                .HasForeignKey(bc => bc.CategoryID);
        }
    }
}