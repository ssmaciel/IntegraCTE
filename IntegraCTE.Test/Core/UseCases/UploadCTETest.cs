using AutoMapper;
using Bogus;
using IntegraCTE.Core.DTO;
using IntegraCTE.Core.Model;
using IntegraCTE.Core.Repository;
using IntegraCTE.Core.UseCases;
using Moq;
using Moq.AutoMock;

namespace IntegraCTE.Test.Core.UseCases
{
    public class UploadCTETest
    {
        private readonly Faker _faker = new();
        private readonly AutoMocker _mocker = new();
        private readonly IMapper _mapper;
        [Fact(DisplayName = "Irá retornar uma mensagem quando não houver planos com a espécie do bem e 30% da renda informada")]
        public void Test1()
        {
            var model = new CTEModel() { Id = Guid.NewGuid(), XML = "HAHAs" };
            var dto = new ArquivoDTO("HAHA");

            _mocker.GetMock<IIntegraCTERepository>().Setup(s => s.AdicionarXML(model));
            _mocker.GetMock<IIntegraCTERepository>().Setup(s => s.SaveChangesAsync()).Returns(Task.FromResult(1));

            var useCase = _mocker.CreateInstance<UploadCTE>();

            useCase.Execute(dto).Wait();

            _mocker.Verify<IIntegraCTERepository>(s => s.SaveChangesAsync(), Times.Exactly(1));
        }
    }
}
