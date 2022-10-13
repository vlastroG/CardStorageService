﻿using FullTextSearchSimpleBenchmark.Entity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FullTextSearchSimpleBenchmark.DBContext
{
    internal class DocumentDbContext : DbContext
    {
        public virtual DbSet<Document> Documents { get; set; }

        public DocumentDbContext(DbContextOptions options) : base(options) { }
    }
}
