using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Project_Test
{
    public partial class About : Page
    {
        TeknikalTestEntities entities = new TeknikalTestEntities();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                LoadData();                
            }
        }

        private void LoadData()
        {
            try
            {
                var data = entities.TBL_PRODUKSI
                     .OrderByDescending(x => x.dCreatedAt)
                     .ToList();

                rptRiwayat.DataSource = data;
                rptRiwayat.DataBind();

                viewRiwayatProduksi.Visible = true;
                formAdd.Visible = false;
            }
            catch (Exception ex)
            {
                ex.ToString();
            }
        }

        protected void btnProses_Click(object sender, EventArgs e)
        {
            try
            {
                int[] input = new int[7];
                try
                {
                    input[0] = int.Parse(txtSenin.Text);
                    input[1] = int.Parse(txtSelasa.Text);
                    input[2] = int.Parse(txtRabu.Text);
                    input[3] = int.Parse(txtKamis.Text);
                    input[4] = int.Parse(txtJumat.Text);
                    input[5] = int.Parse(txtSabtu.Text);
                    input[6] = int.Parse(txtMinggu.Text);
                }
                catch
                {
                    ltHasil.Text = "Pastikan semua input adalah angka!";
                    return;
                }

                var hariAktif = input
                    .Select((val, idx) => new { val, idx })
                    .Where(x => x.val > 0)
                    .ToList();

                int jumlahHariAktif = hariAktif.Count;
                int total = hariAktif.Sum(x => x.val);
                int rata = total / jumlahHariAktif;
                int sisa = total % jumlahHariAktif;

                int[] hasil = new int[7];

                foreach (var h in hariAktif)
                {
                    hasil[h.idx] = rata;
                }

                var prioritas = hariAktif
                    .OrderByDescending(x => x.val)
                    .Select(x => x.idx)
                    .ToList();

                for (int i = 0; i < sisa; i++)
                {
                    hasil[prioritas[i]] += 1;
                }

                // Ini Output
                ltHasil.Text = "<b>Rencana Produksi yang Diratakan:</b><br/>" +
                    $"Senin: {hasil[0]}<br/>" +
                    $"Selasa: {hasil[1]}<br/>" +
                    $"Rabu: {hasil[2]}<br/>" +
                    $"Kamis: {hasil[3]}<br/>" +
                    $"Jumat: {hasil[4]}<br/>" +
                    $"Sabtu: {hasil[5]}<br/>" +
                    $"Minggu: {hasil[6]}";

                ViewState["Input"] = input;
                ViewState["Hasil"] = hasil;

                btnSimpan.Visible = true;
            }
            catch (Exception ex)
            {
                ex.ToString();
            }
        }

        private void ClearData()
        {
            txtTanggalProduksi.Text = string.Empty;
            txtSenin.Text = string.Empty;
            txtSelasa.Text = string.Empty;
            txtRabu.Text = string.Empty;
            txtKamis.Text = string.Empty;
            txtJumat.Text = string.Empty;
            txtSabtu.Text = string.Empty;
            txtMinggu.Text = string.Empty;
            ltHasil.Text = string.Empty;
            btnSimpan.Visible = false;
        }

        protected void btnAddForm_Click(object sender, EventArgs e)
        {
            viewRiwayatProduksi.Visible = false;
            formAdd.Visible = true;
            ClearData();
        }

        private void SimpanKeDatabase(int[] input, int[] hasil)
        {
            var data = new TBL_PRODUKSI()
            {
                iSenin = input[0],
                iSelasa = input[1],
                iRabu = input[2],
                iKamis = input[3],
                iJumat = input[4],
                iSabtu = input[5],
                iMinggu = input[6],
                iSeninFix = hasil[0],
                iSelasaFix = hasil[1],
                iRabuFix = hasil[2],
                iKamisFix = hasil[3],
                iJumatFix = hasil[4],
                iSabtuFix = hasil[5],
                iMingguFix = hasil[6],
                dCreatedAt = DateTime.Parse(txtTanggalProduksi.Text),
                iIsActive = 1,
                dCreatedDate = DateTime.Now
            };
            entities.TBL_PRODUKSI.Add(data);
            entities.SaveChanges();
        }

        protected void rptRiwayat_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            int id = int.Parse(e.CommandArgument.ToString());

            if (e.CommandName == "detail")
            {
                ClearData();                
            }
            else if (e.CommandName == "hapus")
            {
                var item = entities.TBL_PRODUKSI.Find(id);
                if (item != null)
                {
                    item.iIsActive = 0;
                    item.dModifiedDate = DateTime.Now;
                    entities.SaveChanges();
                    LoadData();
                }
            }
        }

        protected void btnSimpan_Click(object sender, EventArgs e)
        {
            try
            {
                if (ViewState["Input"] != null && ViewState["Hasil"] != null)
                {
                    int[] input = (int[])ViewState["Input"];
                    int[] hasil = (int[])ViewState["Hasil"];

                    SimpanKeDatabase(input, hasil);
                    LoadData();

                    ScriptManager.RegisterStartupScript(this, GetType(), "sukses",
                    "Swal.fire({ icon: 'success', title: 'Berhasil', text: 'Data berhasil disimpan!' });", true);
                }
                else
                {
                    ltHasil.Text = "Silakan proses data terlebih dahulu sebelum menyimpan.";
                }
            }
            catch(Exception ex)
            {
                ex.ToString();
                ScriptManager.RegisterStartupScript(this, GetType(), "gagal",
                "Swal.fire({ icon: 'error', title: 'Oops!', text: 'Terjadi kesalahan saat menyimpan!' });", true);
            }
        }
    }
}