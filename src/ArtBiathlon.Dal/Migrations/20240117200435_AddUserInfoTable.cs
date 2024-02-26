using FluentMigrator;

namespace ArtBiathlon.Dal.Migrations;

[Migration(20240117200435, TransactionBehavior.None)]
public class AddUserTable : Migration {
    public override void Up()
    {
        Execute.Sql(@"
            CREATE TABLE IF NOT EXISTS user_info (
                id              serial          not null,
                surname         varchar(50)     not null,
                name            varchar(50)     not null,
                middle_name     varchar(50)         null,
                birth_date      date            not null,
                gender          smallint        not null, 
                rank            smallint        not null, 
                status          smallint        not null, 
                email           varchar             null, 
                user_avatar     bytea               null,
            PRIMARY KEY (id));   
        ");
    }

    public override void Down()
    {
        Execute.Sql(@"DROP TABLE IF EXISTS user_info;");
    }
}