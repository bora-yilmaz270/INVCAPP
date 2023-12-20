using AutoMapper;
using INVCAPP.Core.DTOs;
using INVCAPP.Core.Models;

namespace INVCAPP.Service.Mapping
{
    public class MapProfile : Profile
    {
        public MapProfile()
        {
            CreateMap<Invoice, InvoiceDto>().ReverseMap();
            CreateMap<InvoiceHeader, InvoiceHeaderDto>().ReverseMap();
            CreateMap<InvoiceLine, InvoiceLineDto>().ReverseMap();

            CreateMap<Invoice, InvoiceCreateDto>().ReverseMap();
            CreateMap<InvoiceHeader, InvoiceHeaderCreateDto>().ReverseMap();
            CreateMap<InvoiceLine, InvoiceLineCreateDto>().ReverseMap();
        }
    }
}