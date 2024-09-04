using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Atm_Rod_DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class Data : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("INSERT INTO [dbo].[Accounts]([Name],[LastName],[Balance],[CreatedBy],[CreatedAt],[LastModifiedBy],[LastModified])VALUES ('John','Doe',200000,'Admin',CURRENT_TIMESTAMP,'Admin',CURRENT_TIMESTAMP)");
            migrationBuilder.Sql("INSERT INTO [dbo].[Accounts]([Name],[LastName],[Balance],[CreatedBy],[CreatedAt],[LastModifiedBy],[LastModified])VALUES ('Peter','Panda',400000,'Admin',CURRENT_TIMESTAMP,'Admin',CURRENT_TIMESTAMP)");
            migrationBuilder.Sql("INSERT INTO [dbo].[Accounts]([Name],[LastName],[Balance],[CreatedBy],[CreatedAt],[LastModifiedBy],[LastModified])VALUES ('Juan','Garcia',500000,'Admin',CURRENT_TIMESTAMP,'Admin',CURRENT_TIMESTAMP)");
            migrationBuilder.Sql("INSERT INTO [dbo].[Accounts]([Name],[LastName],[Balance],[CreatedBy],[CreatedAt],[LastModifiedBy],[LastModified])VALUES ('Pedro','Torres',600000,'Admin',CURRENT_TIMESTAMP,'Admin',CURRENT_TIMESTAMP)");
            migrationBuilder.Sql("INSERT INTO [dbo].[Cards]([Number],[Pin],[State],[AccountID],[IsBlocked],[LoginCounter],[CreatedBy],[CreatedAt],[LastModifiedBy],[LastModified]) VALUES(2042,4220,1,1,0,0,'Admin',CURRENT_TIMESTAMP,'Admin',CURRENT_TIMESTAMP)");
            migrationBuilder.Sql("INSERT INTO [dbo].[Cards]([Number],[Pin],[State],[AccountID],[IsBlocked],[LoginCounter],[CreatedBy],[CreatedAt],[LastModifiedBy],[LastModified]) VALUES(1234,3333,1,2,0,0,'Admin',CURRENT_TIMESTAMP,'Admin',CURRENT_TIMESTAMP)");
            migrationBuilder.Sql("INSERT INTO [dbo].[Cards]([Number],[Pin],[State],[AccountID],[IsBlocked],[LoginCounter],[CreatedBy],[CreatedAt],[LastModifiedBy],[LastModified]) VALUES(2445,1222,1,3,0,0,'Admin',CURRENT_TIMESTAMP,'Admin',CURRENT_TIMESTAMP)");
            migrationBuilder.Sql("INSERT INTO [dbo].[Cards]([Number],[Pin],[State],[AccountID],[IsBlocked],[LoginCounter],[CreatedBy],[CreatedAt],[LastModifiedBy],[LastModified]) VALUES(6666,4333,1,4,0,0,'Admin',CURRENT_TIMESTAMP,'Admin',CURRENT_TIMESTAMP)");
            migrationBuilder.Sql("INSERT INTO [dbo].[Transactions]([AccountID],[TransacType],[Amount],[CreatedBy],[CreatedAt],[LastModifiedBy],[LastModified])VALUES(1,1,1000,'Admin',CURRENT_TIMESTAMP,'Admin',CURRENT_TIMESTAMP)");
            migrationBuilder.Sql("INSERT INTO [dbo].[Transactions]([AccountID],[TransacType],[Amount],[CreatedBy],[CreatedAt],[LastModifiedBy],[LastModified])VALUES(2,1,1000,'Admin',CURRENT_TIMESTAMP,'Admin',CURRENT_TIMESTAMP)");
            migrationBuilder.Sql("INSERT INTO [dbo].[Transactions]([AccountID],[TransacType],[Amount],[CreatedBy],[CreatedAt],[LastModifiedBy],[LastModified])VALUES(3,1,1000,'Admin',CURRENT_TIMESTAMP,'Admin',CURRENT_TIMESTAMP)");
            migrationBuilder.Sql("INSERT INTO [dbo].[Transactions]([AccountID],[TransacType],[Amount],[CreatedBy],[CreatedAt],[LastModifiedBy],[LastModified])VALUES(4,1,1000,'Admin',CURRENT_TIMESTAMP,'Admin',CURRENT_TIMESTAMP)");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
