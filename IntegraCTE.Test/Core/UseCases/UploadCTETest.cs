using AutoMapper;
using Bogus;
using IntegraCTE.Core.DTO;
using IntegraCTE.Core.MapProfiles;
using IntegraCTE.Core.Model;
using IntegraCTE.Core.Repository;
using IntegraCTE.Core.UseCases;
using Microsoft.Extensions.Configuration;
using Moq;
using Moq.AutoMock;

namespace IntegraCTE.Test.Core.UseCases
{
    public class UploadCTETest
    {
        private readonly Faker _faker = new();
        private readonly AutoMocker _mocker = new();
        private readonly IMapper _mapper;

        public UploadCTETest()
        {
            _mapper = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<ArquivoProfile>();
            }).CreateMapper();
        }

        [Fact()]
        public void Test1()
        {
            var model = new ArquivoModel(id: Guid.NewGuid(), xML: "HAHAs", dataArquivo: DateTime.Now, "CNX");
            var dto = new ArquivoDTO("HAHA");

            // Mock Mapper
            _mocker.GetMock<IMapper>().Setup(m => m.Map<ArquivoModel>(dto)).Returns(model);
            // Mock Repository
            _mocker.GetMock<IIntegraCTERepository>().Setup(s => s.Adicionar(model));
            _mocker.GetMock<IIntegraCTERepository>().Setup(s => s.SaveChangesAsync()).Returns(Task.FromResult(1));

            // Mock IConfiguration
            _mocker.GetMock<IConfiguration>().Setup(m => m.GetSection("ERPService:SiglaEmpresa").Value).Returns("");

            // Create Instance UC
            var useCase = _mocker.CreateInstance<UploadCTE>();

            useCase.Execute(dto).Wait();

            _mocker.Verify<IIntegraCTERepository>(s => s.Adicionar(model), Times.Once);
        }
    }
}
