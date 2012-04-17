using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NHibernate;
using NHibernate.Tool.hbm2ddl;

namespace FNHMVC.Data.SchemaTool
{
    public static class SchemaTool
    {
        public static void ValidateSchema()
        {
            FNHMVC.Data.Infrastructure.ConnectionHelper.GetConfiguration().ExposeConfiguration(cfg =>
            {
                var schemaValidate = new NHibernate.Tool.hbm2ddl.SchemaValidator(cfg);
                schemaValidate.Validate();
            }).BuildConfiguration();
        }

        public static void CreatSchema(string OutputFile)
        {
            FNHMVC.Data.Infrastructure.ConnectionHelper.GetConfiguration().ExposeConfiguration(cfg =>
            {
                var schemaExport = new NHibernate.Tool.hbm2ddl.SchemaExport(cfg);
                if (!string.IsNullOrEmpty(OutputFile)) schemaExport.SetOutputFile(OutputFile);
                schemaExport.Drop(false, true);
                schemaExport.Create(false, true);
            }).BuildConfiguration();
        }

        public static void UpdateSchema()
        {
            FNHMVC.Data.Infrastructure.ConnectionHelper.GetConfiguration().ExposeConfiguration(cfg =>
            {
                var schemaUpdate = new NHibernate.Tool.hbm2ddl.SchemaUpdate(cfg);
                schemaUpdate.Execute(false, true);
            }).BuildConfiguration();
        }
    }
}
