using Microsoft.AspNetCore.Mvc;
using Moq;
using Patrimonio.Controllers;
using Patrimonio.Domains;
using Patrimonio.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Patrimonio.Teste.Controllers
{
    public class EquipamentoControllerTests
    {
        [Fact] //Descricao
        public void Deve_Retornar_Equipamento()
        {

            //Pre-condicao - Arrange
            Equipamento fakeEquipamento = new Equipamento();
            fakeEquipamento.Id = 11;
            
            var fakeRepo = new Mock<IEquipamentoRepository>();
            fakeRepo.Setup(x => x.BuscarPorID(It.IsAny<int>()))
            .Returns(fakeEquipamento);

            var controller = new EquipamentosController(fakeRepo.Object);

            //Procedimento - Act
            var resultado = controller.GetEquipamento(fakeEquipamento.Id);

            //Resultado esperado - Assert
            Assert.IsType<OkObjectResult>(resultado);
        }

        [Fact] //Descricao
        public void Deve_Retornar_Equipamento_Nao_Encontrado()
        {

            //Pre-condicao - Arrange
            Equipamento fakeEquipamento = new Equipamento();
            fakeEquipamento.Id = 11;

            var fakeRepo = new Mock<IEquipamentoRepository>();
            fakeRepo.Setup(x => x.BuscarPorID(It.IsAny<int>()))
            .Returns((Equipamento)null);

            var controller = new EquipamentosController(fakeRepo.Object);

            //Procedimento - Act
            var resultado = controller.GetEquipamento(fakeEquipamento.Id);

            //Resultado esperado - Assert
            Assert.IsType<NotFoundObjectResult>(resultado);
        }

        [Fact] //Descricao
        public void Deve_Retornar_Lista_Equipamentos()
        {
            //Pre-condicao - Arrange
            List<Equipamento> fakeListaEquipamento = new List<Equipamento>();

            var fakeRepo = new Mock<IEquipamentoRepository>();
            fakeRepo.Setup(x => x.Listar())
            .Returns(fakeListaEquipamento);

            var controller = new EquipamentosController(fakeRepo.Object);

            //Procedimento - Act
            var resultado = controller.GetEquipamentos();

            //Resultado esperado - Assert
            Assert.IsType<OkObjectResult>(resultado);
        }

        [Fact] //Descricao
        public void Deve_Retornar_Equipamento_Atualizado()
        {
            //Pre-condicao - Arrange
            Equipamento fakeEquipamento = new Equipamento();
            int idEquipamento = 1;
            fakeEquipamento.Id = idEquipamento;

            var fakeRepo = new Mock<IEquipamentoRepository>();
            fakeRepo.Setup(x => x.Alterar(It.IsAny<Equipamento>()))
            .Returns(fakeEquipamento);

            var controller = new EquipamentosController(fakeRepo.Object);

            //Procedimento - Act
            var resultado = controller.PutEquipamento(idEquipamento, fakeEquipamento);

            //Resultado esperado - Assert
            Assert.IsType<NoContentResult>(resultado);
        }

        [Fact] //Descricao
        public void Deve_Retornar_Ids_Incondizentes()
        {
            //Pre-condicao - Arrange
            Equipamento fakeEquipamento = new Equipamento();
            int idEquipamento = 1;
            fakeEquipamento.Id = 2;

            var fakeRepo = new Mock<IEquipamentoRepository>();
            fakeRepo.Setup(x => x.Alterar(It.IsAny<Equipamento>()))
            .Returns(fakeEquipamento);

            var controller = new EquipamentosController(fakeRepo.Object);

            //Procedimento - Act
            var resultado = controller.PutEquipamento(idEquipamento, fakeEquipamento);

            //Resultado esperado - Assert
            Assert.IsType<BadRequestObjectResult>(resultado);
        }

        [Fact] //Descrição
        public void Deve_Retornar_Equipamento_Deletar_Nao_Encontrado()
        {
            //Pre-condicao - Arrange
            int idEquipamentoDeletado = 1;

            var fakeRepo = new Mock<IEquipamentoRepository>();
            fakeRepo.Setup(x => x.BuscarPorID(It.IsAny<int>()))
                .Returns((Equipamento)null);

            var controller = new EquipamentosController(fakeRepo.Object);

            //Procedimento - Act
            var resultado = controller.DeleteEquipamento(idEquipamentoDeletado);

            //Resultado esperado - Assert
            Assert.IsType<NotFoundObjectResult>(resultado);
        }

        /// <summary>
        /// Comentado por conta da imagem
        /// </summary>
        /*
        [Fact] //Descrição
        public void Deve_Retornar_Equipamento_Deletado()
        {
            //Pre-condicao - Arrange
            Equipamento fakeEquipamento = new Equipamento();
            fakeEquipamento.Id = 1;
            fakeEquipamento.Imagem = "LETTUCE";

            var fakeRepo = new Mock<IEquipamentoRepository>();
            fakeRepo.Setup(x => x.BuscarPorID(It.IsAny<int>()))
            .Returns(fakeEquipamento);

            var controller = new EquipamentosController(fakeRepo.Object);

            //Procedimento - Act
            var resultado = controller.DeleteEquipamento(fakeEquipamento.Id);

            //Resultado esperado - Assert
            Assert.IsType<NoContentResult>(resultado);
        }
        */
    }
}