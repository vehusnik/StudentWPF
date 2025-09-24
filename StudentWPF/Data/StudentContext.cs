using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentWPF.Data
{
    public class StudentContext : DbContext
    {
        public DbSet<Student> Students => Set<Student>();

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer(
                    "Data Source=(localdb)\\MSSQLLocalDB;" +
                    "Initial Catalog=StudentDbDemo;" +
                    "Integrated Security =True;" +
                    "TrustServerCertificate=True");



            }
        }

        public void EnsureCreatedAndSeed()
        {
            Database.EnsureCreated();
            SeedIfEmpty();
        }

        public void SeedIfEmpty()
        {
            if (!Students.Any())
            {
                var initial = new List<Student>
                {
                    new Student { FirstName = "Jan", LastName = "Novák", year = 1, Email = "jan.nov.@seznam.cz" },
                    new Student { FirstName = "Petr", LastName = "Novotný", year = 2, Email = "petr.novot.@seznam.cz" },
                    new Student { FirstName = "Lucie", LastName = "Dvořáková", year = 1, Email = "lucie.dv.@seznam.cz" },
                    new Student { FirstName = "Marie", LastName = "Dvořálová", year = 1, Email = "marie.dvoralova.@seznam.cz" },
                    new Student { FirstName = "Hele", LastName = "Kunová", year = 1, Email = "Hele.kun.@seznam.cz" },
                    new Student { FirstName = "Jana", LastName = "Nováková", year = 1, Email = "jana.nov.@seznam.cz" },
                    new Student { FirstName = "Lenka", LastName = "Levová", year = 1, Email = "Lenka.lev.@seznam.cz" },
                    new Student { FirstName = "Brenda", LastName = "Hošková", year = 1, Email = "Brenda.hosk.@seznam.cz" },
                    new Student { FirstName = "Karel", LastName = "Černý", year = 1, Email = "Karel.cerny.@seznam.cz" },
                    new Student { FirstName = "Kateřina", LastName = "Noveklová", year = 1, Email = "Kateřina.novekl.@seznam.cz" },

                };
                Students.AddRange(initial);
                SaveChanges();
            }
        }


    }
}
