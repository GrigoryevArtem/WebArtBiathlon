using FluentMigrator;

namespace ArtBiathlon.Dal.Migrations;

[Migration(20240117201629, TransactionBehavior.None)]
public class AddTrainingScheduleTable : Migration {
    public override void Up()
    {
        Execute.Sql(@"
            CREATE TABLE IF NOT EXISTS trainings_schedule (
                id                serial                      not null,
                training_id       smallint                    not null,
                start_date        timestamptz                 not null, 
                end_date          timestamptz                 not null, 
                day_time          smallint                    not null, 
                duration          interval                    not null,
                training_camp_id  smallint                    not null, 
            PRIMARY KEY (id));   
        ");
        
        Execute.Sql(@"
            CREATE INDEX CONCURRENTLY IF NOT EXISTS trainings_schedule_start_date
            ON trainings_schedule (start_date);
        ");
        
        Execute.Sql(@"
            CREATE INDEX CONCURRENTLY IF NOT EXISTS trainings_schedule_end_date
            ON trainings_schedule (end_date);
        ");
    }

    public override void Down()
    {
        Execute.Sql(@"DROP TABLE IF EXISTS trainings_schedule;");
        Execute.Sql(@"DROP INDEX CONCURRENTLY IF EXISTS trainings_schedule_start_date;");
        Execute.Sql(@"DROP INDEX CONCURRENTLY IF EXISTS trainings_schedule_end_date;");
    }
}