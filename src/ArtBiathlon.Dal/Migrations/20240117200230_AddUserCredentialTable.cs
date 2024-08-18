using FluentMigrator;

namespace ArtBiathlon.Dal.Migrations;

[Migration(20231129220535, TransactionBehavior.None)]
public class AddUserCredentialTable: Migration {
    public override void Up()
    {
        Execute.Sql($@"
            CREATE TABLE IF NOT EXISTS user_credential (
                user_info_id    serial          not null,
                login           varchar(20)     not null,
                password        bytea           not null, 
            PRIMARY KEY (user_info_id));   
        ");
    }

    public override void Down()
    {
        Execute.Sql(@"DROP TABLE IF EXISTS user_credential;");
    }
}