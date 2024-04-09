using AutoMapper;
using DiskontPica.DTO;
using DiskontPica.Models;

namespace DiskontPica.Profiles
{
	public class AdministratorProfile : Profile
	{
		public AdministratorProfile() {
			CreateMap<AdministratorCreateDTO,Administrator>();
		}
	}
}
