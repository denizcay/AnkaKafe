using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnkaKafe.Data
{
    public class Siparis
    {
        public int MasaNo { get; set; }
        public List<SiparisDetay> SiparisDetaylar { get; set; } = new List<SiparisDetay>();
        public SiparisDurum Durum { get; set; } // = SiparisDurum.Aktif; -> default zaten ama ekleyebilirsin 
        public DateTime? AcilisZamani { get; set; } = DateTime.Now;
        public DateTime? KapanisZamani { get; set; }
        public decimal OdenenTutar { get; set; }
        public string ToplamTutarTL => "₺" + ToplamTutar().ToString();


        public decimal ToplamTutar()
        {
            //decimal toplam = 0;
            //foreach (SiparisDetay item in SiparisDetaylar)
            //{
            //    toplam += item.Tutar();
            //}
            //return toplam;

            return SiparisDetaylar.Sum(x => x.Tutar());
        }
    }
}
