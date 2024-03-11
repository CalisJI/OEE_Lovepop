using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using OEE_dotNET.Database;
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;

namespace OEE_dotNET.ViewModel
{
    public partial class Option3PageViewModel :ObservableObject
    {
        private List<Rowobject>? rowobjects = new List<Rowobject>();

        [ObservableProperty]
        private DataTable? lazer_config_tbl = new DataTable()
        {
            Columns =
            {
                new DataColumn("id",typeof(int)),
                new DataColumn("color",typeof(string)),
                new DataColumn("tanso",typeof(double)),
                new DataColumn("nangluong",typeof(double)),
                new DataColumn("step_size",typeof(double)),
                new DataColumn("dotre_trunggian",typeof(double)),
                new DataColumn("dotre_tat",typeof(double)),
                new DataColumn("delay",typeof(double)),
                new DataColumn("ucche_nangluong",typeof(double)),
                new DataColumn("ucche_soluong",typeof (double)),
                new DataColumn("thoigian_tamdung",typeof(double)),
                new DataColumn("solan_laplai",typeof(int))
            }
        };

        [ObservableProperty]
        private bool editable = false;

        public Option3PageViewModel()
        {
            try
            {
                Lazer_config_tbl = DatabaseExcute_Main.LoadConfig();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }


        [RelayCommand]
        public void Loaded() 
        {
            Lazer_config_tbl?.AcceptChanges();
        }

        [RelayCommand]
        public void Edit() 
        {
            Editable = true;
        }
        [RelayCommand]
        public void Apply() 
        {
            try
            {
                if (rowobjects != null) 
                {
                    DatabaseExcute_Main.UpdateLazerconfig(rowobjects);
                    rowobjects.Clear();
                    Lazer_config_tbl?.AcceptChanges();
                    Editable = false;
                    MessageBox.Show("OK");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        [RelayCommand]
        public void Cancel() 
        {
            Editable = false;
        }
        [RelayCommand]
        public void Cell_edited() 
        {
            var check = Lazer_config_tbl?.GetChanges(DataRowState.Modified);

            if (check != null && check.Rows.Count > 0)
            {
                // There are modified rows
                foreach (DataRow row in check.Rows)
                {
                    Rowobject rowobject = new Rowobject();
                    rowobject.id = Convert.ToInt32(row["id"].ToString());
                    rowobject.nangluong = Convert.ToDouble(row["nangluong"].ToString());
                    rowobject.color = row["color"].ToString();
                    rowobject.step_size = Convert.ToDouble(row["step_size"].ToString());
                    rowobject.dotre_trunggian = Convert.ToDouble(row["dotre_trunggian"].ToString());
                    rowobject.dotre_tat = Convert.ToDouble(row["dotre_tat"].ToString());
                    rowobject.delay = Convert.ToDouble(row["delay"].ToString());
                    rowobject.ucche_nangluong = Convert.ToDouble(row["ucche_nangluong"].ToString());
                    rowobject.ucche_soluong = Convert.ToDouble(row["ucche_soluong"].ToString());
                    rowobject.thoigian_tamdung = Convert.ToDouble(row["thoigian_tamdung"].ToString());
                    rowobject.solan_laplai = Convert.ToInt32(row["solan_laplai"].ToString());

                    var check_contain = rowobjects?.FirstOrDefault(x => x.id == rowobject.id);
                    if (rowobjects != null && check_contain == null)
                    {
                        rowobjects?.Add(rowobject);
                    }
                    else if (check_contain != null)
                    {
                        check_contain.tanso = rowobject.tanso;
                        check_contain.nangluong = rowobject.nangluong;
                        check_contain.step_size = rowobject.step_size;
                        check_contain.dotre_trunggian = rowobject.dotre_trunggian;
                        check_contain.dotre_tat = rowobject.dotre_tat;
                        check_contain.delay = rowobject.delay;
                        check_contain.ucche_nangluong = rowobject.ucche_nangluong;
                        check_contain.ucche_soluong = rowobject.ucche_soluong;
                        check_contain.thoigian_tamdung = rowobject.thoigian_tamdung;
                        check_contain.solan_laplai = rowobject.solan_laplai;
                    }
                }
                Lazer_config_tbl?.AcceptChanges();
            }
        }
    }
   
    public class Rowobject
    {
        public int id { get; set; }
        public string? color { get; set; }
        public double tanso { get; set; }
        public double nangluong { get; set; }
        public double step_size { get; set; }
        public double dotre_trunggian { get; set; }
        public double dotre_tat { get; set; }
        public double delay { get; set; }
        public double ucche_nangluong { get; set; }
        public double ucche_soluong { get; set; }
        public double thoigian_tamdung { get; set; }
        public int solan_laplai { get; set; }
    }
}
