using AutoMapper;
using VendingMachine.Models.DTOs.Snack;
using VendingMachine.Models.Entities;

namespace VendingMachine.Mappers
{
    public class MapperProfile : Profile
    {
        public MapperProfile()
        {
            // <From, To>
            CreateMap<Snack, SnackResponseDto>();
        }
    }
}
