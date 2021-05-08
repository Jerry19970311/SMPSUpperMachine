using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WindowsFormsApp1
{
    class Para
    {
        //帧开始符
        public const string SOI = "7E";
        //帧结束符
        public const string EOI = "0D";
        //RTN正常码
        public const string RTN_COMMON = "00";
        //RTN错误码
        public const string RTN_VER = "01";
        public const string RTN_CHKSUM = "02";
        public const string RTN_LCHKSUM = "03";
        public const string RTN_CID2 = "04";
        public const string RTN_COMMAND_FORMAT = "05";
        public const string RTN_INVALID = "06";
        public const string RTN_FAILURE = "E0";

        //CID1码
        //交流配电
        public const string CID1_AC = "40";
        //电源模块
        public const string CID1_MODULE = "41";
        //直流配电
        public const string CID1_DC = "42";
        //电表系统
        public const string CID1_ELE_METER_SYSTEM = "44";

        //CID2码

        //交流模块（CID1=40H）
        //获取输入A、B、C相电压、电流、告警，获取交流空开、交流电流过高
        public const string CID2_ABC = "01";
        //交流电压过高告警点设置
        public const string CID2_ACV_HIGH_ALARM_SET = "05";
        //交流电压过高告警点获取
        public const string CID2_ACV_HIGH_ALARM_GET = "06";
        //交流电压过低告警点设置
        public const string CID2_ACV_LOW_ALARM_SET = "07";
        //交流电压过低告警点获取
        public const string CID2_ACV_LOW_ALARM_GET = "08";
        //交流掉电（缺相）告警点设置
        public const string CID2_AC_DOWN_ALARM_SET = "09";
        //交流掉电（缺相）告警点获取
        public const string CID2_AC_DOWN_ALARM_GET = "0A";
        //交流电流过高告警点设置
        public const string CID2_ACI_HIGH_ALARM_SET = "0B";
        //交流电流过高告警点获取
        public const string CID2_ACI_HIGH_ALARM_GET = "0C";

        //电源模块（CID1=41H）
        //获取电源模块输出电流、风机速度、电源模块温度、电源模块输出电压
        //获取电源模块失效、电源模块、超出温度范围、风机失效、限流、超出电压范围
        public const string CID2_MODEL = "0E";
        //遥控开机
        public const string CID2_ON = "10";
        //遥控关机
        public const string CID2_OFF = "11";
        //限流点电流设置
        public const string CID2_LIMIT_I_SET = "14";
        //限流点电流获取
        public const string CID2_LIMIT_I_GET = "15";

        //直流配电（CID1=42H）
        //
        public const string CID2_42 = "16";
        //获取所有的有效在位的模块地址
        public const string CID2_MODEL_ADD = "17";
        //电池温度增益设置
        public const string CID2_BATTERY_ADD_SET = "18";
        //电池温度增益获取
        public const string CID2_BATTERY_ADD_GET = "19";
        //电池温度偏移量设置
        public const string CID2_BATTERY_OFFSET_SET = "1A";
        //电池温度偏移量获取
        public const string CID2_BATTERY_OFFSET_GET = "1B";
        //环境温度增益设置
        public const string CID2_ENV_ADD_SET = "1C";
        //环境温度增益获取
        public const string CID2_ENV_ADD_GET = "1D";
        //环境温度偏移量设置
        public const string CID2_ENV_OFFSET_SET = "1E";
        //环境温度偏移量获取
        public const string CID2_ENV_OFFSET_GET = "1F";
        //监控面板显示语言获取
        public const string CID2_PANEL_LANGUAGE_GET = "21";
        //负载电流过高告警点设置
        public const string CID2_RI_HIGH_ALARM_SET = "22";
        //负载电流过高告警点获取
        public const string CID2_RI_HIGH_ALARM_GET = "23";
        //电池电流过高告警点设置
        public const string CID2_BATI_HIGH_ALARM_SET = "24";
        //电池电流过高告警点获取
        public const string CID2_BATI_HIGH_ALARM_GET = "25";
        //过压告警点设置
        public const string CID2_V_HIGH_SET = "26";
        //过压告警点获取
        public const string CID2_V_HIGH_GET = "27";
        //电池低压告警点设置
        public const string CID2_BATV_LOW_ALARM_SET = "28";
        //电池低压告警点获取
        public const string CID2_BATV_LOW_ALARM_GET = "29";
        //电池温度过低告警点设置
        public const string CID2_BAT_TEMP_LOW_ALARM_SET = "2A";
        //电池温度过低告警点获取
        public const string CID2_BAT_TEMP_LOW_ALARM_GET = "2B";
        //电池温度过高告警点设置
        public const string CID2_BAT_TEMP_HIGH_ALARM_SET = "2C";
        //电池温度过高告警点获取
        public const string CID2_BAT_TEMP_HIGH_ALARM_GET = "2D";
        //环境温度过低告警点设置
        public const string CID2_ENV_TEMP_LOW_ALARM_SET = "2E";
        //环境温度过低告警点获取
        public const string CID2_ENV_TEMP_LOW_ALARM_GET = "2F";
        //环境温度过高告警点设置
        public const string CID2_ENV_TEMP_HIGH_ALARM_SET = "30";
        //环境温度过高告警点获取
        public const string CID2_ENV_TEMP_HIGH_ALARM_GET = "31";
        //欠压断开连接点1设置
        public const string CID2_FREEV_OFF_1_SET = "32";
        //欠压断开连接点1获取
        public const string CID2_FREEV_OFF_1_GET = "33";
        //欠压恢复连接点1设置
        public const string CID2_FREEV_RESTART_1_SET = "34";
        //欠压恢复连接点1获取
        public const string CID2_FREEV_RESTART_1_GET = "35";
        //欠压断开连接点2设置
        public const string CID2_FREEV_OFF_2_SET = "36";
        //欠压断开连接点2获取
        public const string CID2_FREEV_OFF_2_GET = "37";
        //欠压恢复连接点2设置
        public const string CID2_FREEV_RESTART_2_SET = "38";
        //欠压恢复连接点2获取
        public const string CID2_FREEV_RESTART_2_GET = "39";
        //浮充电压点设置
        public const string CID2_FLOATING_V_SET = "3A";
        //浮充电压点获取
        public const string CID2_FLOATING_V_GET = "3B";
        //温度补偿使能设置
        public const string CID2_TEMP_COMPENSATION_SET = "3C";
        //温度补偿使能获取
        public const string CID2_TEMP_COMPENSATION_GET = "3D";
        //电池充电限流百分比设置
        public const string CID2_CHARGING_LIMITED_PERCENT_SET = "40";
        //电池充电限流百分比获取
        public const string CID2_CHARGING_LIMITED_PERCENT_GET = "41";
        //限流最小电压设置
        public const string CID2_LIMITED_MIN_V_SET = "44";
        //限流最小电压获取
        public const string CID2_LIMITED_MIN_V_GET = "45";
        //定期均充使能设置
        public const string CID2_REGULAR_AVG_ENERGY_SET = "46";
        //定期均充使能获取
        public const string CID2_REGULAR_AVG_ENERGY_GET = "47";
        //定期均充时间间隔设置
        public const string CID2_REGULAR_AVG_TIME_GAP_SET = "48";
        //定期均充时间间隔获取 
        public const string CID2_REGULAR_AVG_TIME_GAP_GET = "49";
        //均充持续时间设置
        public const string CID2_REGULAR_AVG_TIME_SET = "4A";
        //均充持续时间获取
        public const string CID2_REGULAR_AVG_TIME_GET = "4B";
        //均充电压点设置
        public const string CID2_REGULAR_AVG_V_SET = "4C";
        //均充电压点获取
        public const string CID2_REGULAR_AVG_V_GET = "4D";
        //手动均充设置
        public const string CID2_MANUAL_AVG_SET = "4E";
        //快充使能设置
        public const string CID2_FAST_CHARGE_SET = "50";
        //快充使能获取
        public const string CID2_FAST_CHARGE_GET = "51";
        //快充电压点设置
        public const string CID2_FAST_V_SET = "52";
        //快充电压点获取
        public const string CID2_FAST_V_GET = "53";
        //快充时间限制设置
        public const string CID2_FAST_TIME_SET = "54";
        //快充时间限制获取
        public const string CID2_FAST_TIME_GET = "55";
        //电池容量1设置（标称容量）
        public const string CID2_BAT_VOL1_SET = "56";
        //电池容量1获取（标称容量）
        public const string CID2_BAT_VOL1_GET = "57";
        //电池容量保持百分比设置
        public const string CID2_BAT_VOL_PERCENT_SET = "58";
        //电池容量保持百分比获取
        public const string CID2_BAT_VOL_PERCENT_GET = "59";
        //电池测试使能设置
        public const string CID2_BAT_TEST_ENERGY_SET = "5A";
        //电池测试使能获取
        public const string CID2_BAT_TEST_ENERGY_GET = "5B";
        //电池测试开始电压设置
        public const string CID2_BAT_TEST_START_V_SET = "5C";
        //电池测试开始电压获取
        public const string CID2_BAT_TEST_START_V_GET = "5D";
        //电池测试完成电压设置
        public const string CID2_BAT_TEST_END_V_SET = "5E";
        //电池测试完成电压获取
        public const string CID2_BAT_TEST_END_V_GET = "5F";
        //获取监控软件版本、产品序列号
        public const string CID2_MONITOR_SOFT_VERSION_GET = "6C";
        //监控模块地址设置
        public const string CID2_MONITOR_ADR_SET = "6D";
        //电池电流斜率设置
        public const string CID2_CURRENT_SLOPE_SET = "70";
        //电池电流斜率获取
        public const string CID2_CURRENT_SLOPE_GET = "71";
        //电池电流零点设置
        public const string CID2_CURRENT_ZERO_SET = "72";
        //电池电流零点获取
        public const string CID2_CURRENT_ZERO_GET = "73";
        //母排输出电压斜率设置
        public const string CID2_BUS_OUTPUT_V_SLOPE_SET = "74";
        //母排输出电压斜率获取
        public const string CID2_BUS_OUTPUT_V_SLOPE_GET = "75";
        //负载电流斜率设置
        public const string CID2_LOAD_CURRENT_SLOPE_SET = "76";
        //负载电流斜率获取
        public const string CID2_LOAD_CURRENT_SLOPE_GET = "77";
        //均充转浮充系数设置
        public const string CID2_AVG_TO_FLOAT_SET = "78";
        //均充转浮充系数获取
        public const string CID2_AVG_TO_FLOAT_GET = "79";
        //浮充转均充系数设置
        public const string CID2_FLOAT_TO_AVG_SET = "7A";
        //浮充转均充系数获取
        public const string CID2_FLOAT_TO_AVG_GET = "7B";
        //A交流电压零点设置
        public const string CID2_A_AC_V_ZERO_SET = "7C";
        //A交流电压零点获取
        public const string CID2_A_AC_V_ZERO_GET = "7D";
        //A交流电压增益设置
        public const string CID2_A_AC_V_ADD_SET = "7E";
        //A交流电压增益获取
        public const string CID2_A_AC_V_ADD_GET = "7F";
        //监控板声音开关设置
        public const string CID2_MONITOR_VOICE_SET = "80";
        //监控板声音开关获取
        public const string CID2_MONITOR_VOICE_GET = "81";
        //继电器状态设置
        public const string CID2_RELAY_STATUS_SET = "85";
        //继电器状态获取
        public const string CID2_RELAY_STATUS_GET = "86";
        //继电器类型设置
        public const string CID2_RELAY_TYPE_SET = "87";
        //继电器类型获取
        public const string CID2_RELAY_TYPE_GET = "88";
        //负载电流零点设置
        public const string CID2_LOAD_CURRENT_ZERO_SET = "89";
        //负载电流零点获取
        public const string CID2_LOAD_CURRENT_ZERO_GET = "8A";
        //时钟获取 
        public const string CID2_CLOCK_GET = "8D";
        //始终设置
        public const string CID2_CLOCK_SET = "8E";
        //历史记录获取
        public const string CID2_HISTORY_GET = "8F";
        //历史首记录地址获取
        public const string CID2_HISTORY_FIRST_ADR_GET = "90";
        //休眠使能设置
        public const string CID2_SLEEP_ENERGY_SET = "91";
        //休眠使能获取
        public const string CID2_SLEEP_ENERGY_GET = "92";
        //休眠间隔时间设置
        public const string CID2_SLEEP_TIME_GAP_SET = "93";
        //休眠间隔时间获取
        public const string CID2_SLEEP_TIME_GAP_GET = "94";
        //清除历史记录
        public const string CID2_HISTORY_CLEAR = "95";
        //电池电流2增益设置
        public const string CID2_BAT_CURRENT2_ADD_SET = "96";
        //电池电流2增益获取
        public const string CID2_BAT_CURRENT2_ADD_GET = "97";
        //B相交流电压增益设置
        public const string CID2_AC_VB_ADD_SET = "A4";
        //B相交流电压增益获取
        public const string CID2_AC_VB_ADD_GET = "A5";
        //C相交流电压增益设置
        public const string CID2_AC_VC_ADD_SET = "A6";
        //C相交流电压增益获取
        public const string CID2_AC_VC_ADD_GET = "A7";
        //电池电流X增益设置
        public const string CID2_BAT_CURRENT_X_ADD_SET = "B9";
        //电池电流X增益获取
        public const string CID2_BAT_CURRENT_X_ADD_GET = "BA";
        //电池电流X偏移量设置
        public const string CID2_BAT_CURRENT_X_OFFSET_SET = "BB";
        //电池电流X偏移量获取
        public const string CID2_BAT_CURRENT_X_OFFSET_GET = "BC";
        //电池测试状态获取
        public const string CID2_BAT_TEST_STATUS_GET = "BD";
        //电池测试结果获取
        public const string CID2_BAT_TEST_RESULT_GET = "BE";
        //设置门禁传感器类型
        public const string CID2_ACCESS_CONTROL_SENSOR_TYPE_SET = "C1";
        //获取门禁传感器类型
        public const string CID2_ACCESS_CONTROL_SENSOR_TYPE_GET = "C2";
        //监控模块波特率设置
        public const string CID2_MONITOR_MODEL_BAUDRATE_SET = "C3";
        //监控模块波特率获取
        public const string CID2_MONITOR_MODEL_BAUDRATE_GET = "C4";

        //电源模块：（CID1=44H）
        //读取（1-12）月记录用电量
        public const string CID2_X_MONTH_GET = "01";
        //读取累计用电量
        public const string CID2_TOTAL_GET = "0D";
        //读取当天用电量
        public const string CID2_DAY_GET = "0E";
        //读取当月用电量
        public const string CID2_MONTH_GET = "0F";
        //读取当年用电量
        public const string CID2_YEAR_GET = "10";
    }
}
