using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FNHMVC.Model;
using FNHMVC.Data.Mappings;
using AutoMapper;

namespace FNHMVC.Data.Infrastructure
{
    public static class AutoMapperConfiguration
    {
        public static void Configure()
        {
            Mapper.Initialize(x =>
            {
                x.AddProfile<DomainToCommandMappingProfile>();
                x.AddProfile<CommandToDomainMappingProfile>();
                x.AddProfile<DomainToDTOMappingProfile>();
            });

            Mapper.AssertConfigurationIsValid();
        }
    }
}
