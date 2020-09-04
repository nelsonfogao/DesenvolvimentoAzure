﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Domain;

namespace WebApi.Data
{
    public class WebApiContext : DbContext
    {
        public WebApiContext (DbContextOptions<WebApiContext> options)
            : base(options)
        {
        }

        public DbSet<Amigo> Amigo { get; set; }
    }
}
