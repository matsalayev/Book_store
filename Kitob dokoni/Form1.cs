using Npgsql;
using System.Data;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace Kitob_dokoni
{
    public partial class Form1 : Form
    {
        private string connectionString = String.Format("Server = localhost; Port=5432; User Id = postgres; Password = matsalayev; Database = kitob_dokoni;");
        private NpgsqlConnection conn;
        private string sql;
        private NpgsqlCommand cmd; 
        private DataTable data;
        private int rowIndex = -1;
        public Form1()
        {
            InitializeComponent();
        }
        public const int WM_NCLBUTTONDOWN = 0xA1;
        public const int HT_CAPTION = 0x2;
        [DllImportAttribute("user32.dll")]
        public static extern int SendMessage(IntPtr hWnd, int Msg, int wParam, int lParam);
        [DllImportAttribute("user32.dll")]
        public static extern bool ReleaseCapture();
        private void Form1_MouseDown(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                ReleaseCapture();
                SendMessage(Handle, WM_NCLBUTTONDOWN, HT_CAPTION, 0);
            }
        }
        bool tekshir(string a)
        {
            bool f = true;
            for (int i = 0; i < a.Length; i++)
            {
                if ((int)a[i] < 48 || (int)a[i] > 57) f = false;
            }
            return f;
        }
        private void btnExit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
        private void Select()
        {
            try
            {
                conn.Open();
                sql = @"select k.nomi as kitob, m.nomi as muallif, b.nomi as bolim, t.nomi as til, k.narxi, d.soni from kitoblar k 
                            inner join mualliflar m on m.id=k.muallif_id 
                            inner join bolimlar b on b.id=k.bolim_id 
                            inner join tillar t on t.id=k.til_id
                            inner join hisob d on d.kitob_id=k.id";
                cmd = new NpgsqlCommand(sql, conn);
                data = new DataTable();
                data.Load(cmd.ExecuteReader());
                conn.Close();
                kitoblarData.DataSource = null;
                kitoblarData.DataSource = data;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void btnMin_Click(object sender, EventArgs e)
        {
            WindowState = FormWindowState.Minimized;
        }
        private void lblKitoblar_Click(object sender, EventArgs e)
        {
            Select();
            panelb();
            lblKitoblar.ForeColor = Color.Black;
            pnlKitoblar.BackColor = Color.Black;
            Kitoblar.Location = new Point(0, 90);
        }
        private void lblBolimlar_Click(object sender, EventArgs e)
        {
            try
            {
                conn.Open();
                sql = @"select b.nomi as bolim from bolimlar b";
                cmd = new NpgsqlCommand(sql, conn);
                data = new DataTable();
                data.Load(cmd.ExecuteReader());
                conn.Close();
                BolimData.DataSource = null;
                BolimData.DataSource = data;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            panelb();
            lblBolimlar.ForeColor = Color.Black;
            pnlBolimlar.BackColor = Color.Black;
            Bolim.Location = new Point(0, 90);
        }
        private void lblMualliflar_Click(object sender, EventArgs e)
        {
            try
            {
                conn.Open();
                sql = @"select m.nomi as muallif from mualliflar m";
                cmd = new NpgsqlCommand(sql, conn);
                data = new DataTable();
                data.Load(cmd.ExecuteReader());
                conn.Close();
                MuallifData.DataSource = null;
                MuallifData.DataSource = data;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            panelb();
            lblMualliflar.ForeColor = Color.Black;
            pnlMualliflar.BackColor = Color.Black;
            Mualliflar.Location = new Point(0, 90);
        }
        private void lblTillar_Click(object sender, EventArgs e)
        {
            try
            {
                conn.Open();
                sql = @"select t.nomi as til from tillar t";
                cmd = new NpgsqlCommand(sql, conn);
                data = new DataTable();
                data.Load(cmd.ExecuteReader());
                conn.Close();
                TilData.DataSource = null;
                TilData.DataSource = data;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            panelb();
            lblTillar.ForeColor = Color.Black;
            pnlTillar.BackColor = Color.Black;
            Til.Location = new Point(0, 90);
        }
        void panelb()
        {
            lblKitoblar.ForeColor = Color.DimGray;
            pnlKitoblar.BackColor = Color.DimGray;
            lblBolimlar.ForeColor = Color.DimGray;
            pnlBolimlar.BackColor = Color.DimGray;
            lblMualliflar.ForeColor = Color.DimGray;
            pnlMualliflar.BackColor = Color.DimGray;
            lblTillar.ForeColor = Color.DimGray;
            pnlTillar.BackColor = Color.DimGray;
            Kitoblar.Location = new Point(-1545, 90);
            Bolim.Location = new Point(-1545, 90);
            Mualliflar.Location = new Point(-1545, 90);
            Til.Location = new Point(-1545, 90);
        }
        private void lblBarcha_Click(object sender, EventArgs e)
        {
            panelk();
            lblBarcha.ForeColor = Color.Black;
            pnlBarcha.BackColor = Color.Black;
            Barcha.Location = new Point(77, 114);
            Select();
        }
        private void lblKirim_Click(object sender, EventArgs e)
        {
            panelk();
            lblKirim.ForeColor = Color.Black;
            pnlKirim.BackColor = Color.Black;
            Kirim.Location = new Point(77, 114);
        }
        private void lblChiqim_Click(object sender, EventArgs e)
        {
            panelk();
            lblChiqim.ForeColor = Color.Black;
            pnlChiqim.BackColor = Color.Black;
            chiqim.Location = new Point(77, 114);
            try
            {
                conn.Open();
                sql = @"select k.nomi as kitob, m.nomi as muallif, b.nomi as bolim, t.nomi as til, k.narxi, h.soni from kitoblar k 
                            inner join mualliflar m on m.id=k.muallif_id 
                            inner join bolimlar b on b.id=k.bolim_id 
                            inner join tillar t on t.id=k.til_id
                            inner join hisob h on h.kitob_id=k.id";
                cmd = new NpgsqlCommand(sql, conn);
                data = new DataTable();
                data.Load(cmd.ExecuteReader());
                conn.Close();
                STavsiyalar.DataSource = null;
                STavsiyalar.DataSource = data;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void lblHisob_Click(object sender, EventArgs e)
        {
            panelk();
            lblHisob.ForeColor = Color.Black;
            pnlHisob.BackColor = Color.Black;
            hisob.Location = new Point(77, 114);

            try
            {
                conn.Open();
                sql = @"select k.narxi, h.soni from kitoblar k inner join hisob h on h.kitob_id=k.id";
                cmd = new NpgsqlCommand(sql, conn);
                data = new DataTable();
                data.Load(cmd.ExecuteReader());
                conn.Close();
                int son = 0;
                int narx = 0;
                for (int i = 0; i < data.Rows.Count; i++)
                {
                    var list = data.Rows[i].ItemArray.ToList();
                    son += int.Parse(list[1].ToString());
                    narx += int.Parse(list[0].ToString()) * int.Parse(list[1].ToString());
                }
                MavjudSon.Text = son.ToString();
                MavjudNarx.Text = som(narx);
            }
            catch { }
        }
        string som(int a)
        {
            string str = a.ToString();
            string str1 = "";
            string str2 = "";
            int j = 0;
            for (int i = str.Length - 1; i >= 0; i--)
            {
                if (j != 0 && j % 3 == 0) str1 += ',';
                str1 += str[i].ToString();
                j++;
            }
            for (int i = str1.Length - 1; i >= 0; i--)
            {
                str2 += str1[i].ToString();
            }
            return str2;
        }
        void panelk()
        {
            lblBarcha.ForeColor = Color.DimGray;
            pnlBarcha.BackColor = Color.DimGray;
            lblKirim.ForeColor = Color.DimGray;
            pnlKirim.BackColor = Color.DimGray;
            lblChiqim.ForeColor = Color.DimGray;
            pnlChiqim.BackColor = Color.DimGray;
            lblHisob.ForeColor = Color.DimGray;
            pnlHisob.BackColor = Color.DimGray;
            Barcha.Location = new Point(-1546, 114);
            Kirim.Location = new Point(-1546, 114);
            chiqim.Location = new Point(-1546, 114);
            hisob.Location = new Point(-1546, 114);
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            conn = new NpgsqlConnection(connectionString);
            Select();
        }
        private void BolimData_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                try
                {
                    rowIndex = e.RowIndex;
                    string s = BolimData.Rows[e.RowIndex].Cells["bolim"].Value.ToString();
                    try
                    {
                        conn.Open();
                        sql = @"select k.nomi as kitob, m.nomi as muallif, t.nomi as til, k.narxi, d.soni from kitoblar k 
                                inner join mualliflar m on m.id=k.muallif_id 
                                inner join bolimlar b on b.id=k.bolim_id 
                                inner join tillar t on t.id=k.til_id
                                inner join hisob d on k.id=d.kitob_id
                                where b.nomi=" + $"'{s}'";
                        cmd = new NpgsqlCommand(sql, conn);
                        data = new DataTable();
                        data.Load(cmd.ExecuteReader());
                        conn.Close();
                        KitobBData.DataSource = null;
                        KitobBData.DataSource = data;
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                    }
                }
                catch { }
            }
        }
        private void MuallifData_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                try
                {
                    rowIndex = e.RowIndex;
                    string s = MuallifData.Rows[e.RowIndex].Cells["muallif"].Value.ToString();
                    try
                    {
                        conn.Open();
                        sql = @"select k.nomi as kitob, b.nomi as bolim, t.nomi as til, k.narxi, d.soni from kitoblar k 
                                    inner join mualliflar m on m.id=k.muallif_id 
                                    inner join bolimlar b on b.id=k.bolim_id 
                                    inner join tillar t on t.id=k.til_id
                                    inner join hisob d on k.id=d.kitob_id
                                    where m.nomi=" + $"'{s}'";
                        cmd = new NpgsqlCommand(sql, conn);
                        data = new DataTable();
                        data.Load(cmd.ExecuteReader());
                        conn.Close();
                        MuallifMdata.DataSource = null;
                        MuallifMdata.DataSource = data;
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                    }
                }
                catch { }
            }
        }
        private void TilData_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                try
                {
                    rowIndex = e.RowIndex;
                    string s = TilData.Rows[e.RowIndex].Cells["til"].Value.ToString();
                    try
                    {
                        conn.Open();
                        sql = @"select k.nomi as kitob, m.nomi as muallif, b.nomi as bolim, k.narxi, d.soni from kitoblar k 
                                    inner join mualliflar m on m.id=k.muallif_id 
                                    inner join bolimlar b on b.id=k.bolim_id 
                                    inner join tillar t on t.id=k.til_id 
                                    inner join hisob d on k.id=d.kitob_id
                                    where t.nomi=" + $"'{s}'";
                        cmd = new NpgsqlCommand(sql, conn);
                        data = new DataTable();
                        data.Load(cmd.ExecuteReader());
                        conn.Close();
                        TilTdata.DataSource = null;
                        TilTdata.DataSource = data;
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                    }
                }
                catch { }
            }
        }
        private void btnMax_Click(object sender, EventArgs e)
        {
            WindowState = (WindowState == FormWindowState.Normal) ? WindowState = FormWindowState.Maximized : FormWindowState.Normal;
        }
        private void btnMin_Click_1(object sender, EventArgs e)
        {
            WindowState = FormWindowState.Minimized;
        }
        bool t = true;
        private void txtNom_Enter(object sender, EventArgs e)
        {
            try
            {
                conn.Open();
                sql = @"select k.nomi as kitob, m.nomi as muallif, b.nomi as bolim, t.nomi as til, k.narxi, h.soni from kitoblar k 
                            inner join mualliflar m on m.id=k.muallif_id 
                            inner join bolimlar b on b.id=k.bolim_id 
                            inner join tillar t on t.id=k.til_id
                            inner join hisob h on h.kitob_id = k.id";
                cmd = new NpgsqlCommand(sql, conn);
                data = new DataTable();
                data.Load(cmd.ExecuteReader());
                conn.Close();
                Tavsiyalar.Tag = "kitoblar";
                Tavsiyalar.DataSource = null;
                Tavsiyalar.DataSource = data;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void txtMuallif_Enter(object sender, EventArgs e)
        {
            try
            {
                conn.Open();
                sql = @"select m.nomi as muallif from mualliflar m";
                cmd = new NpgsqlCommand(sql, conn);
                data = new DataTable();
                data.Load(cmd.ExecuteReader());
                conn.Close();
                Tavsiyalar.Tag = "muallif";
                Tavsiyalar.DataSource = null;
                Tavsiyalar.DataSource = data;
            }
            catch { }
        }
        private void txtbolim_Enter(object sender, EventArgs e)
        {
            try
            {
                conn.Open();
                sql = @"select b.nomi as bolim from bolimlar b";
                cmd = new NpgsqlCommand(sql, conn);
                data = new DataTable();
                data.Load(cmd.ExecuteReader());
                conn.Close();
                Tavsiyalar.Tag = "bolim";
                Tavsiyalar.DataSource = null;
                Tavsiyalar.DataSource = data;
            }
            catch { }
        }
        private void txtTil_Enter(object sender, EventArgs e)
        {
            try
            {
                conn.Open();
                sql = @"select t.nomi as til from tillar t";
                cmd = new NpgsqlCommand(sql, conn);
                data = new DataTable();
                data.Load(cmd.ExecuteReader());
                conn.Close();
                Tavsiyalar.Tag = "til";
                Tavsiyalar.DataSource = null;
                Tavsiyalar.DataSource = data;
            }
            catch { }
        }
        private void Tavsiyalar_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                if (Tavsiyalar.Tag == "kitoblar")
                {
                    try
                    {
                        txtNom.Text = Tavsiyalar.Rows[e.RowIndex].Cells["kitob"].Value.ToString();
                        txtMuallif.Text = Tavsiyalar.Rows[e.RowIndex].Cells["muallif"].Value.ToString();
                        txtbolim.Text = Tavsiyalar.Rows[e.RowIndex].Cells["bolim"].Value.ToString();
                        txtTil.Text = Tavsiyalar.Rows[e.RowIndex].Cells["til"].Value.ToString();
                        txtSnarxi.Text = Tavsiyalar.Rows[e.RowIndex].Cells["narxi"].Value.ToString();
                    }
                    catch { }
                    t = false;
                }
                else if (t)
                {
                    try
                    {
                        txtNom.Text = Tavsiyalar.Rows[e.RowIndex].Cells["kitob"].Value.ToString();

                    }
                    catch { }
                    try
                    {
                        txtMuallif.Text = Tavsiyalar.Rows[e.RowIndex].Cells["muallif"].Value.ToString();
                    }
                    catch { }
                    try
                    {
                        txtbolim.Text = Tavsiyalar.Rows[e.RowIndex].Cells["bolim"].Value.ToString();
                    }
                    catch { }
                    try
                    {
                        txtTil.Text = Tavsiyalar.Rows[e.RowIndex].Cells["til"].Value.ToString();
                    }
                    catch { }
                    try
                    {
                        txtSnarxi.Text = Tavsiyalar.Rows[e.RowIndex].Cells["narxi"].Value.ToString();
                    }
                    catch { }
                }
            }
        }
        private void BtnQoshish_Click(object sender, EventArgs e)
        {
            object result;
            try
            {
                if (t)
                {
                    conn.Open();
                    sql = @"select k.id from mualliflar k where k.nomi=" + $"'{txtMuallif.Text}'";
                    cmd = new NpgsqlCommand(sql, conn);
                    data = new DataTable();
                    data.Load(cmd.ExecuteReader());
                    conn.Close();
                    var list = data.Rows[0].ItemArray.ToList();
                    int muallifID = int.Parse(list[0].ToString());
                    conn.Open();
                    sql = @"select k.id from bolimlar k where k.nomi=" + $"'{txtbolim.Text}'";
                    cmd = new NpgsqlCommand(sql, conn);
                    data = new DataTable();
                    data.Load(cmd.ExecuteReader());
                    conn.Close();
                    list = data.Rows[0].ItemArray.ToList();
                    int bolimID = int.Parse(list[0].ToString());
                    conn.Open();
                    sql = @"select k.id from tillar k where k.nomi=" + $"'{txtTil.Text}'";
                    cmd = new NpgsqlCommand(sql, conn);
                    data = new DataTable();
                    data.Load(cmd.ExecuteReader());
                    conn.Close();
                    list = data.Rows[0].ItemArray.ToList();
                    int tilID = int.Parse(list[0].ToString());
                    {
                        conn.Open();
                        sql = @"INSERT INTO kitoblar (nomi, muallif_id, bolim_id, til_id, narxi) VALUES
                            (:_kitob, :_muallif_id, :_bolim_id, :_til_id,  :_narxi)";
                        cmd = new NpgsqlCommand(sql, conn);
                        cmd.Parameters.AddWithValue("_kitob", txtNom.Text);
                        cmd.Parameters.AddWithValue("_muallif_id", muallifID);
                        cmd.Parameters.AddWithValue("_bolim_id", bolimID);
                        cmd.Parameters.AddWithValue("_til_id", tilID);
                        cmd.Parameters.AddWithValue("_narxi", int.Parse(txtSnarxi.Text));
                        result = cmd.ExecuteScalar();
                        conn.Close();
                        conn.Open();
                        sql = @"select k.id from kitoblar k where k.nomi=" + $"'{txtNom.Text}'";
                        cmd = new NpgsqlCommand(sql, conn);
                        data = new DataTable();
                        data.Load(cmd.ExecuteReader());
                        conn.Close();
                        var s = data.Rows[0].ItemArray.ToList();
                        int kitobD = int.Parse(s[0].ToString());
                        conn.Open();
                        sql = @"INSERT INTO hisob (kitob_id, soni) VALUES
                            (:_kitob_id, :_soni)";
                        cmd = new NpgsqlCommand(sql, conn);
                        cmd.Parameters.AddWithValue("_kitob_id", kitobD);
                        cmd.Parameters.AddWithValue("_soni", int.Parse(txtSoni.Text));
                        result = cmd.ExecuteScalar();
                        conn.Close();
                        if (result != null || result != String.Empty)
                        {
                            MessageBox.Show("Yangi kitob qo'shildi");
                        }
                        else
                        {
                            MessageBox.Show("Qo'shish amalga oshirilmadi.");
                        }
                    }
                }
                conn.Open();
                sql = @"select k.id from kitoblar k where k.nomi=" + $"'{txtNom.Text}'";
                cmd = new NpgsqlCommand(sql, conn);
                data = new DataTable();
                data.Load(cmd.ExecuteReader());
                conn.Close();
                var l = data.Rows[0].ItemArray.ToList();
                int kitobID = int.Parse(l[0].ToString());
                if (!t)
                {
                    try
                    {
                        conn.Open();
                        sql = @"UPDATE kitoblar
                            SET narxi = :_narxi
                            WHERE nomi =" + $"'{txtNom.Text}'";
                        cmd = new NpgsqlCommand(sql, conn);
                        cmd.Parameters.AddWithValue("_narxi", int.Parse(txtSnarxi.Text));
                        result = cmd.ExecuteScalar();
                        conn.Close();
                        conn.Open();
                        sql = @"select k.soni from hisob k where k.kitob_id=" + kitobID;
                        cmd = new NpgsqlCommand(sql, conn);
                        data = new DataTable();
                        data.Load(cmd.ExecuteReader());
                        conn.Close();
                        l = data.Rows[0].ItemArray.ToList();
                        int soni = int.Parse(l[0].ToString()) + int.Parse(txtSoni.Text);
                        conn.Open();
                        sql = @"UPDATE hisob
                            SET soni = :_soni
                            WHERE kitob_id =" + kitobID;
                        cmd = new NpgsqlCommand(sql, conn);
                        cmd.Parameters.AddWithValue("_soni", soni);
                        result = cmd.ExecuteScalar();
                        conn.Close();
                        if (result != null || result != String.Empty)
                        {
                            MessageBox.Show("O'zgartirish amalga oshirildi.", "Update", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                        else
                        {
                            MessageBox.Show("O'zgartirish amalga oshirildi.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                    catch (Exception ex)
                    {
                        conn.Close();
                        MessageBox.Show("O'zgartirish' amalga oshirilmadi. Error: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    result = null;
                }
                {
                    conn.Open();
                    sql = @"INSERT INTO kirim (kitob_id, olish_narxi, soni) VALUES
                            (:_kitob_id, :_olishN, :_soni)";
                    cmd = new NpgsqlCommand(sql, conn);
                    cmd.Parameters.AddWithValue("_kitob_id", kitobID);
                    cmd.Parameters.AddWithValue("_olishN", int.Parse(txtolish.Text));
                    cmd.Parameters.AddWithValue("_soni", int.Parse(txtSoni.Text));
                    result = cmd.ExecuteScalar();
                    conn.Close();
                    if (result != null || result != String.Empty)
                    {
                        MessageBox.Show("Yangi kirim qo'shildi");
                    }
                    else
                    {
                        MessageBox.Show("Qo'shish amalga oshirilmadi.");
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void txtMuallif_TextChanged(object sender, EventArgs e)
        {
            if (t)
            {
                try
                {
                    conn.Open();
                    sql = @"select m.nomi as muallif from mualliflar m where m.nomi LIKE " + $"'%{txtMuallif.Text}%'";
                    cmd = new NpgsqlCommand(sql, conn);
                    data = new DataTable();
                    data.Load(cmd.ExecuteReader());
                    conn.Close();
                    Tavsiyalar.Tag = "muallif";
                    Tavsiyalar.DataSource = null;
                    Tavsiyalar.DataSource = data;
                }
                catch { }
            }
        }
        private void txtbolim_TextChanged(object sender, EventArgs e)
        {
            if (t)
            {
                try
                {
                    conn.Open();
                    sql = @"select b.nomi as bolim from bolimlar b where b.nomi LIKE " + $"'%{txtbolim.Text}%'";
                    cmd = new NpgsqlCommand(sql, conn);
                    data = new DataTable();
                    data.Load(cmd.ExecuteReader());
                    conn.Close();
                    Tavsiyalar.Tag = "bolim";
                    Tavsiyalar.DataSource = null;
                    Tavsiyalar.DataSource = data;
                }
                catch { }
            }
        }
        private void txtNom_TextChanged(object sender, EventArgs e)
        {
            if (t)
            {
                try
                {
                    conn.Open();
                    sql = @"select k.nomi as kitob, m.nomi as muallif, b.nomi as bolim, t.nomi as til, k.narxi, h.soni from kitoblar k 
                            inner join mualliflar m on m.id=k.muallif_id 
                            inner join bolimlar b on b.id=k.bolim_id 
                            inner join tillar t on t.id=k.til_id
                            inner join hisob h on h.kitob_id=k.id
                            where k.nomi LIKE " + $"'%{txtNom.Text}%'";
                    cmd = new NpgsqlCommand(sql, conn);
                    data = new DataTable();
                    data.Load(cmd.ExecuteReader());
                    conn.Close();
                    Tavsiyalar.Tag = "kitoblar";
                    Tavsiyalar.DataSource = null;
                    Tavsiyalar.DataSource = data;
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }
        private void txtTil_TextChanged(object sender, EventArgs e)
        {
            if (t)
            {
                try
                {
                    conn.Open();
                    sql = @"select t.nomi as til from tillar t where t.nomi LIKE " + $"'%{txtTil.Text}%'";
                    cmd = new NpgsqlCommand(sql, conn);
                    data = new DataTable();
                    data.Load(cmd.ExecuteReader());
                    conn.Close();
                    Tavsiyalar.Tag = "til";
                    Tavsiyalar.DataSource = null;
                    Tavsiyalar.DataSource = data;
                }
                catch { }
            }
        }
        private void bolimQosh_Click(object sender, EventArgs e)
        {
            object result;
            Form2 obj = new Form2();
            obj.ShowDialog();
            if (Class1.nomi != "")
            {
                try
                {
                    conn.Open();
                    sql = @"INSERT INTO bolimlar (nomi) VALUES
                            (:_bolim)";
                    cmd = new NpgsqlCommand(sql, conn);
                    cmd.Parameters.AddWithValue("_bolim", Class1.nomi);
                    result = cmd.ExecuteScalar();
                    conn.Close();
                    if (result != null || result != String.Empty)
                    {
                        MessageBox.Show("Yangi bo'lim qo'shildi");
                    }
                    else
                    {
                        MessageBox.Show("Qo'shish amalga oshirilmadi.");
                    }
                }
                catch (Exception ex)
                {
                    conn.Close();
                    MessageBox.Show("Qo'shish amalga oshirilmadi. Error: " + ex.Message);
                }
            }
        }
        private void muallifQosh_Click(object sender, EventArgs e)
        {
            object result;
            Form2 obj = new Form2();
            obj.ShowDialog();
            if (Class1.nomi != "")
            {
                try
                {
                    conn.Open();
                    sql = @"INSERT INTO mualliflar (nomi) VALUES
                            (:_muallif)";
                    cmd = new NpgsqlCommand(sql, conn);
                    cmd.Parameters.AddWithValue("_muallif", Class1.nomi);
                    result = cmd.ExecuteScalar();
                    conn.Close();
                    if (result != null || result != String.Empty)
                    {
                        MessageBox.Show("Yangi muallif qo'shildi");
                    }
                    else
                    {
                        MessageBox.Show("Qo'shish amalga oshirilmadi.");
                    }
                }
                catch (Exception ex)
                {
                    conn.Close();
                    MessageBox.Show("Qo'shish amalga oshirilmadi. Error: " + ex.Message);
                }
            }
        }
        private void tilQosh_Click(object sender, EventArgs e)
        {
            object result;
            Form2 obj = new Form2();
            obj.ShowDialog();
            if (Class1.nomi != "")
            {
                try
                {
                    conn.Open();
                    sql = @"INSERT INTO tillar (nomi) VALUES
                            (:_til)";
                    cmd = new NpgsqlCommand(sql, conn);
                    cmd.Parameters.AddWithValue("_til", Class1.nomi);
                    result = cmd.ExecuteScalar();
                    conn.Close();
                    if (result != null || result != String.Empty)
                    {
                        MessageBox.Show("Yangi Til qo'shildi");
                    }
                    else
                    {
                        MessageBox.Show("Qo'shish amalga oshirilmadi.");
                    }
                }
                catch (Exception ex)
                {
                    conn.Close();
                    MessageBox.Show("Qo'shish amalga oshirilmadi. Error: " + ex.Message);
                }
            }
        }
        private void button1_Click(object sender, EventArgs e)
        {
            txtNom.Text = txtMuallif.Text = txtolish.Text = txtSnarxi.Text = txtSoni.Text = txtTil.Text = txtbolim.Text = "";
            t = true;
        }
        private void txtSotish_TextChanged(object sender, EventArgs e)
        {
            try
            {
                conn.Open();
                sql = @"select k.nomi as kitob, m.nomi as muallif, b.nomi as bolim, t.nomi as til, k.narxi, h.soni from kitoblar k 
                            inner join mualliflar m on m.id=k.muallif_id 
                            inner join bolimlar b on b.id=k.bolim_id 
                            inner join tillar t on t.id=k.til_id
                            inner join hisob h on h.kitob_id=k.id
                            where k.nomi LIKE " + $"'%{txtSotish.Text}%'";
                cmd = new NpgsqlCommand(sql, conn);
                data = new DataTable();
                data.Load(cmd.ExecuteReader());
                conn.Close();
                STavsiyalar.DataSource = null;
                STavsiyalar.DataSource = data;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void STavsiyalar_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
                try
                {
                    txtSotish.Text = STavsiyalar.Rows[e.RowIndex].Cells["kitob"].Value.ToString();
                }
                catch { }
        }
        private void btnSotish_Click(object sender, EventArgs e)
        {
            object result;
            conn.Open();
            sql = @"select k.id from kitoblar k where k.nomi=" + $"'{txtSotish.Text}'";
            cmd = new NpgsqlCommand(sql, conn);
            data = new DataTable();
            data.Load(cmd.ExecuteReader());
            conn.Close();
            var l = data.Rows[0].ItemArray.ToList();
            int kitobID = int.Parse(l[0].ToString());
            conn.Open();
            sql = @"select k.narxi from kitoblar k where k.nomi=" + $"'{txtSotish.Text}'";
            cmd = new NpgsqlCommand(sql, conn);
            data = new DataTable();
            data.Load(cmd.ExecuteReader());
            conn.Close();
            l = data.Rows[0].ItemArray.ToList();
            int sotishN = int.Parse(l[0].ToString());
            {
                conn.Open();
                sql = @"INSERT INTO chiqim (kitob_id, sotish_narxi, soni) VALUES
                            (:_kitob_id, :_sotishN, :_soni)";
                cmd = new NpgsqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("_kitob_id", kitobID);
                cmd.Parameters.AddWithValue("_sotishN", sotishN);
                cmd.Parameters.AddWithValue("_soni", int.Parse(txtSSoni.Text));
                result = cmd.ExecuteScalar();
                conn.Close();
                conn.Open();
                sql = @"select k.soni from hisob k where k.kitob_id=" + kitobID;
                cmd = new NpgsqlCommand(sql, conn);
                data = new DataTable();
                data.Load(cmd.ExecuteReader());
                conn.Close();
                l = data.Rows[0].ItemArray.ToList();
                int soni = int.Parse(l[0].ToString()) - int.Parse(txtSSoni.Text);
                conn.Open();
                sql = @"UPDATE hisob
                            SET soni = :_soni
                            WHERE kitob_id =" + kitobID;
                cmd = new NpgsqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("_soni", soni);
                result = cmd.ExecuteScalar();
                conn.Close();
                if (result != null || result != String.Empty)
                {
                    MessageBox.Show("Yangi Chiqim qo'shildi");
                }
                else
                {
                    MessageBox.Show("Qo'shish amalga oshirilmadi.");
                }
            }
            try
            {
                conn.Open();
                sql = (@"select * from hisob where soni=0");
                cmd = new NpgsqlCommand(sql, conn);
                data = new DataTable();
                data.Load(cmd.ExecuteReader());
                conn.Close();
                var list = data.Rows[0].ItemArray.ToList();
                if(list != null)
                {
                    conn.Open();
                    sql = (@"DELETE FROM kitoblar WHERE id =:_id");
                    cmd = new NpgsqlCommand(sql, conn);
                    cmd.Parameters.AddWithValue("_id", list[0]);
                    result = cmd.ExecuteScalar();
                    conn.Close();
                    conn.Open();
                    sql = (@"DELETE FROM hisob WHERE soni=0");
                    cmd = new NpgsqlCommand(sql, conn);
                    result = cmd.ExecuteScalar();
                    conn.Close();
                    if (result != null || result != String.Empty)
                    {
                        MessageBox.Show("Kitob o'chirildi.");
                        rowIndex = -1;
                        Select();
                    }
                }
                
            }
            catch (Exception ex)
            {
              
            }
        }
        private void txtSSoni_TextChanged(object sender, EventArgs e)
        {
            if (tekshir(txtSSoni.Text)) txtSSoni.ForeColor = Color.Black;
            else txtSSoni.ForeColor = Color.Red;
            try
            {
                conn.Open();
                sql = @"select h.soni from hisob h inner join kitoblar k on k.id=h.kitob_id where k.nomi=" + $"'{txtSotish.Text}'";
                cmd = new NpgsqlCommand(sql, conn);
                data = new DataTable();
                data.Load(cmd.ExecuteReader());
                var list = data.Rows[0].ItemArray.ToList();
                conn.Close();
                if (int.Parse(list[0].ToString()) >= int.Parse(txtSSoni.Text)) txtSSoni.ForeColor = Color.Black;
                else txtSSoni.ForeColor = Color.Red;
            }
            catch { }
            try
            {
                conn.Open();
                sql = @"select k.narxi from kitoblar k where k.nomi=" + $"'{txtSotish.Text}'";
                cmd = new NpgsqlCommand(sql, conn);
                data = new DataTable();
                data.Load(cmd.ExecuteReader());
                var list = data.Rows[0].ItemArray.ToList();
                conn.Close();
                lblNarx.Text = (int.Parse(list[0].ToString()) * int.Parse(txtSSoni.Text)) + " so'm";
            }
            catch { }
        }
        private void txtSnarxi_TextChanged(object sender, EventArgs e)
        {
            if (tekshir(txtSnarxi.Text)) txtSnarxi.ForeColor = Color.Black;
            else txtSnarxi.ForeColor = Color.Red;
        }

        private void txtolish_TextChanged(object sender, EventArgs e)
        {
            if (tekshir(txtolish.Text)) txtolish.ForeColor = Color.Black;
            else txtolish.ForeColor = Color.Red;
        }

        private void txtSoni_TextChanged(object sender, EventArgs e)
        {
            if (tekshir(txtSoni.Text)) txtSoni.ForeColor = Color.Black;
            else txtSoni.ForeColor = Color.Red;
        }

        private void btnkorish_Click(object sender, EventArgs e)
        {
            DateTime boshi = date1.Value;
            DateTime oxiri = date2.Value.AddHours(12);
            int kirim=0, chiqim=0;
            try
            {
                conn.Open();
                sql = @"select soni, olish_narxi from kirim where " + $"sana>'{boshi}' and sana<'{oxiri}'";
                cmd = new NpgsqlCommand(sql, conn);
                data = new DataTable();
                data.Load(cmd.ExecuteReader());
                conn.Close();
                int son = 0;
                int narx = 0;
                for (int i = 0; i < data.Rows.Count; i++)
                {
                    var list = data.Rows[i].ItemArray.ToList();
                    son += int.Parse(list[0].ToString());
                    narx += int.Parse(list[0].ToString()) * int.Parse(list[1].ToString());
                }
                kirim = narx;
                olinganSon.Text = son.ToString();
                olinganNarx.Text = som(narx);
            }
            catch { }
            try
            {
                conn.Open();
                sql = @"select soni, sotish_narxi from chiqim where " + $"sana>'{boshi}' and sana<'{oxiri}'";
                cmd = new NpgsqlCommand(sql, conn);
                data = new DataTable();
                data.Load(cmd.ExecuteReader());
                conn.Close();
                int son = 0;
                int narx = 0;
                for (int i = 0; i < data.Rows.Count; i++)
                {
                    var list = data.Rows[i].ItemArray.ToList();
                    son += int.Parse(list[0].ToString());
                    narx += int.Parse(list[0].ToString()) * int.Parse(list[1].ToString());
                }
                chiqim = narx;
                sotilganSon.Text = son.ToString();
                sotilganNarx.Text = som(narx);
            }
            catch { }
            try
            {
                conn.Open();
                sql = @"select c.soni, k.olish_narxi, c.sotish_narxi from chiqim c inner join kirim k on k.kitob_id=c.kitob_id where " + $"c.sana>'{boshi}' and c.sana<'{oxiri}'";
                cmd = new NpgsqlCommand(sql, conn);
                data = new DataTable();
                data.Load(cmd.ExecuteReader());
                conn.Close();
                int foyda = 0;
                for (int i = 0; i < data.Rows.Count; i++)
                {
                   var list = data.Rows[i].ItemArray.ToList();
                    foyda += int.Parse(list[0].ToString()) * (int.Parse(list[2].ToString()) - int.Parse(list[1].ToString()));
                }
                lblfoyda.Text = som(foyda);
            }
            catch { }
        }
    }
    public class Class1
    {
        public static string nomi;
    }
}