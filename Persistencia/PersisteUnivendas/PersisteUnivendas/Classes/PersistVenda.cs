using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PersisteUnivendas.Classes
{
    public class PersistVenda
    {
        private venda _venda;


        public venda Venda
        {
            get
            {
                return _venda;
            }

            set
            {
                _venda = value;
            }
        }
    }
}
