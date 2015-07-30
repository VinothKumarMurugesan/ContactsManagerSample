using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.Entity;
using System.Data.Entity.Core.Mapping;
using System.Data.Entity.Core.Metadata.Edm;
using System.Data.Entity.Core.Objects;
using System.Data.Entity.Infrastructure;
using System.Linq;

namespace Xen.Helpers
{
    public static class DbContextHelperExtension
    {
        public static string GetTableName<T>(this DbContext context) where T : class
        {
            ObjectContext objectContext = ((IObjectContextAdapter)context).ObjectContext;
            return objectContext.GetTableName(typeof(T));
        }

        public static string GetTableName(this DbContext context, Type t)
        {
            ObjectContext objectContext = ((IObjectContextAdapter)context).ObjectContext;
            return objectContext.GetTableName(t);
        }

        private static readonly Dictionary<Type, string> tableNames = new Dictionary<Type, string>();

        public static string GetTableName(this ObjectContext context, Type t)
        {
            string result;

            if (!tableNames.TryGetValue(t, out result))
            {
                lock (tableNames)
                {
                    if (!tableNames.TryGetValue(t, out result))
                    {

                        string entityName = t.Name;

                        ReadOnlyCollection<EntityContainerMapping> storageMetadata = context.MetadataWorkspace.GetItems<EntityContainerMapping>(DataSpace.CSSpace);

                        foreach (EntityContainerMapping ecm in storageMetadata)
                        {
                            EntitySet entitySet;
                            if (ecm.StoreEntityContainer.TryGetEntitySetByName(entityName, true, out entitySet))
                            {
                                result = entitySet.Schema + "." + entitySet.Table;
                                break;
                            }
                        }

                        tableNames.Add(t, result);
                    }
                }
            }
            return result;
        }
    }
}
