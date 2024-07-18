using System.ComponentModel.DataAnnotations;

namespace ExaminationCenter.Models
{
    public class Examination
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public string bloodType { get; set; }

        public string rt_eye { get; set; }
        public string? rt_eye_note { get; set; }

        public string lt_eye { get; set; }
        public string? lt_eye_note { get; set; }

        public string color_disc { get; set; }
        public string? color_disc_note { get; set; }

        public string rt_ear { get; set; }
        public string? rt_ear_note { get;set; }

        public string lt_ear { get; set; }
        public string? lt_ear_note { get; set; }


        public string vocal { get; set; }
        public string? vocal_note { get; set; }

        public string bp { get; set; }
        public string? bp_note { get; set; }

        public string cvs { get; set; }
        public string? cvs_note { get; set; }

        public string resp { get; set; }
        public string? resp_note { get; set; }

        public string cns { get; set; }
        public string? cns_note { get; set; }

        public string git { get; set; }
        public string? git_note { get; set; }

        public string psyc { get; set; }
        public string? psyc_note { get; set; }

        public string dis { get; set; }
        public string? dis_note { get; set; }

        public string hep_b { get; set; }
        public string? hep_b_note { get; set; }

        public string hep_c { get; set; }
        public string? hep_c_note { get; set; }

        public string hiv { get; set; }
        public string? hiv_note { get; set; }

        public string xray { get; set; }
        public string? xray_note { get; set; }

        public string urine { get; set; }
        public string? urine_note { get; set; }

        public string cbc { get; set; }
        public string? cbc_note { get; set; }

        public string lft { get; set; }
        public string? lft_note { get; set; }

        public string uc { get; set; }
        public string? uc_note { get; set; }

        public string fbs { get; set; }
        public string? fbs_note { get; set; }

        public string narcotics { get; set; }
        public string? narcotics_note { get; set; }

        public string mso { get; set; }
        public string? mso_note { get; set; }

        public string others { get; set; }
        public string? others_note { get; set; }
    }
}
