using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Runtime.Serialization;
using FNHMVC.Model;
using FluentNHibernate.Mapping;

namespace FNHMVC.Data.Mappings
{
    public class CategoryMap : ClassMap<Category>
    {
        public CategoryMap()
        {
            Id(x => x.CategoryId).GeneratedBy.Identity();
            Map(x => x.Name).Length(100).Not.Nullable();
            Map(x => x.Description).Length(256).Nullable();
            HasMany(x => x.Expenses).LazyLoad().Inverse().Cascade.All();
        }
    }
}