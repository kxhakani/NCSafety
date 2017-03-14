namespace NCSafety.DAL.NCSafetyEntities.Migrations
{
    using Models;
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<NCSafety.DAL.NCSafetyEntities.NCSafetyCFEntities>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
            MigrationsDirectory = @"DAL\NCSafetyEntities\Migrations";
        }

        protected override void Seed(NCSafety.DAL.NCSafetyEntities.NCSafetyCFEntities context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data. E.g.
            //
            //    context.People.AddOrUpdate(
            //      p => p.FullName,
            //      new Person { FullName = "Andrew Peters" },
            //      new Person { FullName = "Brice Lambson" },
            //      new Person { FullName = "Rowan Miller" }
            //    );
            //

            var schools = new List<School>
            {
                new School { schName = "Media", ascDeanFirst="Jane", ascDeanLast="Doe", ascDeanEmail="janedoe@outlook.com", ascDeanAssistantEmail="janedoeassistant@outlook.com" },
                new School {schName = "Trades", ascDeanFirst="John", ascDeanLast="Smith", ascDeanEmail="johnsmith@outlook.com", ascDeanAssistantEmail="johnsmithassistant@outlook.com" },
                new School {schName = "Technology", ascDeanFirst="Sarah", ascDeanLast="Taylor", ascDeanEmail="sarahtaylor@outlook.com", ascDeanAssistantEmail="sarahtaylorassistant@outlook.com" }
            };

            schools.ForEach(s => context.Schools.AddOrUpdate(n => n.schName, s));
            context.SaveChanges();

            var hazards = new List<Hazard>
            {
                new Models.Hazard { hazName="Guard Missing", hazDescription="The safety guard is missing from the equipment" },
                new Hazard {hazName="Food spill", hazDescription="Food has been spilled on or around the equipment" },
                new Hazard {hazName="Chemical Storage", hazDescription="Hazardous chemicals are not stored correctly" },
                new Hazard {hazName="Frayed Cords", hazDescription="Cords are frayed" },
                new Hazard {hazName="Improper Wiring", hazDescription="Wires are exposed" }
            };

            hazards.ForEach(h => context.Hazards.AddOrUpdate(n => n.hazName, h));
            context.SaveChanges();

            var labs = new List<Lab>
            {
                new Lab { labBuilding = "TC", labNumber = "02" },
                new Lab { labBuilding = "L", labNumber = "117" },
                new Lab {labBuilding = "V", labNumber = "115"},
                new Lab {labBuilding = "L", labNumber = "116"},
                new Lab {labBuilding = "V", labNumber = "001"},
                new Lab {labBuilding = "TC", labNumber = "04"}
            };

            labs.ForEach(l => context.Labs.AddOrUpdate(b => b.labBuilding, l));
            context.SaveChanges();

            var equipments = new List<Equipment>
            {
                new Equipment { eqName="Rug", LabID=1 },
                new Equipment { eqName="Saw", LabID=2 },
                new Equipment {eqName="Vehicle", LabID=3 }
            };
            equipments.ForEach(e => context.Equipments.AddOrUpdate(n => n.eqName, e));
            context.SaveChanges();

            var inspections = new List<Inspection>
            {
                new Inspection { inspDate=(Convert.ToDateTime("07-10-2016")), LabID=1, SchoolID=1 },
                new Inspection { inspDate=(Convert.ToDateTime("08-01-2015")), LabID=2, SchoolID=3 }
            };
            inspections.ForEach(i => context.Inspections.AddOrUpdate(n => n.inspDate, i));
            context.SaveChanges();

            var items = new List<Item>
            {
               new Item { HazardID=1, InspectionID=1, EquipmentID=1, isFault=true, isGood=false, itemComment="Who stole the guard??", itemCorrActionDue=(Convert.ToDateTime("07-10-2016"))},
                new Item { HazardID=2, InspectionID=2, EquipmentID=2, isFault=false, isGood=true, itemComment="Very well done", itemCorrActionDue=(Convert.ToDateTime("03-02-2017")), itemCorrActionCompleted=(Convert.ToDateTime("03-03-2017")) },
                new Item { HazardID=3, InspectionID=2, EquipmentID=3, isFault=true, isGood=false, itemComment="Fumes", itemCorrActionDue=(Convert.ToDateTime("03-18-2017")), itemCorrActionCompleted=(Convert.ToDateTime("03-13-2017")) }
            };
            items.ForEach(i => context.Items.AddOrUpdate(n => n.itemComment, i));
            context.SaveChanges();

            var faqs = new List<FAQ>
            {
                new FAQ { faqQuestion = "How do you schedule an inspection?", faqAnswer = "Inspections will be scheduled by the SuperAdmin through the calendar featured on this web application." },
                new FAQ { faqQuestion = "Can I go back and edit an inspection after I have submitted one?", faqAnswer = "No, unfortunately you cannot go back and edit anything from an inspection you have submitted. Only the Associate Dean can make changes to inspections after submission. If you have any concerns regarding an inspection you have submitted, please contact the Associate Dean of your department." },
                new FAQ { faqQuestion = "Will I be reminded of any upcoming inspections?", faqAnswer = "Yes, when an inspection is scheduled, you will automatically receive an email reminder 7 days prior to the inspection due date." }
            };

            faqs.ForEach(f => context.FAQs.AddOrUpdate(q => q.faqQuestion, f));
            context.SaveChanges();
        }
    }
    
}
