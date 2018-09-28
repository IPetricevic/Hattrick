using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace Hattrick
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        List<string> final_bet = new List<string>();
        List<string> pregled_bet = new List<string>();
        List<string> koeficijent = new List<string>();
        List<string> koeficijentK = new List<string>();
        List<string> koeficijentR = new List<string>();
        List<string> str_selectedItem = new List<string>();
        List<string> transakcije = new List<string>();
        double ukupnaKvota = 1;
        double stanje = 1000, bonus = 0;
        int bonusN = 0, bonusK = 0, bonusR = 0;
        bool flag_bonus5 = false, flag_bonus10 = false;
        int N = -1, K = -1, R = -1;


        private void Form1_Load(object sender, EventArgs e)
        {
            load_List();
            load_List1();
            load_List2();
            textBox_stanje.Text = stanje.ToString("N2");
            textBox1_stanje.Text = stanje.ToString("N2");
        }

        private void listBox1_Selected(object sender, EventArgs e)
        {
            K = -1;
            R = -1;
            load_Kvota();
        }
        
        private void button1_click(object sender, EventArgs e)
        {
            if (textBox_1.Text == "")
                MessageBox.Show("Odaberite par!");
            else
            {
                list_Oklada.Items.Clear();
                if (N >= 0)
                {
                    bonusN++;
                    final_bet.Add(listBox1.GetItemText(listBox1.SelectedItem) + ", TIP: 1, " + textBox_1.Text +"\n");
                    list_Oklada.Items.AddRange(final_bet.ToArray());
                    ukupnaKvota *= Convert.ToDouble(textBox_1.Text.ToString());
                    kvota_text.Text = ukupnaKvota.ToString("N2");
                    listBox1.Items.RemoveAt(listBox1.SelectedIndex);
                    koeficijent.RemoveAt(N*2);
                    koeficijent.RemoveAt(N*2);
                }
                else if (K >= 0)
                {
                    ++bonusK;
                    final_bet.Add(listBox2.GetItemText(listBox2.SelectedItem) + ", TIP: 1, " + textBox_1.Text + "\n");
                    list_Oklada.Items.AddRange(final_bet.ToArray());
                    ukupnaKvota *= Convert.ToDouble(textBox_1.Text.ToString());
                    kvota_text.Text = ukupnaKvota.ToString("N2");
                    listBox2.Items.RemoveAt(listBox2.SelectedIndex);
                    koeficijentK.RemoveAt(K * 2);
                    koeficijentK.RemoveAt(K * 2);
                }
                else if (R >= 0)
                {
                    ++bonusR;
                    final_bet.Add(listBox3.GetItemText(listBox3.SelectedItem) + ", TIP: 1, " + textBox_1.Text + "\n");
                    list_Oklada.Items.AddRange(final_bet.ToArray());
                    ukupnaKvota *= Convert.ToDouble(textBox_1.Text.ToString());
                    kvota_text.Text = ukupnaKvota.ToString("N2");
                    listBox3.Items.RemoveAt(listBox3.SelectedIndex);
                    koeficijentR.RemoveAt(R * 2);
                    koeficijentR.RemoveAt(R * 2);
                }
                izracunaj_bonus();
                izracunaj_dobitak();
                textBox_1.Text = ""; textBox_X.Text = ""; textBox_2.Text = "";
            }
        }

        private void buttonX_click(object sender, EventArgs e)
        {
            if (textBox_X.Text == "")
                MessageBox.Show("Odaberite par!");
            else
            {
                list_Oklada.Items.Clear();
                if (N >= 0)
                {
                    ++bonusN;
                    final_bet.Add(listBox1.GetItemText(listBox1.SelectedItem) + ", TIP: X, " + textBox_X.Text + "\n");
                    list_Oklada.Items.AddRange(final_bet.ToArray());
                    ukupnaKvota *= Convert.ToDouble(textBox_X.Text.ToString());
                    kvota_text.Text = ukupnaKvota.ToString("N2");
                    listBox1.Items.RemoveAt(listBox1.SelectedIndex);
                    koeficijent.RemoveAt(N*2);
                    koeficijent.RemoveAt(N*2);
                }
                else if (K >= 0)
                {
                    ++bonusK;
                    final_bet.Add(listBox2.GetItemText(listBox2.SelectedItem) + ", TIP: X, " + textBox_X.Text + "\n");
                    list_Oklada.Items.AddRange(final_bet.ToArray());
                    ukupnaKvota *= Convert.ToDouble(textBox_X.Text.ToString());
                    kvota_text.Text = ukupnaKvota.ToString("N2");
                    listBox2.Items.RemoveAt(listBox2.SelectedIndex);
                    koeficijentK.RemoveAt(K * 2);
                    koeficijentK.RemoveAt(K * 2);
                }
                else if (R >= 0)
                {
                    ++bonusR;
                    final_bet.Add(listBox3.GetItemText(listBox3.SelectedItem) + ", TIP: X, " + textBox_X.Text + "\n");
                    list_Oklada.Items.AddRange(final_bet.ToArray());
                    ukupnaKvota *= Convert.ToDouble(textBox_X.Text.ToString());
                    kvota_text.Text = ukupnaKvota.ToString("N2");
                    listBox3.Items.RemoveAt(listBox3.SelectedIndex);
                    koeficijentR.RemoveAt(R * 2);
                    koeficijentR.RemoveAt(R * 2);
                }
                izracunaj_dobitak();
                izracunaj_bonus();
                textBox_1.Text = ""; textBox_X.Text = ""; textBox_2.Text = "";
            }
        }

        private void button2_click(object sender, EventArgs e)
        {
            if (textBox_2.Text == "")
                MessageBox.Show("Odaberite par!");
            else
            {
                list_Oklada.Items.Clear();
                if (N >= 0)
                {
                    ++bonusN;
                    final_bet.Add(listBox1.GetItemText(listBox1.SelectedItem) + ", TIP: 2, " + textBox_2.Text + "\n");
                    list_Oklada.Items.AddRange(final_bet.ToArray());
                    ukupnaKvota *= Convert.ToDouble(textBox_2.Text.ToString());
                    kvota_text.Text = ukupnaKvota.ToString("N2");
                    listBox1.Items.RemoveAt(listBox1.SelectedIndex);
                    koeficijent.RemoveAt(N*2);
                    koeficijent.RemoveAt(N*2);
                }
                else if (K >= 0)
                {
                    ++bonusK;
                    final_bet.Add(listBox2.GetItemText(listBox2.SelectedItem) + ", TIP: 2, " + textBox_2.Text + "\n");
                    list_Oklada.Items.AddRange(final_bet.ToArray());
                    ukupnaKvota *= Convert.ToDouble(textBox_2.Text.ToString());
                    kvota_text.Text = ukupnaKvota.ToString("N2");
                    listBox2.Items.RemoveAt(listBox2.SelectedIndex);
                    koeficijentK.RemoveAt(K * 2);
                    koeficijentK.RemoveAt(K * 2);
                }
                else if (R >= 0)
                {
                    ++bonusR;
                    final_bet.Add(listBox3.GetItemText(listBox3.SelectedItem) + ", TIP: 2, " + textBox_2.Text + "\n");
                    list_Oklada.Items.AddRange(final_bet.ToArray());
                    ukupnaKvota *= Convert.ToDouble(textBox_2.Text.ToString());
                    kvota_text.Text = ukupnaKvota.ToString("N2");
                    listBox3.Items.RemoveAt(listBox3.SelectedIndex);
                    koeficijentR.RemoveAt(R * 2);
                    koeficijentR.RemoveAt(R * 2);
                }
                izracunaj_bonus();
                izracunaj_dobitak();
                textBox_1.Text = ""; textBox_X.Text = ""; textBox_2.Text = "";
            }
        }

        private void uplata_textChanged(object sender, EventArgs e)
        {

        }       

        private void Final_bet_click(object sender, EventArgs e)
        {
            if (numericUpDown1.Value > 0 && kvota_text.Text != "")
            {
                if ((stanje < 0) || ((stanje - (double)numericUpDown1.Value) < 0))
                    MessageBox.Show("Nedovoljan iznos na racunu!");
                else
                {
                    list_pregled.Items.Clear();
                    list_Oklada.Items.Clear();
                    list_transakcije.Items.Clear();
                    double prethodno_stanje = stanje;
                    stanje = stanje - (double)numericUpDown1.Value;
                    textBox_stanje.Text = stanje.ToString("N2");
                    textBox1_stanje.Text = stanje.ToString("N2");
                    transakcije.Add("Prethodno stanje: " + prethodno_stanje.ToString("N2") + " - " + numericUpDown1.Value.ToString("N2") + " (HRK),  Trenutno stanje: " + stanje.ToString("N2") + " (HRK),  Dobitak: " + dobitak_text.Text + " (HRK)");
                    pregled_bet.AddRange(final_bet);
                    if (flag_bonus5) pregled_bet.Add("BONUS +5 NA KVOTU DODAN: Odigrali ste 3 ili vise parova istog sporta");
                    if (flag_bonus10) pregled_bet.Add("BONUS +10 NA KVOTU DODAN: Odigrali ste parove iz svih sportova");
                    pregled_bet.Add("-----" + "Uplata: " + numericUpDown1.Value.ToString("N2") + " (HRK), Ukupna kvota: " + ukupnaKvota.ToString("N2") + ", Dobitak: " + dobitak_text.Text + " (HRK), Bonus: " + bonus.ToString("N2") + "(HRK)-----");
                    pregled_bet.Add("");
                    list_transakcije.Items.AddRange(transakcije.ToArray());
                    numericUpDown1.Value = 5;
                    ukupnaKvota = 1;
                    bonus = 0;
                    bonusN = 0;
                    bonusK = 0;
                    bonusR = 0;
                    kvota_text.Text = "";
                    textBox_Bonus.Text = "";
                    dobitak_text.Text = "";
                    flag_bonus5 = false;
                    flag_bonus10 = false;
                    list_pregled.Items.AddRange(pregled_bet.ToArray());
                    final_bet.Clear();
                    koeficijent.Clear();
                    koeficijentK.Clear();
                    koeficijentR.Clear();
                    listBox1.Items.Clear();
                    listBox2.Items.Clear();
                    listBox3.Items.Clear();
                    load_List();
                    load_List1();
                    load_List2();
                }
            }

            else {
                if (numericUpDown1.Value < 1 && kvota_text.Text != "")
                    MessageBox.Show("Unesite iznos oklade!");
                else
                    MessageBox.Show("Odaberite par!");
            }

        }

        private void ponisti_button_Click(object sender, EventArgs e)
        {
            textBox_1.Text = ""; textBox_X.Text = ""; textBox_2.Text = "";
            dobitak_text.Text = "";
            numericUpDown1.Value = 5;
            ukupnaKvota = 1;
            dobitak_text.Text = "";
            kvota_text.Text = "";
            textBox_Bonus.Text = "";
            flag_bonus5 = false;
            flag_bonus10 = false;
            bonus = 0;
            bonusN = 0;
            bonusK = 0;
            bonusR = 0;
            list_Oklada.Items.Clear();
            final_bet.Clear();
            koeficijent.Clear();
            koeficijentK.Clear();
            koeficijentR.Clear();
            listBox1.Items.Clear();
            listBox2.Items.Clear();
            listBox3.Items.Clear();
            load_List();
            load_List1();
            load_List2();
        }

        private void numericUpDown1_ValueChanged(object sender, EventArgs e)
        {
            if (kvota_text.Text != "")
            {
                izracunaj_dobitak();
                izracunaj_bonus();
            }
        }

        private void Uplati_click(object sender, EventArgs e)
        {
            
            stanje = stanje + (double)numeric_stanje.Value;
            textBox_stanje.Text = stanje.ToString("N2");
            textBox1_stanje.Text = stanje.ToString("N2");
            transakcije.Add("Uplaceno na racun: " + numeric_stanje.Value.ToString("N2") + " (HRK),  Trenutno stanje: " + stanje.ToString("N2") + " (HRK)");
            list_transakcije.Items.Clear();
            list_transakcije.Items.AddRange(transakcije.ToArray());
            numeric_stanje.Value = 0;

        }

        private void listBox2_Selected(object sender, EventArgs e)
        {
            N = -1;
            R = -1;
            if (listBox2.SelectedIndex > -1)
            {
                K = listBox2.SelectedIndex;
                textBox_1.Text = koeficijentK[K*2];
                textBox_2.Text = koeficijentK[K*2 + 1];
                textBox_X.Text = (Convert.ToDouble(koeficijentK[K*2]) + Convert.ToDouble(koeficijentK[K*2 + 1])).ToString();
            }

        }

        private void listBox3_Selected(object sender, EventArgs e)
        {
            N = -1;
            K = -1;
            if (listBox3.SelectedIndex > -1)
            {
                R = listBox3.SelectedIndex;
                textBox_1.Text = koeficijentR[R*2];
                textBox_2.Text = koeficijentR[R*2 + 1];
                textBox_X.Text = (Convert.ToDouble(koeficijentR[R*2]) + Convert.ToDouble(koeficijentR[R*2 + 1])).ToString();
            }
        }



        #region Metode


        public void load_List()
        {

            string cn_string = Properties.Settings.Default.NogometConnectionString;
            //-< Database >

            SqlConnection cn_connection = new SqlConnection(cn_string);

            if (cn_connection.State != ConnectionState.Open) cn_connection.Open();
            string sql_Text = "SELECT * FROM tbl_Nogomet";

            DataTable tbl = new DataTable();
            SqlDataAdapter adapter = new SqlDataAdapter(sql_Text, cn_connection);
            adapter.Fill(tbl);

            List<string> teams = new List<string>();
            int i = 1;
            string par1 = "", par2 = "";

            foreach (DataRow dataRow in tbl.Rows)
            {
                if (i % 2 == 1)
                {
                    par1 = dataRow["Klub"].ToString();
                    koeficijent.Add(dataRow["Rank"].ToString());
                }
                if (i % 2 == 0)
                {
                    par2 = dataRow["Klub"].ToString();
                    koeficijent.Add(dataRow["Rank"].ToString());
                    teams.Add(par1 + " - " + par2);
                }
                i++;

            }
            //listBox1.DataSource = teams;
            listBox1.Items.AddRange(teams.ToArray());
        }

        public void load_List1()
        {
            string cn_string = Properties.Settings.Default.NogometConnectionString;
            //-< Database >

            SqlConnection cn_connection = new SqlConnection(cn_string);

            if (cn_connection.State != ConnectionState.Open) cn_connection.Open();
            string sql_Text = "SELECT * FROM tbl_Kosarka";

            DataTable tbl = new DataTable();
            SqlDataAdapter adapter = new SqlDataAdapter(sql_Text, cn_connection);
            adapter.Fill(tbl);

            List<string> teams = new List<string>();
            int i = 1;
            string par1 = "", par2 = "";

            foreach (DataRow dataRow in tbl.Rows)
            {
                if (i % 2 == 1)
                {
                    par1 = dataRow["Klub"].ToString();
                    koeficijentK.Add(dataRow["Rank"].ToString());
                }
                if (i % 2 == 0)
                {
                    par2 = dataRow["Klub"].ToString();
                    koeficijentK.Add(dataRow["Rank"].ToString());
                    teams.Add(par1 + " - " + par2);
                }
                i++;

            }
            //listBox1.DataSource = teams;
            listBox2.Items.AddRange(teams.ToArray());
        }

        public void load_List2()
        {

            string cn_string = Properties.Settings.Default.NogometConnectionString;
            //-< Database >

            SqlConnection cn_connection = new SqlConnection(cn_string);

            if (cn_connection.State != ConnectionState.Open) cn_connection.Open();
            string sql_Text = "SELECT * FROM tbl_Rukomet1";

            DataTable tbl = new DataTable();
            SqlDataAdapter adapter = new SqlDataAdapter(sql_Text, cn_connection);
            adapter.Fill(tbl);

            List<string> teams = new List<string>();
            int i = 1;
            string par1 = "", par2 = "";

            foreach (DataRow dataRow in tbl.Rows)
            {
                if (i % 2 == 1)
                {
                    par1 = dataRow["Klub"].ToString();
                    koeficijentR.Add(dataRow["Rank"].ToString());
                }
                if (i % 2 == 0)
                {
                    par2 = dataRow["Klub"].ToString();
                    koeficijentR.Add(dataRow["Rank"].ToString());
                    teams.Add(par1 + " - " + par2);
                }
                i++;

            }
            //listBox1.DataSource = teams;
            listBox3.Items.AddRange(teams.ToArray());
        }

        public void load_Kvota()
        {
            if (listBox1.SelectedIndex > -1)
            {
                N = listBox1.SelectedIndex;
                textBox_1.Text = koeficijent[N*2];
                textBox_2.Text = koeficijent[N*2 + 1];
                textBox_X.Text = (Convert.ToDouble(koeficijent[N*2]) + Convert.ToDouble(koeficijent[N*2 + 1])).ToString();
            }
        }

        public void izracunaj_dobitak()
        {
            double dobitak = Convert.ToDouble(ukupnaKvota) * (double)numericUpDown1.Value;
            dobitak_text.Text = dobitak.ToString("N2");
        }

        public void izracunaj_bonus()
        {
            if ((bonusK > 2 || bonusN > 2 || bonusR > 2))
            {
                bonus = (double)numericUpDown1.Value * 5;         
                flag_bonus5 = true;
            }
            if (bonusK > 0 && bonusN > 0 && bonusR > 0)
            {
                bonus = (double)numericUpDown1.Value * 10;
                flag_bonus10 = true;
            }
            if (flag_bonus5 && flag_bonus10)
                bonus = (double)numericUpDown1.Value * 15;
            textBox_Bonus.Text = bonus.ToString("N2");
        }

        #endregion /Metode

    }
}
