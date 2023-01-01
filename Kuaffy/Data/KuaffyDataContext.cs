using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Kuaffy.Models;

    public class KuaffyDataContext : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Server=(localdb)\mssqllocaldb;Database=KuaffyDB;Trusted_Connection=true;MultipleActiveResultSets=true");

        }
    public DbSet<Kuaffy.Models.Appointment> Appointments { get; set; } = default!;
    public DbSet<Kuaffy.Models.Comment> Comments { get; set; } = default!;
    public DbSet<Kuaffy.Models.Company> Companies { get; set; } = default!;
    public DbSet<Kuaffy.Models.CompanyType> CompanyTypes { get; set; } = default!;
}