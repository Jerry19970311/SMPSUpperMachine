using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Drawing;

namespace WindowsFormsApp1
{
    partial class Form1
    {
        private const string STATUS_BLOCK = "Status";
        private int modelCount;
        private Thread status;
        //键：模块号；值：对应的行号。
        private Dictionary<int, int> datas = new Dictionary<int, int>();
        private void statusInit()
        {
            blocks.Add(STATUS_BLOCK, true);
            particularSet.Add(Para.CID1_DC + Para.CID2_42);
            particularSet.Add(Para.CID1_AC + Para.CID2_ABC);
            particularSet.Add(Para.CID1_DC + Para.CID2_MODEL_ADD);
            particularSet.Add(Para.CID1_MODULE + Para.CID2_MODEL);
            dataGripViewInit();
            //status.Suspend();
        }
        private void dataGripViewInit()
        {
            //dataGridView1.Rows.Add(12);
            for (int i = 0; i < 12; i++)
            {
                int index = dataGridView1.Rows.Add(new string[] { "模块" + Convert.ToString(i + 1), "不在位", "0V", "0A", "正常", "正常", "正常" });
                datas.Add(i + 1, index);
            }
        }
        private void button121_Click(object sender, EventArgs e)
        {
            Console.WriteLine("线程测试");
            if ("开启跟踪".Equals(button121.Text))
            {
                blocks[STATUS_BLOCK] = false;
                Console.WriteLine("线程测试1");
                //status.Resume();
                status = new Thread(new ThreadStart(getStatus));
                status.Start();
                button121.Text = "关闭跟踪";
            }else
            {
                Console.WriteLine("线程测试2");
                button121.Text = "开启跟踪";
                //先敬酒
                blocks[STATUS_BLOCK] = true;
                Delay(100);
                //status.Suspend();
                //status.Abort();
                /*while (status.ThreadState != System.Threading.ThreadState.Stopped)
                {
                    status.Join();
                    Delay(50);
                }*/
                double test2 = new TimeSpan(DateTime.Now.Ticks).TotalMilliseconds;
                while (status.ThreadState.Equals(ThreadState.Running))
                {
                    double test3 = new TimeSpan(DateTime.Now.Ticks).TotalMilliseconds;
                    Console.WriteLine(test3 - test2);
                    Console.WriteLine(status.ThreadState.ToString());
                    //敬酒不吃吃罚酒
                    if (test3 - test2 > 200)
                    {
                        status.Abort();
                    }
                }
            }
        }
        private void getStatus()
        {
            try {
                while (false == blocks[STATUS_BLOCK])
                {
                    Send(Para.CID1_DC, Para.CID2_42, "");
                    if (blocks[STATUS_BLOCK])
                    {
                        return;
                    }
                    Delay(500);
                    Send(Para.CID1_AC, Para.CID2_ABC, "");
                    if (blocks[STATUS_BLOCK])
                    {
                        return;
                    }
                    Delay(500);
                    Send(Para.CID1_DC, Para.CID2_MODEL_ADD, "");
                    if (blocks[STATUS_BLOCK])
                    {
                        return;
                    }
                    Delay(500);
                    /*Send(Para.CID1_MODULE, Para.CID2_MODEL, "02");
                    Delay(100);*/
                }
            } catch (Exception e)
            {
                Console.WriteLine(e.Message);
                if (false==cid2Queue.Count.Equals(0))
                {
                    while ((Para.CID2_42.Equals(cid2Queue.Peek())) || (Para.CID2_ABC.Equals(cid2Queue.Peek())) || (Para.CID2_MODEL_ADD.Equals(cid2Queue.Peek())) || (Para.CID2_MODEL.Equals(cid2Queue.Peek())))
                    {
                        cid2Queue.Dequeue();
                    }
                }
            }
        }
        private void statusDeal(string cid1,string cid2,string data)
        {
            /*richTextBox1.AppendText(cid1 + "\n");
            richTextBox1.AppendText(cid2 + "\n");
            richTextBox1.AppendText(data + "\n");*/
            if (Para.CID1_DC.Equals(cid1) && Para.CID2_42.Equals(cid2))
            {
                double outV = ((double)Convert.ToInt32(data.Substring(0, 4), 16)/(double)100);
                //-48V输出电压（母排电压）
                textBox32.Text = Convert.ToString(outV);
                //电池电流
                textBox33.Text = Convert.ToString(quarterNumber(data.Substring(4, 4), 10));
                //-48V输出电流（负载电流）
                textBox34.Text = Convert.ToString(quarterNumber(data.Substring(8, 4), 10));
                //电池温度
                textBox35.Text = Convert.ToString(quarterNumber(data.Substring(12, 4), 10));
                //环境温度
                textBox37.Text = Convert.ToString(quarterNumber(data.Substring(16, 4), 10));
                //电源模块数量
                textBox30.Text = Convert.ToString(Convert.ToInt32(data.Substring(20, 2), 16));
                //环境湿度
                textBox38.Text = Convert.ToString(quarterNumber(data.Substring(22, 4), 10));
                //电池状态
                batStatus.Text = getBatStatus(Convert.ToInt32(data.Substring(32, 2), 16));

                //状态量1处理
                int status1 = Convert.ToInt32(data.Substring(26, 2), 16);
                //1路低压断开操作告警（负载下电告警）
                if (0 != (status1 & 1))
                {
                    pictureBox7.BackColor = Color.Red;
                }
                else
                {
                    pictureBox7.BackColor = Color.Lime;
                }
                //系统负载电流过高告警（负载过流告警）
                if (0 != (status1 & 2))
                {
                    pictureBox3.BackColor = Color.Red;
                }
                else
                {
                    pictureBox3.BackColor = Color.Lime;
                }
                //电池低压告警
                if(0 != (status1 & 4))
                {
                    pictureBox2.BackColor = Color.Red;
                }
                else
                {
                    pictureBox2.BackColor = Color.Lime;
                }
                //输出过压告警
                if (0 != (status1 & 16))
                {
                    pictureBox1.BackColor = Color.Red;
                }
                else
                {
                    pictureBox1.BackColor = Color.Lime;
                }
                //电池温度传感器故障告警
                /*if (0 != (status1 & 32))
                {
                    pictureBox1.BackColor = Color.Red;
                }
                else
                {
                    pictureBox1.BackColor = Color.Lime;
                }*/

                //状态量2处理
                int status2 = Convert.ToInt32(data.Substring(28, 2), 16);
                //电池电流过高告警（电池过流）
                if (0 != (status2 & 1))
                {
                    pictureBox4.BackColor = Color.Red;
                }
                else
                {
                    pictureBox4.BackColor = Color.Lime;
                }
                //电池熔丝断
                if (0 != (status2 & 2))
                {
                    pictureBox10.BackColor = Color.Red;
                }
                else
                {
                    pictureBox10.BackColor = Color.Lime;
                }
                //负载熔丝断
                if (0 != (status2 & 4))
                {
                    pictureBox17.BackColor = Color.Red;
                }
                else
                {
                    pictureBox17.BackColor = Color.Lime;
                }
                //环境温度过低告警
                if (0 != (status2 & 16))
                {
                    pictureBox18.BackColor = Color.Red;
                }
                else
                {
                    pictureBox18.BackColor = Color.Lime;
                }
                //环境温度过高告警
                if (0 != (status2 & 32))
                {
                    pictureBox9.BackColor = Color.Red;
                }
                else
                {
                    pictureBox9.BackColor = Color.Lime;
                }
                //2路低压断开操作告警（电池下电告警）
                if (0 != (status2 & 64))
                {
                    pictureBox8.BackColor = Color.Red;
                }
                else
                {
                    pictureBox8.BackColor = Color.Lime;
                }

                //状态量3处理
                int status3 = Convert.ToInt32(data.Substring(30, 2), 16);
                //电池温度过低告警
                if (0 != (status3 & 32))
                {
                    pictureBox6.BackColor = Color.Red;
                }
                else
                {
                    pictureBox6.BackColor = Color.Lime;
                }
                //电池温度过高告警
                if (0 != (status3 & 64))
                {
                    pictureBox5.BackColor = Color.Red;
                }
                else
                {
                    pictureBox5.BackColor = Color.Lime;
                }

                //状态量5处理
                int status5 = Convert.ToInt32(data.Substring(34, 2), 16);
                //烟雾告警
                if (0 != (status5 & 1))
                {
                    pictureBox16.BackColor = Color.Red;
                }
                else
                {
                    pictureBox16.BackColor = Color.Lime;
                }
                //水浸告警
                if (0 != (status5 & 2))
                {
                    pictureBox14.BackColor = Color.Red;
                }
                else
                {
                    pictureBox14.BackColor = Color.Lime;
                }
                //防雷告警
                if (0 != (status5 & 4))
                {
                    pictureBox12.BackColor = Color.Red;
                }
                else
                {
                    pictureBox12.BackColor = Color.Lime;
                }
                //门禁告警
                if (0 != (status5 & 8))
                {
                    pictureBox13.BackColor = Color.Red;
                }
                else
                {
                    pictureBox13.BackColor = Color.Lime;
                }
            }
            if (Para.CID1_AC.Equals(cid1) && Para.CID2_ABC.Equals(cid2))
            {
                double av = quarterNumber(data.Substring(0, 4), 10);
                double bv = quarterNumber(data.Substring(10, 4), 10);
                double cv = quarterNumber(data.Substring(20, 4), 10);
                double v = (av + bv + cv) / 3;
                textBox31.Text = v.ToString("0.00");
                int statusA = Convert.ToInt32(data.Substring(8, 2), 16);
                int statusB = Convert.ToInt32(data.Substring(18, 2), 16);
                int statusC = Convert.ToInt32(data.Substring(28, 2), 16);
                //柴油机状态，目前不需要
                if (0 != (statusA & 64))
                {
                    
                }
                //电池对称故障，目前不需要
                if (0 != (statusB & 64))
                {

                }
                int stat = statusA | statusB | statusC;
                if (0 == (stat & 63))
                {
                    label47.Text = "正常";
                    textBox36.BackColor = Color.Lime;
                    pictureBox15.BackColor = Color.Lime;
                }
                else
                {
                    textBox36.BackColor = Color.Red;
                    if (0 != (stat & 1))
                    {
                        label47.Text = "掉电";
                        //pictureBox15.BackColor = Color.Lime;
                    }
                    if (0 != (stat & 2))
                    {
                        label47.Text = "低压";
                        //pictureBox15.BackColor = Color.Lime;
                    }
                    if (0 != (stat & 4))
                    {
                        label47.Text = "过高";
                        //pictureBox15.BackColor = Color.Lime;
                    }
                    if (0 != (stat & 16))
                    {
                        label47.Text = "过流";
                        //pictureBox15.BackColor = Color.Lime;
                    }
                    if (0 != (stat & 32))
                    {
                        label47.Text = "空开";
                        pictureBox15.BackColor = Color.Red;
                    }
                    else
                    {
                        pictureBox15.BackColor = Color.Lime;
                    }
                }
            }
            //获取有效的模块地址（单模块测试通，多模块未测试）
            if (Para.CID1_DC.Equals(cid1) && Para.CID2_MODEL_ADD.Equals(cid2))
            {
                HashSet<int> on = new HashSet<int>();
                int length = data.Length / 2;
                Console.WriteLine("测试：data:" + data);
                for(int i = 0; i < length; i++)
                {
                    string ad = data.Substring(i * 2, 2);
                    Console.WriteLine("测试：ad:" + ad);
                    on.Add(Convert.ToInt32(ad, 16));
                    /*Send(Para.CID1_MODULE, Para.CID2_MODEL, ad);
                    Delay(500);*/
                    stadQueue.Enqueue(ad);
                }
                for(int i = 1; i <= 12; i++)
                {
                    if (false==on.Contains(i))
                    {
                        dataGridView1.Rows[datas[i]].Cells["onStatus"].Value = "不在位";
                    }
                }
                if (0 != stadQueue.Count)
                {
                    Send(Para.CID1_MODULE, Para.CID2_MODEL, stadQueue.Dequeue());
                }
            }
            //单个模块状态（尚未测试，电压和电流数值进位情况尚未确定）
            if (Para.CID1_MODULE.Equals(cid1) && Para.CID2_MODEL.Equals(cid2))
            {
                int ad = Convert.ToInt32(data.Substring(0, 2), 16);
                double v = (double)Convert.ToInt32(data.Substring(2, 4), 16)/100d;
                double i = (double)Convert.ToInt32(data.Substring(6, 4), 16)/100d;
                int st = Convert.ToInt32(data.Substring(18, 2), 16);
                dataGridView1.Rows[datas[ad]].Cells[2].Value = ((v == 0) ? "0" : v.ToString("0.00")) + "V";
                dataGridView1.Rows[datas[ad]].Cells[3].Value = ((i == 0) ? "0" : i.ToString("0.00")) + "A";
                //限流状态
                if (0 != (st & 1))
                {
                    dataGridView1.Rows[datas[ad]].Cells[6].Value = "告警";
                }
                else
                {
                    dataGridView1.Rows[datas[ad]].Cells[6].Value = "正常";
                }
                //开关机状态
                if (0 != (st & 4))
                {
                    dataGridView1.Rows[datas[ad]].Cells[1].Value = "关机";
                }
                else
                {
                    dataGridView1.Rows[datas[ad]].Cells[1].Value = "开机";
                }
                //风扇故障
                if (0 != (st & 16))
                {
                    dataGridView1.Rows[datas[ad]].Cells[5].Value = "告警";
                }
                else
                {
                    dataGridView1.Rows[datas[ad]].Cells[5].Value = "正常";
                }
                //模块保护
                if (0 != (st & 64))
                {
                    dataGridView1.Rows[datas[ad]].Cells[4].Value = "告警";
                }
                else
                {
                    dataGridView1.Rows[datas[ad]].Cells[5].Value = "正常";
                }
                //继续取地址
                if (0 != stadQueue.Count)
                {
                    Send(Para.CID1_MODULE, Para.CID2_MODEL, stadQueue.Dequeue());
                }
            }
        }
        private Queue<string> stadQueue = new Queue<string>();
        private bool isMinus(String hex)
        {
            return ((Convert.ToInt32(hex.Substring(0, 1)) & ((int)8)) == 0) ? false : true;
        }
        private double doubleNumber(string s)
        {
            return 0;
        }
        private double quarterNumber(string s,double bas)
        {
            int origin = Convert.ToInt32(s, 16);
            int unsign = origin & 32767;
            double result;
            if ((origin & 32768) == 0)
            {
                result = unsign;
            }
            else
            {
                result = 0 - unsign;
            }
            return result / bas;
        }
        private string getBatStatus(int hex)
        {
            if (0 != (hex & 1))
            {
                return "电池测试";
            }
            else if(0!=(hex&2))
            {
                return "快充";
            }else if (0 != (hex & 4))
            {
                return "浮充";
            }else if (0 != (hex & 16))
            {
                return "均充";
            }
            else
            {
                return "无状态";
            }
        }
    }
}
