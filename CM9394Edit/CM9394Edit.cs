using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Newtonsoft.Json;

namespace CM9394Edit
{
    public partial class CM9394Edit : Form
    {    
        int index = 0;
        DataUtil du;
        private string savegamepath;
        private string savegameFilename;

        public CM9394Edit()
        {
            FileDialog();
            InitializeComponent();
            du = new DataUtil(savegamepath,null);
            InitView();
            this.TopMost = true;
            this.Focus();
        }

        public void FileDialog()
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Title = "Open CM9394 Savegame";
            ofd.Filter = "CM9394 EOS Savegame | SVGAME*";

            if (ofd.ShowDialog() == DialogResult.OK)
            {
                savegamepath = ofd.FileName;
                savegameFilename = ofd.SafeFileName;
            }
            else
            {
                System.Environment.Exit(0);
            }
        }

        public void InitView()
        {
            List<string> clubList = new List<string>();
            for (int i = 0; i < du.Clubs.Count; i++) clubList.Add(du.Clubs[i].Name);
            clubList.Sort();
            comboClubs.Items.AddRange(clubList.ToArray());
            comboSearchClubs.Items.Add("");
            comboSearchClubs.Items.AddRange(clubList.ToArray());
            comboSearchClubs.SelectedIndex = 0;
            ShowPlayer(index);
        }
   
        public void ShowPlayer(int index)
        {
            Player p = du.Players[index];
            lblId.Text = p.Id.ToString();
            lblName.Text = p.FirstName + " " + p.SurName;
            lblAge.Text = p.Age.ToString();
            comboClubs.SelectedIndex = comboClubs.FindStringExact(p.Club);
            lblPassing.Text = p.Passing.ToString();
            lblTackling.Text = p.Tackling.ToString();
            lblPace.Text = p.Pace.ToString();
            lblHeading.Text = p.Heading.ToString();
            lblFlair.Text = p.Flair.ToString();
            lblCreativity.Text = p.Creativity.ToString();
            lblStamina.Text = p.Stamina.ToString();
            lblInfluence.Text = p.Influence.ToString();
            lblAgility.Text = p.Agility.ToString();
            lblStrength.Text = p.Strength.ToString();
            lblFitness.Text = p.Fitness.ToString();
            lblIllness.Text = p.Illness.ToString();
            lblWeeks.Text = p.WeeksNA.ToString();
            lblMorale.Text = p.Morale.ToString();
            lblGoalscoring.Text = p.GoalScoring.ToString();
            lblCurrentSkill.Text = p.CurrentSkill.ToString();
            lblPotentialSkill.Text = p.PotentialSkill.ToString();
            cbxG.Checked = p.Position.G;
            cbxD.Checked = p.Position.D;
            cbxM.Checked = p.Position.M;
            cbxA.Checked = p.Position.A;
            cbxR.Checked = p.Position.R;
            cbxC.Checked = p.Position.C;
            cbxL.Checked = p.Position.L;
            groupBoxPosition.Text = "Position: " + p.Position.descriptor;
        }

        private void btnNext_Click(object sender, EventArgs e)
        {
            if(index<1859) index++;
            ShowPlayer(index);
        }

        private void btnPrev_Click(object sender, EventArgs e)
        {
            if (index >= 1) index--;
            ShowPlayer(index);
        }

        private void btnGoto_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            string input = txtGoto.Text;
            if (IsInteger(input))
            {
                if (Convert.ToInt32(input) <= 0)
                {
                    MessageBox.Show("Minimum player id is 1");
                }
                else
                {
                    if (Convert.ToInt32(input) > 1860)
                    {
                        MessageBox.Show("Maximum player id is 1860");
                    }
                    else
                    {
                        index = Convert.ToInt32(input) - 1;
                        ShowPlayer(index);
                    }
                }
            }
            else
            {
                MessageBox.Show("Input not valid");
            }
        }

        private void txtGoto_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Return)
            {
                if (txtGoto.Text.Length > 0) btnGoto_LinkClicked(sender, null);
            }
        }

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if (keyData == Keys.N && tabMain.SelectedIndex==0) btnNext_Click(null, null);
            else if (keyData == Keys.P && tabMain.SelectedIndex == 0) btnPrev_Click(null, null);
            else return false;
            return true;
        }

        private bool IsInteger(string txt)
        {
            try
            {
                int p = Convert.ToInt32(txt);
                return true;
            }
            catch (Exception e)
            {
                string err = e.ToString();
                return false;
            }
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            Position p = null;
            if (cbxUseSearch.Checked)
            {
                p = new Position();
                p.G = cbxG_search.Checked;
                p.D = cbxD_search.Checked;
                p.M = cbxM_search.Checked; 
                p.A = cbxA_search.Checked;
                p.R = cbxR_search.Checked; 
                p.C = cbxC_search.Checked;
                p.L = cbxL_search.Checked;
            }
            List<Player> filtered = du.FindPlayers(txtLastnameSearch.Text, txtFirstnameSearch.Text, (int)numMinAgeSearch.Value, (int)numMaxAgeSearch.Value, (int)numGoalscoring_search.Value, (int)numMinCurrSkillSearch.Value, (int)numMinPotSkillSearch.Value, (string)comboSearchClubs.SelectedItem, p);
            UpdateSearchView(filtered);
        }

        private void cbxUseSearch_CheckedChanged(object sender, EventArgs e)
        {
            if (cbxUseSearch.Checked)
            {
                cbxG_search.Enabled = true;
                cbxD_search.Enabled = true;
                cbxM_search.Enabled = true;
                cbxA_search.Enabled = true;
                cbxR_search.Enabled = true;
                cbxC_search.Enabled = true;
                cbxL_search.Enabled = true;
            }
            else
            {
                cbxG_search.Enabled = false;
                cbxD_search.Enabled = false;
                cbxM_search.Enabled = false;
                cbxA_search.Enabled = false;
                cbxR_search.Enabled = false;
                cbxC_search.Enabled = false;
                cbxL_search.Enabled = false;
            }
        }

        private void UpdateSearchView(List<Player> filteredPlayers)
        {
            dataGridView1.Rows.Clear();

            foreach (Player p in filteredPlayers)
            {
                int n = dataGridView1.Rows.Add();

                dataGridView1.Rows[n].Cells[colNo.Name].Value = p.Id;
                dataGridView1.Rows[n].Cells[colName.Name].Value = p.SurName + ", " + p.FirstName;
                dataGridView1.Rows[n].Cells[colAge.Name].Value = p.Age;
                dataGridView1.Rows[n].Cells[colClub.Name].Value = p.Club;
                dataGridView1.Rows[n].Cells[colGoal.Name].Value = p.GoalScoring;
                dataGridView1.Rows[n].Cells[colCurrSkill.Name].Value = p.CurrentSkill;
                dataGridView1.Rows[n].Cells[colPotSkill.Name].Value = p.PotentialSkill;
                dataGridView1.Rows[n].Cells[colPos.Name].Value = p.Position.descriptor;

                dataGridView1.Rows[n].Tag = p;
            }

            lblSearchInfo.Text = "Search returned " + filteredPlayers.Count + " players";
        }

        private void dataGridView1_CellClick(object sender, System.Windows.Forms.DataGridViewCellEventArgs e)
        {
            index = ((Player)dataGridView1.Rows[e.RowIndex].Tag).Id-1;
            ShowPlayer(index);
            tabMain.SelectedIndex = 0;
        }

        private void lblShowAllSearch_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            UpdateSearchView(du.Players);
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            Position pos = Position.GetPosition(cbxG.Checked, cbxD.Checked, cbxM.Checked, cbxA.Checked, cbxR.Checked, cbxC.Checked, cbxR.Checked);
            if (pos != null) du.SavePlayer(index, (int)lblAge.Value, (string)comboClubs.SelectedItem, (int)lblPassing.Value, (int)lblTackling.Value, (int)lblPace.Value, (int)lblHeading.Value, (int)lblFlair.Value, (int)lblCreativity.Value, (int)lblStamina.Value, (int)lblInfluence.Value, (int)lblAgility.Value, (int)lblStrength.Value, (int)lblFitness.Value, (int)lblIllness.Value, (int)lblWeeks.Value, (int)lblMorale.Value, (int)lblGoalscoring.Value, (int)lblCurrentSkill.Value, (int)lblPotentialSkill.Value, pos);
            else MessageBox.Show("Invalid player position");
        }

        void dataGridView1_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
        {
            if (e.Control && e.KeyCode == Keys.C)
            {
                DataObject d = dataGridView1.GetClipboardContent();
                Clipboard.SetDataObject(d);
                e.Handled = true;
            }
        }

        private void btnSuperman_Click(object sender, EventArgs e)
        {
            lblAge.Value = 18;
            lblPassing.Value = 20;
            lblTackling.Value = 20;
            lblPace.Value = 20;
            lblHeading.Value = 20;
            lblFlair.Value = 20;
            lblCreativity.Value = 20;
            lblStamina.Value = 20;
            lblInfluence.Value = 20;
            lblAgility.Value = 20;
            lblStrength.Value = 20;
            lblFitness.Value = 100;
            lblIllness.Value = 0;
            lblWeeks.Value = 0;
            lblMorale.Value = 255;
            lblGoalscoring.Value = 20;
            lblCurrentSkill.Value = 200;
            lblPotentialSkill.Value = 200;
            cbxG.Checked = true;
            cbxD.Checked = true;
            cbxM.Checked = true;
            cbxA.Checked = true;
            cbxR.Checked = true;
            cbxC.Checked = true;
            cbxL.Checked = true;
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            if(comboSearchClubs.SelectedIndex>0)
            {
                List<Player> filteredPlayers = du.FindPlayers(null, null, 0, 100, 0, 0, 0,(string) comboSearchClubs.SelectedItem, null);
                UpdateSearchView(filteredPlayers);
            }
        }

        private void btnLoadExtendedNames_Click(object sender, EventArgs e)
        {
            FileInfo file = new FileInfo(savegamepath + "_extendednames.txt");
            if(file.Exists)
            {
                du = new DataUtil(savegamepath, file.ToString());
                InitView();
                this.TopMost = true;
                this.Focus();
                MessageBox.Show("Extended names loaded");
            }
            else
            {
                MessageBox.Show("No extended name list for this savegame. You need to add a text file named <savegame>_extendednames.txt");
            }
        }

        private void btnExport_Click(object sender, EventArgs e)
        {
            SaveFileDialog sfd = new SaveFileDialog();
            sfd.Title = "Save CM9394 Data";
            sfd.Filter = "CM9394 Data | *.json";
            if(sfd.ShowDialog()==DialogResult.OK)
            {
                ExportData exportData = new ExportData();
                exportData.Clubs = du.Clubs;
                exportData.Players = du.Players;
                string output = JsonConvert.SerializeObject(exportData);
                string filename = sfd.FileName;
                File.WriteAllText(filename, output);
            }
            

        }

        private void btnPlayerHistory_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            
            PlayerHistoryForm playerHistoryForm = new PlayerHistoryForm();
            playerHistoryForm.Text = "History for " + du.GetPlayer(index).FirstName + " " + du.GetPlayer(index).SurName;

            List<DataUtil.PlayerHistory> playerHistory = du.PlayerHistoryList(index);

            foreach (var ph in playerHistory)
            {
                ListViewItem item = new ListViewItem("19" + ph.Year);
                item.SubItems.Add(du.GetClubName(ph.ClubId));
                item.SubItems.Add(""+ ph.Apps);
                item.SubItems.Add(""+ ph.Goals);
                item.SubItems.Add(string.Format("{0:N2}",ph.Avg));

                playerHistoryForm.listViewPlHist.Items.Add(item);
            }

            playerHistoryForm.ShowDialog(this);
        }
    }
    
}
