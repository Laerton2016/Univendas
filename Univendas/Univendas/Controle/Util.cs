using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Univendas.Controle
{
    /// <summary>
    /// Classe que contém métodos úteis para uso no projeto
    /// <Autor>Laerton Marques de Figueiredo</Autor>
    /// <_data>01/01/2016</_data>
    /// </summary>                                      
    public class Util
    {
        /// <summary>
        /// Método que calcula o dígito verificador do EAN repassado
        /// </summary>
        /// <param name="EAN">Código EAN a ter o dígito calculado, deve conter 12 dígitos.</param>
        /// <returns>Dígito verificador</returns>
        public static Int32 CalculaDigitoEAN (String EAN)
        {
            if (EAN.Length != 12) { throw new Exception("EAN deve conter 12 digitos!"); }
           
            string sTemp = EAN;
            int iSum = 0;
            int iDigit = 0;

            // Calcula o digito verificador do Código EAN repassado
            for (int i = 0; i <= EAN.Length -1; i++)
            {
                iDigit = Convert.ToInt32(sTemp.Substring(i, 1));
                if ((i+1) % 2 == 0)
                {   // odd
                    iSum += iDigit * 3;
                }
                else
                {   // even
                    iSum += iDigit * 1;
                }
            }

            int iCheckSum = (10 - (iSum % 10)) % 10;
            return iCheckSum;
        }
        /// <summary>
        /// Método que verifica se o dígito verificador do Código EAN está correto
        /// </summary>
        /// <param name="EAN">Código EAN a ser verificado deve conter 13 dígitos</param>
        /// <returns>Booleando de confirmação </returns>
        public static  bool VerificaDigEAN(String EAN)
        {
            if (EAN.Length != 13) { throw new Exception("O código EAN deve conter 13 dígitos já com o verificador"); }

            int iDigit = Convert.ToInt32(EAN.Substring(EAN.Length - 1, 1));
            //String VEAN = EAN.Substring(EAN.Length - 1, 11);
            String VEAN = esquerda(EAN, 11);
            return (iDigit == CalculaDigitoEAN(VEAN));
        }


        /// <summary>
        /// Metodo verifica se na String repassada como argumento contém algum caracter simbolico.
        /// </summary>
        /// <param name="dado">String para a analise.</param>
        /// <returns>Booleano de retorno.</returns>
        public static Boolean ContemPontuacao(string dado)
        {
            if (dado.Where(c => char.IsPunctuation(c)).Count() > 0)
            {
                return true;
            }
            return false;
        }

        /// <summary>
        /// Método verifica se a string recebida contem algum número.
        /// </summary>
        /// <param name="dado">String para analise</param>
        /// <returns>Bolean de retorno</returns>
        public static Boolean ContemNumeros(String dado)
        {
            if (dado.Where(c => char.IsNumber(c)).Count() > 0)
            {
                return true;
            }
            return false;
        }
        /// <summary>
        /// Método verifica se a string recebida como atributo de analise contém somente carcateres alfabeticos.
        /// </summary>
        /// <param name="dados">String para analiese</param>
        /// <returns>Boolean de retorno</returns>
        public static Boolean Soletras(string dados)
        {
            if ((dados.Where(c => char.IsLetter(c)).Count() > 0) && (dados.Where(c => char.IsNumber(c)).Count() == 0))
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        /// <summary>
        /// Método verifica se os dados contem somente números
        /// </summary>
        /// <param name="dados">String contendo dados para analise</param>
        /// <returns>Retorna um Boolean</returns>
        public static bool Sonumeros(string dados)
        {
            if ((dados.Where(c => char.IsLetter(c)).Count() == 0) && (dados.Where(c => char.IsNumber(c)).Count() > 0))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// Método para validar o CPF
        /// </summary>
        /// <param name="doc">String contendo o CPF</param>
        /// <returns>Retorna boolean</returns>
        public static Boolean validaCPF(String doc)
        {
            String dado = doc.Trim();
            dado = dado.Replace(".", "");
            dado = dado.Replace("-", "");
            switch (dado)
            {
                case "00000000000":
                    return false;
                case "11111111111":
                    return false;
                case "22222222222":
                    return false;
                case "33333333333":
                    return false;
                case "44444444444":
                    return false;
                case "55555555555":
                    return false;
                case "66666666666":
                    return false;
                case "77777777777":
                    return false;
                case "88888888888":
                    return false;
                case "99999999999":
                    return false;

            }
            return modulo11CPF(dado);
        }

        /// <summary>
        /// Método que valida o CPNJ
        /// </summary>
        /// <param name="doc">String contendo o cnpj</param>
        /// <returns>Retorna um boolean</returns>
        public static Boolean validaCNPJ(String doc)
        {
            String dado = doc.Trim();
            dado = dado.Replace(".", "");
            dado = dado.Replace("-", "");
            dado = dado.Replace("/", "");
            return modulo11CNPJ(dado);

        }

        private static Boolean modulo11CNPJ(String dado)
        {
            if (dado.Length < 14)
            {
                return false;
            }
            int soma = 0, digito = 0, contar = 0, multiplica = 6;
            String DV;
            String digitos = dado;

            foreach (var item in dado)
            {
                digito = Convert.ToInt16(Convert.ToString(item));
                soma += (multiplica * digito);
                contar++;

                multiplica = (multiplica == 9) ? 2 : multiplica + 1;

                if (contar > dado.Length - 3) { break; }
            }

            DV = ((soma % 11) >= 10) ? "0" : Convert.ToString(soma % 11);

            soma = 0;
            contar = 0;
            multiplica = 5;

            foreach (var item in dado)
            {
                digito = Convert.ToInt16(Convert.ToString(item));
                soma += (multiplica * digito);
                contar++;
                multiplica = (multiplica == 9) ? 2 : multiplica + 1;
                if (contar > dado.Length - 2) { break; }
            }

            DV += ((soma % 11) >= 10) ? "0" : Convert.ToString(soma % 11);
            String DVOriginal = Convert.ToString(digitos[(digitos.Length - 2)]) + Convert.ToString(digitos[(digitos.Length - 1)]);

            return DV.Equals(DVOriginal);
        }
        private static Boolean modulo11CPF(String dado)
        {
            int soma = 0, digito = 0, contar = 1;
            String DV;
            String digitos = dado.Replace("/", "");

            foreach (var item in digitos)
            {

                digito = Convert.ToInt16(Convert.ToString(item));
                soma += (contar * digito);
                contar++;
                if (contar > dado.Length - 2) { break; }
            }

            DV = ((soma % 11) >= 10) ? "0" : Convert.ToString(soma % 11);

            soma = 0;
            contar = 0;

            foreach (var item in digitos)
            {
                digito = Convert.ToInt16(Convert.ToString(item));
                soma += (contar * digito);
                contar++;
                if (contar > dado.Length - 2) { break; }
            }

            DV += ((soma % 11) >= 10) ? "0" : Convert.ToString(soma % 11);
            String DVOriginal = Convert.ToString(digitos[(digitos.Length - 2)]) + Convert.ToString(digitos[(digitos.Length - 1)]);

            return DV.Equals(DVOriginal);
        }
        

        /// <summary>
        /// Método verifica se é um e-mail
        /// </summary>
        /// <param name="email">String contendo o e-mail</param>
        /// <returns>Retorna um Boolean</returns>
        public static bool ValidaEmail(string email)
        {
            return email.Contains('@');

        }

        /// <summary>
        /// Metodo Recebe uma string com o tamnho dos caracteres limite que precisa a direita.
        /// </summary>
        /// <param name="campo">String que deve ser analizada</param>
        /// <param name="posicao">Posição final da String</param>
        /// <returns>String novo formato</returns>

        public static String direita(String campo, Int16 posicao)
        {
            if (posicao < 0)
            {
                return null;
            }
            String retorno = null;

            Int16 indice = Convert.ToInt16(campo.Length);
            indice--;

            Int16 loca = indice;

            loca -= (posicao);

            for (int i = loca; i <= indice; i++)
            {
                retorno += campo[i];
            }
            return retorno;
        }

        /// <summary>
        /// Metodo Recebe uma string com o tamnho dos caracteres limite que precisa a esquerda.
        /// </summary>
        /// <param name="campo">String que deve ser analizada</param>
        /// <param name="posicao">Posição final da String</param>
        /// <returns>String novo formato</returns>


        public static String esquerda(String campo, Int16 posicao)
        {

            String retorno = null;
            String[] grupo = campo.Split();
            /**
            if (posicao >= grupo.Length)
            {
                return null;
            }
            **/
            for (int i = 0; i <= posicao; i++)
            {
                retorno += campo[i];
            }
            return retorno;
        }
        /// <summary>
        /// Método para verificação de inscrição instadual 
        /// </summary>
        /// <param name="IE">Inscrição estadual a ser verificada</param>
        /// <returns></returns>
        private static Boolean IEPB(String IE)
        {
            if (!(IE.Contains("-")))
            {

                return false;
            }
            Int32 Soma = 0, peso = 9, digito = 0, dv1 = 0;
            String IEParcial = IE.Replace(".", "").Trim();
            String DV = IEParcial.Split('-')[1];
            String Dverificador = "";
            IEParcial = IEParcial.Split('-')[0];
            foreach (var item in IEParcial)
            {

                digito = Convert.ToInt16(Convert.ToString(item));
                Soma += (digito * peso);
                peso--;
            }
            dv1 = 11 - (Soma % 11);
            Dverificador = (dv1 >= 10) ? "0" : Convert.ToString(dv1);
            return DV.Equals(Dverificador);
        }
        /// <summary>
        /// Método valida uma Inscrição estadual
        /// </summary>
        /// <param name="IE">String contendo a IE</param>
        /// <returns>Retorna boolean</returns>

        public Boolean ValidaIE(String IE, String UF)
        {
            switch (UF)
            {
                case ("PB"):
                    return IEPB(IE);
                case ("AC"):
                    return IEPB(IE);
                default:
                    break;
            }


            return true;
        }

        public static String aplicaMascara(String dado, String mascara)
        {
            String retorno = "";
            int j = 0;
            for (int i = 0; i < mascara.Length; i++)
            {
                if (mascara[i].Equals('9'))
                {
                    retorno += dado[j];
                    j++;
                }
                else if (mascara[i].Equals('.') || mascara[i].Equals('-') || mascara[i].Equals('/') || mascara[i].Equals(","))
                {
                    retorno += mascara[i];
                }
                else
                {
                    throw new ArgumentException("Mascara de entrada inválida.");
                }

            }
            return retorno;
        }


        /// <summary>
        /// Método que verifica os dados e cria uma mascara especifica para ele
        /// </summary>
        /// <param name="dado"></param>
        /// <returns>Uma mascara de acordo com o dado, como padrão segue a de telefone</returns>
        public static string criaMascara(string dado)
        {
            return criaMascara(dado, "PB");
        }


        public static string criaMascara(string dado, String UF)
        {
            string mascara = "";
            string informa = dado.Replace(" ", "");
            informa = informa.Replace(".", "");
            informa = informa.Replace("-", "");
            informa = informa.Replace("/", "");
            informa = informa.Replace("(", "");
            informa = informa.Replace(")", "");

            if (validaCPF(informa))
            {
                mascara = "999.999.999-99";
            }
            else if (validaCNPJ(informa))
            {
                mascara = "99.999.999/9999-99";
            }
            else
            {

                mascara = "(99)9999-9999";

            }
            return mascara;
        }

    }

}
