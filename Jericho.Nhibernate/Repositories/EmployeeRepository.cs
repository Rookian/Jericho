﻿using Jericho.Core.Domain;
using Jericho.Core.Repositories;

namespace Jericho.Nhibernate.Repositories
{
    public class EmployeeRepository : Repository<Employee>, IEmployeeRepository
    {

    }
}