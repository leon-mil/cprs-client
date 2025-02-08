/**********************************************************************
Econ App Name : Construction Progress Report Survey (CPRS)
Project Name  : Construction Progress Report Survey(CPRS)
Program Name  : frmNPCSpecialsPopup.cs
Programmer    : Christine Zhang
Creation Date : Jan 21 2020
Parameters    : respid
Inputs        : N/A
Outputs       : N/A
Description   : Add/Edit NPC Specials Info
Change Request: 
Detail Design : 
Rev History   : See Below
Other         : N/A
 ***********************************************************************
Modified Date :
Modified By   :
Keyword       :
Change Request:
Description   :
***********************************************************************/

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using CprsDAL;
using CprsBLL;

namespace Cprs
{
    public partial class frmNPCSpecialPopup : Form
    {
        public frmNPCSpecialPopup()
        {
            InitializeComponent();
        }

        public string Respid = string.Empty;
        private NPCSpecialsData dataObject = new NPCSpecialsData();
        private NPCSpecial npc;

        private const int CP_NOCLOSE_BUTTON = 0x200;

        protected override CreateParams CreateParams
        {
            get
            {
                CreateParams myCp = base.CreateParams;
                myCp.ClassStyle = myCp.ClassStyle | CP_NOCLOSE_BUTTON;
                return myCp;
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnColtec_Click(object sender, EventArgs e)
        {
            frmNPCSpecialColtecSel popup = new frmNPCSpecialColtecSel();
            popup.StartPosition = FormStartPosition.CenterParent;
            DialogResult dialogresult = popup.ShowDialog();

            if (dialogresult == DialogResult.OK)
            {
                txtColtec.Text = popup.selectedColtec;
            }

            popup.Dispose();
        }

        private void frmNPCSpecialPopup_Load(object sender, EventArgs e)
        {
            DataTable dt = dataObject.GetNPCUserList();

            cbUser.DataSource = dt;
            cbUser.ValueMember = "usrnme";
            cbUser.DisplayMember = "usrnme";

            if (Respid != "")
            {
                txtRespId.ReadOnly = true;
                npc = dataObject.GetNPCSpecial(Respid);
                DisplayData();
            }
            else
            {
                txtRespId.Text = "";
                txtColtec.Text = "";
                txtRespname.Text = "";
                cbUser.SelectedIndex = -1;
             
                rd1n.Checked = true;
                rd1y.Checked = false;
                rd1q.Checked = false;
                rd2n.Checked = true;
                rd2y.Checked = false;
                rd3n.Checked = true;
                rd3y.Checked = false;
                rd4n.Checked = true;
                rd4y.Checked = false;
                rd5n.Checked = true;
                rd5y.Checked = false;
                rdr1n.Checked = true;
                rdr1y.Checked = false;
                rdr1q.Checked = false;
                rdr2n.Checked = true;
                rdr2y.Checked = false;
                rdr3n.Checked = true;
                rdr3y.Checked = false;
                rdr4n.Checked = true;
                rdr4y.Checked = false;
                rdr4q.Checked = false;
                rdr5n.Checked = true;
                rdr5y.Checked = false;
                rdr5w.Checked = false;
                rdr6n.Checked = true;
                rdr6y.Checked = false;

            }
        }

        private void DisplayData()
        {
            npc = dataObject.GetNPCSpecial(Respid);
            txtRespId.Text = npc.Respid;
            txtColtec.Text = npc.Coltec;
            txtRespname.Text = npc.Resporg;
            if (Respid == "")
                cbUser.SelectedIndex = -1;
            else
                cbUser.SelectedValue= npc.Username;

            rd1n.Checked = false;
            rd1y.Checked = false;
            rd1q.Checked = false;
            rd2n.Checked = false;
            rd2y.Checked = false;
            rd3n.Checked = false;
            rd3y.Checked = false;
            rd4n.Checked = false;
            rd4y.Checked = false;
            rd5n.Checked = false;
            rd5y.Checked = false;

            if (npc.Cstatus1 == "N")
                rd1n.Checked = true;
            else if (npc.Cstatus1 == "Y")
                rd1y.Checked = true;
            else
                rd1q.Checked = true;

            if (npc.Cstatus2 == "N")
                rd2n.Checked = true;
            else if (npc.Cstatus2 == "Y")
                rd2y.Checked = true;

            if (npc.Cstatus3 == "N")
                rd3n.Checked = true;
            else if (npc.Cstatus3 == "Y")
                rd3y.Checked = true;

            if (npc.Cstatus4 == "N")
                rd4n.Checked = true;
            else if (npc.Cstatus4 == "Y")
                rd4y.Checked = true;

            if (npc.Cstatus5 == "N")
                rd5n.Checked = true;
            else if (npc.Cstatus5 == "Y")
                rd5y.Checked = true;

            rdr1n.Checked = false;
            rdr1y.Checked = false;
            rdr1q.Checked = false;
            rdr2n.Checked = false;
            rdr2y.Checked = false;
            rdr3n.Checked = false;
            rdr3y.Checked = false;
            rdr4n.Checked = false;
            rdr4y.Checked = false;
            rdr4q.Checked = false;
            rdr5n.Checked = false;
            rdr5y.Checked = false;
            rdr5w.Checked = false;
            rdr6n.Checked = false;
            rdr6y.Checked = false;

            if (npc.Rstatus1 == "N")
                rdr1n.Checked = true;
            else if (npc.Rstatus1 == "Y")
                rdr1y.Checked = true;
            else
                rdr1q.Checked = true;

            if (npc.Rstatus2 == "N")
                rdr2n.Checked = true;
            else if (npc.Rstatus2 == "Y")
                rdr2y.Checked = true;
 
            if (npc.Rstatus3 == "N")
                rdr3n.Checked = true;
            else if (npc.Rstatus3 == "Y")
                rdr3y.Checked = true;

            if (npc.Rstatus4 == "N")
                rdr4n.Checked = true;
            else if (npc.Rstatus4 == "Y")
                rdr4y.Checked = true;
            else
                rdr4q.Checked = true;

            if (npc.Rstatus5 == "N")
                rdr5n.Checked = true;
            else if (npc.Rstatus5 == "Y")
                rdr5y.Checked = true;
            else
                rdr5w.Checked = true;

            if (npc.Rstatus6 == "N")
                rdr6n.Checked = true;
            else if (npc.Rstatus6 == "Y")
                rdr6y.Checked = true;
        }

        private void ReadData()
        {
            if (Respid == "")
                npc = new NPCSpecial(txtRespId.Text);

            npc.Coltec = txtColtec.Text;
            npc.Username = cbUser.Text;

            if (rd1n.Checked)
                npc.Cstatus1 = "N";
            else if (rd1y.Checked)
                npc.Cstatus1 = "Y";
            else if (rd1q.Checked)
                npc.Cstatus1 = "Q";

            if (rd2n.Checked)
                npc.Cstatus2 = "N";
            else if (rd2y.Checked)
                npc.Cstatus2 = "Y";

            if (rd3n.Checked)
                npc.Cstatus3 = "N";
            else if (rd3y.Checked)
                npc.Cstatus3 = "Y";

            if (rd4n.Checked)
                npc.Cstatus4 = "N";
            else if (rd4y.Checked)
                npc.Cstatus4 = "Y";

            if (rd5n.Checked)
                npc.Cstatus5 = "N";
            else if (rd5y.Checked)
                npc.Cstatus5 = "Y";

            if (rdr1n.Checked)
                npc.Rstatus1 = "N";
            else if (rdr1y.Checked)
                npc.Rstatus1 = "Y";
            else if (rdr1q.Checked)
                npc.Rstatus1 = "Q";

            if (rdr2n.Checked)
                npc.Rstatus2 = "N";
            else if (rdr2y.Checked)
                npc.Rstatus2 = "Y";

            if (rdr3n.Checked)
                npc.Rstatus3 = "N";
            else if (rdr3y.Checked)
                npc.Rstatus3 = "Y";

            if (rdr4n.Checked)
                npc.Rstatus4 = "N";
            else if (rdr4y.Checked)
                npc.Rstatus4 = "Y";
            else 
                npc.Rstatus4 = "S";

            if (rdr5n.Checked)
                npc.Rstatus5 = "N";
            else if (rdr5y.Checked)
                npc.Rstatus5 = "Y";
            else
                npc.Rstatus5 = "W";

            if (rdr6n.Checked)
                npc.Rstatus6 = "N";
            else if (rdr6y.Checked)
                npc.Rstatus6 = "Y";
            
        }

        private bool ValidateData()
        {
            if (Respid == "")
            {
                if (txtRespId.Text.Trim() != "")
                {
                    string respid = txtRespId.Text.Trim();

                    if (!(respid.Length == 7))
                    {
                        MessageBox.Show("RESPID should be 7 digits.");
                        txtRespId.Text = "";
                        txtRespId.Focus();
                        btnSave.DialogResult = DialogResult.None;
                        return false;
                    }
                    else if (!GeneralDataFuctions.ChkRespid(respid))
                    {
                        MessageBox.Show("Invalid RESPID.");
                        txtRespId.Text = "";
                        txtRespId.Focus();
                        btnSave.DialogResult = DialogResult.None;
                        return false;
                    }
                    else if (dataObject.CheckRespidExist(respid))
                    {
                        MessageBox.Show("RESPID already assigned.");
                        txtRespId.Text = "";
                        txtRespId.Focus();
                        btnSave.DialogResult = DialogResult.None;
                        return false;
                    }
                }
                else
                {
                    MessageBox.Show("Please enter RESPID.");
                    txtRespId.Focus();
                    btnSave.DialogResult = DialogResult.None;
                    return false;
                }
            }
           
            if (cbUser.SelectedIndex == -1)
            {
                MessageBox.Show("Please select a user");
                cbUser.Focus();
                btnSave.DialogResult = DialogResult.None;
                return false;
            }
            if (txtColtec.Text == "")
            {
                MessageBox.Show("Please select coltec");
                txtColtec.Focus();
                btnSave.DialogResult = DialogResult.None;
                return false;
            }

            return true;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (ValidateData())
            {
                ReadData();
                if (Respid != "")
                {
                    dataObject.UpdateNPCSpecialsData(npc);
                    Respid = npc.Respid;
                }
                else
                    dataObject.AddNPCSpecialsData(npc);
                this.Close();
            }
            
        }

        private void txtRespId_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
        }

        private void frmNPCSpecialPopup_Activated(object sender, EventArgs e)
        {
            if (Respid == "")
                txtRespId.Focus();
            else
                cbUser.Focus();
        }

    }
}
