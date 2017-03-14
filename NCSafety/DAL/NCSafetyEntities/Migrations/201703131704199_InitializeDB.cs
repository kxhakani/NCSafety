namespace NCSafety.DAL.NCSafetyEntities.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitializeDB : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Equipment",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        eqName = c.String(nullable: false),
                        LabID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Lab", t => t.LabID)
                .Index(t => t.LabID);
            
            CreateTable(
                "dbo.Item",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        HazardID = c.Int(nullable: false),
                        EquipmentID = c.Int(nullable: false),
                        InspectionID = c.Int(nullable: false),
                        isGood = c.Boolean(nullable: false),
                        isFault = c.Boolean(nullable: false),
                        itemCorrActionDue = c.DateTime(nullable: false),
                        itemCorrActionCompleted = c.DateTime(),
                        itemComment = c.String(),
                        imageContent = c.Binary(),
                        imageMimeType = c.String(maxLength: 256),
                        imageFileName = c.String(maxLength: 100),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Equipment", t => t.EquipmentID)
                .ForeignKey("dbo.Hazard", t => t.HazardID)
                .ForeignKey("dbo.Inspection", t => t.InspectionID)
                .Index(t => t.HazardID)
                .Index(t => t.EquipmentID)
                .Index(t => t.InspectionID);
            
            CreateTable(
                "dbo.Hazard",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        hazName = c.String(nullable: false),
                        hazDescription = c.String(),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.Lab",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        labBuilding = c.String(nullable: false),
                        labNumber = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.School",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        schName = c.String(nullable: false),
                        ascDeanFirst = c.String(),
                        ascDeanLast = c.String(),
                        ascDeanEmail = c.String(),
                        ascDeanAssistantEmail = c.String(),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.Inspection",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        SchoolID = c.Int(nullable: false),
                        LabID = c.Int(nullable: false),
                        inspDate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Lab", t => t.LabID)
                .ForeignKey("dbo.School", t => t.SchoolID)
                .Index(t => t.SchoolID)
                .Index(t => t.LabID);
            
            CreateTable(
                "dbo.UploadedPhotos",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        FileContent = c.Binary(nullable: false),
                        MimeType = c.String(nullable: false, maxLength: 256),
                        PhotoName = c.String(nullable: false, maxLength: 255),
                        PhotoDescription = c.String(maxLength: 100),
                        ItemID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Item", t => t.ItemID)
                .Index(t => t.ItemID);
            
            CreateTable(
                "dbo.FAQ",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        faqQuestion = c.String(nullable: false),
                        faqAnswer = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.LabHazard",
                c => new
                    {
                        Lab_ID = c.Int(nullable: false),
                        Hazard_ID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Lab_ID, t.Hazard_ID })
                .ForeignKey("dbo.Lab", t => t.Lab_ID, cascadeDelete: true)
                .ForeignKey("dbo.Hazard", t => t.Hazard_ID, cascadeDelete: true)
                .Index(t => t.Lab_ID)
                .Index(t => t.Hazard_ID);
            
            CreateTable(
                "dbo.SchoolLab",
                c => new
                    {
                        School_ID = c.Int(nullable: false),
                        Lab_ID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.School_ID, t.Lab_ID })
                .ForeignKey("dbo.School", t => t.School_ID, cascadeDelete: true)
                .ForeignKey("dbo.Lab", t => t.Lab_ID, cascadeDelete: true)
                .Index(t => t.School_ID)
                .Index(t => t.Lab_ID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.UploadedPhotos", "ItemID", "dbo.Item");
            DropForeignKey("dbo.SchoolLab", "Lab_ID", "dbo.Lab");
            DropForeignKey("dbo.SchoolLab", "School_ID", "dbo.School");
            DropForeignKey("dbo.Inspection", "SchoolID", "dbo.School");
            DropForeignKey("dbo.Inspection", "LabID", "dbo.Lab");
            DropForeignKey("dbo.Item", "InspectionID", "dbo.Inspection");
            DropForeignKey("dbo.LabHazard", "Hazard_ID", "dbo.Hazard");
            DropForeignKey("dbo.LabHazard", "Lab_ID", "dbo.Lab");
            DropForeignKey("dbo.Equipment", "LabID", "dbo.Lab");
            DropForeignKey("dbo.Item", "HazardID", "dbo.Hazard");
            DropForeignKey("dbo.Item", "EquipmentID", "dbo.Equipment");
            DropIndex("dbo.SchoolLab", new[] { "Lab_ID" });
            DropIndex("dbo.SchoolLab", new[] { "School_ID" });
            DropIndex("dbo.LabHazard", new[] { "Hazard_ID" });
            DropIndex("dbo.LabHazard", new[] { "Lab_ID" });
            DropIndex("dbo.UploadedPhotos", new[] { "ItemID" });
            DropIndex("dbo.Inspection", new[] { "LabID" });
            DropIndex("dbo.Inspection", new[] { "SchoolID" });
            DropIndex("dbo.Item", new[] { "InspectionID" });
            DropIndex("dbo.Item", new[] { "EquipmentID" });
            DropIndex("dbo.Item", new[] { "HazardID" });
            DropIndex("dbo.Equipment", new[] { "LabID" });
            DropTable("dbo.SchoolLab");
            DropTable("dbo.LabHazard");
            DropTable("dbo.FAQ");
            DropTable("dbo.UploadedPhotos");
            DropTable("dbo.Inspection");
            DropTable("dbo.School");
            DropTable("dbo.Lab");
            DropTable("dbo.Hazard");
            DropTable("dbo.Item");
            DropTable("dbo.Equipment");
        }
    }
}
