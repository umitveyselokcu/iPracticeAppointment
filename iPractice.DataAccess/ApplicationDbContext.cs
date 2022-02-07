using System;
using System.IO;
using System.Linq;
using System.Reflection;
using iPractice.DataAccess.Models;
using Microsoft.EntityFrameworkCore;

namespace iPractice.DataAccess
{
    public class ApplicationDbContext : DbContext
    {
        public DbSet<Psychologist> Psychologists { get; set; }
        public DbSet<Client> Clients { get; set; }
        public DbSet<AppointmentSlot> AppointmentSlots { get; set; }

        public ApplicationDbContext() { }
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        { }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            var path = $"{Assembly.GetExecutingAssembly().Location.Split(@"\bin\")[0]}\\iPractice.db";
            
            optionsBuilder.UseSqlite($@"Data Source={path};Cache=Shared");
        }
        
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<AppointmentSlot>().HasKey(a => a.Id);
            modelBuilder.Entity<Psychologist>().HasKey(psychologist => psychologist.Id);
            modelBuilder.Entity<Client>().HasKey(client => client.Id);
            modelBuilder.Entity<Psychologist>().HasMany(p => p.Clients).WithMany(b => b.Psychologists);
            modelBuilder.Entity<Client>().HasMany(p => p.Psychologists).WithMany(b => b.Clients);
            modelBuilder.Entity<Psychologist>().HasMany(p => p.AppointmentSlots).WithOne(x => x.Psychologist);
            modelBuilder.Entity<Client>().HasMany(p => p.Appointments);
        }
    }
}
