using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PersisteUnivendas.Classes
{
    public class Vendas
    {
        /// <summary>
        /// Busca uma lista de vendas de um determindado cliente
        /// </summary>
        /// <param name="id_cliente">id do cliente</param>
        /// <returns>Lista de vendas</returns>
        public List<venda> FindForClient(long id_cliente)
        {
            vendaEnt context = new vendaEnt();
            List<venda> result = context.vendas.Where(c => c.CLIENTE == id_cliente).ToList();
            return result;
        }
        /// <summary>
        /// Busca uma lista de vendas de um cliente a partir do id do cliente em uma respectiva data
        /// </summary>
        /// <param name="id_cliente">id do cliente</param>
        /// <param name="data">Data a ser referenciada </param>
        /// <returns>lista de vendas</returns>
        public List<venda> FindForClient (long id_cliente, DateTime data)
        {
            vendaEnt context = new vendaEnt();
            List<venda> result = context.vendas.Where(c => c.CLIENTE == id_cliente && c.DATA == data).ToList();
            return result;
        }
        /// <summary>
        /// Busca por vendas de um cliente em uma determinda faixa de datas
        /// </summary>
        /// <param name="id_cliente">id do cliente</param>
        /// <param name="inicio">Data inicio da venda</param>
        /// <param name="fim">Data final de vendas</param>
        /// <returns>Lista de venda</returns>
        public List<venda> FindForClient (long id_cliente, DateTime inicio, DateTime fim)
        {
            vendaEnt context = new vendaEnt();
            List<venda> result = context.vendas.Where(c => c.CLIENTE == id_cliente && (c.DATA >= inicio && c.DATA <=  fim)).ToList();
            return result;
        }

        /// <summary>
        /// Busca uma venda a pertir do id
        /// </summary>
        /// <param name="id_venda">Id da venda</param>
        /// <returns></returns>
        public venda FindForID (long id_venda)
        {
            vendaEnt context = new vendaEnt();
            venda result = context.vendas.Where(c => c.ID_VENDA == id_venda).First();
            return result;
        }
        /// <summary>
        /// Busca todas as vendas de um determinado usuário por um determinado periodo
        /// </summary>
        /// <param name="id_user">Id do usuário </param>
        /// <param name="inicio">data inicio da pesquisa</param>
        /// <param name="fim">Data final da pesquisa</param>
        /// <returns>Lista de vendas</returns>
        public List<venda> FindForUser (long id_user, DateTime inicio, DateTime fim)
        {
            vendaEnt context = new vendaEnt();
            List<venda> result = context.vendas.Where(c => c.USUARIO_ID_USUARIO == id_user && c.DATA >= inicio && c.DATA <= fim).ToList();
            return result; 
        }

        /// <summary>
        /// Busca todas as vendas em uma determinada data
        /// </summary>
        /// <param name="date">data para buscar as vendas</param>
        /// <returns>Lista de vendas</returns>
        public List<venda> FindforDate (DateTime date)
        {
            vendaEnt context = new vendaEnt();
            List<venda> result = context.vendas.Where(c => c.DATA == date).ToList();
            return result;
        }

        /// <summary>
        /// Busca todas as vendas em um determinado periodo
        /// </summary>
        /// <param name="inicio">Data do inicio do peridodo</param>
        /// <param name="fim">Data final do pedirodo</param>
        /// <returns>Lista de vendas</returns>
        public  List<venda> FindFordate(DateTime inicio, DateTime fim)
        {
            vendaEnt context = new vendaEnt();
            List<venda> result = context.vendas.Where(c => c.DATA >= inicio && c.DATA <= fim).ToList();
            return result;
        }

        /// <summary>
        /// Busca vendas que contenham um determinado produto a partir do EAN do mesmo
        /// </summary>
        /// <param name="EAN">Codigo de barras do produto</param>
        /// <returns>Lista de vendas localizadas</returns>
        public List<venda> FindForProduct( String EAN)
        {
            /*var query = from e in context.vendas
                        where e.itens_de_venda.Where(p => p.COD_BARRAS == EAN).Count() > 0
                        select e;
                        */

            vendaEnt context = new vendaEnt();
            List<venda> result = context.vendas.Where(p => p.itens_de_venda.Where(c => c.COD_BARRAS == EAN).Count() >0).ToList();
            return result;
        }

        /// <summary>
        /// Busca por uma venda em um determinado produto por um periodo informado
        /// </summary>
        /// <param name="EAN">EAN do produto</param>
        /// <param name="inicio">Data de inicio da busca</param>
        /// <param name="fim">Data final da busca</param>
        /// <returns>Lista de vendas </returns>
        public List<venda> FindForProduct(String EAN, DateTime inicio, DateTime fim)
        {
            vendaEnt context = new vendaEnt();
            List<venda> result = context.vendas.Where(p =>
                                 p.itens_de_venda.Where(c => c.COD_BARRAS == EAN).Count() > 0
                                 && (p.DATA >= inicio && p.DATA <= fim)).ToList();
            return result;
            
        }

        /// <summary>
        /// Busca todas as vendas que ainda não foram realizadas 
        /// </summary>
        /// <returns>Lista de vendas</returns>
        public List<venda> FindNotRelise()
        {
            vendaEnt context = new vendaEnt();
            List<venda> result = context.vendas.Where(p => p.prevendas.Where(c => c.REALIZADO == false).Count() > 0).ToList();
            return result;
        }

        /// <summary>
        /// Busca todas as vendas não realizadas de um determinado produto a partir do EAN repassado
        /// </summary>
        /// <param name="EAN">EAN do produto</param>
        /// <returns>Lista de produtos</returns>
        public List<venda> FindNotRelise(String EAN)
        {
            vendaEnt context = new vendaEnt();
            List<venda> result = context.vendas.Where(p => p.prevendas.Where(c => c.REALIZADO == false).Count() > 0 && 
                                 p.itens_de_venda.Where(r => r.COD_BARRAS == EAN).Count() > 0).ToList();
            return result;
        }




    }
}
