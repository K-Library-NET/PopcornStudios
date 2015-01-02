using Pstudio.MVC.Entities.Departures;
using Pstudio.MVC.Entities.Exports;
using Pstudio.MVC.Entities.Immigrations;
using Pstudio.MVC.Entities.Imports;
using Pstudio.MVC.Entities.Infos;
using Pstudio.MVC.Entities.Records;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pstudio.MVC.Entities
{
    public class EntityDbContext : DbContext
    {
        public EntityDbContext()
            : base()
        {

        }

        public EntityDbContext(string nameOrConnectionString)
            : base(nameOrConnectionString)
        {

        }

        public DbSet<Organization> Organizations { get; set; }

        public DbSet<Privilege> Privileges { get; set; }

        public DbSet<SysConfig> SysConfigs { get; set; }

        #region infos

        public DbSet<CitizenInfo> CitizenInfos { get; set; }

        public DbSet<ProductInfo> ProductInfos { get; set; }

        public DbSet<ShipInterInfo> ShipInterInfo { get; set; }

        public DbSet<VehicleInfo> VehicleInfo { get; set; }

        public DbSet<VehicleInterInfo> VehicleInterInfo { get; set; }

        #endregion

        #region departures

        public DbSet<DepartureApply> DepartureApplies { get; set; }

        public DbSet<DepartureApplyProduct> DepartureApplyProducts { get; set; }

        public DbSet<DepartureApplyChangeRecord> DepartureApplyChangeRecords { get; set; }

        public DbSet<DepartureCargoLoading> DepartureCargoLoadings { get; set; }

        public DbSet<DepartureCargoLoadingProduct> DepartureCargoLoadingProducts { get; set; }

        #endregion

        #region immigrations

        public DbSet<ImmigrationApply> ImmigrationApplies { get; set; }

        public DbSet<ImmigrationApplyProduct> ImmigrationApplyProducts { get; set; }

        public DbSet<ImmigrationApplyChangeRecord> ImmigrationApplyChangeRecords { get; set; }

        public DbSet<ImmigrationCargoLoading> ImmigrationCargoLoadings { get; set; }

        public DbSet<ImmigrationCargoLoadingProduct> ImmigrationCargoLoadingProducts { get; set; }

        #endregion

        #region exports

        public DbSet<ExportApply> ExportApplies { get; set; }

        public DbSet<ExportApplyProduct> ExportApplyProducts { get; set; }

        public DbSet<ExportApplyChangeRecord> ExportApplyChangeRecords { get; set; }

        #endregion

        #region Imports

        public DbSet<ImportApply> ImportApplies { get; set; }

        public DbSet<ImportApplyProduct> ImportApplyProducts { get; set; }

        public DbSet<ImportApplyChangeRecord> ImportApplyChangeRecords { get; set; }

        #endregion

        #region records

        public DbSet<ImmiDeparPersonSubTotalPerDay> ImmiDeparPersonDayDIM { get; set; }

        public DbSet<ImpExpProductsPerDay> ImpExpProductsDayDIM { get; set; }

        #endregion
    }
}
