using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Univendas.Controle
{
    /// <summary>
    /// Classe encarregada de cuidar das regras de negócio de um Usuário.
    /// <Auto>Daniel Rodrigues Coura</Auto>
    /// <Data>04/01/2017</Data>
    /// </summary>
    class Usuario
    {
        //private Context_User _CTU = new Context_User();
        private string _senha;
        public string _id { get; private set; }

        /// <summary>
        /// CUsuario cuida das regras de negócio que envolve os usuários dos bancos de dados.
        /// </summary>
        /// <param name="login">Login obrigatório para que o sistema possa fazer a busca nos bancos de dados.</param>
        /// <param name="senha">Senha obrigatória para que o sistema possa confirmar a identidade do usuário.</param>
        public Usuario(string login, string senha)
        {
            //_id = _CTU.USUARIO.Where(u => u.LOGIN == login).First<USUARIO>();
            //_senha = _id.SENHA;

            if (_id == null) { throw new Exception("Usuário inexistente."); }
            if (_senha != senha) { throw new Exception("Senha não corresponde."); }
        }

        /// <summary>
        /// Método retorna o tipo de usuário
        /// </summary>
        /// <returns>Inteiro que identifica o tipo de usuário</returns>
        /*
        public int GetTipo()
        {
            return _id.TIPO();
        }
        */


    }
}
