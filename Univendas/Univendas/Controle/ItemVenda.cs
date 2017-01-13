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
    /// <_data>04/01/2017</_data>
    /// </summary>
    class ItemVenda
    {
        public CProduto _cp { get; private set; }
        public Int32 _quant { get; private set; }
        public Decimal _soma { get; private set; }
        public int _nEstoqueS { get; private set; }
        public int _nEstoqueL { get; private set; }

        /// <summary>
        /// ItemVenda cuida das regras de negócio que envolve o estoque dos bancos de dados.
        /// </summary>
        /// <param name="cp">Código do produto obrigatório para que o sistema possa buscar o estoque nos bancos de dados.</param>
        /// <param name="quant">Quantidade obrigatória para que o sistema possa conferir se há estoque suficiente.</param>
        public ItemVenda(CProduto cp, int quant)
        {
            if (quant < 1) { throw new Exception("Quantidade não pode ser menor que 1."); }
            if (quant > cp.EstoqueGeral()) { throw new Exception("Estoque insuficiente."); }

            _cp = cp;
            _quant = quant;
        }

        /// <summary>
        /// Método dá baixa no estoque dos bancos
        /// </summary>
        public void CalculaEstoque()
        {
            if (_cp.LojaMaiorEstoque() == "L")
            {
                if (_quant <= _cp.EstoqueL)
                {
                    _cp.EstoqueL -= _quant;
                    _nEstoqueL = _quant;
                }
                else
                {
                    int diminui = _quant - _cp.EstoqueL;
                    _cp.EstoqueL = 0;
                    _cp.EstoqueS -= diminui;
                    _nEstoqueS = diminui;
                    _nEstoqueL = _quant - diminui;
                }
            }
            else
            {
                if (_quant <= _cp.EstoqueS)
                {
                    _cp.EstoqueS -= _quant;
                    _nEstoqueS = _quant;
                }
                else
                {
                    int diminui = _quant - _cp.EstoqueS;
                    _cp.EstoqueS = 0;
                    _cp.EstoqueL -= diminui;
                    _nEstoqueL = diminui;
                    _nEstoqueS = _quant - diminui;
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
