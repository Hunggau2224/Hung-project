
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DemoMVC.Migrations
{
    public partial class ThemKhoaVaLienKetSinhVien : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            // 1. Tạo bảng Khoas
            migrationBuilder.CreateTable(
                name: "Khoas",
                columns: table => new
                {
                    Id = table.Column<int>(
                        type: "int",
                        nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),

                    TenKhoa = table.Column<string>(
                        type: "nvarchar(100)",
                        maxLength: 100,
                        nullable: false),

                    MoTa = table.Column<string>(
                        type: "nvarchar(200)",
                        maxLength: 200,
                        nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Khoas", x => x.Id);
                });

            // 2. Thêm cột KhoaId, tạm thời cho phép NULL
            migrationBuilder.AddColumn<int>(
                name: "KhoaId",
                table: "SinhViens",
                type: "int",
                nullable: true);

            // 3. Tạo khoa mặc định và lấy đúng Id được SQL Server cấp
            migrationBuilder.Sql(@"
                SET IDENTITY_INSERT [Khoas] ON;

                INSERT INTO [Khoas] ([Id], [TenKhoa], [MoTa])
                VALUES (
                    1,
                    N'Chưa phân khoa',
                    N'Khoa mặc định cho sinh viên chưa được phân khoa'
                );

                SET IDENTITY_INSERT [Khoas] OFF;
            ");

            // 4. Gán khoa mặc định cho sinh viên hiện có
            migrationBuilder.Sql(@"
                UPDATE [SinhViens]
                SET [KhoaId] = 1
                WHERE [KhoaId] IS NULL;
            ");

            // 5. Chuyển KhoaId thành trường bắt buộc
            migrationBuilder.AlterColumn<int>(
                name: "KhoaId",
                table: "SinhViens",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            // 6. Tạo chỉ mục cho khóa ngoại
            migrationBuilder.CreateIndex(
                name: "IX_SinhViens_KhoaId",
                table: "SinhViens",
                column: "KhoaId");

            // 7. Tạo khóa ngoại SinhViens -> Khoas
            migrationBuilder.AddForeignKey(
                name: "FK_SinhViens_Khoas_KhoaId",
                table: "SinhViens",
                column: "KhoaId",
                principalTable: "Khoas",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            // Xóa khóa ngoại trước
            migrationBuilder.DropForeignKey(
                name: "FK_SinhViens_Khoas_KhoaId",
                table: "SinhViens");

            // Xóa chỉ mục
            migrationBuilder.DropIndex(
                name: "IX_SinhViens_KhoaId",
                table: "SinhViens");

            // Xóa cột KhoaId
            migrationBuilder.DropColumn(
                name: "KhoaId",
                table: "SinhViens");

            // Xóa bảng Khoas
            migrationBuilder.DropTable(
                name: "Khoas");
        }
    }
}
