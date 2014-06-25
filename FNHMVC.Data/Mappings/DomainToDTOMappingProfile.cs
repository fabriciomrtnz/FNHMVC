using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using FNHMVC.Model;

namespace FNHMVC.Data.Mappings
{
    public class DomainToDTOMappingProfile : Profile
    {
        public override string ProfileName
        {
            get { return "DomainToDTOMappingProfile"; }
        }

        protected override void Configure()
        {
            Mapper.CreateMap<Category, DTOCategory>();
            Mapper.CreateMap<Expense, DTOExpense>();
            Mapper.CreateMap<User, DTOUser>();
        }
    }
}
