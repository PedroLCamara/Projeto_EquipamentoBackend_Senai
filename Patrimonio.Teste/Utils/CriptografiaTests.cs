using Patrimonio.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Xunit;

namespace Patrimonio.Teste.Utils
{
    public class CriptografiaTests
    {
        [Fact] //Descricao
        public void Deve_Retornar_Hash_Em_BCrypt()
        {
            //Pre-condicao - Arrange
            var senha = Criptografia.GerarHash("123456789");
            var regex = new Regex(@"^\$2[ayb]\$.{56}$");

            //Procedimento - Act
            var retorno = regex.IsMatch(senha);

            //Resultado esperado - Assert
            Assert.True(retorno);
        }

        [Fact] //Descricao
        public void Deve_Retornar_Comparacao_Valida()
        {
            //Pre-condicao
            var senha = "123456789";
            var hashBanco = "$2a$11$rE65HmNdOy5xPwuOWrL.o.HaLksuW8od0myjGuC4m//JOwch9So0i";

            //Procedimento
            var comparacao = Criptografia.Comparar(senha, hashBanco);

            //Resutado esperado
            Assert.True(comparacao);
        }
    }
}