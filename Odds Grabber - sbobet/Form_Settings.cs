using Odds_Grabber___sbobet.Properties;
using System;
using System.Configuration;
using System.Diagnostics;
using System.IO;
using System.Windows.Forms;

namespace Odds_Grabber___sbobet
{
    public partial class Form_Settings : Form
    {
        private string __app__website_name = "Odds Grabber - sbobet";

        public Form_Settings()
        {
            InitializeComponent();

            ___Reset();
        }

        private string ___Replace(string name)
        {
            name = name.Replace("<setting name=\"______", "").Replace("_", " ").Replace("is", "").Replace("\" serializeAs=\"String\">", "").Replace("<value>", "").Replace("</value>", "").Replace("amp;", "");
            name = System.Threading.Thread.CurrentThread.CurrentCulture.TextInfo.ToTitleCase(name.ToLower().Trim());

            return name;
        }

        private string ___Replace_RealName_Link(string name)
        {
            name = name.Replace("<setting name=\"", "").Replace("\" serializeAs=\"String\">", "").Replace("<value>", "").Replace("</value>", "").Replace("amp;", "").Trim();

            return name;
        }

        private void button_cancel_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void button_save_Click(object sender, EventArgs e)
        {
            bool _is_blank = false;

            try
            {
                foreach (DataGridViewRow row in dataGridView_settings.Rows)
                {
                    string _real_name = row.Cells["real_name"].Value.ToString().Trim();
                    string _value = row.Cells["value"].Value.ToString().Trim();

                    if (_real_name == "______is_send_telegram")
                    {
                        if (_value == "True" || _value == "False")
                        {
                            if (_value != "")
                            {
                                Settings.Default[_real_name] = bool.Parse(_value);
                            }
                            else
                            {
                                _is_blank = true;
                                MessageBox.Show("Fill up the blank row.", __app__website_name, MessageBoxButtons.OK, MessageBoxIcon.Information);
                                break;
                            }
                        }
                        else
                        {
                            _is_blank = true;
                            MessageBox.Show("Send Telegram can accept \"True\" or \"False\" format only.", __app__website_name, MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                    }
                    else
                    {
                        if (_value != "")
                        {
                            Settings.Default[_real_name] = _value;
                        }
                        else
                        {
                            _is_blank = true;
                            MessageBox.Show("Fill up the blank row.", __app__website_name, MessageBoxButtons.OK, MessageBoxIcon.Information);
                            break;
                        }
                    }
                }

                if (!_is_blank)
                {
                    Settings.Default.Save();
                    ___Reset();

                    Process.Start(Application.ExecutablePath);
                    Environment.Exit(0);
                }
            }
            catch (Exception err)
            {
                MessageBox.Show("Fill out the blank row.", __app__website_name, MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void ___Reset()
        {
            try
            {
                dataGridView_settings.Rows.Clear();
                dataGridView_settings.ClearSelection();

                var path = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.PerUserRoamingAndLocal).FilePath;
                using (StreamReader file = new StreamReader(path))
                {
                    string ln;
                    string _real_name = "";
                    string _name = "";
                    string _value = "";
                    bool _is_next = false;

                    int _count = 0;
                    while ((ln = file.ReadLine()) != null)
                    {
                        if (_is_next)
                        {
                            _value = ___Replace_RealName_Link(ln);
                            //if (_value != "True" && _value != "False")
                            //{
                            //    _value = ___Replace(ln).ToLower();
                            //}
                            dataGridView_settings.Rows.Insert(_count, _real_name, _name, _value);
                            _count++;

                            _is_next = false;
                        }

                        if (ln.Contains("<setting name="))
                        {
                            if (!ln.Contains("______odds_iswaiting_01") && !ln.Contains("______odds_iswaiting_02") && !ln.Contains("______odds_issend_01")
                                && !ln.Contains("______odds_issend_02") && !ln.Contains("______odds"))
                            {
                                _real_name = ___Replace_RealName_Link(ln);
                                _name = ___Replace(ln);
                                _is_next = true;
                            }
                        }

                    }

                    file.Close();
                }
            }
            catch (Exception err)
            {
                if (err.ToString().ToLower().Contains("directorynotfound"))
                {
                    SettingsPropertyCollection props = Settings.Default.Properties;
                    foreach (SettingsProperty propItem in props)
                    {
                        if (!propItem.Name.Contains("______odds_iswaiting_01") && !propItem.Name.Contains("______odds_iswaiting_02") && !propItem.Name.Contains("______odds_issend_01")
                                && !propItem.Name.Contains("______odds_issend_02") && !propItem.Name.Contains("______odds"))
                        {
                            if (propItem.Name == "______is_send_telegram")
                            {
                                Settings.Default[propItem.Name] = bool.Parse(propItem.DefaultValue.ToString());
                            }
                            else
                            {
                                Settings.Default[propItem.Name] = propItem.DefaultValue;
                            }

                            Settings.Default.Save();
                        }
                    }

                    ___Reset();
                }
            }
        }

        private void button_save_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                Close();
            }
        }

        private void button_cancel_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                Close();
            }
        }

        private void dataGridView_settings_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                Close();
            }
        }
    }
}
