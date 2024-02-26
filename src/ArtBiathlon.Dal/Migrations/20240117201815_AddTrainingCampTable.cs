using FluentMigrator;

namespace ArtBiathlon.Dal.Migrations;

[Migration(20240117201815, TransactionBehavior.None)]
public class AddTrainingCampTable : Migration {
    public override void Up()
    {
        Execute.Sql(@"
            CREATE TABLE IF NOT EXISTS training_camp (
                id              serial      not null,
                camp_start      date        not null, 
                camp_end        date        not null, 
            PRIMARY KEY (id));   
        ");
    }

    public override void Down()
    {
        Execute.Sql(@"DROP TABLE IF EXISTS training_camp;");
    }
}