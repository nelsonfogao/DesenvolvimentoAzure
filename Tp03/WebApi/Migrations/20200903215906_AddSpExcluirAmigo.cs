﻿using Microsoft.EntityFrameworkCore.Migrations;

namespace WebApi.Migrations
{
    public partial class AddSpExcluirAmigo : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"
                CREATE PROCEDURE[dbo].[ExcluirAmigo]
                    @id int
                AS
                    DELETE FROM Amigo WHERE id = @id
                RETURN 0
            ");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"
                DROP PROCEDURE[dbo].[ExcluirAmigo]
            ");
        }
    }
}
