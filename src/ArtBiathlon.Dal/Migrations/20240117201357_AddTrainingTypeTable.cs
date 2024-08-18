using FluentMigrator;

namespace ArtBiathlon.Dal.Migrations;

[Migration(20240117201357, TransactionBehavior.None)]
public class AddTrainingTypesTable : Migration {
    public override void Up()
    {
        Execute.Sql(@"
            CREATE TABLE IF NOT EXISTS training_type (
                id              serial           not null,
                type_name       varchar(100)     not null, 
            PRIMARY KEY (id));   
        ");
    }

    public override void Down()
    {
        Execute.Sql(@"DROP TABLE IF EXISTS training_type;");
    }
}