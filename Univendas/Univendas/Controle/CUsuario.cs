using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Univendas.CException;
using Univendas.Model;
using PersisteUnivendas;


namespace Univendas.Controle
{
    /// <summary>
    /// Enum para classificar o tipo de usuário.
    /// <Autor>Laerton Marques de Figueiredo</Autor>
    /// <Data>29/01/2017</Data>
    /// </summary>
    public enum TipoUser
    {
        ADMINISTRADOR=1, CAIXA=2, VENDEDOR=3, CONSULTA=4
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

        /// <summary>
        /// Usuário que está atualmente controlando o controler de Usuário
        /// </summary>
        public usuario User { get; set; }

        /// <summary>
        /// Usuario cuida das regras de negócio que envolve os usuários dos bancos de dados.
        /// </summary>
        /// <param name="login">Login obrigatório para que o sistema possa fazer a busca nos bancos de dados.</param>
        /// <param name="senha">Senha obrigatória para que o sistema possa confirmar a identidade do usuário.</param>
        public CUsuario(string login, string senha)
        {
            User = _context.usuario.Where(c => c.LOGIN == login).First();
            _senha = User.SENHA;
            if (User == null) { throw new UserException("Usuário inexistente."); }
            if (_senha != senha) { throw new UserException("Senha não corresponde."); }
        }

        /// <summary>
        /// Método retorna o tipo de usuário
        /// </summary>
        /// <returns>Inteiro que identifica o tipo de usuário</returns>
        public TipoUser GetTipo()
        {
            return (TipoUser)User.TIPO;
        }
        /// <summary>
        /// Método busca por um usuário na base de dados a partir do ID, disponível somente para usuário do tipo administrador
        /// </summary>
        /// <param name="id">Id do usuário, inteiro positivo maior que zero.</param>
        /// <returns>Usuário localizado</returns>
        public usuario FindUser(Int16 id)
        {
            
            if (!IsAdmin())
            {
                throw new UserException("Usuário atual não tem permissão para criação de um novo usuário.");
            }
            if (id <= 0)
            {
                throw new UserException("Id Usuário deve ser inteiro positivo maior que zero.");
            }

            usuario user = _context.usuario.Where(c => c.ID_USUARIO == id).First();
            if (user == null)
            {
                throw new UserException("Usuário não localizado.");
            }
            return user;
        }

        /// <summary>
        /// Cuida da criação de um novo usuário sendo somente permitido a criação por um usuário do tipo Administrador
        /// </summary>
        public usuario NewUser ()
        {
            if (!IsAdmin())
            {
                throw new UserException("Usuário atual não tem permissão para criação de um novo usuário.");
            }
            return new usuario();
        }
        /// <summary>
        /// Método adiciona um novo usuário ao sistema, permitido somente a usuários do tipo admisnitrador
        /// </summary>
        /// <param name="user">Usuário a ser adicnionando ao sistema</param>
        public void SaveOtherUser(usuario user)
        {
            if (!IsAdmin())
            {
                throw new UserException("Usuário atual não tem permissão para criação de um novo usuário.");
            }
            _context.usuario.Add(user);
            _context.SaveChanges();
        }

        /// <summary>
        /// Remove um usuário do sistema, permitido somente para usuários do tipo adminsitrador
        /// </summary>
        /// <param name="user">Usuários a ser removido do sistema</param>
        public void RemoveOtherUser(usuario user)
        {
            if (!IsAdmin())
            {
                throw new UserException("Usuário atual não tem permissão para criação de um novo usuário.");
            }
            _context.usuario.Remove(user);
            _context.SaveChanges();
        }

        /// <summary>
        /// Salva as alterações feitas no usuário atual do controler.
        /// </summary>
        public void Save()
        {
            _context.usuario.Add(this.User);
            _context.SaveChanges();
        }

        /// <summary>
        /// Valida se o usuário controlador é do tipo administrador
        /// </summary>
        /// <returns>Booleano de confirmação</returns>
        private bool IsAdmin()
        {
            return (this.GetTipo() == TipoUser.ADMINISTRADOR);
        }

        public void BloqueaUsuario(usuario user)
        {
            if (!this.IsAdmin())
            {
                throw new Exception("Usuário sem permissão para esta ação!");
            }
            
        }



        
    }
}
