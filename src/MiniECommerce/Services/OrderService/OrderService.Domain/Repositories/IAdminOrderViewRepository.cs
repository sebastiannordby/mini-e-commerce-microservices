﻿using OrderService.Library.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderService.Domain.Repositories
{
    public interface IAdminOrderViewRepository
    {
        Task<IEnumerable<OrderView>> List();
    }
}
