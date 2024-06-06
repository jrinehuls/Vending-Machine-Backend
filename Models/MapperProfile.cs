using AutoMapper;
using VendingMachine.Models.DTOs;
using VendingMachine.Models.Entities;

namespace VendingMachine.Models
{
    public class MapperProfile : Profile
    {
        public MapperProfile() {
            // <From, To>
            CreateMap<Snack, SnackResponseDto>();
        }
    }
}
