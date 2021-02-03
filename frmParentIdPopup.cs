
/**********************************************************************
Econ App Name : Construction Progress Report Survey (CPRS)
Project Name  : Construction Progress Report Survey(CPRS)
Program Name  : frmSample.cs
Programmer    : 
Creation Date : May 3 2016
Parameters    : N/A
Inputs        : masterid , id
Outputs       : N/A
Description   : enter parent id 
Change Request: 
Specification : 
Rev History   : See Below

Other         : N/A
 ***********************************************************************/
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using CprsBLL;
using CprsDAL;

namespace Cprs
{
    public partial class frmParentIdPopup : Form
    {
        private SampleData sdata;
        private MasterData mdata;
        private int master_id;
        private string id;

        public frmParentIdPopup(string passed_id, int passed_masterid)
        {
            InitializeComponent();
            master_id = passed_masterid;
            id = passed_id;
        }

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

        private void frmC700ParentIdPopup_Load(object sender, EventArgs e)
        {
            sdata = new SampleData();
            mdata = new MasterData();
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            if (txtId.TextLength < 7)
            {
                MessageBox.Show("You have entered a ID less than 7 digits!");
                txtId.Focus();
                this.DialogResult = DialogResult.None;
            }
            else
            {
                bool idexist = GeneralDataFuctions.ValidateSampleId(txtId.Text);

                if (!idexist || txtId.Text == id)
                {
                    MessageBox.Show("You entered an invalid ID.");
                    txtId.Focus();
                    this.DialogResult = DialogResult.None;
                }
                else
                {
                    Sample smp = sdata.GetSampleData(txtId.Text);
                    Master mst = mdata.GetMasterData(smp.Masterid);

                    if (mst.Owner != "M")
                    {
                        MessageBox.Show("The Parent ID entered must be a Multi-family case.");
                        txtId.Focus();
                        this.DialogResult = DialogResult.None;
                    }
                    else if (smp.Status != "1" && smp.Status != "2" && smp.Status != "3" && smp.Status != "4" && smp.Status != "7")
                    {
                        MessageBox.Show("You must enter the ID of the Original Project.");
                        txtId.Focus();
                        this.DialogResult = DialogResult.None;
                    }
                    else
                    {
                        //update subproject data
                        SubprojectData subpData = new SubprojectData();
                        bool result = subpData.AddSubprojectParent(master_id, mst.Masterid, "5");

                        //get parent subproject data
                        int tbldgs = 0;
                        int tunits = 0;
                        subpData.GetSubprojectParentBldgsUnits(mst.Masterid, ref tbldgs, ref tunits);

                        //update parent soc data
                        SocData scdata = new SocData();
                        Soc sc = scdata.GetSocData(mst.Masterid);
                        sc.Bldgs = tbldgs;
                        sc.Units = tunits;

                        if (sc.Rbldgs == tbldgs && sc.Runits == tunits)
                            sc.Unitflg = "";
                        else
                            sc.Unitflg = "*";

                        scdata.SaveSocData(sc);

                        //Save status for ID
                        Sample sp = sdata.GetSampleData(id);
                        sp.Status = "5";
                        bool resualt = sdata.SaveSampleData(sp);

                        this.Dispose();

                    }
                }

            }
        }

        private void txtId_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
        }
        
    }
}
