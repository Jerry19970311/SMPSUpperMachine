using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Threading;
using System.Net.Sockets;
using System.Timers;

namespace WindowsFormsApp1
{
    //private Send send=null;
    public partial class Form1 : Form
    {
        private Socket Socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
        //装填消息用的队列
        //由于串口通信，接收到的信息很可能不是一次性收到，
        //故需要准备队列将每次收到的信息进行收集，等到读到结束位(OD)时才可进行输出。
        private Queue<string> queue = new Queue<string>();
        //时间标记，每次在开启接口或者接收事件触发时都会更新。
        private double receivedTime = new TimeSpan(DateTime.Now.Ticks).TotalMilliseconds;
        //存储CID2码的队列
        //由于串口通信，获取信息的响应指令不带CID2码，计算机无法通过响应指令得知数据对应的位置
        //故需要将发送指令的CID2码存下，
        //同时为了防止操作员操作过快，造成响应混淆，故需要准备队列来保证顺序。
        //（注：只有产生响应信息的指令需要用到此队列）
        private Queue<string> cid2Queue = new Queue<string>();
        //cid1和cid2与对应文本框的映射
        private Dictionary<string, Control> forCid = new Dictionary<string, Control>();
        private HashSet<string> boolSet = new HashSet<string>();
        private HashSet<string> string10Set = new HashSet<string>();
        private HashSet<string> string100Set = new HashSet<string>();
        private HashSet<string> particularSet = new HashSet<string>();
        private HashSet<string> stringSet = new HashSet<string>();
        /*private Mutex mutex;
        private string mutexName = "mutex";*/
        private static bool mutex = true;
        private static Object ob = new Object();
        private static Object ob2 = new Object();
        private double test1;
        private double test2;
        //键：cid值；值：指令数据长度（字节）。
        private Dictionary<string, int> forLength = new Dictionary<string, int>();
        private Dictionary<string, string> histories = new Dictionary<string, string>();
        //键：类型信号；值：类型。
        private Dictionary<string, string> reverseRelayType = new Dictionary<string, string>();
        private Dictionary<string, bool> blocks = new Dictionary<string, bool>();
        private System.Threading.Timer timer;

        public Form1()
        {
            InitializeComponent();
            init();
            //statusInit();
        }
        public void LoadClose(object sender, FormClosingEventArgs e)
        {
        }
        //补充一些UI控件的设定（Designer文件代码为生成器生成，故不能直接在那里写）
        private void init()
        {
            comm.Items.AddRange(System.IO.Ports.SerialPort.GetPortNames());
            Form1.CheckForIllegalCrossThreadCalls = false;
            //禁止列排序
            for (int i = 0; i < this.dataGridView1.Columns.Count; i++)
            {
                this.dataGridView1.Columns[i].SortMode = DataGridViewColumnSortMode.NotSortable;
            }
            comboBox2.Text = "自定义";
            comboBox3.Text = "自定义";
            comboBox4.Text = "自定义";
            comboBox5.Text = "自定义";
            comboBox6.Text = "开";
            comboBox7.Text = "开";
            comboBox8.Text = "开";
            comboBox9.Text = "开";
            serialPort1.ErrorReceived += serialError;
            forCidInit();
            boolSetInit();
            stringSetInit();
            string10SetInit();
            string100SetInit();
            statusInit();
            writeInit();
            historyInit();
            reverseRelayTypeInit();
            TimerInit();
        }
        //将参数1面板里所有的【读】按钮事件，加至【一键刷新】
        private void addAllRead1()
        {
            //button69.Click += ;
        }
        //初始化forCid字典（更新到参数2下方）
        private void forCidInit()
        {
            forCid.Add(Para.CID1_AC + Para.CID2_ACV_HIGH_ALARM_GET, textBox1);
            forCid.Add(Para.CID1_AC + Para.CID2_ACV_LOW_ALARM_GET, textBox2);
            forCid.Add(Para.CID1_AC + Para.CID2_AC_DOWN_ALARM_GET, textBox3);
            forCid.Add(Para.CID1_AC + Para.CID2_ACI_HIGH_ALARM_GET, textBox4);
            forCid.Add(Para.CID1_MODULE + Para.CID2_LIMIT_I_GET, textBox5);
            forCid.Add(Para.CID1_DC + Para.CID2_RI_HIGH_ALARM_GET, textBox6);
            forCid.Add(Para.CID1_DC + Para.CID2_BATI_HIGH_ALARM_GET, textBox7);
            forCid.Add(Para.CID1_DC + Para.CID2_V_HIGH_GET, textBox8);
            forCid.Add(Para.CID1_DC + Para.CID2_BATV_LOW_ALARM_GET, textBox9);
            forCid.Add(Para.CID1_DC + Para.CID2_FREEV_OFF_1_GET, textBox10);
            forCid.Add(Para.CID1_DC + Para.CID2_FREEV_RESTART_1_GET, textBox11);
            forCid.Add(Para.CID1_DC + Para.CID2_FREEV_OFF_2_GET, textBox12);
            forCid.Add(Para.CID1_DC + Para.CID2_FREEV_RESTART_2_GET, textBox13);
            forCid.Add(Para.CID1_DC + Para.CID2_FLOATING_V_GET, textBox14);
            forCid.Add(Para.CID1_DC + Para.CID2_REGULAR_AVG_V_GET, textBox15);
            forCid.Add(Para.CID1_DC + Para.CID2_AVG_TO_FLOAT_GET, textBox16);
            forCid.Add(Para.CID1_DC + Para.CID2_FLOAT_TO_AVG_GET, textBox17);
            forCid.Add(Para.CID1_DC + Para.CID2_CHARGING_LIMITED_PERCENT_GET, textBox18);
            forCid.Add(Para.CID1_DC + Para.CID2_TEMP_COMPENSATION_GET, checkBox1);
            forCid.Add(Para.CID1_DC + "3F", textBox19);
            forCid.Add(Para.CID1_DC + Para.CID2_REGULAR_AVG_ENERGY_GET, checkBox3);
            forCid.Add(Para.CID1_DC + Para.CID2_REGULAR_AVG_TIME_GAP_GET, textBox20);
            forCid.Add(Para.CID1_DC + Para.CID2_REGULAR_AVG_TIME_GET, textBox21);
            forCid.Add(Para.CID1_DC + Para.CID2_MONITOR_VOICE_GET, comboBox1);
            forCid.Add(Para.CID1_DC + Para.CID2_CLOCK_GET, textBox23);
            forCid.Add(Para.CID1_DC + Para.CID2_BAT_VOL1_GET, textBox22);
            forCid.Add(Para.CID1_DC + Para.CID2_BAT_VOL_PERCENT_GET, textBox24);
            forCid.Add(Para.CID1_DC + Para.CID2_SLEEP_ENERGY_GET, checkBox4);
            forCid.Add(Para.CID1_DC + Para.CID2_SLEEP_TIME_GAP_GET, textBox25);
            forCid.Add(Para.CID1_DC + Para.CID2_LIMITED_MIN_V_GET, textBox26);
            forCid.Add(Para.CID1_DC + Para.CID2_CURRENT_SLOPE_GET, textBox27);
            forCid.Add(Para.CID1_DC + Para.CID2_CURRENT_ZERO_GET, textBox28);
            forCid.Add(Para.CID1_DC + Para.CID2_BUS_OUTPUT_V_SLOPE_GET, textBox29);
            forCid.Add(Para.CID1_DC + Para.CID2_BAT_TEST_ENERGY_GET, checkBox5);
            forCid.Add(Para.CID1_DC + Para.CID2_BAT_TEST_START_V_GET,textBox39);
            forCid.Add(Para.CID1_DC + Para.CID2_BAT_TEST_END_V_GET, textBox40);
            forCid.Add(Para.CID1_DC + Para.CID2_FAST_CHARGE_GET, checkBox6);
            forCid.Add(Para.CID1_DC + Para.CID2_FAST_V_GET, textBox41);
            forCid.Add(Para.CID1_DC + Para.CID2_FAST_TIME_GET, textBox42);
            //（电池低温告警点目前尚未解决）
            forCid.Add(Para.CID1_DC + Para.CID2_BAT_TEMP_LOW_ALARM_GET, textBox43);
            forCid.Add(Para.CID1_DC + Para.CID2_BAT_TEMP_HIGH_ALARM_GET, textBox44);
            forCid.Add(Para.CID1_DC + Para.CID2_LOAD_CURRENT_SLOPE_GET, textBox46);
            forCid.Add(Para.CID1_DC + Para.CID2_BATTERY_ADD_GET, textBox50);
            forCid.Add(Para.CID1_DC + Para.CID2_BATTERY_OFFSET_GET, textBox45);
            forCid.Add(Para.CID1_DC + Para.CID2_ENV_ADD_GET, textBox49);
            forCid.Add(Para.CID1_DC + Para.CID2_ENV_OFFSET_GET, textBox47);
            forCid.Add(Para.CID1_DC + Para.CID2_A_AC_V_ADD_GET, textBox51);
            forCid.Add(Para.CID1_DC + Para.CID2_A_AC_V_ZERO_GET, textBox48);
            forCid.Add(Para.CID1_DC + Para.CID2_LOAD_CURRENT_ZERO_GET, textBox52);
            forCid.Add(Para.CID1_DC + Para.CID2_PANEL_LANGUAGE_GET, comboBox10);
            //干结点类型，必须特殊处理
            forCid.Add(Para.CID1_DC + Para.CID2_RELAY_TYPE_GET + "01", comboBox2);
            forCid.Add(Para.CID1_DC + Para.CID2_RELAY_TYPE_GET + "02", comboBox3);
            forCid.Add(Para.CID1_DC + Para.CID2_RELAY_TYPE_GET + "03", comboBox4);
            forCid.Add(Para.CID1_DC + Para.CID2_RELAY_TYPE_GET + "04", comboBox5);
            //干结点状态，必须特殊处理
            forCid.Add(Para.CID1_DC + Para.CID2_RELAY_STATUS_GET + "01", comboBox6);
            forCid.Add(Para.CID1_DC + Para.CID2_RELAY_STATUS_GET + "02", comboBox7);
            forCid.Add(Para.CID1_DC + Para.CID2_RELAY_STATUS_GET + "03", comboBox8);
            forCid.Add(Para.CID1_DC + Para.CID2_RELAY_STATUS_GET + "04", comboBox9);

            forCid.Add(Para.CID1_DC + Para.CID2_HISTORY_FIRST_ADR_GET, textBox53);
            forCid.Add(Para.CID1_DC + Para.CID2_HISTORY_GET, dataGridView2);
        }
        //初始化boolSet（更新到参数1）
        private void boolSetInit() {
            boolSet.Add(Para.CID1_DC + Para.CID2_TEMP_COMPENSATION_GET);
            boolSet.Add(Para.CID1_DC + Para.CID2_REGULAR_AVG_ENERGY_GET);
            boolSet.Add(Para.CID1_DC + Para.CID2_SLEEP_ENERGY_GET);
            boolSet.Add(Para.CID1_DC + Para.CID2_BAT_TEST_ENERGY_GET);
            boolSet.Add(Para.CID1_DC + Para.CID2_FAST_CHARGE_GET);
        }
        //初始化string10Set（更新到参数1）
        private void string10SetInit()
        {
            string10Set.Add(Para.CID1_AC + Para.CID2_ACV_HIGH_ALARM_GET);
            string10Set.Add(Para.CID1_AC + Para.CID2_ACV_LOW_ALARM_GET);
            string10Set.Add(Para.CID1_AC + Para.CID2_AC_DOWN_ALARM_GET);
            string10Set.Add(Para.CID1_AC + Para.CID2_ACI_HIGH_ALARM_GET);
            string10Set.Add(Para.CID1_MODULE + Para.CID2_LIMIT_I_GET);
            string10Set.Add(Para.CID1_DC + Para.CID2_RI_HIGH_ALARM_GET);
            string10Set.Add(Para.CID1_DC + Para.CID2_BATI_HIGH_ALARM_GET);
            string10Set.Add(Para.CID1_DC + Para.CID2_AVG_TO_FLOAT_GET);
            string10Set.Add(Para.CID1_DC + Para.CID2_FLOAT_TO_AVG_GET);
            string10Set.Add(Para.CID1_DC + Para.CID2_CHARGING_LIMITED_PERCENT_GET);
            string10Set.Add(Para.CID1_DC + Para.CID2_BAT_VOL_PERCENT_GET);
            string10Set.Add(Para.CID1_DC + Para.CID2_BAT_TEMP_HIGH_ALARM_GET);

            string10Set.Add(Para.CID1_AC + Para.CID2_ACV_HIGH_ALARM_SET);
            string10Set.Add(Para.CID1_AC + Para.CID2_ACV_LOW_ALARM_SET);
            string10Set.Add(Para.CID1_AC + Para.CID2_AC_DOWN_ALARM_SET);
            string10Set.Add(Para.CID1_AC + Para.CID2_ACI_HIGH_ALARM_SET);
            string10Set.Add(Para.CID1_MODULE + Para.CID2_LIMIT_I_SET);
            string10Set.Add(Para.CID1_DC + Para.CID2_RI_HIGH_ALARM_SET);
            string10Set.Add(Para.CID1_DC + Para.CID2_BATI_HIGH_ALARM_SET);
            string10Set.Add(Para.CID1_DC + Para.CID2_AVG_TO_FLOAT_SET);
            string10Set.Add(Para.CID1_DC + Para.CID2_FLOAT_TO_AVG_SET);
            string10Set.Add(Para.CID1_DC + Para.CID2_CHARGING_LIMITED_PERCENT_SET);
            string10Set.Add(Para.CID1_DC + Para.CID2_BAT_VOL_PERCENT_SET);
            string10Set.Add(Para.CID1_DC + Para.CID2_BAT_TEMP_HIGH_ALARM_SET);
        }
        //初始化string100Set（更新到参数1）
        private void string100SetInit()
        {
            string100Set.Add(Para.CID1_DC + Para.CID2_V_HIGH_GET);
            string100Set.Add(Para.CID1_DC + Para.CID2_BATV_LOW_ALARM_GET);
            string100Set.Add(Para.CID1_DC + Para.CID2_FREEV_OFF_1_GET);
            string100Set.Add(Para.CID1_DC + Para.CID2_FREEV_RESTART_1_GET);
            string100Set.Add(Para.CID1_DC + Para.CID2_FREEV_OFF_2_GET);
            string100Set.Add(Para.CID1_DC + Para.CID2_FREEV_RESTART_2_GET);
            string100Set.Add(Para.CID1_DC + Para.CID2_FLOATING_V_GET);
            string100Set.Add(Para.CID1_DC + Para.CID2_REGULAR_AVG_V_GET);
            string100Set.Add(Para.CID1_DC + Para.CID2_LIMITED_MIN_V_GET);
            string100Set.Add(Para.CID1_DC + Para.CID2_BAT_TEST_START_V_GET);
            string100Set.Add(Para.CID1_DC + Para.CID2_BAT_TEST_END_V_GET);
            string100Set.Add(Para.CID1_DC + Para.CID2_FAST_V_GET);

            string100Set.Add(Para.CID1_DC + Para.CID2_V_HIGH_SET);
            string100Set.Add(Para.CID1_DC + Para.CID2_BATV_LOW_ALARM_SET);
            string100Set.Add(Para.CID1_DC + Para.CID2_FREEV_OFF_1_SET);
            string100Set.Add(Para.CID1_DC + Para.CID2_FREEV_RESTART_1_SET);
            string100Set.Add(Para.CID1_DC + Para.CID2_FREEV_OFF_2_SET);
            string100Set.Add(Para.CID1_DC + Para.CID2_FREEV_RESTART_2_SET);
            string100Set.Add(Para.CID1_DC + Para.CID2_FLOATING_V_SET);
            string100Set.Add(Para.CID1_DC + Para.CID2_REGULAR_AVG_V_SET);
            string100Set.Add(Para.CID1_DC + Para.CID2_LIMITED_MIN_V_SET);
            //string100Set.Add(Para.CID1_DC + Para.CID2_CURRENT_SLOPE_SET);
            //string100Set.Add(Para.CID1_DC + Para.CID2_CURRENT_ZERO_SET);
            //string100Set.Add(Para.CID1_DC + Para.CID2_BUS_OUTPUT_V_SLOPE_SET);
            string100Set.Add(Para.CID1_DC + Para.CID2_BAT_TEST_START_V_SET);
            string100Set.Add(Para.CID1_DC + Para.CID2_BAT_TEST_END_V_SET);
            string100Set.Add(Para.CID1_DC + Para.CID2_FAST_V_SET);
        }
        //初始化stringSet（更新到参数1）
        private void stringSetInit()
        {
            stringSet.Add(Para.CID1_DC + "3F");
            stringSet.Add(Para.CID1_DC + Para.CID2_REGULAR_AVG_TIME_GAP_GET);
            stringSet.Add(Para.CID1_DC + Para.CID2_REGULAR_AVG_TIME_GET);
            stringSet.Add(Para.CID1_DC + Para.CID2_BAT_VOL1_GET);
            stringSet.Add(Para.CID1_DC + Para.CID2_SLEEP_TIME_GAP_GET);
            stringSet.Add(Para.CID1_DC + Para.CID2_FAST_TIME_GET);
            stringSet.Add(Para.CID1_DC + Para.CID2_LOAD_CURRENT_SLOPE_GET);
            stringSet.Add(Para.CID1_DC + Para.CID2_BATTERY_ADD_GET);
            stringSet.Add(Para.CID1_DC + Para.CID2_BATTERY_OFFSET_GET);
            stringSet.Add(Para.CID1_DC + Para.CID2_ENV_ADD_GET);
            stringSet.Add(Para.CID1_DC + Para.CID2_ENV_OFFSET_GET);
            stringSet.Add(Para.CID1_DC + Para.CID2_A_AC_V_ADD_GET);
            stringSet.Add(Para.CID1_DC + Para.CID2_A_AC_V_ZERO_GET);
            stringSet.Add(Para.CID1_DC + Para.CID2_LOAD_CURRENT_ZERO_GET);
            stringSet.Add(Para.CID1_DC + Para.CID2_CURRENT_SLOPE_GET);
            stringSet.Add(Para.CID1_DC + Para.CID2_CURRENT_ZERO_GET);
            stringSet.Add(Para.CID1_DC + Para.CID2_BUS_OUTPUT_V_SLOPE_GET);
            stringSet.Add(Para.CID1_DC + Para.CID2_HISTORY_FIRST_ADR_GET);
        }
        private void reverseRelayTypeInit()
        {
            reverseRelayType.Add("00", "自定义");
            reverseRelayType.Add("01", "交流停电");
            reverseRelayType.Add("02", "交流过欠压");
            reverseRelayType.Add("03", "直流过欠压");
            reverseRelayType.Add("04", "电源模块");
            reverseRelayType.Add("05", "LVD");
            reverseRelayType.Add("06", "一次下电");
            reverseRelayType.Add("07", "负载熔丝");
            reverseRelayType.Add("08", "电池熔丝");
            reverseRelayType.Add("09", "环境温度告警");
            reverseRelayType.Add("0A", "门禁");
            reverseRelayType.Add("0B", "烟禁");
            reverseRelayType.Add("0C", "水浸");
            reverseRelayType.Add("0D", "防雷/系统故障");
        }
        private void TimerInit()
        {
            timer = new System.Threading.Timer(getTimer, "Processing timer event",0,500);
        }
        private void getTimer(object o)
        {
            toolStripStatusLabel1.Text = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
        }
        private void Clear()
        {
            cid2Queue.Clear();
            mutex = true;
        }

        //考虑返回情况的发送指令：
        private void Send(string cid1, string cid2, string data)
        {
            Console.WriteLine("待发送:" + cid1 + "\t\t" + cid2 + "\t\t" + data);
            //如果队列非空，则检查上一次发送距离现在的时间差，
            //如果超过400毫秒，说明存在没有被下位机接收到的指令，
            //那么就需要清空队列，防止接收的时候出错。
            if (0 != cid2Queue.Count)
            {
                double time = new TimeSpan(DateTime.Now.Ticks).TotalMilliseconds;
                if (time - receivedTime > 500)
                {
                    Console.WriteLine("--------------------------------------------------------------------疑似有废指令" + "\n");
                    Console.WriteLine("废指令时间：" + time);
                    Console.WriteLine("间隔时间为：" + (time - receivedTime));
                    Clear();
                }
            }
            /*if ("CLOSE".Equals(button42.Text))
            {
                Mutex.OpenExisting(mutexName).WaitOne();
            }*/
            test1 = new TimeSpan(DateTime.Now.Ticks).TotalMilliseconds;
            //Console.WriteLine(cid1 + cid2 + "发送时间:" + test1);
            SendWithoutRespond(cid1, cid2, data);
        }
        //发送指令（不考虑返回）：
        //①生成指令
        //②将指令打在协议信息栏内
        //③将指令转化为字节格式，传送至串口
        private void SendWithoutRespond(string cid1, string cid2, string data)
        {
            string command = generate_Command(cid1, cid2, data);
            //richTextBox1.Text = richTextBox1.Text + command + "\n";
            richTextBox1.AppendText(command + "\n");
            byte[] bytes = StringToHex(command);
            int l = bytes.Length;
            //只有在确认连接打开的时候，才能进行指令发送，否则会产生一系列错误。
            if ("CLOSE".Equals(button42.Text))
            {
                while (true)
                {
                    //Console.WriteLine("轮询cid" + cid1 + cid2 + "\n");
                    lock (ob2)
                    {
                        //Console.WriteLine("进锁cid:" + cid1 + cid2 + "\t\t" + "mutex:" + mutex + "\n");
                        if (mutex)
                        {
                            mutex = false;
                            break;
                        }
                    }
                    Delay(10);
                }
                if (radioButton1.Checked)
                {
                    cid2Queue.Enqueue(cid2);
                    Console.WriteLine(cid1 + cid2 + "已发送");
                    serialPort1.Write(bytes, 0, l);
                }
                //（LAN口部分的发送，目前尚未实现）
                if (radioButton2.Checked)
                {
                    try
                    {
                        Socket.Send(bytes);
                        Console.WriteLine(cid1 + cid2 + "已发送");
                        string m = Receive(10000);
                        Console.WriteLine(cid1 + cid2 + "已接收");
                        Console.WriteLine(m);
                        if (m.Length.Equals(0))
                        {
                            Console.WriteLine("空字符串");
                            mutex = true;
                            return;
                        }
                        putData(m, cid2);
                    }
                    catch (SocketException ex)
                    {
                        Console.WriteLine(ex.Message.ToString());
                        if ("远程主机强迫关闭了一个现有的连接。".Equals(ex.Message.ToString())){
                            openConnect();
                        }
                    }
                }
            }
        }

        //指令生成
        //注:使用此方法时，data无空格且长度为偶数，若原数据长度为奇数，注意在开头补一个0
        private string generate_Command(String cid1,String cid2,String data)
        {
            //界面相应
            cid1TextBox.Text = cid1;
            cid2TextBox.Text = cid2;
            dataTextBox.Text = data;
            //生成指令
            StringBuilder stringBuilder = new StringBuilder();
            int chknum = 0;
            //开始符不用参与检验码计算，直接加入StringBuilder
            stringBuilder.Append(Para.SOI);
            stringBuilder.Append(" ");
            //取地址
            string ad = adrTextBox.Text;
            if (1 == ad.Length)
            {
                ad = "0" + ad;
            }
            Queue<string> queue = deal(ad, cid1, cid2, data);
            //处理队列数据：①加入StringBuilder；②计算检验码。
            while (0 != queue.Count())
            {
                string temp = queue.Dequeue();
                stringBuilder.Append(temp);
                stringBuilder.Append(" ");
                chknum += Convert.ToInt32(temp, 16);
            }
            //将检验码转化为字符串，取最后两位，拆分，装入stringBuilder
            string chk = Convert.ToString(chknum, 16);
            int chkIndex = chk.Length - 2;
            chk = chk.Substring(chkIndex, 2).ToUpper();
            string[] chks = split(chk);
            stringBuilder.Append(chks[0]);
            stringBuilder.Append(" ");
            stringBuilder.Append(chks[1]);
            stringBuilder.Append(" ");
            //最后将结束符装入StringBuilder
            stringBuilder.Append(Para.EOI);
            return stringBuilder.ToString();
        }

        //将除了SOI、CHKNUM、EOI的符号进行处理，装填入队列
        private Queue<string> deal(String ad, String cid1, String cid2, String data)
        {
            Queue<string> queue = new Queue<string>();
            addQueue(split(ad), queue);
            addQueue(split(cid1), queue);
            addQueue(split(cid2), queue);
            int length = data.Length / 2;
            String len = Convert.ToString(length, 16);
            //（如果还有时间，这里可以加一个限制length大小在FF以内的功能）
            if (0 != len.Length % 2)
            {
                len = "0" + len;
            }
            addQueue(split(len), queue);
            //数据长度不为0的时候，才能把数据拆分入队
            if (0 != length)
            {
                //若长度为奇数，补0
                if (0 != data.Length % 2)
                {
                    data = "0" + data;
                }
                addQueue(splitData(data), queue);
            }
            return queue;
        }
        
        //将字符串数组装填入队
        private void addQueue(string[] s,Queue<string> q)
        {
            for(int i = 0; i < s.Length; i++)
            {
                q.Enqueue(s[i]);
            }
        }


        //将除了SOI和EOI的指令拆分为符合通行协议的指令
        //输入：单个字节的16进制字符串
        //输出：长度为2的字符串
        private string[] split(string single)
        {
            string[] result = new string[2];
            result[0] = "3" + single.Substring(0, 1);
            result[1] = "3" + single.Substring(1, 1);
            return result;
        }
        private string[] splitData(string single)
        {
            int length = single.Length;
            string[] result = new string[length];
            for(int i = 0; i < length; i++)
            {
                result[i] = "3" + single.Substring(i, 1);
            }
            return result;
        }

        //接收串口消息：有响应信息时，触发此方法。
        //①将信息转化为字符串
        //②存入队列，等到读取到EOI时，进行输出
        private void read(object sender,EventArgs e)
        {
            Console.WriteLine("有返回:");
            System.IO.Ports.SerialPort sp = (System.IO.Ports.SerialPort)sender;
            //（解析方法讲解，详见SerialPortExample）
            byte[] bytes = new byte[100];
            int length = sp.Read(bytes, 0, 100);
            String ss = null;
            for (int i = 0; i < length; i++)
            {
                byte temp = bytes[i];
                ss = Convert.ToString(temp, 16).ToUpper();
                if (ss.Length.Equals(1))
                {
                    ss = "0" + ss;
                }
                //Console.WriteLine("ss1:" + ss);
                Console.Write(ss + " ");
                queue.Enqueue(ss);
            }
            Console.WriteLine();
            if ("0D".Equals(ss))
            {
                StringBuilder stringBuilder = new StringBuilder();
                while (0 != queue.Count())
                {
                    stringBuilder.Append(queue.Dequeue());
                    stringBuilder.Append(" ");
                }
                string result = stringBuilder.ToString();
                //richTextBox1.Text += result + "\n";
                Console.Write("接收:");
                Console.WriteLine(result);
                //Mutex.OpenExisting(mutexName).ReleaseMutex();
                string cid2 = cid2Queue.Dequeue();                
                putData(result,cid2);
            }
            receivedTime = new TimeSpan(DateTime.Now.Ticks).TotalMilliseconds;
            Console.WriteLine("有接收，当前时间为:" + receivedTime);
            Console.WriteLine("收发时间" + (receivedTime - test1));
        }
        //解析从串口收到的指令，将其中的数据填到对应的文本框中。
        private void putData(string receive,string cid2)
        {
            richTextBox1.AppendText(receive + "\n");
            richTextBox1.AppendText("测试：cid2=" + cid2 + "\n");
            mutex = true;
            string[] s = receive.Split(new string[] { " " }, StringSplitOptions.RemoveEmptyEntries);
            string cid1 = s[3].Substring(1, 1) + s[4].Substring(1, 1);
            string rtn = s[5].Substring(1, 1) + s[6].Substring(1, 1);
            string len = s[7].Substring(1, 1) + s[8].Substring(1, 1);
            int length = Convert.ToInt32(len, 16);
            length *= 2;
            //将数据分情况进行解析
            string cid12 = cid1 + cid2;
            Console.WriteLine("测试：cid12=" + cid12);
            //如果是RTN类信息，则没有回填的意义，另作处理
            if (false == "00".Equals(rtn))
            {
                Console.WriteLine("RTN测试：有错误码" + rtn + "，停止处理");
                return;
            }
            //状态回填（状态信号没有加入forCid，故需提前处理，否则会被当作纯响应信息。）
            if (particularSet.Contains(cid12))
            {
                Console.WriteLine("测试:" + cid12 + "被作为状态量处理");
                StringBuilder dataBuilder = new StringBuilder();
                for (int i = 0; i < length; i++)
                {
                    dataBuilder.Append(s[9 + i].Substring(1, 1));
                }
                statusDeal(cid1, cid2, dataBuilder.ToString());
                return;
            }
            //干结点的类型和状态，需要做预处理
            if ((Para.CID1_DC + Para.CID2_RELAY_TYPE_GET).Equals(cid12))
            {
                cid12 += s[9].Substring(1, 1) + s[10].Substring(1, 1);
            }
            if ((Para.CID1_DC + Para.CID2_RELAY_STATUS_GET).Equals(cid12))
            {
                cid12 += s[9].Substring(1, 1) + s[10].Substring(1, 1);
            }
            //纯响应信息，不需要做回填
            if (false == forCid.ContainsKey(cid12))
            {
                putRtn(cid1, cid2, rtn);
                return;
            }
            //数据回填（TextBox、ComboBox部分，以及系统总体状态获取部分已经实现）
            Control control = forCid[cid12];
            if (string10Set.Contains(cid12))
            {
                Console.WriteLine("进入10倍处理");
                StringBuilder dataBuilder = new StringBuilder();
                for (int i = 0; i < length; i++)
                {
                    dataBuilder.Append(s[9 + i].Substring(1, 1));
                    Console.WriteLine(dataBuilder.ToString());
                }
                string dataString = dataBuilder.ToString();
                double data = ((double)Convert.ToInt32(dataString, 16))/((double)10);
                ((TextBox)control).Text = Convert.ToString(data);
            }
            if (string100Set.Contains(cid12))
            {
                StringBuilder dataBuilder = new StringBuilder();
                for (int i = 0; i < length; i++)
                {
                    dataBuilder.Append(s[9 + i].Substring(1, 1));
                }
                string dataString = dataBuilder.ToString();
                double data = ((double)Convert.ToInt32(dataString, 16)) / ((double)100);
                ((TextBox)control).Text = Convert.ToString(data);
            }
            if (stringSet.Contains(cid12))
            {
                StringBuilder dataBuilder = new StringBuilder();
                for (int i = 0; i < length; i++)
                {
                    dataBuilder.Append(s[9 + i].Substring(1, 1));
                }
                string dataString = dataBuilder.ToString();
                double data = (double)Convert.ToInt32(dataString, 16);
                ((TextBox)control).Text = Convert.ToString(data);
            }
            if (boolSet.Contains(cid12))
            {
                if ("30".Equals(s[10]))
                {
                    ((CheckBox)control).Checked = false;
                }
                if ("31".Equals(s[10]))
                {
                    ((CheckBox)control).Checked = true;
                }
            }
            //其它情况需要特殊处理
            //蜂鸣告警
            if ((Para.CID1_DC + Para.CID2_MONITOR_VOICE_GET).Equals(cid12))
            {
                if ("30".Equals(s[10]))
                {
                    ((ComboBox)control).Text = "关闭";
                }
                if ("31".Equals(s[10]))
                {
                    ((ComboBox)control).Text = "开启";
                }
                if ("32".Equals(s[10]))
                {
                    ((ComboBox)control).Text = "本次关";
                }
            }
            //时间
            if ((Para.CID1_DC + Para.CID2_CLOCK_GET).Equals(cid12))
            {
                int second = Convert.ToInt32(s[9].Substring(1, 1) + s[10].Substring(1, 1),16);
                int d = Convert.ToInt32(s[11].Substring(1, 1) + s[12].Substring(1, 1),16);
                int hour = Convert.ToInt32(s[13].Substring(1, 1) + s[14].Substring(1, 1),16);
                int day = Convert.ToInt32(s[15].Substring(1, 1) + s[16].Substring(1, 1),16);
                int month = Convert.ToInt32(s[19].Substring(1, 1) + s[20].Substring(1, 1),16);
                int year = Convert.ToInt32(s[21].Substring(1, 1) + s[22].Substring(1, 1),16);
                //Console.WriteLine("时间测试：");
                //Console.WriteLine("秒：" + String.Format("{0:D2}",second) + "\t分：" + String.Format("{0:D2}",d) + "\t时：" + String.Format("{0:D2}", hour) + "\t日：" + String.Format("{0:D2}", day) + "\t月：" + String.Format("{0:D2}", month) + "\t年：" + String.Format("{0:D2}", year));
                string time = "20" + String.Format("{0:D2}", year) + "-" + String.Format("{0:D2}", month) + "-" + String.Format("{0:D2}", day) + " " + String.Format("{0:D2}", hour) + ":" + String.Format("{0:D2}", d) + ":" + String.Format("{0:D2}", second);
                //Console.WriteLine(time);
                textBox23.Text = time;
            }
            //干结点类型
            if (cid12.StartsWith(Para.CID1_DC + Para.CID2_RELAY_TYPE_GET))
            {
                Console.WriteLine("干结点类型测试代码块，当前为" + cid12);
                Console.WriteLine("数据为" + s[11].Substring(1, 1) + s[12].Substring(1, 1));
                forCid[cid12].Text = reverseRelayType[s[11].Substring(1, 1) + s[12].Substring(1, 1)];
            }
            if (cid12.StartsWith(Para.CID1_DC + Para.CID2_RELAY_STATUS_GET))
            {
                Console.WriteLine("干结点类型测试代码块，当前为" + cid12);
                Console.WriteLine("数据为" + s[11].Substring(1, 1) + s[12].Substring(1, 1));
                if ("30".Equals(s[12]))
                {
                    forCid[cid12].Text = "开";
                }
                if ("31".Equals(s[12]))
                {
                    forCid[cid12].Text = "关";
                }
            }
            //监控语言
            if ((Para.CID1_DC + Para.CID2_PANEL_LANGUAGE_GET).Equals(cid12))
            {
                //（由于尚未清楚中英文所对应的代码，故暂时搁置）
                if ("30".Equals(s[10]))
                {
                    comboBox10.Text = "中文";
                }
                if ("31".Equals(s[10]))
                {
                    comboBox10.Text = "英文";
                }
                if ("32".Equals(s[10]))
                {
                    comboBox10.Text = "西班牙语";
                }
            }
            //获取历史记录的首记录地址（测试）
            if ((Para.CID1_DC + Para.CID2_HISTORY_FIRST_ADR_GET).Equals(cid12))
            {
                int count = Convert.ToInt32(s[9].Substring(1, 1) + s[10].Substring(1, 1) + s[11].Substring(1, 1) + s[12].Substring(1, 1), 16);
                forCid[cid12].Text = count.ToString();
                if (radioButton2.Checked)
                {
                    temp = count - 1;
                    new Thread(new ThreadStart(lanSendHistories)).Start();
                    /*for (int i = count - 1; i >= 0; i--)
                    {
                        string data = String.Format("{0:X4}", i);
                        Delay(200);
                        new Thread(new ThreadStart(Send(Para.CID1_DC, Para.CID2_HISTORY_GET, data)));
                        Delay(100);
                    }*/
                }
                if (radioButton1.Checked)
                {
                    string data = String.Format("{0:X4}", --count);
                    Delay(500);
                    Send(Para.CID1_DC, Para.CID2_HISTORY_GET, data);
                    temp = count;
                }
                //Delay(5000);
                //data = String.Format("{0:X4}", count - 2);
                //Send(Para.CID1_DC, Para.CID2_HISTORY_GET, data);
            }
            //按地址查找对应的历史记录（测试）
            if((Para.CID1_DC + Para.CID2_HISTORY_GET).Equals(cid12))
            {
                string year = "20" + s[9].Substring(1, 1) + s[10].Substring(1, 1);
                string month = s[11].Substring(1, 1) + s[12].Substring(1, 1);
                string day = s[13].Substring(1, 1) + s[14].Substring(1, 1);
                string hour = s[15].Substring(1, 1) + s[16].Substring(1, 1);
                string minute = s[17].Substring(1, 1) + s[18].Substring(1, 1);
                string second = s[19].Substring(1, 1) + s[20].Substring(1, 1);
                string code = s[21].Substring(1, 1) + s[22].Substring(1, 1) + s[23].Substring(1, 1) + s[24].Substring(1, 1);
                string time = year + "-" + month + "-" + day + " " + hour + ":" + minute + ":" + second;
                string message = histories[code];
                ((DataGridView)control).Rows.Add(new string[] { time, message });
                if (radioButton1.Checked)
                {
                    if (temp > 0)
                    {
                        Send(Para.CID1_DC, Para.CID2_HISTORY_GET, String.Format("{0:X4}", --temp));
                    }
                }
            }
        }
        private int temp;
        //接收网口消息
        private string Receive(int timeout)
        {
            StringBuilder result = new StringBuilder();
            Socket.ReceiveTimeout = timeout;
            byte[] bytes = new byte[1024];
            Queue<string> ssQueue = new Queue<string>();
            int length = 0;
            try
            {
                while ((length = Socket.Receive(bytes)) > 0)
                {
                    for (int i = 0; i < length; i++)
                    {
                        string s = Convert.ToString(bytes[i], 16).ToUpper();
                        if (s.Length.Equals(1))
                        {
                            s = "0" + s;
                        }
                        ssQueue.Enqueue(s);
                    }
                    if (length < bytes.Length)
                    {
                        break;
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            while (false == 0.Equals(ssQueue.Count))
            {
                result.Append(ssQueue.Dequeue() + " ");
            }
            return result.ToString();
        }
        private void putRtn(string cid1,string cid2,string rtn) { }
        private void serialError(object sender, EventArgs e)
        {
            Console.WriteLine("有错误");
        }
        private void lanSendHistories()
        {
            while (temp > 0)
            {
                string data = String.Format("{0:X4}", temp--);
                Delay(100);
                Send(Para.CID1_DC, Para.CID2_HISTORY_GET, data);
            }
        }

        //16进制字符串转化为字节数组
        static byte[] StringToHex(string origin)
        {
            //去掉指令中的空格
            string withoutspace = origin.Replace(" ", "");
            //若非偶数个，则在末尾再加一个空格，使字符个数成为偶数
            if ((withoutspace.Length % 2) != 0)
                withoutspace += " ";
            //一个16进制数为4位，一个byte为8位，故字节数组的长度为字符串一半
            byte[] returnBytes = new byte[withoutspace.Length / 2];
            //每两个16进制字符转换成一个字节
            for (int i = 0; i < returnBytes.Length; i++)
                returnBytes[i] = Convert.ToByte(withoutspace.Substring(i * 2, 2), 16);
            return returnBytes;
        }

        //com框加载出来的时候，扫描可用串口，在其下拉列表显示。
        private void comm_Load(object sender,EventArgs e)
        {
            string[] ports=System.IO.Ports.SerialPort.GetPortNames();
            for(int i = 0; i < ports.Length; i++)
            {
                comm.Items.Add(ports[i]);
            }
        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            lanPanel.Visible = false;
            rsPanel.Visible = true;
        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            lanPanel.Visible = true;
            rsPanel.Visible = false;
        }
        //连接
        private void button42_Click(object sender, EventArgs e)
        {
            try
            {
                if (button42.Text == "OPEN")
                {
                    //打开连接代码（目前没写网口部分）
                    openConnect();
                    //初始化信号量
                    //mutex = new Mutex(false,mutexName);
                    mutex = true;
                    //更新接收时间
                    receivedTime = new TimeSpan(DateTime.Now.Ticks).TotalMilliseconds;
                    //界面处理代码
                    button42.Text = "CLOSE";
                    radioButton1.Enabled = false;
                    radioButton2.Enabled = false;
                    rsPanel.Enabled = false;
                    lanPanel.Enabled = false;
                }
                else
                {
                    //关闭连接代码（目前没写网口部分）
                    closeConnect();
                    //关闭连接后，队列中如果还有剩余的指令，也应该清空，因为它们没有任何被执行的可能了。
                    queue.Clear();
                    //界面处理代码
                    button42.Text = "OPEN";
                    radioButton1.Enabled = true;
                    radioButton2.Enabled = true;
                    rsPanel.Enabled = true;
                    lanPanel.Enabled = true;
                }
            }
            catch (UnauthorizedAccessException ex)
            {
                MessageBox.Show(ex.Message);
            }
            catch (SocketException ex) {
                MessageBox.Show(ex.Message);
            }
                /*catch(ArgumentException ex)
            {
                MessageBox.Show(ex.Message);
            }*/
        }
        private void openConnect() {
            //如果选择的是RS485端口的连接
            if (radioButton1.Checked)
            {
                serialPort1 = new System.IO.Ports.SerialPort();
                //提取comm框和baud框的数据
                serialPort1.PortName = comm.Text;
                serialPort1.BaudRate = Convert.ToInt32(baudrate.Text);
                serialPort1.DataReceived += read;
                //（校验位和停止位目前没有）
                serialPort1.Open();
                Console.WriteLine("串口已打开");
            }
            //如果选择的是网口的连接
            if (radioButton2.Checked)
            {
                string host = ipTextBox.Text;
                int port = Convert.ToInt32(portTextBox.Text);
                Socket.Connect(host, port);
            }
        }
        private void closeConnect() {
            //如果选择的是RS485端口的连接
            if (radioButton1.Checked)
            {
                serialPort1.Close();
                Console.WriteLine("串口已关闭");
            }
            //如果选择的是网口的连接
            if (radioButton2.Checked)
            {
                if (Socket.Connected)
                {
                    Socket.Shutdown(SocketShutdown.Both);
                }
                Socket.Close();
            }
        }

        //按下面板内所有读/写按钮
        private void preshAll(Panel tabPage,string text)
        {
            bool s;
            button68.Enabled = false;
            button69.Enabled = false;
            button122.Enabled = false;
            button123.Enabled = false;
            if ("关闭跟踪".Equals(button121.Text))
            {
                s = true;
            }
            else
            {
                s = false;
            }
            if (s)
            {
                button121.PerformClick();
            }
            button121.Enabled = false;
            Delay(100);
            try
            {
                Queue<Control> q = new Queue<Control>();
                q.Enqueue(tabPage);
                while (0 != q.Count)
                {
                    Control control = q.Dequeue();
                    if (control is Panel)
                    {
                        int c = ((Panel)control).Controls.Count;
                        for (int i = 0; i < c; i++)
                        {
                            q.Enqueue(((Panel)control).Controls[i]);
                        }
                    }
                    if (control is Button)
                    {
                        //richTextBox1.AppendText("测试：" + control.Text + "\n");
                        if (text.Equals(control.Text))
                        {
                            ((Button)control).PerformClick();
                            Console.WriteLine("测试："+control.Name+"被按下");
                            if ("写".Equals(text))
                            {
                                Delay(500);
                            }
                            if ("读".Equals(text))
                            {
                                Delay(150);
                            }
                        }
                    }
                }
            }
            catch (NullReferenceException) { }
            Delay(100);
            button121.Enabled = true;
            if (s)
            {
                button121.PerformClick();
            }
            button68.Enabled = true;
            button69.Enabled = true;
            button122.Enabled = true;
            button123.Enabled = true;
        }
        public static void Delay(int mm)
        {
            DateTime current = DateTime.Now;
            while (current.AddMilliseconds(mm) > DateTime.Now)
            {
                Application.DoEvents();
            }
            return;
        }

        private void comm_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void label53_Click(object sender, EventArgs e)
        {

        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void dataGridView1_CellContentClick_1(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void dataGridView1_CellContentClick_2(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void tableLayoutPanel9_Paint(object sender, PaintEventArgs e)
        {

        }

        private void comboBox10_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        //交流电压过高的读操作
        private void button1_Click(object sender, EventArgs e)
        {
            Send(Para.CID1_AC, Para.CID2_ACV_HIGH_ALARM_GET, "");
        }
        //交流电压过低（读）
        private void button2_Click(object sender, EventArgs e)
        {
            Send(Para.CID1_AC, Para.CID2_ACV_LOW_ALARM_GET, "");
        }
        //交流电压掉电（读）
        private void button3_Click(object sender, EventArgs e)
        {
            Send(Para.CID1_AC, Para.CID2_AC_DOWN_ALARM_GET, "");
        }
        //交流电流过高（读）
        private void button4_Click(object sender, EventArgs e)
        {
            Send(Para.CID1_AC, Para.CID2_ACI_HIGH_ALARM_GET,"");
        }
        //48V模块限流点（读）
        private void button5_Click(object sender, EventArgs e)
        {
            Send(Para.CID1_MODULE, Para.CID2_LIMIT_I_GET, "");
        }
        //负载电流过高（读）
        private void button6_Click(object sender, EventArgs e)
        {
            Send(Para.CID1_DC, Para.CID2_RI_HIGH_ALARM_GET, "");
        }
        //电池电流过高（读）
        private void button7_Click(object sender, EventArgs e)
        {
            Send(Para.CID1_DC, Para.CID2_BATI_HIGH_ALARM_GET, "");
        }
        //输出过压告警（读）
        private void button8_Click(object sender, EventArgs e)
        {
            Send(Para.CID1_DC, Para.CID2_V_HIGH_GET, "");
        }
        //电池低压告警（读）
        private void button9_Click(object sender, EventArgs e)
        {
            Send(Para.CID1_DC, Para.CID2_BATV_LOW_ALARM_GET, "");
        }
        //负载下电告警（读）
        private void button10_Click(object sender, EventArgs e)
        {
            Send(Para.CID1_DC, Para.CID2_FREEV_OFF_1_GET, "");
        }
        //负载下电恢复（读）
        private void button11_Click(object sender, EventArgs e)
        {
            Send(Para.CID1_DC, Para.CID2_FREEV_RESTART_1_GET, "");
        }
        //电池下电告警（读）
        private void button12_Click(object sender, EventArgs e)
        {
            Send(Para.CID1_DC, Para.CID2_FREEV_OFF_2_GET, "");
        }
        //电池下电恢复（读）
        private void button14_Click(object sender, EventArgs e)
        {
            Send(Para.CID1_DC, Para.CID2_FREEV_RESTART_2_GET, "");
        }
        //浮充电压设定（读）
        private void button15_Click(object sender, EventArgs e)
        {
            Send(Para.CID1_DC, Para.CID2_FLOATING_V_GET, "");
        }
        //均充电压设定（读）
        private void button13_Click(object sender, EventArgs e)
        {
            Send(Para.CID1_DC, Para.CID2_REGULAR_AVG_V_GET, "");
        }
        //均转浮系数（读）
        private void button16_Click(object sender, EventArgs e)
        {
            Send(Para.CID1_DC, Para.CID2_AVG_TO_FLOAT_GET, "");
        }
        //浮转均系数（读）
        private void button17_Click(object sender, EventArgs e)
        {
            Send(Para.CID1_DC, Para.CID2_FLOAT_TO_AVG_GET, "");
        }
        //充电限流系数（读）
        private void button35_Click(object sender, EventArgs e)
        {
            Send(Para.CID1_DC, Para.CID2_CHARGING_LIMITED_PERCENT_GET, "");
        }
        //温补使能（读）
        private void button36_Click(object sender, EventArgs e)
        {
            Send(Para.CID1_DC, Para.CID2_TEMP_COMPENSATION_GET, "");
        }
        //温补系数（读）
        private void button37_Click(object sender, EventArgs e)
        {
            Send(Para.CID1_DC, "3F", "");
        }
        //定期均充（读）
        private void button43_Click(object sender, EventArgs e)
        {
            Send(Para.CID1_DC, Para.CID2_REGULAR_AVG_ENERGY_GET, "");
        }
        //定期均充间隔（读）
        private void button44_Click(object sender, EventArgs e)
        {
            Send(Para.CID1_DC, Para.CID2_REGULAR_AVG_TIME_GAP_GET, "");
        }
        //持续均充时间（读）
        private void button47_Click(object sender, EventArgs e)
        {
            Send(Para.CID1_DC, Para.CID2_REGULAR_AVG_TIME_GET, "");
        }
        //蜂鸣告警（读）
        private void button51_Click(object sender, EventArgs e)
        {
            Send(Para.CID1_DC, Para.CID2_MONITOR_VOICE_GET, "");
        }
        //时间（读）
        private void button52_Click(object sender, EventArgs e)
        {
            Send(Para.CID1_DC, Para.CID2_CLOCK_GET, "");
        }
        //电池容量（读）
        private void button53_Click(object sender, EventArgs e)
        {
            Send(Para.CID1_DC, Para.CID2_BAT_VOL1_GET, "");
        }
        //电池容量保持（读）
        private void button54_Click(object sender, EventArgs e)
        {
            Send(Para.CID1_DC, Para.CID2_BAT_VOL_PERCENT_GET, "");
        }
        //休眠使能（读）
        private void button56_Click(object sender, EventArgs e)
        {
            Send(Para.CID1_DC, Para.CID2_SLEEP_ENERGY_GET, "");
        }
        //休眠间隔时间（读）
        private void button58_Click(object sender, EventArgs e)
        {
            Send(Para.CID1_DC, Para.CID2_SLEEP_TIME_GAP_GET, "");
        }
        //限流最小电压（读）
        private void button60_Click(object sender, EventArgs e)
        {
            Send(Para.CID1_DC, Para.CID2_LIMITED_MIN_V_GET, "");
        }
        //电池电流增益（读）
        private void button62_Click(object sender, EventArgs e)
        {
            Send(Para.CID1_DC, Para.CID2_CURRENT_SLOPE_GET, "");
        }
        //电池电流偏移（读）
        private void button64_Click(object sender, EventArgs e)
        {
            Send(Para.CID1_DC, Para.CID2_CURRENT_ZERO_GET, "");
        }
        //母排电压增益（读）
        private void button66_Click(object sender, EventArgs e)
        {
            Send(Para.CID1_DC, Para.CID2_BUS_OUTPUT_V_SLOPE_GET, "");
        }
        //参数1设置：一键刷新
        private void button69_Click(object sender, EventArgs e)
        {
            //button69.Enabled = false;
            //new Thread(new ThreadStart(b69)).Start();
            preshAll(tabPage1, "读");
            //button69.Enabled = true;
        }
        private void b69()
        {
            preshAll(tabPage1, "读");
        }
        //清空richTextBox
        private void clearRichTextBoxButton_Click(object sender, EventArgs e)
        {
            richTextBox1.Clear();
        }
        //参数1设置：一键写入
        private void button68_Click(object sender, EventArgs e)
        {
            //button68.Enabled = false;
            preshAll(tabPage1, "写");
            //button68.Enabled = true;
        }
        //参数2设置：一键写入
        private void button123_Click(object sender, EventArgs e)
        {
            //button123.Enabled = false;
            preshAll(tabPage2, "写");
            //button123.Enabled = true;
        }
        //参数2设置：一键刷新
        private void button122_Click(object sender, EventArgs e)
        {
            //button122.Enabled = false;
            preshAll(tabPage2, "读");
            //button122.Enabled = true;
        }
        //参数2设置
        //干结点1类型（读）
        private void button72_Click(object sender, EventArgs e)
        {
            Send(Para.CID1_DC, Para.CID2_RELAY_TYPE_GET, "01");
        }
        //干结点2类型（读）
        private void button77_Click(object sender, EventArgs e)
        {
            Send(Para.CID1_DC, Para.CID2_RELAY_TYPE_GET, "02");
        }
        //干结点3类型（读）
        private void button81_Click(object sender, EventArgs e)
        {
            Send(Para.CID1_DC, Para.CID2_RELAY_TYPE_GET, "03");
        }
        //干结点4类型（读）
        private void button85_Click(object sender, EventArgs e)
        {
            Send(Para.CID1_DC, Para.CID2_RELAY_TYPE_GET, "04");
        }
        //干结点1状态（读）
        private void button75_Click(object sender, EventArgs e)
        {
            Send(Para.CID1_DC, Para.CID2_RELAY_STATUS_GET, "01");
        }
        //干结点2状态（读）
        private void button79_Click(object sender, EventArgs e)
        {
            Send(Para.CID1_DC, Para.CID2_RELAY_STATUS_GET, "02");
        }
        //干结点3状态（读）
        private void button82_Click(object sender, EventArgs e)
        {
            Send(Para.CID1_DC, Para.CID2_RELAY_STATUS_GET, "03");
        }
        //干结点4状态（读）
        private void button86_Click(object sender, EventArgs e)
        {
            Send(Para.CID1_DC, Para.CID2_RELAY_STATUS_GET, "04");
        }
        //电池测试使能（读）
        private void button88_Click(object sender, EventArgs e)
        {
            Send(Para.CID1_DC, Para.CID2_BAT_TEST_ENERGY_GET, "");
        }
        //电池测试开始电压（读）
        private void button94_Click(object sender, EventArgs e)
        {
            Send(Para.CID1_DC, Para.CID2_BAT_TEST_START_V_GET, "");
        }
        //电池测试结束电压（读）
        private void button89_Click(object sender, EventArgs e)
        {
            Send(Para.CID1_DC, Para.CID2_BAT_TEST_END_V_GET, "");
        }
        //快充使能（读）
        private void button95_Click(object sender, EventArgs e)
        {
            Send(Para.CID1_DC, Para.CID2_FAST_CHARGE_GET, "");
        }
        //快充电压（读）
        private void button90_Click(object sender, EventArgs e)
        {
            Send(Para.CID1_DC, Para.CID2_FAST_V_GET, "");
        }
        //快充时间（读）
        private void button91_Click(object sender, EventArgs e)
        {
            Send(Para.CID1_DC, Para.CID2_FAST_TIME_GET, "");
        }
        //电池低温告警点（读）
        private void button92_Click(object sender, EventArgs e)
        {
            Send(Para.CID1_DC, Para.CID2_BAT_TEMP_LOW_ALARM_GET, "");
        }
        //电池高温告警点（读）
        private void button93_Click(object sender, EventArgs e)
        {
            Send(Para.CID1_DC, Para.CID2_BAT_TEMP_HIGH_ALARM_GET, "");
        }
        //模块数量配置（读）
        //（由于下位机中并没有对应的指令，且不需要这个功能，故此方法废弃）
        private void button120_Click(object sender, EventArgs e)
        {
            //Send(Para.CID1_DC,"")
        }
        //负载电流增益（读）
        private void button105_Click(object sender, EventArgs e)
        {
            Send(Para.CID1_DC, Para.CID2_LOAD_CURRENT_SLOPE_GET, "");
        }
        //电池温度增益（读）
        private void button108_Click(object sender, EventArgs e)
        {
            Send(Para.CID1_DC, Para.CID2_BATTERY_ADD_GET, "");
        }
        //电池温度偏移（读）
        private void button111_Click(object sender, EventArgs e)
        {
            Send(Para.CID1_DC, Para.CID2_BATTERY_OFFSET_GET, "");
        }
        //环境温度增益（读）
        private void button104_Click(object sender, EventArgs e)
        {
            Send(Para.CID1_DC, Para.CID2_ENV_ADD_GET, "");
        }
        //环境温度偏移（读）
        private void button107_Click(object sender, EventArgs e)
        {
            Send(Para.CID1_DC, Para.CID2_ENV_OFFSET_GET, "");
        }
        //交流电压增益（读）
        private void button110_Click(object sender, EventArgs e)
        {
            Send(Para.CID1_DC, Para.CID2_A_AC_V_ADD_GET, "");
        }
        //交流电压偏移（读）
        private void button106_Click(object sender, EventArgs e)
        {
            Send(Para.CID1_DC, Para.CID2_A_AC_V_ZERO_GET, "");
        }
        //负载电流偏移（读）
        private void button109_Click(object sender, EventArgs e)
        {
            Send(Para.CID1_DC, Para.CID2_LOAD_CURRENT_ZERO_GET, "");
        }
        //获取监控语言（读）
        private void button120_Click_1(object sender, EventArgs e)
        {
            Send(Para.CID1_DC, Para.CID2_PANEL_LANGUAGE_GET, "");
        }
        //获取历史记录
        private void button125_Click(object sender, EventArgs e)
        {
            if ("关闭跟踪".Equals(button121.Text))
            {
                button121.PerformClick();
                Delay(100);
                cid2Queue.Clear();
            }
            dataGridView2.Rows.Clear();
            Send(Para.CID1_DC, Para.CID2_HISTORY_FIRST_ADR_GET, "000101010101");
            Console.WriteLine("125");
        }

        private void toolStripStatusLabel1_Click(object sender, EventArgs e)
        {

        }

        private void timer1_Tick(object sender, EventArgs e)
        {

        }
    }
}
