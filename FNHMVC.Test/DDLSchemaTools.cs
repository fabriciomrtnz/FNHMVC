using FNHMVC.Model;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Specialized;
using System.Collections.Generic;
using Autofac;
using Autofac.Configuration;
using FNHMVC.CommandProcessor.Dispatcher;
using FNHMVC.Data.Repositories;
using FNHMVC.Data.Infrastructure;
using FNHMVC.Data.Mappings;
using FNHMVC.Domain.Commands;
using FNHMVC.Core.Common;
using FNHMVC.CommandProcessor.Command;
using System.Reflection;
using NHibernate;
using FluentNHibernate;
using FluentNHibernate.Cfg;
using FluentNHibernate.Cfg.Db;
using FNHMVC.Data.SchemaTool;
//using HibernatingRhinos; //User NH Profiler to debug your NHibernate's queries

namespace FNHMVC.Test
{
    [TestClass]
    public class DDLSchemaTools
    {
        private TestContext testContextInstance;

        public TestContext TestContext
        {
            get
            {
                return testContextInstance;
            }
            set
            {
                testContextInstance = value;
            }
        }

        public DDLSchemaTools()
        {
            //HibernatingRhinos.Profiler.Appender.NHibernate.NHibernateProfiler.Initialize(); //NH Profiler
        }

        [TestMethod]
        public void CreateSchema()
        {
            FNHMVC.Data.SchemaTool.SchemaTool.CreatSchema(string.Empty);
        }

        [TestMethod]
        public void UpdateSchema()
        {
            FNHMVC.Data.SchemaTool.SchemaTool.UpdateSchema();
        }

        [TestMethod]
        public void ValidateSchema()
        {
            FNHMVC.Data.SchemaTool.SchemaTool.ValidateSchema();
        }
    }
}