using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Univendas.Model;

namespace Univendas.Controle
{
    /// <summary>
    /// Classe encarregada de cuidar das regras de negócio de venda.
    /// <Auto>Daniel Rodrigues Coura</Auto>
    /// <_data>03/01/2017</_data>
    /// </summary>
    class CVenda
    {
        private List<ItemVenda> _lista = new List<ItemVenda>();
        private Context_L cl = new Context_L();
        private Context_S cs = new Context_S();
        private Context_Venda cv = new Context_Venda();
        public Cliente _client { get; private set; }
        public CUsuario _user { get; private set; }
        public DateTime _data { get; private set; }
        public string _modalidade { get; private set; }
        public string _obs { get; private set; }

        /// <summary>
        /// Venda declara as variáveis globais para inserção no banco de dados.
        /// </summary>
        /// <param name="usuario">Usuario obrigatório para inserção no bancos de dados.</param>
        /// <param name="cliente">Cliente obrigatório para inserção no bancos de dados.</param>
        /// <param name="data">_data obrigatória para inserção no bancos de dados.</param>
        /// <param name="modalidade">_modalidade obrigatória para inserção no bancos de dados.</param>
        /// <param name="obs">Observação não obrigatória</param>
        public CVenda(CUsuario usuario, Cliente cliente, DateTime data, string modalidade, string obs = "")
        {
            this._user = usuario;
            this._client = cliente;
            this._data = data;
            this._modalidade = modalidade;
            this._obs = obs;
        }

        /// <summary>
        /// Método adiciona itens de venda à lista 
        /// </summary>
        public void AdicionaItem(ItemVenda item)
        {
            _lista.Add(item);
        }

        /// <summary>
        /// Método retorna a lista dos itens de venda da venda instanciada
        /// </summary>
        /// <returns>Retorna lista do tipo ItemVenda</returns>
        public List<ItemVenda> GetLista()
        {
            return this._lista;
        }

        /// <summary>
        /// Método retorna o Id da venda instanciada
        /// </summary>
        /// <returns>Retorna Id do tipo inteiro</returns>
        public int GetId()
        {
            using (Context_Venda cv = new Context_Venda())
            {
                return cv.venda.ToList().Last().ID_VENDA;
            }
        }

        /// <summary>
        /// Método retorna o valor total da venda 
        /// </summary>
        /// <returns>Retorna Decimal</returns>
        public decimal CalculaTotal()
        {
            return _lista.Sum(c => c._soma);
        }

        /// <summary>
        /// Método insere os dados dos itens de venda no banco de dados
        /// </summary>
        public void InsertItemVenda()
        {
            using (Context_Venda cv = new Context_Venda())
            {
                foreach (var item in _lista)
                {
                    if (item._nEstoqueL > 0)
                    {
                        itens_de_venda ll = new itens_de_venda { COD_BARRAS = item._cp.GetEAN(), QUANTIDADE = item._nEstoqueL, VALOR_UNITARIO = (double)item._cp.ValorVenda(), VENDA_ID_VENDA = GetId(), LOJA_ID_LOJA = "l" };
                        cv.itens_de_venda.Add(ll);
                        cv.SaveChanges();
                    }
                    if (item._nEstoqueS > 0)
                    {
                        itens_de_venda ls = new itens_de_venda { COD_BARRAS = item._cp.GetEAN(), QUANTIDADE = item._nEstoqueS, VALOR_UNITARIO = (double)item._cp.ValorVenda(), VENDA_ID_VENDA = GetId(), LOJA_ID_LOJA = "s" };
                        cv.itens_de_venda.Add(ls);
                        cv.SaveChanges();
                    }
                }
            }
        }

        /// <summary>
        /// Método insere os dados da venda instanciada no banco de dados
        /// </summary>
        public void InsertVenda()
        {
            using (Context_Venda cv = new Context_Venda())
            {
                venda vend = new venda { CLIENTE = _client._id, DATA = _data, MODALIDADE = _modalidade, OBS = _obs, USUARIO_ID_USUARIO = _user._id.ID_USUARIO, VALOR_TOTAL = (float)CalculaTotal() };
                cv.venda.Add(vend);
                cv.SaveChanges();
            }
        }

        /*
        /// <summary>
        /// Método encarregado de atualizar o estoque após a venda
        /// </summary>
        public void UpdateEstoque()
        {
            foreach (var item in _lista)
            {
                string a = item._cp.GetEAN();
                TESTOQUE obj = cl.TESTOQUE.Where(p => p.CODBARRAS == a).First<TESTOQUE>();
                obj.QTDEREAL = item._cp.EstoqueL;
                cl.SaveChanges();
            }

            foreach (var item in _lista)
            {
                string a = item._cp.GetEAN();
                TESTOQUE obj = cs.TESTOQUE.Where(p => p.CODBARRAS == a).First<TESTOQUE>();
                obj.QTDEREAL = item._cp.EstoqueS;
                cs.SaveChanges();
            }

        }
        */

        /// <summary>
        /// Método encarregado de inserir os dados na prevenda
        /// </summary>
        public void InsertPrevenda()
        {
            TPREVENDA pl = cl.TPREVENDA.ToList().Last();
            TPREVENDA ps = cl.TPREVENDA.ToList().Last();
            foreach (var item in _lista)
            {
                if (item._nEstoqueL > 0)
                {
                    prevenda pre = new prevenda() { VENDA_ID_VENDAS = GetId(), CONTROLE = (pl.CONTROLE + 1), LOJA_ID_LOJA = "l" };
                    cv.prevenda.Add(pre);
                    cv.SaveChanges();
                    break;
                }
            }
            foreach (var item in _lista)
            {
                if (item._nEstoqueS > 0)
                {
                    prevenda pre = new prevenda() { VENDA_ID_VENDAS = GetId(), CONTROLE = (ps.CONTROLE + 1), LOJA_ID_LOJA = "s" };
                    cv.prevenda.Add(pre);
                    cv.SaveChanges();
                    break;
                }
            }
        }

    }
}
