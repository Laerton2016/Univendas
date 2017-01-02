using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Univendas.Model;

namespace Univendas.Controle
{
    /// <summary>
    /// Classe encarregada de cuidar das regras de negócio de um produto.
    /// <Auto>Laerton Marques de Figueiredo</Auto>
    /// <Data>01/01/2017</Data>
    /// </summary>
    public class CProduto
    {
        private TESTOQUE _produto_L;
        private TESTOQUE _produto_S;
        private Context_L _CTL = new Context_L();
        private Context_S _CTS = new Context_S();
        private Int32 _estoqueL = 0;
        private Int32 _estoqueS = 0;

        
        /// <summary>
        /// CProduto cuida das regras de negócio que envolve os produtos dos bancos de dados.
        /// </summary>
        /// <param name="EAN">Código EAN obrigatório para que o sistema possa fazer a busca nos bancos de dados. Támanho fixo de 13 dígitos numéricos.</param>
        public CProduto (String EAN)
        {
            if (!Util.Sonumeros(EAN)) { throw new Exception("O Código EAN só pode conter números."); }
            if (EAN.Length != 13) { throw new Exception("O Código EAN deve conter 13 dígitos."); }
            _produto_L = (TESTOQUE) _CTL.TESTOQUE.Where(p => p.CODBARRAS == EAN);
            _produto_S = (TESTOQUE) _CTS.TESTOQUE.Where(p => p.CODBARRAS == EAN);

            if (_produto_L == null && _produto_S == null) { throw new Exception("Não foi encontrado nenhum produto com esse código de barras."); }

            if (_produto_L != null)
            {
                EstoqueL = (Int32) _produto_L.QTDEREAL;
            }

            if (_produto_S != null)
            {
                EstoqueS = (Int32)_produto_S.QTDEREAL;
            }
        }

        /// <summary>
        /// Método retorna uma lista de produtos a partir de um termo repassado
        /// </summary>
        /// <param name="termo">Termo para pesquisa, não permitido uso de caracteres especiais ou envio de string em branco</param>
        /// <returns>Retorna uma lista de objetos TESTOQUE, retorna uma lista vazia caso não localize</returns>
        public static List<TESTOQUE> Lista (String termo)
        {
            if (Util.ContemPontuacao(termo)) { throw new Exception("Não é permitido o uso de caracteres especias."); }
            if (termo.Trim().Equals("")) { throw new Exception("Não é permitido uso de string vazia."); }
            List<TESTOQUE> retorno = new List<TESTOQUE>();
            using (Context_S context = new Context_S())
            {
                var query = context.TESTOQUE.Where(p => p.PRODUTO.Contains(termo));
                foreach (var item in query)
                {
                    retorno.Add(item);
                }
            }
            using (Context_L context = new Context_L())
            {
                var query = context.TESTOQUE.Where(p => p.PRODUTO.Contains(termo));
                foreach (var item in query)
                {
                    if (!retorno.Contains(item))
                    {
                        retorno.Add(item);
                    }
                }
            }
            return retorno;
        }

        /// <summary>
        /// Método retorna a descrição do produto
        /// </summary>
        /// <returns>Descrição do produto</returns>
        public String Descricao()
        {
            if (_produto_L != null)
            {
                return _produto_L.PRODUTO;
            }else
            {
                return _produto_S.PRODUTO;
            }
        }

        /// <summary>
        /// Método retorna o estoque total do produto
        /// </summary>
        /// <returns>Inteiro positivo maior que zero.</returns>
        public Int32 EstoqueGeral()
        {
            return EstoqueL + EstoqueS;
        }

        /// <summary>
        /// Método retorna o preço de venda do produto baseado na média entre os dois bancos de dados e seus respectivos estoques.
        /// </summary>
        /// <returns>Decimal maior ou igual a zero.</returns>
        public Decimal ValorVenda()
        {
            decimal v1 = 0;
            Decimal v2 = 0;
            if (_produto_L != null)
            {
                v1 = _produto_L.PRECOVENDA * EstoqueL;
            }

            if (_produto_S != null)
            {
                v2 = _produto_S.PRECOVENDA * EstoqueS;
            }

            return (v2 + v1) / EstoqueGeral();
        }

        /// <summary>
        /// Método retorna a loja que tem maior estoque
        /// </summary>
        /// <returns>Inteiro maior ou igual a zero</returns>
        public String LojaMaiorEstoque()
        {
            if (EstoqueL > EstoqueS)
            {
                return "L";
            }
            return "S";
        }

        /// <summary>
        /// Estoque da loja L
        /// </summary>
        public int EstoqueL
        {
            get
            {
                return _estoqueL;
            }

            set
            {
                _estoqueL = value;
            }
        }
        /// <summary>
        /// Estoque da loja S
        /// </summary>
        public int EstoqueS
        {
            get
            {
                return _estoqueS;
            }

            set
            {
                _estoqueS = value;
            }
        }

        /// <summary>
        /// Retorna o endereço da imagem do produto
        /// </summary>
        /// <returns>String contendo o endereçod a imagem.</returns>
        public String Imagem()
        {
            if (_produto_S != null)
            {
                return _produto_S.CAMPO1;
            }
            return _produto_L.CAMPO1;
        }

        public override string ToString()
        {
            return Descricao().ToUpper();
        }

    }
}
