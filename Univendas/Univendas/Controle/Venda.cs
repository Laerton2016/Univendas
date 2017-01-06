using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Univendas.Model;

namespace Univendas.Controle
{
    /// <summary>
    /// Classe encarregada de cuidar das regras de venda.
    /// <Auto>Daniel Rodrigues Coura</Auto>
    /// <Data>03/01/2017</Data>
    /// </summary>
    class Venda
    {
        public List<IVenda> Lista { get; private set; }

        public Venda (Usuario usuario, Cliente cliente, string data, string modalidade, string obs = "")
        {

        }

        /// <summary>
        /// Método adiciona itens de venda à lista 
        /// </summary>
        public void AdicionaItem(IVenda item)
        {
            Lista.Add(item);
        }

        /// <summary>
        /// Método retorna o valor total da venda 
        /// </summary>
        /// <returns>Decimal </returns>
        public decimal CalculaTotal()
        {
            return Lista.Sum(c => c._soma);
        }
    }
}
