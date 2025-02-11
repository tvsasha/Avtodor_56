﻿//------------------------------------------------------------------------------
// <auto-generated>
//     Этот код создан по шаблону.
//
//     Изменения, вносимые в этот файл вручную, могут привести к непредвиденной работе приложения.
//     Изменения, вносимые в этот файл вручную, будут перезаписаны при повторном создании кода.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Avtodor_56
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    using System.Data.Entity.Core.Objects;
    using System.Linq;
    
    public partial class Avtodor_56Entities : DbContext
    {
        public Avtodor_56Entities()
            : base("name=Avtodor_56Entities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<Client> Client { get; set; }
        public virtual DbSet<Employee> Employee { get; set; }
        public virtual DbSet<Materials> Materials { get; set; }
        public virtual DbSet<OrderDetails> OrderDetails { get; set; }
        public virtual DbSet<OrderHistory> OrderHistory { get; set; }
        public virtual DbSet<Orders> Orders { get; set; }
        public virtual DbSet<PurchaseDetails> PurchaseDetails { get; set; }
        public virtual DbSet<Purchases> Purchases { get; set; }
        public virtual DbSet<Supplier> Supplier { get; set; }
        public virtual DbSet<sysdiagrams> sysdiagrams { get; set; }
        public virtual DbSet<User> User { get; set; }
    
        [DbFunction("Avtodor_56Entities", "GetClientContactInfo")]
        public virtual IQueryable<GetClientContactInfo_Result> GetClientContactInfo(Nullable<int> clientID)
        {
            var clientIDParameter = clientID.HasValue ?
                new ObjectParameter("ClientID", clientID) :
                new ObjectParameter("ClientID", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.CreateQuery<GetClientContactInfo_Result>("[Avtodor_56Entities].[GetClientContactInfo](@ClientID)", clientIDParameter);
        }
    
        [DbFunction("Avtodor_56Entities", "GetOrderDetailsByMaterial")]
        public virtual IQueryable<GetOrderDetailsByMaterial_Result> GetOrderDetailsByMaterial(Nullable<int> materialID)
        {
            var materialIDParameter = materialID.HasValue ?
                new ObjectParameter("MaterialID", materialID) :
                new ObjectParameter("MaterialID", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.CreateQuery<GetOrderDetailsByMaterial_Result>("[Avtodor_56Entities].[GetOrderDetailsByMaterial](@MaterialID)", materialIDParameter);
        }
    
        public virtual int CalculateTotalOrderAmount(Nullable<int> orderID, ObjectParameter totalAmount)
        {
            var orderIDParameter = orderID.HasValue ?
                new ObjectParameter("OrderID", orderID) :
                new ObjectParameter("OrderID", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("CalculateTotalOrderAmount", orderIDParameter, totalAmount);
        }
    
        public virtual ObjectResult<GetOrderDetailsByClient_Result> GetOrderDetailsByClient(Nullable<int> clientID)
        {
            var clientIDParameter = clientID.HasValue ?
                new ObjectParameter("ClientID", clientID) :
                new ObjectParameter("ClientID", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<GetOrderDetailsByClient_Result>("GetOrderDetailsByClient", clientIDParameter);
        }
    
        public virtual ObjectResult<GetOrdersByEmployee_Result> GetOrdersByEmployee(Nullable<int> employeeID)
        {
            var employeeIDParameter = employeeID.HasValue ?
                new ObjectParameter("EmployeeID", employeeID) :
                new ObjectParameter("EmployeeID", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<GetOrdersByEmployee_Result>("GetOrdersByEmployee", employeeIDParameter);
        }
    
        public virtual ObjectResult<GetStockReport_Result> GetStockReport(Nullable<decimal> minStockLevel)
        {
            var minStockLevelParameter = minStockLevel.HasValue ?
                new ObjectParameter("MinStockLevel", minStockLevel) :
                new ObjectParameter("MinStockLevel", typeof(decimal));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<GetStockReport_Result>("GetStockReport", minStockLevelParameter);
        }
    
        public virtual int UpdateOrderStatus(string orderIDs, string newStatus)
        {
            var orderIDsParameter = orderIDs != null ?
                new ObjectParameter("OrderIDs", orderIDs) :
                new ObjectParameter("OrderIDs", typeof(string));
    
            var newStatusParameter = newStatus != null ?
                new ObjectParameter("NewStatus", newStatus) :
                new ObjectParameter("NewStatus", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("UpdateOrderStatus", orderIDsParameter, newStatusParameter);
        }
    }
}
