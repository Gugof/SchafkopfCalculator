using Android.App;
using Android.OS;
using Android.Support.V7.App;
using Android.Runtime;
using Android.Widget;
using Java.Lang;

namespace SchafkopfCalculator
{
    [Activity(Label = "@string/app_name", Theme = "@style/Theme.Design.NoActionBar", Icon = "@drawable/SchafkopfCalcIcon", MainLauncher = true)]
    public class MainActivity : AppCompatActivity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {

            int result = 0;
            

            base.OnCreate(savedInstanceState);
            Xamarin.Essentials.Platform.Init(this, savedInstanceState);
            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.activity_main);

            var seekBarDoppler = FindViewById<SeekBar>(Resource.Id.seekBarDoppler);
            var varDoppler = FindViewById<TextView>(Resource.Id.varDoppler);

            var varTarifSau = FindViewById<EditText>(Resource.Id.varTarifSau);
            var varTarifSolo= FindViewById<EditText>(Resource.Id.varTarifSolo);

            var chkbSchneider = FindViewById<CheckBox>(Resource.Id.chkb_Schneider);
            var chkbSchwarz = FindViewById<CheckBox>(Resource.Id.chkb_Schwarz);

            var rbtnTout = FindViewById<RadioButton>(Resource.Id.rbtn_Tout);

            varTarifSau.Text = "10";
            varTarifSolo.Text = "20";

            //Attaching Lambda to Seekbar Progress
            seekBarDoppler.ProgressChanged += (s, e) => {
                varDoppler.Text = string.Format("{0}",e.Progress); 
            };


            var seekBarLauf = FindViewById<SeekBar>(Resource.Id.seekBarLauf);
            var varLauf = FindViewById<TextView>(Resource.Id.varLauf);

            
            seekBarLauf.ProgressChanged += (s, e) => {
                varLauf.Text = string.Format("{0}", e.Progress);
            };


            //Uncheck Schneider/Schwarz if other is checked
            chkbSchneider.CheckedChange += (s, e) => {
                if(chkbSchneider.Checked) { chkbSchwarz.Checked = false; }
               
            };

            chkbSchwarz.CheckedChange += (s, e) => {
                if (chkbSchwarz.Checked) { chkbSchneider.Checked = false; }
            };

            //Disable schwarz/Schneider if Tout is set
            rbtnTout.CheckedChange += (s, e) =>
            {
                if (rbtnTout.Checked)
                {
                    chkbSchwarz.Checked = false;
                    chkbSchneider.Checked = false;

                    chkbSchneider.Clickable = false;
                    chkbSchwarz.Clickable = false;
                }
                else if (rbtnTout.Checked == false)
                {
                    chkbSchneider.Clickable = true;
                    chkbSchwarz.Clickable = true;

                }


            };

            var btnBerechnen = FindViewById<Button>(Resource.Id.btnBerechnen);

           btnBerechnen.Click +=delegate
           {
                var varBetrag = FindViewById<TextView>(Resource.Id.varBetrag);

                var rbtnSau         = FindViewById<RadioButton>(Resource.Id.rbtn_sau);
                var rbtnSolo        = FindViewById<RadioButton>(Resource.Id.rbtn_solo);
                

                var chkbSchneider = FindViewById<CheckBox>(Resource.Id.chkb_Schneider);
                var chkbSchwarz = FindViewById<CheckBox>(Resource.Id.chkb_Schwarz);

                if (rbtnSau.Checked)
                {
                   if(chkbSchneider.Checked)
                   {
                       // (GrundtarifSauspiel + GrundtarifSauspiel(Schneider) + (GrundtarifSauspiel * Lauf)) * 2^AnzahlDoppler
                       result = (int.Parse(varTarifSau.Text) + int.Parse(varTarifSau.Text) + ((int.Parse(varTarifSau.Text) * int.Parse(varLauf.Text)))) * int.Parse((Math.Pow(2, double.Parse(varDoppler.Text))).ToString());
                   }

                   else if (chkbSchwarz.Checked)
                   {
                        result = (int.Parse(varTarifSau.Text) + int.Parse(varTarifSau.Text) + int.Parse(varTarifSau.Text) + ((int.Parse(varTarifSau.Text) * int.Parse(varLauf.Text)))) * int.Parse((Math.Pow(2, double.Parse(varDoppler.Text))).ToString());
                   } 

                   else
                   {
                       result = (int.Parse(varTarifSau.Text) + ((int.Parse(varTarifSau.Text) * int.Parse(varLauf.Text))))   * int.Parse((Math.Pow(2, double.Parse(varDoppler.Text))).ToString()); 

                   }

                }

                if (rbtnSolo.Checked)
                {
                   if(chkbSchneider.Checked)
                   {
                       result = (int.Parse(varTarifSolo.Text) + int.Parse(varTarifSau.Text) + ((int.Parse(varTarifSau.Text) * int.Parse(varLauf.Text)))) * int.Parse((Math.Pow(2, double.Parse(varDoppler.Text))).ToString());

                   }
                   else if(chkbSchwarz.Checked)
                   {
                       result = (int.Parse(varTarifSolo.Text) + int.Parse(varTarifSau.Text) + int.Parse(varTarifSau.Text) + ((int.Parse(varTarifSolo.Text) * int.Parse(varLauf.Text)))) * int.Parse((Math.Pow(2, double.Parse(varDoppler.Text))).ToString());

                   }
                   else
                   {
                       result = (int.Parse(varTarifSolo.Text) + ((int.Parse(varTarifSau.Text) * int.Parse(varLauf.Text)))) * int.Parse((Math.Pow(2, double.Parse(varDoppler.Text))).ToString());


                   }
                }

               if (rbtnTout.Checked)
               {
                   result = 2*((int.Parse(varTarifSolo.Text) + int.Parse(varTarifSau.Text) + int.Parse(varTarifSau.Text) + ((int.Parse(varTarifSau.Text) * int.Parse(varLauf.Text)))) * int.Parse((Math.Pow(2, double.Parse(varDoppler.Text))).ToString()));
               }
               

                varBetrag.Text = result.ToString();  
            };


        }
        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, [GeneratedEnum] Android.Content.PM.Permission[] grantResults)
        {
            Xamarin.Essentials.Platform.OnRequestPermissionsResult(requestCode, permissions, grantResults);

            base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        }
    }
}