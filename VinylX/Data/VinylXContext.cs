using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using VinylX.Models;

namespace VinylX.Data
{
    public class VinylXContext : DbContext
    {
        public VinylXContext (DbContextOptions<VinylXContext> options)
            : base(options)
        {
        }

        public DbSet<VinylX.Models.Artist> Artist { get; set; } = default!;
        public DbSet<VinylX.Models.Folder> Folder { get; set; } = default!;
        public DbSet<VinylX.Models.Media> Media { get; set; } = default!;
        public DbSet<VinylX.Models.RecordLabel> RecordLabel { get; set; } = default!;
        public DbSet<VinylX.Models.Release> Release { get; set; } = default!;
        public DbSet<VinylX.Models.ReleaseInstance> ReleaseInstance { get; set; } = default!;
        public DbSet<VinylX.Models.MasterRelease> MasterRelease { get; set; } = default!;
        public DbSet<VinylX.Models.User> User { get; set; } = default!;
    }
}
