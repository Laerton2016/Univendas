using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Univendas.Model;

namespace Univendas.Controle
{
    /// <summary>
    /// Classe encarregada de cuidar das regras de negócio dos itens de venda.
    /// <Auto>Daniel Rodrigues Coura</Auto>
    /// <Data>04/01/2017</Data>
    /// </summary>
    class IVenda
    {
        public CProduto _cp { get; private set; }
        public Int32 _quant { get; private set; }
        public Decimal _soma { get; private set; }

        /// <summary>
        /// IVenda cuida das regras de negócio que envolve o estoque dos bancos de dados.
        /// </summary>
        /// <param name="cp">Código do produto obrigatório para que o sistema possa buscar o estoque nos bancos de dados.</param>
        /// <param name="quant">Quantidade obrigatória para que o sistema possa conferir se há estoque suficiente.</param>
        public IVenda(CProduto cp, int quant)
        {
            if (quant < 1) { throw new Exception("Quantidade não pode ser menor que 1."); }
            if (quant < cp.EstoqueGeral()) { throw new Exception("Estoque insuficiente."); }

            _cp = cp;
            _quant = quant;
        }

        /// <summary>
        /// Método dá baixa no estoque dos bancos
        /// </summary>
        public void Baixa()
        {
            if (_cp.LojaMaiorEstoque() == "L")
            {
                if (_quant <= _cp.EstoqueL)
                {
                    _cp.EstoqueL = 0;
                }
                else
                {
                    _quant -= _cp.EstoqueL;
                    _cp.EstoqueL = 0;
                    _cp.EstoqueS -= _quant;
                }
            }
            else
            {
                if (_quant <= _cp.EstoqueS)
                {
                    _cp.EstoqueS = 0;
                }
                else
                {
                    _quant -= _cp.EstoqueS;
                    _cp.EstoqueS = 0;
                    _cp.EstoqueL -= _quant;
                }
            }
        }

        /// <summary>
        /// Método calcula o valor dos produtos
        /// </summary>
        public void SomaProduto()
        {
            _soma = _cp.ValorVenda() * _quant;
        }
    }
}
