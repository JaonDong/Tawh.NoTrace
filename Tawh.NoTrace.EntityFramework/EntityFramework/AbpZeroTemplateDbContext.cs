﻿using System.Data.Common;
using System.Data.Entity;
using Abp.Zero.EntityFramework;
using Tawh.NoTrace.Authorization.Roles;
using Tawh.NoTrace.Authorization.Users;
using Tawh.NoTrace.MultiTenancy;
using Tawh.NoTrace.Storage;

namespace Tawh.NoTrace.EntityFramework
{
    public class AbpZeroTemplateDbContext : AbpZeroDbContext<Tenant, Role, User>
    {
        /* Define an IDbSet for each entity of the application */

        public virtual IDbSet<BinaryObject> BinaryObjects { get; set; }

        /* Setting "Default" to base class helps us when working migration commands on Package Manager Console.
         * But it may cause problems when working Migrate.exe of EF. ABP works either way.         * 
         */
        public AbpZeroTemplateDbContext()
            : base("Default")
        {

        }

        /* This constructor is used by ABP to pass connection string defined in AbpZeroTemplateDataModule.PreInitialize.
         * Notice that, actually you will not directly create an instance of AbpZeroTemplateDbContext since ABP automatically handles it.
         */
        public AbpZeroTemplateDbContext(string nameOrConnectionString)
            : base(nameOrConnectionString)
        {

        }

        /* This constructor is used in tests to pass a fake/mock connection.
         */
        public AbpZeroTemplateDbContext(DbConnection dbConnection)
            : base(dbConnection, true)
        {

        }
    }
}
