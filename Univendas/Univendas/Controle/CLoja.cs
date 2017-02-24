using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Univendas.Model;
using PersisteUnivendas;

namespace Univendas.Controle
{
    /// <summary>
    /// Classe encarregada de cuidar das regras de negócio de uma Loja.
    /// <Auto>Daniel Rodrigues Coura</Auto>
    /// <_data>12/01/2017</_data>
    /// </summary>
    class CLoja
    {
        private Context_Venda _context = new Context_Venda();
        public loja _id { get; private set; }
        /// <summary>
        /// CLoja cuida das regras de negócio que envolve as lojas do bancos de dados.
        /// </summary>
        /// <param name="loja">Nome da loja obrigatório para que o sistema possa validar a busca no bancos de dados.</param>
        public CLoja(string loja)
        {
            _id = _context.loja.Where(c => c.LOJA1 == loja).First();
            if (_id == null) throw new Exception("Loja inexistente.");
        }
    }
}
