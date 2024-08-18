using FluentMigrator;

namespace ArtBiathlon.Dal.Migrations;

[Migration(20240117200913, TransactionBehavior.None)]
public class AddTrainingTable : Migration {
    public override void Up()
    {
        Execute.Sql(@"
            CREATE TABLE IF NOT EXISTS training (
                id                serial           not null,
                training_name     varchar(100)     not null, 
                training_type_id  smallint         not null, 
            PRIMARY KEY (id));   
        ");
    }

    public override void Down()
    {
        Execute.Sql(@"DROP TABLE IF EXISTS training;");
    }
}