using Microsoft.EntityFrameworkCore.Migrations;

namespace WebApi.Migrations
{
    public partial class AddSpAdicionarAmigo : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"
                CREATE PROCEDURE [dbo].[AdicionarAmigo]
	                @nome nvarchar(max),
	                @sobrenome nvarchar(max),
	                @email nvarchar(max),
	                @telefone nvarchar(max),
	                @dataDeAniversario datetime2(7)
                AS
	                INSERT INTO [dbo].[Amigo] ([Nome], [Sobrenome], [Email], [Telefone], [DataDeAniversario]) 
	                VALUES (@nome, @sobrenome, @email, @telefone, @dataDeAniversario)
                RETURN 0
            ");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"
                DROP PROCEDURE [dbo].[AdicionarAmigo]
            ");
        }
    }
}
