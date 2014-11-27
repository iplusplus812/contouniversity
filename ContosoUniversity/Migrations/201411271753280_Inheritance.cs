namespace ContosoUniversity.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Inheritance : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Enrollment","StudentID","dbo.Student");
            DropIndex("dbo.Enrollment",new []{"StudentID"});
            RenameTable(name:"dbo.Instructor",newName:"Person");
            AddColumn("dbo.Person","EnrollmentDate",c=>c.DateTime());
            AddColumn("dbo.Person","Discriminator",c=>c.String(nullable:false,maxLength:128,defaultValue:"Instructor"));
            AlterColumn("dbo.Person","HireDate",c=>c.DateTime());
            AddColumn("dbo.Person","OldId",c=>c.Int(nullable:true));
            Sql("INSERT INTO dbo.Person(LastName,FirstName,HireDate,EnrollmentDate,Discriminator,OldId) SELECT LastName,FirstName,null AS HireDate,EnrollmentDate,'Student' AS Discriminator,ID as OldId FROM dbo.Student");
            Sql("UPDATE dbo.Enrollment SET StudentId = (SELECT ID FROM dbo.Person WHERE OldId=Enrollment.StudentId AND Discriminator='Student')");
            DropColumn("dbo.Person","OldId");
            DropTable("dbo.Student");

            AddForeignKey("dbo.Enrollment","StudentID","dbo.Person","ID",cascadeDelete:true);
            CreateIndex("dbo.Enrollment","StudentID");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.Student",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        LastName = c.String(nullable: false, maxLength: 50),
                        FirstName = c.String(nullable: false, maxLength: 50),
                        EnrollmentDate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.ID);
            
            AlterColumn("dbo.Person", "HireDate", c => c.DateTime(nullable: false));
            DropColumn("dbo.Person", "Discriminator");
            DropColumn("dbo.Person", "EnrollmentDate");
            RenameTable(name: "dbo.Person", newName: "Instructor");
        }
    }
}
