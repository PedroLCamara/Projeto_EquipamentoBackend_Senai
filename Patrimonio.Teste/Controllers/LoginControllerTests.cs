using Microsoft.AspNetCore.Mvc;
using Moq;
using Patrimonio.Controllers;
using Patrimonio.Domains;
using Patrimonio.Interfaces;
using Patrimonio.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Patrimonio.Teste.Controllers
{
    public class LoginControllerTests
    {
        [Fact] //Descricao
        public void Deve_Retornar_Usuario_Invalido()
        {
            //Pre-condicao - Arrange
            var fakeRepo = new Mock<IUsuarioRepository>();
            fakeRepo.Setup(x => x.Login(It.IsAny<string>(), It.IsAny<string>()))
            .Returns((Usuario)null);

            var fakeViewModel = new LoginViewModel();
            fakeViewModel.Email = "junin@email.com";
            fakeViewModel.Senha = "BalaTensaDoJunin";

            var controller = new LoginController(fakeRepo.Object);

            //Procedimento - Act
            var resultado = controller.Login(fakeViewModel);

            //Resultado esperado - Assert
            Assert.IsType<UnauthorizedObjectResult>(resultado);
        }

        [Fact] //Descricao
        public void Deve_Retornar_Usuario_Valido()
        {
            //Pre-condicao - Arrange
            Usuario fakeUsuario = new Usuario();
            fakeUsuario.Email = "junia@email.com";
            fakeUsuario.Senha = "BalaDensaDaJunia";

            var fakeRepo = new Mock<IUsuarioRepository>();
            fakeRepo.Setup(x => x.Login(It.IsAny<string>(), It.IsAny<string>()))
            .Returns(fakeUsuario);

            var fakeViewModel = new LoginViewModel();
            fakeViewModel.Email = "junin@email.com";
            fakeViewModel.Senha = "BalaTensaDoJunin";

            var controller = new LoginController(fakeRepo.Object);

            //Procedimento - Act
            var resultado = controller.Login(fakeViewModel);

            //Resultado esperado - Assert
            Assert.IsType<OkObjectResult>(resultado);
        }
    }
}
