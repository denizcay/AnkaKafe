using AnkaKafe.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AnkaKafe.UI
{
    public partial class SiparisForm : Form
    {
        private readonly KafeVeri _db;
        private readonly Siparis _siparis;
        public SiparisForm(KafeVeri kafeVeri, Siparis siparis)
        {
            //yeni bir siparis formu olusturulurken bu parametreler zorunlu
            _db = kafeVeri;
            _siparis = siparis;
            InitializeComponent();
            UrunleriGoster();
            MasaNoGuncelle();
            FiyatGuncelle();
            DetaylariListele();
        }

        private void UrunleriGoster()
        {
            cbUrun.DataSource = _db.Urunler;
        }

        private void FiyatGuncelle()
        {
            lblTutar.Text = _siparis.ToplamTutarTL;
            
        }

        private void MasaNoGuncelle()
        {
            Text = $"Masa {_siparis.MasaNo} Siparis Bilgileri";
            lblMasaNo.Text = _siparis.MasaNo.ToString("00");
        }

        private void btnEkle_Click(object sender, EventArgs e)
        {
            Urun urun = (Urun)cbUrun.SelectedItem;
            SiparisDetay siparisDetay = new SiparisDetay()
            {
                UrunAd = urun.UrunAd,
                BirimFiyat = urun.BirimFiyat,
                Adet = (int)nudAdet.Value

            };
            _siparis.SiparisDetaylar.Add(siparisDetay);
            DetaylariListele();
            FiyatGuncelle();
       

        }

        private void DetaylariListele()
        {
            dgvData.DataSource = null;
            dgvData.DataSource = _siparis.SiparisDetaylar;
        }
    }
}
