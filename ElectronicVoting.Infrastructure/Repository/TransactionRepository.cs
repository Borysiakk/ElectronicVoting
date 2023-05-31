﻿using ElectronicVoting.Domain.Table;
using ElectronicVoting.Persistence;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElectronicVoting.Infrastructure.Repository
{
    public class TransactionRepository : Repository<RegisteredTransaction>
    {
        public TransactionRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
        }
    }
}