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
        private readonly BindingList<SiparisDetay> _blSiparisDetaylar;
        public SiparisForm(KafeVeri kafeVeri, Siparis siparis)
        {
            //yeni bir siparis formu olusturulurken bu parametreler zorunlu
            _db = kafeVeri;
            _siparis = siparis;
            _blSiparisDetaylar = new BindingList<SiparisDetay>(siparis.SiparisDetaylar);
            InitializeComponent();
            UrunleriGoster();
            MasaNoGuncelle();
            FiyatGuncelle();
            DetaylariListele();
            _blSiparisDetaylar.ListChanged += _blSiparisDetaylar_ListChanged;

        }

        private void _blSiparisDetaylar_ListChanged(object sender, ListChangedEventArgs e)
        {
            FiyatGuncelle();
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
            // _blSiparisDetaylar icinde _siparis.siparisDetaylar'i da icerdigi icin ayni zamanda Form'da gelen _siparis nesnesinin detaylarina da bu detayi ekler
            //DataGridView'i kendindeki verilerin degistigi konusunda bilgilendirir.
            _blSiparisDetaylar.Add(siparisDetay);
        }

        private void DetaylariListele()
        {
            dgvData.DataSource = _blSiparisDetaylar;
        }

        private void dgvData_UserDeletingRow(object sender, DataGridViewRowCancelEventArgs e)
        {
            DialogResult dr = MessageBox.Show(
                "Secili siparis detaylari silinecektir. Emin misiniz?", //text
                "Silme", //caption
                MessageBoxButtons.YesNo, //buttons
                icon: MessageBoxIcon.Exclamation, //degerleri isimleriyle girebiliriz
                defaultButton: MessageBoxDefaultButton.Button2
                );
            e.Cancel = dr == DialogResult.No;
        }
    }
}
