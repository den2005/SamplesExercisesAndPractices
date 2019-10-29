using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VS2017.BankSystem.AdoNetClsLib;

namespace VS2017.BankSystems.AspNetCoreWebApp.Models
{
    public class Repository : IRepository
    {
        private IDataAccessLayer _dataLib;

        public Repository()
        {
            _dataLib = new DataAccessLayer();
        }


    }
}
