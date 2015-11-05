namespace CrmCoreLite.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Initial : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Person",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Firstname = c.String(nullable: false, maxLength: 30),
                        Middlename = c.String(maxLength: 30),
                        Lastname = c.String(nullable: false, maxLength: 30),
                        EmailAddress = c.String(nullable: false),
                        Telephone = c.Int(nullable: false),
                        Mobile = c.Int(nullable: false),
                        Birthplace = c.String(maxLength: 100),
                        PermanentAddress = c.String(nullable: false, maxLength: 100),
                        Nationality = c.String(nullable: false, maxLength: 20),
                        CivilStatus = c.Int(nullable: false),
                        BirthDay = c.DateTime(nullable: false),
                        DateCreated = c.DateTime(),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.Product",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        ProductNo = c.String(),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.Bill",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        BillNo = c.String(maxLength: 30),
                        BillAmount = c.Decimal(nullable: false, precision: 18, scale: 2),
                        OrderID = c.Int(nullable: false),
                        BillCode = c.String(),
                        CustomerID = c.Int(),
                        BillRemarks = c.String(maxLength: 50),
                        BillDue = c.DateTime(),
                        DateCreated = c.DateTime(),
                        BillStatus = c.Int(),
                        PaymentClass = c.Int(),
                        PaymentSource = c.String(maxLength: 50),
                        PaymentAmount = c.Decimal(precision: 18, scale: 2),
                        PaymentCreated = c.DateTime(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Customer", t => t.CustomerID)
                .ForeignKey("dbo.Order", t => t.OrderID, cascadeDelete: true)
                .Index(t => t.OrderID)
                .Index(t => t.CustomerID);
            
            CreateTable(
                "dbo.Order",
                c => new
                    {
                        OrderID = c.Int(nullable: false, identity: true),
                        OrderNo = c.String(),
                        TotalOrderAmount = c.Decimal(nullable: false, precision: 18, scale: 2),
                        PaymentType = c.Int(),
                        ContractStatus = c.Int(),
                        CustomerID = c.Int(nullable: false),
                        AgentID = c.Int(nullable: false),
                        ReservationFee = c.Decimal(nullable: false, precision: 18, scale: 2),
                        DateCreated = c.DateTime(),
                    })
                .PrimaryKey(t => t.OrderID)
                .ForeignKey("dbo.Agent", t => t.AgentID)
                .ForeignKey("dbo.Customer", t => t.CustomerID)
                .Index(t => t.CustomerID)
                .Index(t => t.AgentID);
            
            CreateTable(
                "dbo.Insentive",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        InsentiveAmount = c.Decimal(nullable: false, precision: 18, scale: 2),
                        InsentiveStatus = c.Int(),
                        ReleaseDate = c.DateTime(nullable: false),
                        AgentID = c.Int(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Agent", t => t.AgentID)
                .Index(t => t.AgentID);
            
            CreateTable(
                "dbo.OrderItem",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Amount = c.Decimal(nullable: false, precision: 18, scale: 2),
                        OrderID = c.Int(nullable: false),
                        ProductID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Order", t => t.OrderID, cascadeDelete: true)
                .ForeignKey("dbo.Product", t => t.ProductID, cascadeDelete: true)
                .Index(t => t.OrderID)
                .Index(t => t.ProductID);
            
            CreateTable(
                "dbo.Agent",
                c => new
                    {
                        ID = c.Int(nullable: false),
                        AgentNo = c.String(maxLength: 30),
                        Agency = c.String(),
                        BrokerID = c.Int(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Person", t => t.ID)
                .ForeignKey("dbo.Broker", t => t.BrokerID)
                .Index(t => t.ID)
                .Index(t => t.BrokerID);
            
            CreateTable(
                "dbo.Basement",
                c => new
                    {
                        ID = c.Int(nullable: false),
                        UnitNo = c.String(),
                        BaseNo = c.Int(),
                        UnitArea = c.String(),
                        TotalContractPrice = c.Decimal(nullable: false, precision: 18, scale: 2),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Product", t => t.ID)
                .Index(t => t.ID);
            
            CreateTable(
                "dbo.Broker",
                c => new
                    {
                        ID = c.Int(nullable: false),
                        AgencyName = c.String(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Person", t => t.ID)
                .Index(t => t.ID);
            
            CreateTable(
                "dbo.Customer",
                c => new
                    {
                        ID = c.Int(nullable: false),
                        AccountNo = c.String(maxLength: 30),
                        EmploymentStatus = c.Int(),
                        CompanyName = c.String(maxLength: 20),
                        NatureofBusiness = c.String(maxLength: 20),
                        Occupation = c.String(nullable: false, maxLength: 20),
                        CompanyAddress = c.String(maxLength: 100),
                        CompanyTelephoneNumbers = c.Int(),
                        YearsWithCompany = c.Int(),
                        MonthlyIncome = c.Decimal(precision: 18, scale: 2),
                        TaxIdentificationNumber = c.String(maxLength: 30),
                        Passport = c.String(maxLength: 30),
                        ValidGovernmentID = c.String(maxLength: 30),
                        PlaceIssue = c.String(maxLength: 30),
                        DateIssue = c.DateTime(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Person", t => t.ID)
                .Index(t => t.ID);
            
            CreateTable(
                "dbo.Unit",
                c => new
                    {
                        ID = c.Int(nullable: false),
                        UnitNo = c.String(maxLength: 30),
                        UnitType = c.Int(),
                        uViewType = c.Int(),
                        UnitArea = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Balcony = c.Decimal(nullable: false, precision: 18, scale: 2),
                        TotalArea = c.Decimal(nullable: false, precision: 18, scale: 2),
                        TotalListPrice = c.Decimal(nullable: false, precision: 18, scale: 2),
                        TransferMiscFees = c.Decimal(nullable: false, precision: 18, scale: 2),
                        TotalContractPrice = c.Decimal(nullable: false, precision: 18, scale: 2),
                        UnitStatus = c.Int(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Product", t => t.ID)
                .Index(t => t.ID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Unit", "ID", "dbo.Product");
            DropForeignKey("dbo.Customer", "ID", "dbo.Person");
            DropForeignKey("dbo.Broker", "ID", "dbo.Person");
            DropForeignKey("dbo.Basement", "ID", "dbo.Product");
            DropForeignKey("dbo.Agent", "BrokerID", "dbo.Broker");
            DropForeignKey("dbo.Agent", "ID", "dbo.Person");
            DropForeignKey("dbo.OrderItem", "ProductID", "dbo.Product");
            DropForeignKey("dbo.OrderItem", "OrderID", "dbo.Order");
            DropForeignKey("dbo.Insentive", "AgentID", "dbo.Agent");
            DropForeignKey("dbo.Bill", "OrderID", "dbo.Order");
            DropForeignKey("dbo.Order", "CustomerID", "dbo.Customer");
            DropForeignKey("dbo.Order", "AgentID", "dbo.Agent");
            DropForeignKey("dbo.Bill", "CustomerID", "dbo.Customer");
            DropIndex("dbo.Unit", new[] { "ID" });
            DropIndex("dbo.Customer", new[] { "ID" });
            DropIndex("dbo.Broker", new[] { "ID" });
            DropIndex("dbo.Basement", new[] { "ID" });
            DropIndex("dbo.Agent", new[] { "BrokerID" });
            DropIndex("dbo.Agent", new[] { "ID" });
            DropIndex("dbo.OrderItem", new[] { "ProductID" });
            DropIndex("dbo.OrderItem", new[] { "OrderID" });
            DropIndex("dbo.Insentive", new[] { "AgentID" });
            DropIndex("dbo.Order", new[] { "AgentID" });
            DropIndex("dbo.Order", new[] { "CustomerID" });
            DropIndex("dbo.Bill", new[] { "CustomerID" });
            DropIndex("dbo.Bill", new[] { "OrderID" });
            DropTable("dbo.Unit");
            DropTable("dbo.Customer");
            DropTable("dbo.Broker");
            DropTable("dbo.Basement");
            DropTable("dbo.Agent");
            DropTable("dbo.OrderItem");
            DropTable("dbo.Insentive");
            DropTable("dbo.Order");
            DropTable("dbo.Bill");
            DropTable("dbo.Product");
            DropTable("dbo.Person");
        }
    }
}
