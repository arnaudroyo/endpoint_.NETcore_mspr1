using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ENDPOINT
{
    class Code
    {
        public int numeroPromo { get; set; }
        public int pourcentage { get; set; }
        public DateTime dateFin { get; set; }
        public int seuilMinimum { get; set; }
        public string Qrcode { get; set; }


        public Code(int numeroPromo, int pourcentage, DateTime dateFin, int seuilMinimum, string Qrcode)
        {
            this.numeroPromo = numeroPromo;
            this.pourcentage = pourcentage;
            this.dateFin = dateFin;
            this.seuilMinimum = seuilMinimum;
            this.Qrcode = Qrcode;
        }


        public Code()
        {
        }
    }
}
