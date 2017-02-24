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
    /// Classe encarregada de cuidar das regras de negócio de venda.
    /// <Auto>Daniel Rodrigues Coura</Auto>
    /// <_data>03/01/2017</_data>
    /// </summary>
    public class CVenda
    {
        //private List<ItemVenda> _lista = new List<ItemVenda>();
        private readonly Context_L _cl = new Context_L();//Contexto com a base de dados Laerton
        private readonly Context_S _cs = new Context_S(); //Contetxo com a base de dados SIL
        private readonly Context_Venda _cv = new Context_Venda(); //Conetxto com a base de dados Mysql Vendas
        private TCLIENTE _client;
        private usuario _user;
        private venda _venda;

        /// <summary>
        /// Cliente que cuida da venda atual
        /// </summary>
        public TCLIENTE Client
        {
            get { return _client; }
            set
            {
                _client = value;
                Venda.CLIENTE = Client.CONTROLE;
            }
        }
        /// <summary>
        /// Usuario que tem acesso a venda atual
        /// </summary>
        private usuario User
        {
            get { return _user; }
            set
            {
                _user = value;
                Venda.USUARIO_ID_USUARIO = _user.ID_USUARIO;
            }
        }
        /// <summary>
        /// Venda em questão do controle
        /// </summary>
        public venda Venda
        {
            get { return _venda; }
            set
            {
                _venda = value;
                _user = _cv.usuario.Where(c => c.ID_USUARIO == Venda.USUARIO_ID_USUARIO).First();
                _client = _cs.TCLIENTE.Where(c => c.CONTROLE == Venda.CLIENTE).First();
            }
        }


        /// <summary>
        /// Venda declara as variáveis globais para inserção no banco de dados.
        /// </summary>
        /// <param name="usuario">Usuario obrigatório para inserção no bancos de dados.</param>
        /// <param name="cliente">Cliente obrigatório para inserção no bancos de dados.</param>
        /// <param name="data">_data obrigatória para inserção no bancos de dados.</param>
        /// <param name="modalidade">_modalidade obrigatória para inserção no bancos de dados.</param>
        /// <param name="obs">Observação não obrigatória</param>
        public CVenda(usuario usuario, TCLIENTE cliente, DateTime data, string modalidade = "", string obs = "")
        {
            _user = usuario;
            _client = cliente;
            MontaVenda(usuario, cliente.CONTROLE, data, modalidade, obs);
            
        }

        /// <summary>
        /// Venda declara as variáveis globais para inserção no banco de dados, usando para casos onde o cliente não está definido.
        /// </summary>
        /// <param name="usuario">Usuario obrigatório para inserção no bancos de dados.</param>
        /// <param name="data">_data obrigatória para inserção no bancos de dados.</param>
        /// <param name="modalidade">_modalidade obrigatória para inserção no bancos de dados.</param>
        /// <param name="obs">Observação não obrigatória</param>
        public CVenda(usuario usuario, DateTime data, string modalidade = "", string obs = "")
        {
            _user = usuario;
            _client = _cs.TCLIENTE.Where(c => c.CONTROLE == 1).First();
            MontaVenda(usuario, 1, data, modalidade, obs);
        }

        
        /// <summary>
        /// Método encarregado de criar uma venda a partir dos atributos repassados.
        /// </summary>
        /// <param name="usuario">Usuário que criou a venda</param>
        /// <param name="Idcliente">Id do cliente que pertence a venda</param>
        /// <param name="data">Data da venda</param>
        /// <param name="modalidade">Modalidade da venda </param>
        /// <param name="obs">Observação da venda.</param>
        private void MontaVenda(usuario usuario, Int32 Idcliente, DateTime data, string modalidade, string obs)
        {
            _venda = new venda();
            _venda.USUARIO_ID_USUARIO = usuario.ID_USUARIO;
            _venda.CLIENTE = Idcliente;
            _venda.DATA = data;
            _venda.MODALIDADE = (modalidade.Equals("") ? "DINHEIRO" : modalidade);
            _venda.OBS = obs;

        }

        /// <summary>
        /// Método adiciona itens de venda à lista 
        /// </summary>
        public void AdicionaItem(itens_de_venda item)
        {
            Venda.itens_de_venda.Add(item);
        }

        public void RemoveItemVenda(itens_de_venda item)
        {
            Venda.itens_de_venda.Remove(item);
        }

        /// <summary>
        /// Método retorna a lista dos itens de venda da venda instanciada
        /// </summary>
        /// <returns>Retorna lista do tipo ItemVenda</returns>
        public ICollection<itens_de_venda> GetLista()
        {
            return Venda.itens_de_venda;
        }

        /// <summary>
        /// Método retorna o Id da venda instanciada
        /// </summary>
        /// <returns>Retorna Id do tipo inteiro</returns>
        public int GetId()
        {
            return Venda.ID_VENDA;
        }

        /// <summary>
        /// Método retorna o valor total da venda 
        /// </summary>
        /// <returns>Retorna Decimal</returns>
        public decimal CalculaTotal()
        {
            return (decimal)Venda.itens_de_venda.Sum(c => (c.VALOR_UNITARIO * c.QUANTIDADE));  //_lista.Sum(c => c._soma);
        }

        /// <summary>
        /// Método insere os dados da venda instanciada no banco de dados
        /// </summary>
        public void InsertVenda()
        {
                _cv.venda.Add(Venda);
                _cv.SaveChanges();
        }

        private TPREVENDA NewPrevenda()
        {
            TPREVENDA pre = new TPREVENDA() { CODCLIENTE = this.Client.CONTROLE, CLIENTE = this.Client.CLIENTE, CNPJCPF = ((this.Client.CPF == null) ? this.Client.CNPJ : this.Client.CPF), TCLIENTE = this.Client, DATAHORACADASTRO = Venda.DATA, VALORTOTAL = this.CalculaTotal() };
            return pre;
        }

        /// <summary>
        /// Método encarregado de inserir os dados na prevenda
        /// </summary>
        public void InsertPrevenda()
        {
            //criando os rotulos das prévendas
            TPREVENDA pl = new TPREVENDA() { CLIENTE = this.Client.CLIENTE, CNPJCPF = (this.Client.CPF == null)? this.Client.CNPJ: this.Client.CPF, TCLIENTE = this.Client, DATAHORACADASTRO = DateTime.Now} ; //_cl.TPREVENDA.ToList().Last();
            TPREVENDA ps = new TPREVENDA() { CLIENTE = this.Client.CLIENTE, CNPJCPF = (this.Client.CPF == null) ? this.Client.CNPJ : this.Client.CPF, TCLIENTE = this.Client, DATAHORACADASTRO = DateTime.Now };//_cl.TPREVENDA.ToList().Last();



            //Lista de itens da venda corrente.
            var lista = Venda.itens_de_venda;

            foreach (var item in lista)
            {
                CProduto produto = new CProduto(item.COD_BARRAS);
                
                decimal reservado_l = produto.QuantARealizarPrevenda("L");
                decimal reservado_s = produto.QuantARealizarPrevenda("S");
                decimal reservado = reservado_l + reservado_s;
                decimal restante = 0;

                //Verifica se o estoque geral é suficiente para venda. Inclusive verificando as reservas.        
                if ((produto.EstoqueGeral() - reservado) < item.QUANTIDADE)
                {

                    throw new Exception("Quantidade insuficiente para efetuar a venda do item " + produto.Descricao()+ ((reservado > 0)?(" Existem pré-vendas a serem reaizadas, verifique se as mesmas serão canceldas ou realizadas e tente novamente assim que liberar. Existem " + reservado + " unidades reservadas pendente de realização."):""));
                }
                
                //Verifica qual dos estoques está maior, neste caso a Laerton
                if ((produto.EstoqueL - reservado_l) > (produto.EstoqueS - reservado_s))
                {
                    //Estoque da laerton é insuficente para a venda, separa o restante para a SIL
                    if ((produto.EstoqueL - reservado_l) < item.QUANTIDADE)
                    {
                        restante = item.QUANTIDADE - (produto.EstoqueL - reservado_l);
                    }

                    prevenda pre = new prevenda() { VENDA_ID_VENDAS = GetId(), CONTROLE = (pl.CONTROLE + 1), LOJA_ID_LOJA = "l" };
                    _cv.prevenda.Add(pre);
                    _cv.SaveChanges();
                    break;
                }else if { }
            }
            foreach (var item in _lista)
            {
                if (item._nEstoqueS > 0)
                {
                    prevenda pre = new prevenda() { VENDA_ID_VENDAS = GetId(), CONTROLE = (ps.CONTROLE + 1), LOJA_ID_LOJA = "s" };
                    _cv.prevenda.Add(pre);
                    _cv.SaveChanges();
                    break;
                }
            }
        }
        
    }
}
