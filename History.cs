using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WindowsFormsApp1
{
    partial class Form1
    {
        private void historyInit()
        {
            putHistories();
        }
        private void putHistories()
        {
            histories.Add("0101", "A相掉电");
            histories.Add("0102", "B相掉电");
            histories.Add("0103", "C相掉电");
            histories.Add("0104", "A相过压");
            histories.Add("0105", "B相过压");
            histories.Add("0106", "C相过压");
            histories.Add("0107", "A相欠压");
            histories.Add("0108", "B相欠压");
            histories.Add("0109", "C相欠压");
            histories.Add("010A", "A相过流");
            histories.Add("010B", "B相过流");
            histories.Add("010C", "C相过流");
            histories.Add("010D", "交流空开断");

            histories.Add("0201", "环境温度过高");
            histories.Add("0202", "环境温度过低");
            histories.Add("0203", "电池温度过高");
            histories.Add("0204", "电池温度过低");
            histories.Add("0205", "防雷失效");
            histories.Add("0206", "烟雾告警");
            histories.Add("0207", "水侵告警");
            histories.Add("0208", "门禁告警");
            histories.Add("0211", "空调1告警");
            histories.Add("0212", "空调2告警");
            histories.Add("0213", "空调3告警");
            histories.Add("0214", "空调4告警");
            histories.Add("0215", "倾斜告警");
            histories.Add("0216", "振动告警");

            histories.Add("0217", "MFD-24告警");
            histories.Add("0218", "MFD-48告警");
            histories.Add("0219", "1仓温低告警");
            histories.Add("021A", "2仓温低告警");
            histories.Add("021B", "3仓温低告警");
            histories.Add("021C", "4仓温低告警");
            histories.Add("021D", "1仓温高告警");
            histories.Add("021E", "2仓温高告警");
            histories.Add("022F", "3仓温高告警");
            histories.Add("0220", "4仓温高告警");

            histories.Add("0301", "输出过压");
            histories.Add("0302", "电池低压");
            histories.Add("0303", "一次下电");
            histories.Add("0304", "二次下电");
            histories.Add("0305", "负载熔丝断");
            histories.Add("0306", "电池熔丝断");
            histories.Add("0307", "电池电流过压");
            histories.Add("0308", "负载电流过高");

            histories.Add("04", "模块过压");
            histories.Add("05", "模块过温");
            histories.Add("06", "模块短路");
            histories.Add("07", "风机故障");
        }
    }
}
