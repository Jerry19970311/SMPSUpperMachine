using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;


namespace WindowsFormsApp1
{
    partial class Form1
    {
        private Dictionary<int, string> forFormat = new Dictionary<int, string>();
        private Dictionary<string, Control> forText = new Dictionary<string, Control>();
        //键：文本信息；值：对应发送的值
        private Dictionary<string, string> relayType = new Dictionary<string, string>();
        private void writeInit()
        {
            forLengthInit();
            forFormatInit();
            forTextInit();
            relayTypeInit();
        }
        private void forLengthInit()
        {
            forLength.Add(Para.CID1_AC + Para.CID2_ACV_HIGH_ALARM_SET, 2);
            forLength.Add(Para.CID1_AC + Para.CID2_ACV_LOW_ALARM_SET, 2);
            forLength.Add(Para.CID1_AC + Para.CID2_AC_DOWN_ALARM_SET, 2);
            forLength.Add(Para.CID1_AC + Para.CID2_ACI_HIGH_ALARM_SET, 2);
            forLength.Add(Para.CID1_MODULE + Para.CID2_LIMIT_I_SET, 2);
            forLength.Add(Para.CID1_DC + Para.CID2_RI_HIGH_ALARM_SET, 2);
            forLength.Add(Para.CID1_DC + Para.CID2_BATI_HIGH_ALARM_SET, 2);
            forLength.Add(Para.CID1_DC + Para.CID2_V_HIGH_SET, 2);
            forLength.Add(Para.CID1_DC + Para.CID2_BATV_LOW_ALARM_SET, 2);
            forLength.Add(Para.CID1_DC + Para.CID2_FREEV_OFF_1_SET, 2);
            forLength.Add(Para.CID1_DC + Para.CID2_FREEV_RESTART_1_SET, 2);
            forLength.Add(Para.CID1_DC + Para.CID2_FREEV_OFF_2_SET, 2);
            forLength.Add(Para.CID1_DC + Para.CID2_FREEV_RESTART_2_SET, 2);
            forLength.Add(Para.CID1_DC + Para.CID2_FLOATING_V_SET, 2);
            forLength.Add(Para.CID1_DC + Para.CID2_REGULAR_AVG_V_SET, 2);
            forLength.Add(Para.CID1_DC + Para.CID2_AVG_TO_FLOAT_SET, 2);
            forLength.Add(Para.CID1_DC + Para.CID2_FLOAT_TO_AVG_SET, 2);
            forLength.Add(Para.CID1_DC + Para.CID2_CHARGING_LIMITED_PERCENT_SET, 2);
            forLength.Add(Para.CID1_DC + Para.CID2_TEMP_COMPENSATION_SET, 1);
            forLength.Add(Para.CID1_DC + "3E", 2);
            forLength.Add(Para.CID1_DC + Para.CID2_MANUAL_AVG_SET, 1);
            forLength.Add(Para.CID1_DC + Para.CID2_REGULAR_AVG_ENERGY_SET, 1);
            forLength.Add(Para.CID1_DC + Para.CID2_REGULAR_AVG_TIME_GAP_SET, 2);
            forLength.Add(Para.CID1_DC + Para.CID2_REGULAR_AVG_TIME_SET, 2);
            forLength.Add(Para.CID1_DC + Para.CID2_MONITOR_VOICE_SET, 1);
            forLength.Add(Para.CID1_DC + Para.CID2_CLOCK_SET, 7);
            forLength.Add(Para.CID1_DC + Para.CID2_BAT_VOL1_SET, 2);
            forLength.Add(Para.CID1_DC + Para.CID2_BAT_VOL_PERCENT_SET, 2);
            forLength.Add(Para.CID1_DC + Para.CID2_SLEEP_ENERGY_SET, 1);
            forLength.Add(Para.CID1_DC + Para.CID2_SLEEP_TIME_GAP_SET, 2);
            forLength.Add(Para.CID1_DC + Para.CID2_LIMITED_MIN_V_SET, 2);
            forLength.Add(Para.CID1_DC + Para.CID2_CURRENT_SLOPE_SET, 2);
            forLength.Add(Para.CID1_DC + Para.CID2_CURRENT_ZERO_SET, 2);
            forLength.Add(Para.CID1_DC + Para.CID2_BUS_OUTPUT_V_SLOPE_SET, 2);
            forLength.Add(Para.CID1_DC + Para.CID2_BAT_TEST_ENERGY_SET, 1);
            forLength.Add(Para.CID1_DC + Para.CID2_BAT_TEST_START_V_SET, 2);
            forLength.Add(Para.CID1_DC + Para.CID2_BAT_TEST_END_V_SET, 2);
            forLength.Add(Para.CID1_DC + Para.CID2_FAST_CHARGE_SET, 1);
            forLength.Add(Para.CID1_DC + Para.CID2_FAST_V_SET, 2);
            forLength.Add(Para.CID1_DC + Para.CID2_FAST_TIME_SET, 2);
            //（电池低温告警点目前尚未解决）
            forLength.Add(Para.CID1_DC + Para.CID2_BAT_TEMP_LOW_ALARM_SET, 2);
            forLength.Add(Para.CID1_DC + Para.CID2_BAT_TEMP_HIGH_ALARM_SET, 2);
            forLength.Add(Para.CID1_DC + Para.CID2_LOAD_CURRENT_SLOPE_SET, 2);
            forLength.Add(Para.CID1_DC + Para.CID2_BATTERY_ADD_SET, 2);
            forLength.Add(Para.CID1_DC + Para.CID2_BATTERY_OFFSET_SET, 2);
            forLength.Add(Para.CID1_DC + Para.CID2_ENV_ADD_SET, 2);
            forLength.Add(Para.CID1_DC + Para.CID2_ENV_OFFSET_SET, 2);
            forLength.Add(Para.CID1_DC + Para.CID2_A_AC_V_ADD_SET, 2);
            forLength.Add(Para.CID1_DC + Para.CID2_A_AC_V_ZERO_SET, 2);
            forLength.Add(Para.CID1_DC + Para.CID2_LOAD_CURRENT_ZERO_SET, 2);
        }
        private void forFormatInit()
        {
            forFormat.Add(1, "{0:X2}");
            forFormat.Add(2, "{0:X4}");
            forFormat.Add(3, "{0:X6}");
            forFormat.Add(4, "{0:X8}");
        }
        private void forTextInit()
        {
            forText.Add(Para.CID1_AC + Para.CID2_ACV_HIGH_ALARM_SET, textBox1);
            forText.Add(Para.CID1_AC + Para.CID2_ACV_LOW_ALARM_SET, textBox2);
            forText.Add(Para.CID1_AC + Para.CID2_AC_DOWN_ALARM_SET, textBox3);
            forText.Add(Para.CID1_AC + Para.CID2_ACI_HIGH_ALARM_SET, textBox4);
            forText.Add(Para.CID1_MODULE + Para.CID2_LIMIT_I_SET, textBox5);
            forText.Add(Para.CID1_DC + Para.CID2_RI_HIGH_ALARM_SET, textBox6);
            forText.Add(Para.CID1_DC + Para.CID2_BATI_HIGH_ALARM_SET, textBox7);
            forText.Add(Para.CID1_DC + Para.CID2_V_HIGH_SET, textBox8);
            forText.Add(Para.CID1_DC + Para.CID2_BATV_LOW_ALARM_SET, textBox9);
            forText.Add(Para.CID1_DC + Para.CID2_FREEV_OFF_1_SET, textBox10);
            forText.Add(Para.CID1_DC + Para.CID2_FREEV_RESTART_1_SET, textBox11);
            forText.Add(Para.CID1_DC + Para.CID2_FREEV_OFF_2_SET, textBox12);
            forText.Add(Para.CID1_DC + Para.CID2_FREEV_RESTART_2_SET, textBox13);
            forText.Add(Para.CID1_DC + Para.CID2_FLOATING_V_SET, textBox14);
            forText.Add(Para.CID1_DC + Para.CID2_REGULAR_AVG_V_SET, textBox15);
            forText.Add(Para.CID1_DC + Para.CID2_AVG_TO_FLOAT_SET, textBox16);
            forText.Add(Para.CID1_DC + Para.CID2_FLOAT_TO_AVG_SET, textBox17);
            forText.Add(Para.CID1_DC + Para.CID2_CHARGING_LIMITED_PERCENT_SET, textBox18);
            forText.Add(Para.CID1_DC + Para.CID2_TEMP_COMPENSATION_SET, checkBox1);
            forText.Add(Para.CID1_DC + "3E", textBox19);
            forText.Add(Para.CID1_DC + Para.CID2_MANUAL_AVG_SET, checkBox2);
            forText.Add(Para.CID1_DC + Para.CID2_REGULAR_AVG_ENERGY_SET, checkBox3);
            forText.Add(Para.CID1_DC + Para.CID2_REGULAR_AVG_TIME_GAP_SET, textBox20);
            forText.Add(Para.CID1_DC + Para.CID2_REGULAR_AVG_TIME_SET, textBox21);
            forText.Add(Para.CID1_DC + Para.CID2_MONITOR_VOICE_SET, comboBox1);
            forText.Add(Para.CID1_DC + Para.CID2_CLOCK_SET, textBox23);
            forText.Add(Para.CID1_DC + Para.CID2_BAT_VOL1_SET, textBox22);
            forText.Add(Para.CID1_DC + Para.CID2_BAT_VOL_PERCENT_SET, textBox24);
            forText.Add(Para.CID1_DC + Para.CID2_SLEEP_ENERGY_SET, checkBox4);
            forText.Add(Para.CID1_DC + Para.CID2_SLEEP_TIME_GAP_SET, textBox25);
            forText.Add(Para.CID1_DC + Para.CID2_LIMITED_MIN_V_SET, textBox26);
            forText.Add(Para.CID1_DC + Para.CID2_CURRENT_SLOPE_SET, textBox27);
            forText.Add(Para.CID1_DC + Para.CID2_CURRENT_ZERO_SET, textBox28);
            forText.Add(Para.CID1_DC + Para.CID2_BUS_OUTPUT_V_SLOPE_SET, textBox29);
            forText.Add(Para.CID1_DC + Para.CID2_BAT_TEST_ENERGY_SET, checkBox5);
            forText.Add(Para.CID1_DC + Para.CID2_BAT_TEST_START_V_SET, textBox39);
            forText.Add(Para.CID1_DC + Para.CID2_BAT_TEST_END_V_SET, textBox40);
            forText.Add(Para.CID1_DC + Para.CID2_FAST_CHARGE_SET, checkBox6);
            forText.Add(Para.CID1_DC + Para.CID2_FAST_V_SET, textBox41);
            forText.Add(Para.CID1_DC + Para.CID2_FAST_TIME_SET, textBox42);
            //（电池低温告警点目前尚未解决）
            forText.Add(Para.CID1_DC + Para.CID2_BAT_TEMP_LOW_ALARM_SET, textBox43);
            forText.Add(Para.CID1_DC + Para.CID2_BAT_TEMP_HIGH_ALARM_SET, textBox44);
            forText.Add(Para.CID1_DC + Para.CID2_LOAD_CURRENT_SLOPE_SET, textBox46);
            forText.Add(Para.CID1_DC + Para.CID2_BATTERY_ADD_SET, textBox50);
            forText.Add(Para.CID1_DC + Para.CID2_BATTERY_OFFSET_SET, textBox45);
            forText.Add(Para.CID1_DC + Para.CID2_ENV_ADD_SET, textBox49);
            forText.Add(Para.CID1_DC + Para.CID2_ENV_OFFSET_SET, textBox47);
            forText.Add(Para.CID1_DC + Para.CID2_A_AC_V_ADD_SET, textBox51);
            forText.Add(Para.CID1_DC + Para.CID2_A_AC_V_ZERO_SET, textBox48);
            forText.Add(Para.CID1_DC + Para.CID2_LOAD_CURRENT_ZERO_SET, textBox52);

            //干结点类型，必须特殊处理
            forText.Add(Para.CID1_DC + Para.CID2_RELAY_TYPE_SET + "01", comboBox2);
            forText.Add(Para.CID1_DC + Para.CID2_RELAY_TYPE_SET + "02", comboBox3);
            forText.Add(Para.CID1_DC + Para.CID2_RELAY_TYPE_SET + "03", comboBox4);
            forText.Add(Para.CID1_DC + Para.CID2_RELAY_TYPE_SET + "04", comboBox5);
            //干结点状态，必须特殊处理
            forText.Add(Para.CID1_DC + Para.CID2_RELAY_STATUS_SET + "01", comboBox6);
            forText.Add(Para.CID1_DC + Para.CID2_RELAY_STATUS_SET + "02", comboBox7);
            forText.Add(Para.CID1_DC + Para.CID2_RELAY_STATUS_SET + "03", comboBox8);
            forText.Add(Para.CID1_DC + Para.CID2_RELAY_STATUS_SET + "04", comboBox9);
        }
        //（继电器的对应关系确定以后，直接往里面写）
        private void relayTypeInit()
        {
            relayType.Add("自定义","00");
            relayType.Add("交流停电", "01");
            relayType.Add("交流过欠压", "02");
            relayType.Add("直流过欠压", "03");
            relayType.Add("电源模块", "04");
            relayType.Add("LVD", "05");
            relayType.Add("一次下电", "06");
            relayType.Add("负载熔丝", "07");
            relayType.Add("电池熔丝", "08");
            relayType.Add("环境温度告警", "09");
            relayType.Add("门禁", "0A");
            relayType.Add("烟禁", "0B");
            relayType.Add("水浸", "0C");
            relayType.Add("防雷/系统故障", "0D");
        }
        private void WriteNumber(string cid1,string cid2)
        {
            string s = forText[cid1 + cid2].Text;
            double origin = Convert.ToDouble(s);
            if (string10Set.Contains(cid1 + cid2)){
                origin = origin * 10;
            }
            if (string100Set.Contains(cid1 + cid2))
            {
                origin = origin * 100;
            }
            int length = forLength[cid1 + cid2];
            string data = String.Format(forFormat[length], Convert.ToInt32(origin));
            Write(cid1, cid2, data);
        }
        private void WriteChecked(string cid1, string cid2)
        {
            CheckBox checkBox = (CheckBox)forText[cid1 + cid2];
            string data;
            if (checkBox.Checked)
            {
                data = "01";
            }
            else
            {
                data = "00";
            }
            Write(cid1, cid2, data);
        }
        //继电器写方法，inde为继电器序号，isType为类型或状态，true为类型，false为状态。
        private void WriteRelayType(int inde)
        {
            string cid1 = Para.CID1_DC;
            string cid2 = Para.CID2_RELAY_TYPE_SET;
            string ad = String.Format("{0:X2}", inde);
            ComboBox combo = (ComboBox)forText[cid1 + cid2 + ad];
            string data = ad + relayType[combo.Text];
            Write(cid1, cid2, data);
        }
        private void WriteRelayStatus(int inde)
        {
            string cid1 = Para.CID1_DC;
            string cid2 = Para.CID2_RELAY_STATUS_SET;
            string ad = String.Format("{0:X2}", inde);
            ComboBox combo = (ComboBox)forText[cid1 + cid2 + ad];
            if ("开".Equals(combo.Text))
            {
                Write(cid1, cid2, ad + "00");
            }
            if ("关".Equals(combo.Text))
            {
                Write(cid1, cid2, ad + "01");
            }
        }
        private void Write(string cid1, string cid2, string data)
        {
            //待所有写指令测试无误后，再来使用这一方法。
            Send(cid1, cid2, data);
            Console.Write("测试：");
            Console.Write("Cid1:" + cid1);
            Console.Write("\t\tCid2:" + cid2);
            Console.WriteLine("\t\tData:" + data);
        }
        //交流电压过高（写）
        private void button18_Click(object sender, EventArgs e)
        {
            WriteNumber(Para.CID1_AC, Para.CID2_ACV_HIGH_ALARM_SET);
        }
        //交流电压过低（写）
        private void button19_Click(object sender, EventArgs e)
        {
            WriteNumber(Para.CID1_AC, Para.CID2_ACV_LOW_ALARM_SET);
        }
        //交流电压掉电（写）
        private void button20_Click(object sender, EventArgs e)
        {
            WriteNumber(Para.CID1_AC, Para.CID2_AC_DOWN_ALARM_SET);
        }
        //交流电流过高（写）
        private void button21_Click(object sender, EventArgs e)
        {
            WriteNumber(Para.CID1_AC, Para.CID2_ACI_HIGH_ALARM_SET);
        }
        //48V模块限流点（写）
        private void button22_Click(object sender, EventArgs e)
        {
            WriteNumber(Para.CID1_MODULE, Para.CID2_LIMIT_I_SET);
        }
        //负载电流过高（写）
        private void button23_Click(object sender, EventArgs e)
        {
            WriteNumber(Para.CID1_DC, Para.CID2_RI_HIGH_ALARM_SET);
        }
        //电池电流过高（写）
        private void button24_Click(object sender, EventArgs e)
        {
            WriteNumber(Para.CID1_DC, Para.CID2_BATI_HIGH_ALARM_SET);
        }
        //输出过压告警（写）
        private void button25_Click(object sender, EventArgs e)
        {
            WriteNumber(Para.CID1_DC, Para.CID2_V_HIGH_SET);
        }
        //电池低压告警（写）
        private void button26_Click(object sender, EventArgs e)
        {
            WriteNumber(Para.CID1_DC, Para.CID2_BATV_LOW_ALARM_SET);
        }
        //负载下电告警（写）
        private void button27_Click(object sender, EventArgs e)
        {
            WriteNumber(Para.CID1_DC, Para.CID2_FREEV_OFF_1_SET);
        }
        //负载下电恢复（写）
        private void button28_Click(object sender, EventArgs e)
        {
            WriteNumber(Para.CID1_DC, Para.CID2_FREEV_RESTART_1_SET);
        }
        //电池下电告警（写）
        private void button29_Click(object sender, EventArgs e)
        {
            WriteNumber(Para.CID1_DC, Para.CID2_FREEV_OFF_2_SET);
        }
        //电池下电恢复（写）
        private void button30_Click(object sender, EventArgs e)
        {
            WriteNumber(Para.CID1_DC, Para.CID2_FREEV_RESTART_2_SET);
        }
        //浮充电压设定（写）
        private void button31_Click(object sender, EventArgs e)
        {
            WriteNumber(Para.CID1_DC, Para.CID2_FLOATING_V_SET);
        }
        //均充电压设定（写）
        private void button32_Click(object sender, EventArgs e)
        {
            WriteNumber(Para.CID1_DC, Para.CID2_REGULAR_AVG_V_SET);
        }
        //均转浮系数（写）
        private void button33_Click(object sender, EventArgs e)
        {
            WriteNumber(Para.CID1_DC, Para.CID2_AVG_TO_FLOAT_SET);
        }
        //浮转均系数（写）
        private void button34_Click(object sender, EventArgs e)
        {
            WriteNumber(Para.CID1_DC, Para.CID2_FLOAT_TO_AVG_SET);
        }
        //充电限流系数（写）
        private void button38_Click(object sender, EventArgs e)
        {
            WriteNumber(Para.CID1_DC, Para.CID2_CHARGING_LIMITED_PERCENT_SET);
        }
        //温补使能（写）
        private void button39_Click(object sender, EventArgs e)
        {
            WriteChecked(Para.CID1_DC, Para.CID2_TEMP_COMPENSATION_SET);
        }
        //温补系数（写）
        private void button40_Click(object sender, EventArgs e)
        {
            WriteNumber(Para.CID1_DC, "3E");
        }
        //手动补充&定期补充（写）
        private void button41_Click(object sender, EventArgs e)
        {
            WriteChecked(Para.CID1_DC, Para.CID2_MANUAL_AVG_SET);
            Delay(100);
            WriteChecked(Para.CID1_DC, Para.CID2_REGULAR_AVG_ENERGY_SET);
        }
        //定期均充间隔（写）
        private void button45_Click(object sender, EventArgs e)
        {
            WriteNumber(Para.CID1_DC, Para.CID2_REGULAR_AVG_TIME_GAP_SET);
        }
        //持续均充时间（写）
        private void button46_Click(object sender, EventArgs e)
        {
            WriteNumber(Para.CID1_DC, Para.CID2_REGULAR_AVG_TIME_SET);
        }
        //蜂鸣告警（写）
        private void button50_Click(object sender, EventArgs e)
        {
            ComboBox combo = (ComboBox)forText[Para.CID1_DC + Para.CID2_MONITOR_VOICE_SET];
            if ("关闭".Equals(combo.Text))
            {
                Write(Para.CID1_DC, Para.CID2_MONITOR_VOICE_SET, "00");
            }
            if ("开启".Equals(combo.Text))
            {
                Write(Para.CID1_DC, Para.CID2_MONITOR_VOICE_SET, "01");
            }
            if ("本次关".Equals(combo.Text))
            {
                Write(Para.CID1_DC, Para.CID2_MONITOR_VOICE_SET, "02");
            }
        }
        //时间校准
        private void button49_Click(object sender, EventArgs e)
        {
            DateTime dateTime = DateTime.Now;
            //Console.WriteLine(dateTime.ToString("yyyy-MM-dd HH:mm:ss"));
            textBox23.Text = dateTime.ToString("yyyy-MM-dd HH:mm:ss");
            int year = Convert.ToInt32(dateTime.ToString("yy"));
            int month = Convert.ToInt32(dateTime.ToString("MM"));
            int day = Convert.ToInt32(dateTime.ToString("dd"));
            int hour = Convert.ToInt32(dateTime.ToString("HH"));
            int minute = Convert.ToInt32(dateTime.ToString("mm"));
            int second = Convert.ToInt32(dateTime.ToString("ss"));
            StringBuilder time = new StringBuilder();
            time.Append(String.Format("{0:X2}", second));
            time.Append(String.Format("{0:X2}", minute));
            time.Append(String.Format("{0:X2}", hour));
            time.Append(String.Format("{0:X2}", day));
            time.Append(String.Format("{0:X2}", 0));
            time.Append(String.Format("{0:X2}", month));
            time.Append(String.Format("{0:X2}", year));
            Write(Para.CID1_DC, Para.CID2_CLOCK_SET, time.ToString());
        }
        //电池容量（写）
        private void button48_Click(object sender, EventArgs e)
        {
            WriteNumber(Para.CID1_DC, Para.CID2_BAT_VOL1_SET);
        }
        //电池容量保持（写）
        private void button55_Click(object sender, EventArgs e)
        {
            WriteNumber(Para.CID1_DC, Para.CID2_BAT_VOL_PERCENT_SET);
        }
        //休眠使能（写）
        private void button57_Click(object sender, EventArgs e)
        {
            WriteChecked(Para.CID1_DC, Para.CID2_SLEEP_ENERGY_SET);
        }
        //休眠间隔时间（写）
        private void button59_Click(object sender, EventArgs e)
        {
            WriteNumber(Para.CID1_DC, Para.CID2_SLEEP_TIME_GAP_SET);
        }
        //限流最小电压（写）
        private void button61_Click(object sender, EventArgs e)
        {
            WriteNumber(Para.CID1_DC, Para.CID2_LIMITED_MIN_V_SET);
        }
        //电池电流增益（写）
        private void button63_Click(object sender, EventArgs e)
        {
            WriteNumber(Para.CID1_DC, Para.CID2_CURRENT_SLOPE_SET);
        }
        //电池电流偏移（写）
        private void button65_Click(object sender, EventArgs e)
        {
            WriteNumber(Para.CID1_DC, Para.CID2_CURRENT_ZERO_SET);
        }
        //母排电压增益（写）
        private void button67_Click(object sender, EventArgs e)
        {
            WriteNumber(Para.CID1_DC, Para.CID2_BUS_OUTPUT_V_SLOPE_SET);
        }
        //干结点1类型（写）
        private void button73_Click(object sender, EventArgs e)
        {
            WriteRelayType(1);
        }
        //干结点2类型（写）
        private void button76_Click(object sender, EventArgs e)
        {
            WriteRelayType(2);
        }
        //干结点3类型（写）
        private void button80_Click(object sender, EventArgs e)
        {
            WriteRelayType(3);
        }
        //干结点4类型（写）
        private void button84_Click(object sender, EventArgs e)
        {
            WriteRelayType(4);
        }
        //干结点1状态（写）
        private void button74_Click(object sender, EventArgs e)
        {
            WriteRelayStatus(1);
        }
        //干结点2状态（写）
        private void button78_Click(object sender, EventArgs e)
        {
            WriteRelayStatus(2);
        }
        //干结点3状态（写）
        private void button83_Click(object sender, EventArgs e)
        {
            WriteRelayStatus(3);
        }
        //干结点4状态（写）
        private void button87_Click(object sender, EventArgs e)
        {
            WriteRelayStatus(4);
        }
        //电池测试使能（写）
        private void button96_Click(object sender, EventArgs e)
        {
            WriteChecked(Para.CID1_DC, Para.CID2_BAT_TEST_ENERGY_SET);
        }
        //电池测试开始电压（写）
        private void button101_Click(object sender, EventArgs e)
        {
            WriteNumber(Para.CID1_DC, Para.CID2_BAT_TEST_START_V_SET);
        }
        //电池测试结束电压（写）
        private void button97_Click(object sender, EventArgs e)
        {
            WriteNumber(Para.CID1_DC, Para.CID2_BAT_TEST_END_V_SET);
        }
        //快充使能（写）
        private void button102_Click(object sender, EventArgs e)
        {
            WriteChecked(Para.CID1_DC, Para.CID2_FAST_CHARGE_SET);
        }
        //快充电压（写）
        private void button103_Click(object sender, EventArgs e)
        {
            WriteNumber(Para.CID1_DC, Para.CID2_FAST_V_SET);
        }
        //快充时间（写）
        private void button98_Click(object sender, EventArgs e)
        {
            WriteNumber(Para.CID1_DC, Para.CID2_FAST_TIME_SET);
        }
        //电池低温告警点（写）
        //（由于解析问题尚未解决，故此方法暂不实现）
        private void button99_Click(object sender, EventArgs e)
        {

        }
        //电池高温告警点（写）
        private void button100_Click(object sender, EventArgs e)
        {
            WriteNumber(Para.CID1_DC, Para.CID2_BAT_TEMP_HIGH_ALARM_SET);
        }
        //负载电流增益（写）
        private void button113_Click(object sender, EventArgs e)
        {
            WriteNumber(Para.CID1_DC, Para.CID2_LOAD_CURRENT_SLOPE_SET);
        }
        //电池温度增益（写）
        private void button115_Click(object sender, EventArgs e)
        {
            WriteNumber(Para.CID1_DC, Para.CID2_BATTERY_ADD_SET);
        }
        //电池温度偏移（写）
        private void button117_Click(object sender, EventArgs e)
        {
            WriteNumber(Para.CID1_DC, Para.CID2_BATTERY_OFFSET_SET);
        }
        //环境温度增益（写）
        private void button119_Click(object sender, EventArgs e)
        {
            WriteNumber(Para.CID1_DC, Para.CID2_ENV_ADD_SET);
        }
        //环境温度偏移（写）
        private void button112_Click(object sender, EventArgs e)
        {
            WriteNumber(Para.CID1_DC, Para.CID2_ENV_OFFSET_SET);
        }
        //交流电压增益（写）
        private void button114_Click(object sender, EventArgs e)
        {
            WriteNumber(Para.CID1_DC, Para.CID2_A_AC_V_ADD_SET);
        }
        //交流电压偏移（写）
        private void button116_Click(object sender, EventArgs e)
        {
            WriteNumber(Para.CID1_DC, Para.CID2_A_AC_V_ZERO_SET);
        }
        //负载电压偏移（写）
        private void button118_Click(object sender, EventArgs e)
        {
            WriteNumber(Para.CID1_DC, Para.CID2_LOAD_CURRENT_ZERO_SET);
        }
        //清除历史记录
        private void button126_Click(object sender, EventArgs e)
        {
            Write(Para.CID1_DC, Para.CID2_HISTORY_CLEAR, "");
        }
    }
}
