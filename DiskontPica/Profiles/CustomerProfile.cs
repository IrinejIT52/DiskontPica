using AutoMapper;
using DiskontPica.DTO;
using DiskontPica.Models;

namespace DiskontPica.Profiles
{
	public class CustomerProfile : Profile
	{
		public CustomerProfile() {
			CreateMap<CustomerCreateDTO, Customer>();
		}
	}
}
