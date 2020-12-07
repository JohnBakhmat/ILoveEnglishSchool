using System;
using System.Collections.Generic;
using System.Text;
using ILoveEnglishSchool.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace ILoveEnglishSchool.Data {
    public class ApplicationDbContext : IdentityDbContext {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options) { }
        public DbSet<Lesson> Lessons { get; set; }
        public DbSet<Part> Parts { get; set; }
        public DbSet<PartModules> PartModulesEnumerable { get; set; }
        protected override void OnModelCreating(ModelBuilder builder) {
            base.OnModelCreating(builder);
            builder.Entity<Part>()
                .HasOne(p => p.Lesson)
                .WithMany(p => p.Parts)
                .HasForeignKey(p => p.LessonId);
            builder.Entity<PartModules>()
                .HasOne(m => m.Part)
                .WithMany(m => m.Modules)
                .HasForeignKey(p => p.PartId);
        }
    }
}