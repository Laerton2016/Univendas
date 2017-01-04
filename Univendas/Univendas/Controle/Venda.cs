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
        private Usuario usuario;
        private List<IVenda> lista;

        public Venda (Usuario usuario, Cliente cliente, string data, string modalidade, string obs)
        {

        }

        public Venda(Usuario usuario, Cliente cliente, string data, string modalidade)
        {

        }

        /// <summary>
        /// Método adiciona itens de venda à lista 
        /// </summary>
        public void AdicionaItem(IVenda item)
        {
            lista.Add(item);
        }

        /// <summary>
        /// Método retorna o valor total da venda 
        /// </summary>
        /// <returns>Decimal </returns>
        public decimal CalculaTotal()
        {
            return lista.Sum(c => c._soma);
        }

    }
}
