using AutoMapper;
using INVCAPP.API.Controllers;
using INVCAPP.Core.DTOs;
using INVCAPP.Core.Services;
using INVCAPP.Service.Mapping;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace INVCAPP.ApiTest
{
    public class InvoiceControllerTests
    {
        private readonly Mock<IInvoiceService> _invoiceServiceMock;
        private readonly IMapper _mapper;
        private readonly InvoiceController _invoiceController;

        public InvoiceControllerTests()
        {
            _invoiceServiceMock = new Mock<IInvoiceService>();
            _mapper = new MapperConfiguration(cfg => cfg.AddProfile<MapProfile>()).CreateMapper();
            _invoiceController = new InvoiceController(_invoiceServiceMock.Object);
        }

        [Fact]
        public async Task Create_Invoice_Created()
        {
            //Arrange
            var invoiceCreateDto = new InvoiceCreateDto
            {
                InvoiceHeader = new InvoiceHeaderCreateDto
                {
                    InvoiceId = "SVS202300000001",
                    SenderTitle = "Gönderici Şirket",
                    ReceiverTitle = "Alıcı Şirket",
                    Date = "2023-12-21",
                    Email = "ornek@email.com"
                },
                InvoiceLines = new List<InvoiceLineCreateDto>
                {
                    new InvoiceLineCreateDto
                    {
                        Name = "Ürün 1",
                        Quantity = 2,
                        UnitCode = "ADET",
                        UnitPrice = 50.00m
                    },
                    new InvoiceLineCreateDto
                    {
                        Name = "Ürün 2",
                        Quantity = 3,
                        UnitCode = "ADET",
                        UnitPrice = 30.00m
                    }
                }
            };

            _invoiceServiceMock.Setup(x => x.AddInvoiceAsync(It.IsAny<InvoiceCreateDto>()))
                .Returns(Task.FromResult(FakeCreateMethod(invoiceCreateDto)));

            //Act
            var actionResult = await _invoiceController.AddInvoice(invoiceCreateDto);
            //Assert
            var objectResult = (ObjectResult)actionResult;
            Assert.Equal(objectResult.StatusCode, (int)System.Net.HttpStatusCode.Created);
        }

        [Fact]
        public async Task Create_Invoice_Created_Not_Ok()
        {
            //Arrange
            var invoiceCreateDto = new InvoiceCreateDto
            {
                InvoiceHeader = new InvoiceHeaderCreateDto
                {
                    InvoiceId = "SVS202300000001",
                    SenderTitle = "Gönderici Şirket",
                    ReceiverTitle = "Alıcı Şirket",
                    Date = "2023-12-21",
                    Email = ""
                },
                InvoiceLines = new List<InvoiceLineCreateDto>
                {
                    new InvoiceLineCreateDto
                    {
                        Name = "Ürün 1",
                        Quantity = 2,
                        UnitCode = "ADET",
                        UnitPrice = 50.00m
                    },
                    new InvoiceLineCreateDto
                    {
                        Name = "Ürün 2",
                        Quantity = 3,
                        UnitCode = "ADET",
                        UnitPrice = 30.00m
                    }
                }
            };

            _invoiceServiceMock.Setup(x => x.AddInvoiceAsync(It.IsAny<InvoiceCreateDto>()))
                .Returns(Task.FromResult(FakeCreateMethod(invoiceCreateDto)));

            //Act
            var actionResult = await _invoiceController.AddInvoice(invoiceCreateDto);
            //Assert
            var objectResult = (ObjectResult)actionResult;
            Assert.Equal(objectResult.StatusCode, (int)System.Net.HttpStatusCode.BadRequest);

        }
        
        [Fact]
        public async Task Get_All_InvoiceHeaders_OK()
        {
            //Arrange
            _invoiceServiceMock.Setup(x => x.GetAllInvoiceHeadersAsync())
                .Returns(Task.FromResult(GetAllInvoiceHeadersFake()));

            //Act
            var actionResult = await _invoiceController.GetAllInvoiceHeaders();

            //Assert
            var objectResult = (ObjectResult)actionResult;

            Assert.Equal(objectResult.StatusCode, (int)System.Net.HttpStatusCode.OK);
            Assert.IsType<CustomResponseDto<List<InvoiceHeaderDto>>>(objectResult.Value);
        }

        [Fact]
        public async Task Get_InvoiceDetails_OK()
        {
            //Arrange
            var fakeId = "SVS202300000001";

            _invoiceServiceMock.Setup(x => x.GetInvoiceDetailsAsync(It.IsAny<string>()))
                .Returns(Task.FromResult(GetDetailByInvoiceIdFake(fakeId)));
            //Act
            var actionResult = await _invoiceController.GetInvoiceDetails(fakeId);
            //Assert
            var objectResult = (ObjectResult)actionResult;
            var response = (CustomResponseDto<InvoiceDto>)objectResult.Value;

            Assert.Equal(objectResult.StatusCode, (int)System.Net.HttpStatusCode.OK);
            Assert.NotNull(response);
            Assert.NotNull(response.Data);
            Assert.Equal(response.Data.InvoiceHeader.InvoiceId, fakeId);
        }
        [Fact]
        public async Task Get_InvoiceDetails_Not_OK()
        {
            //Arrange
            var fakeId = "";

            _invoiceServiceMock.Setup(x => x.GetInvoiceDetailsAsync(It.IsAny<string>()))
                .Returns(Task.FromResult(GetDetailByInvoiceIdFake(fakeId)));
            //Act
            var actionResult = await _invoiceController.GetInvoiceDetails(fakeId);
            //Assert
            var objectResult = (ObjectResult)actionResult;
            var response = (CustomResponseDto<InvoiceDto>)objectResult.Value;

            Assert.Equal(objectResult.StatusCode, (int)System.Net.HttpStatusCode.NotFound);
          
        }
        [Fact]
        public async Task Get_InvoiceDetails_Not_OK2()
        {
            //Arrange
            var fakeId = "sssadasdasdasdasd";

            _invoiceServiceMock.Setup(x => x.GetInvoiceDetailsAsync(It.IsAny<string>()))
                .Returns(Task.FromResult(GetDetailByInvoiceIdFake(fakeId)));
            //Act
            var actionResult = await _invoiceController.GetInvoiceDetails(fakeId);
            //Assert
            var objectResult = (ObjectResult)actionResult;
            var response = (CustomResponseDto<InvoiceDto>)objectResult.Value;

            Assert.Equal(objectResult.StatusCode, (int)System.Net.HttpStatusCode.NotFound);

        }
        private CustomResponseDto<List<InvoiceHeaderDto>> GetAllInvoiceHeadersFake()
        {
            List<InvoiceHeaderDto> invoiceHeaderDtos = new List<InvoiceHeaderDto>();

            for (int i = 1; i <= 3; i++)
            {
                string senderTitle = $"Gönderici Şirket{i}";
                string receiverTitle = $"Alıcı Şirket{i}";
                string email = $"ornek{i}@email.com";
                string date = $"2023-12-21";

                InvoiceHeaderDto headerCreateDto = new InvoiceHeaderDto
                {
                    InvoiceId = $"SVS20230000000{i}",
                    SenderTitle = senderTitle,
                    ReceiverTitle = receiverTitle,
                    Date = date,
                    Email = email
                };

              invoiceHeaderDtos.Add(headerCreateDto);
            }
            var invoiceHeadersDto = _mapper.Map<List<InvoiceHeaderDto>>(invoiceHeaderDtos);
            return CustomResponseDto<List<InvoiceHeaderDto>>.Success(200, invoiceHeadersDto);
        }
        private INVCAPP.Core.DTOs.CustomResponseDto<NoContentDto> FakeCreateMethod(InvoiceCreateDto invoiceCreateDto)
        {
            if (string.IsNullOrEmpty(invoiceCreateDto.InvoiceHeader.Email))
            {
                return INVCAPP.Core.DTOs.CustomResponseDto<NoContentDto>.Fail(400, "Email alanı boş olamaz.");
            }
            return INVCAPP.Core.DTOs.CustomResponseDto<NoContentDto>.Success(201);
        }
        private CustomResponseDto<InvoiceDto> GetDetailByInvoiceIdFake(string id)
        {
            var fakeId = "SVS202300000001";
            if (fakeId == id)
            {

                var invoiceDto = new InvoiceDto
                {
                    InvoiceHeader = new InvoiceHeaderDto
                    {
                        InvoiceId = "SVS202300000001",
                        SenderTitle = "Gönderici Şirket",
                        ReceiverTitle = "Alıcı Şirket",
                        Date = "2023-12-21",
                        Email = "ornek@email.com"
                    },
                    InvoiceLines = new List<InvoiceLineDto>
                    {
                        new InvoiceLineDto
                        {   Id=1,
                            Name = "Ürün 1",
                            Quantity = 2,
                            UnitCode = "ADET",
                            UnitPrice = 50.00m
                        },
                        new InvoiceLineDto
                        {   Id=2,
                            Name = "Ürün 2",
                            Quantity = 3,
                            UnitCode = "ADET",
                            UnitPrice = 30.00m
                        }
                    }
                };

                return CustomResponseDto<InvoiceDto>.Success(200, invoiceDto);

            }

            return CustomResponseDto<InvoiceDto>.Fail(404, "Invoice not found.");

        }
    }
}
