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
    public partial class UrunlerForm : Form
    {
        private readonly KafeVeri _db;
        private readonly BindingList<Urun> _blUrunler;
        public UrunlerForm(KafeVeri db)
        {
            _db = db;
            _blUrunler = new BindingList<Urun>(_db.Urunler);
            InitializeComponent();
            // dgvUrunler.AutoGenerateColumns = false;
            UrunleriListele();
        }

        private void UrunleriListele()
        {
            dgvUrunler.DataSource = _blUrunler;
        }

        private void btnUrunEkle_Click(object sender, EventArgs e)
        {
            string urunAd = txtUrunAdi.Text.Trim();
            if (urunAd == "")
            {
                MessageBox.Show("Lütfen bir ürün adı giriniz.");
                return;
            }
            if (_duzenlenen == null) //duzenlenen yoksa ekle
            {
                _blUrunler.Add(new Urun()
                {
                    UrunAd = urunAd,
                    BirimFiyat = nudBirimFiyat.Value
                });
            }
            else // duzenlenen varsa guncelle
            {
                _duzenlenen.UrunAd = urunAd;
                _duzenlenen.BirimFiyat = nudBirimFiyat.Value;
                _blUrunler.ResetBindings();
            }
            EkleFormuSifirla();
        }

        private void EkleFormuSifirla()
        {
            txtUrunAdi.Clear();
            nudBirimFiyat.Value = 0;
            btnUrunEkle.Text = "EKLE";
            btnIptal.Hide();
            _duzenlenen = null;
        }

        private void dgvUrunler_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Delete && dgvUrunler.SelectedRows.Count > 0)
            {
                DialogResult dr = MessageBox.Show(
                    "Seçili ürün silinecektir. Onaylıyor musunuz?",
                    "Ürün sılme onayı",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Exclamation,
                    MessageBoxDefaultButton.Button2);
                if (dr == DialogResult.Yes)
                {
                    //secili satira bagli urunu al
                    Urun urun = (Urun)dgvUrunler.SelectedRows[0].DataBoundItem;
                    _blUrunler.Remove(urun);
                }

            }
        }

        Urun _duzenlenen;
        private void btnGuncelle_Click(object sender, EventArgs e)
        {
            if (dgvUrunler.SelectedRows.Count == 0)
            {
                MessageBox.Show("Ürün güncellemek için önce ürün seçmelisiniz.");
                return;
            }
            _duzenlenen = (Urun)dgvUrunler.SelectedRows[0].DataBoundItem;
            txtUrunAdi.Text = _duzenlenen.UrunAd;
            nudBirimFiyat.Value = _duzenlenen.BirimFiyat;
            btnUrunEkle.Text = "KAYDET";
            btnIptal.Show();
        }

        private void btnIptal_Click(object sender, EventArgs e)
        {
            EkleFormuSifirla();
        }
    }
}

