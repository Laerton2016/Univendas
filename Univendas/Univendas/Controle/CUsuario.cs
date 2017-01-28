using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Univendas.Model;

namespace Univendas.Controle
{
    public enum TipoUser
    {
        ADMINISTRADOR = 1, VENDEDOR = 2, CAIXA = 3, CONSULTA = 4
    }
    /// <summary>
    /// Classe encarregada de cuidar das regras de negócio de um Usuário.
    /// <Auto>Daniel Rodrigues Coura</Auto>
    /// <_data>04/01/2017</_data>
    /// </summary>
    class CUsuario
    {
        private Context_Venda _context = new Context_Venda();
        private string _senha;
        public usuario _id { get; private set; }

        /// <summary>
        /// Usuario cuida das regras de negócio que envolve os usuários dos bancos de dados.
        /// </summary>
        /// <param name="login">Login obrigatório para que o sistema possa fazer a busca nos bancos de dados.</param>
        /// <param name="senha">Senha obrigatória para que o sistema possa confirmar a identidade do usuário.</param>
        public CUsuario(string login, string senha)
        {
            _id = _context.usuario.Where(c => c.LOGIN == login).First();
            _senha = _id.SENHA;

            if (_id == null) { throw new Exception("Usuário inexistente."); }
            if (_senha != senha) { throw new Exception("Senha não corresponde."); }
        }

        /// <summary>
        /// Método retorna o tipo de usuário
        /// </summary>
        /// <returns>Inteiro que identifica o tipo de usuário</returns>
        public int GetTipo()
        {
            return _id.TIPO;
        }

        public void UpdateUser()
        {

        }

        public void NewUsuario(usuario novo)
        {
            if (criador.TIPO != (int) TipoUser.ADMINISTRADOR)
            {
                throw new Exception("Usário sem privilégio para criar um novo usuário.");
            }
           _context.usuario.Add(novo);
           _context.SaveChanges();
            
        }
    }
}
