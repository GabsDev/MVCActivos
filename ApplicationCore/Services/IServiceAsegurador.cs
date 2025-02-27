﻿using Infraestructure.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.Services
{
    public interface IServiceAsegurador
    {
        IEnumerable<Asegurador> GetAsegurador();
        Asegurador GetAseguradorByID(int id);
        void DeleteAsegurador(int id);
        Asegurador Save(Asegurador asegurador);
    }
}
