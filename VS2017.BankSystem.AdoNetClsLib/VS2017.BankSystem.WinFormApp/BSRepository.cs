using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VS2017.BankSystem.AdoNetClsLib;

namespace VS2017.BankSystem.WinFormApp
{
    public class BSRepository : IBSRepository
    {
        private IDataAccessLayer _dataLib;

        public BSRepository()
        {
            _dataLib = new DataAccessLayer();
        }
    }
}
