﻿using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using MinAppApi.Data.Configurations;
using MinAppApi.Entities;

namespace MinAppApi.Data
{
    public class MiniAppApiDbContext : IdentityDbContext<AppUser>
    {

        public MiniAppApiDbContext(DbContextOptions options) : base(options)
        {

        }
        public DbSet<Event> Events { get; set; }
        public DbSet<Ticket> Tickets { get; set; }
        public DbSet<Organizer> Organizers { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfiguration(new EventConfiguration());
            modelBuilder.ApplyConfiguration(new OrganizerConfiguration());
            modelBuilder.ApplyConfiguration(new TicketConfiguration());
        }
    }
}
