using FluentMigrator;

namespace ArtBiathlon.Dal.Migrations;

[Migration(20240117200617, TransactionBehavior.None)]
public class AddHrvIndicatorTable : Migration {
    public override void Up()
    {
        Execute.Sql(@"
            CREATE TABLE IF NOT EXISTS hrv_indicator (
                id                  serial                  not null,
                created_at          date                    not null, 
                user_info_id        integer                 not null,
                readiness           double precision        not null,
                heart               integer                 not null,
                rmssd               integer                 not null,
                rr                  double precision        not null,
                sdnn                double precision        not null,
                sd                  double precision        not null,
                tp                  double precision        not null,
                hf                  double precision        not null,
                lf                  double precision        not null,
                si                  double precision        not null,
                load                double precision            null,
            PRIMARY KEY (id));   
        ");
        
        Execute.Sql(@"
            CREATE INDEX CONCURRENTLY IF NOT EXISTS hrv_indicator_user_info_id
            ON hrv_indicator (user_info_id);
        ");
        
        Execute.Sql(@"
            CREATE INDEX CONCURRENTLY IF NOT EXISTS hrv_indicator_created_at
            ON hrv_indicator (created_at);
        ");
    }

    public override void Down()
    {
        Execute.Sql(@"DROP TABLE IF EXISTS hrv_indicator;");
        Execute.Sql(@"DROP INDEX CONCURRENTLY IF EXISTS hrv_indicator_user_info_id;");
        Execute.Sql(@"DROP INDEX CONCURRENTLY IF EXISTS hrv_indicator_created_at;");
    }
}