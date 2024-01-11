using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Ticari_Otomasyon
{
    public partial class FrmMusteriler : Form
    {
        public FrmMusteriler()
        {
            InitializeComponent();
        }
        sqlbaglantisi bgl = new sqlbaglantisi();

        void listele()
        {
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter("Select * From TBL_MUSTERILER", bgl.baglanti());
            da.Fill(dt);
            gridControl1.DataSource = dt;
        }
        void ilGetir()
        {
            SqlCommand command = new SqlCommand ("Select SEHIR From TBL_ILLER", bgl.baglanti());
            SqlDataReader dr = command.ExecuteReader();
            while (dr.Read())
            {
                CmbIl.Properties.Items.Add(dr[0]);
            }
            bgl.baglanti().Close();
        }
        private void FrmMusteriler_Load(object sender, EventArgs e)
        {
            listele();
            ilGetir();
        }

        private void CmbIl_SelectedIndexChanged(object sender, EventArgs e)
        {
            CmbIlce.Properties.Items.Clear();
            SqlCommand cmd = new SqlCommand("Select ILCE From TBL_ILCELER where SEHIR = @p1", bgl.baglanti());
            cmd.Parameters.AddWithValue("@p1",CmbIl.SelectedIndex+1);
            SqlDataReader dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                CmbIlce.Properties.Items.Add(dr[0]);
            }
            bgl.baglanti().Close();
        }

        private void BtnKaydet_Click(object sender, EventArgs e)
        {
            SqlCommand cmd = new SqlCommand("insert into TBL_MUSTERILER (AD,SOYAD,TELEFON,TELEFON2,TC,MAIL,IL,ILCE,ADRES,VERGIDAIRE) values (@p1,@p2,@p3,@p4,@p5,@p6,@p7,@p8,@p9,@p10)",bgl.baglanti());
            cmd.Parameters.AddWithValue("@p1", TxtAd.Text);
            cmd.Parameters.AddWithValue("@p2", TxtSoyad.Text);
            cmd.Parameters.AddWithValue("@p3", MskTelefon1.Text);
            cmd.Parameters.AddWithValue("@p4", MskTelefon2.Text);
            cmd.Parameters.AddWithValue("@p5", MskTC.Text);
            cmd.Parameters.AddWithValue("@p6", TxtMail.Text);
            cmd.Parameters.AddWithValue("@p7", CmbIl.Text);
            cmd.Parameters.AddWithValue("@p8", CmbIlce.Text);
            cmd.Parameters.AddWithValue("@p9", RchAdres.Text);
            cmd.Parameters.AddWithValue("@p10", TxtVergiD.Text);
            cmd.ExecuteNonQuery();
            bgl.baglanti().Close();
            MessageBox.Show("Müşteri sisteme eklendi.", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
            listele();
        }

        private void gridView1_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            DataRow dr = gridView1.GetDataRow(gridView1.FocusedRowHandle);
            if(dr != null)
            {
                TxtID.Text = dr["ID"].ToString();
                TxtAd.Text = dr["AD"].ToString();
                TxtSoyad.Text = dr["SOYAD"].ToString();
                MskTelefon1.Text = dr["TELEFON"].ToString();
                MskTelefon2.Text = dr["TELEFON2"].ToString();
                MskTC.Text = dr["TC"].ToString();
                TxtMail.Text = dr["MAIL"].ToString();
                CmbIl.Text = dr["IL"].ToString();
                CmbIlce.Text = dr["ILCE"].ToString();
                RchAdres.Text = dr["ADRES"].ToString();
                TxtVergiD.Text = dr["VERGIDAIRE"].ToString();
            }
        }

        private void BtnSil_Click(object sender, EventArgs e)
        {
            SqlCommand cmd = new SqlCommand("Delete From TBL_MUSTERILER where ID = @p1",bgl.baglanti());
            cmd.Parameters.AddWithValue("@p1",TxtID.Text);
            if(MessageBox.Show($"{TxtAd.Text} {TxtSoyad.Text} isimli müşteriyi sistemden silmek istediğinizden emin misiniz?", "Müşteri Silinmek Üzere", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning) == DialogResult.OK)
            {
                cmd.ExecuteNonQuery();
                MessageBox.Show($"{TxtAd.Text} {TxtSoyad.Text} isimli müşteri sistemden silindi", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            bgl.baglanti().Close();
            
            listele();
        }

        private void BtnGuncelle_Click(object sender, EventArgs e)
        {
            SqlCommand cmd = new SqlCommand("Update TBL_MUSTERILER set AD=@p1, SOYAD=@p2, TELEFON=@p3, TELEFON2=@p4, TC=@p5, MAIL=@p6, IL=@p7, ILCE=@p8, ADRES=@p9, VERGIDAIRE=@p10 where ID=@p11",bgl.baglanti());
            cmd.Parameters.AddWithValue("@p11", TxtID.Text);
            cmd.Parameters.AddWithValue("@p1",TxtAd.Text);
            cmd.Parameters.AddWithValue("@p2",TxtSoyad.Text);
            cmd.Parameters.AddWithValue("@p3",MskTelefon1.Text);
            cmd.Parameters.AddWithValue("@p4",MskTelefon2.Text);
            cmd.Parameters.AddWithValue("@p5",MskTC.Text);
            cmd.Parameters.AddWithValue("@p6",TxtMail.Text);
            cmd.Parameters.AddWithValue("@p7",CmbIl.Text);
            cmd.Parameters.AddWithValue("@p8",CmbIlce.Text);
            cmd.Parameters.AddWithValue("@p9",RchAdres.Text);
            cmd.Parameters.AddWithValue("@p10",TxtVergiD.Text);
            cmd.ExecuteNonQuery();
            MessageBox.Show("Müşteri bilgileri güncellendi.", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
            bgl.baglanti().Close();
            listele();
        }
    }
}
