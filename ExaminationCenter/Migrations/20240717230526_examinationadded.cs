using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ExaminationCenter.Migrations
{
    /// <inheritdoc />
    public partial class examinationadded : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "examinations",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    bloodType = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    rt_eye = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    rt_eye_note = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    lt_eye = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    lt_eye_note = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    color_disc = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    color_disc_note = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    rt_ear = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    rt_ear_note = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    lt_ear = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    lt_ear_note = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    vocal = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    vocal_note = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    bp = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    bp_note = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    cvs = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    cvs_note = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    resp = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    resp_note = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    cns = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    cns_note = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    git = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    git_note = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    psyc = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    psyc_note = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    dis = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    dis_note = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    hep_b = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    hep_b_note = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    hep_c = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    hep_c_note = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    hiv = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    hiv_note = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    xray = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    xray_note = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    urine = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    urine_note = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    cbc = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    cbc_note = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    lft = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    lft_note = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    uc = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    uc_note = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    fbs = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    fbs_note = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    narcotics = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    narcotics_note = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    mso = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    mso_note = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    others = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    others_note = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_examinations", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "examinations");
        }
    }
}
