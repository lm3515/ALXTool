using Newtonsoft.Json;
using Renci.SshNet;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WinSCP;

namespace ALXTool
{
    public partial class Form1 : Form
    {
        bool isOld = true;
        SshClient sshClient;//= new SshClient("192.168.18.1", "dg", "ivanlee");
        string ip;
        string username;
        string password;
        StreamWriter successList = new StreamWriter("C:\\tmp\\successList.txt",true);

        public Form1()
        {
            successList.AutoFlush = true;
            InitializeComponent();
            string name = Dns.GetHostName();
            IPAddress[] ipadrlist = Dns.GetHostAddresses(name);
            foreach (IPAddress ipa in ipadrlist)
            {
                if (ipa.AddressFamily == AddressFamily.InterNetwork)
                {
                    logArea.AppendText("本机IP：" + ipa.ToString() + "\n");
                    if (ipa.ToString().StartsWith("192.168.11"))// 旧固件
                    {
                        isOld = true;
                        ip = "192.168.11.123";
                        username = "root";
                        password = "admin";
                    }
                    else
                    {
                        isOld = false;
                        ip = "192.168.18.1";
                        username = "dg";
                        password = "ivanlee";
                    }
                    sshClient = new SshClient(ip, username, password);
                }
            }
        }


    private void button_ConnectPrintBox_Click(object sender, EventArgs e)
        {
            try
            {
                button_ConnectPrintBox.Enabled = false;
                //logArea.AppendText("正在连接盒子...\n");
                if (sshClient.IsConnected)
                {
                    button_ConnectPrintBox.Enabled = true;
                    return;
                }

                logArea.Clear();
                logArea.AppendText("正在连接盒子...\n");

                int i = 0;
                while (!sshClient.IsConnected)
                {
                    try
                    {
                        sshClient.Connect();
                    }
                    catch
                    {
                        logArea.AppendText("正在连接盒子...\n");
                    }
                    if (++i >= 10)
                    {
                        MessageBox.Show("连接盒子失败！");
                        button_ConnectPrintBox.Enabled = true;
                        return;
                    }
                }
                logArea.AppendText("盒子连接成功\n");
                label_connectStatus.BackColor = Color.Green;

                SshCommand sshCommand = sshClient.RunCommand("hexdump /dev/mtd3 -C -s 1024 -n 16 |head -1 |awk  -F \"|\" '{print $2}' |tr -d \".\"");
                string imei = sshCommand.Result.Trim();
                logArea.AppendText("IMEI: "); logArea.AppendText(imei); logArea.AppendText("\n");

                SshCommand sshCommand2 = sshClient.RunCommand("hexdump /dev/mtd3 -C -s 4 -n 6 |head -1 |awk  -F  \" \" '{print $2\":\"$3\":\"$4\":\"$5\":\"$6\":\"$7}'");
                string mac = sshCommand2.Result.Trim();
                logArea.AppendText("Mac地址: "); logArea.AppendText(mac); logArea.AppendText("\n");

                // 保存IMEI和Mac地址
                string infoJson = @"C:\tmp\deviceInfo.json";
                if (File.Exists(infoJson))
                    File.Delete(infoJson);
                Dictionary<string, string> deviceInfo = new Dictionary<string, string>();
                deviceInfo.Add("imei", imei);
                deviceInfo.Add("mac", mac);
                string json = JsonConvert.SerializeObject(deviceInfo);
                File.WriteAllText(infoJson, json);

                // 删除缓存密钥
                sshClient.RunCommand("uci delete wireless.@wifi-iface[0].YJSecret");

                if (!Upload())
                {
                    sshClient.Disconnect();
                    label_connectStatus.BackColor = Color.Red;
                    button_ConnectPrintBox.Enabled = true;
                    return;
                }

                logArea.AppendText("擦除中...\n");
                sshClient.RunCommand("mtd erase ALL");
                logArea.AppendText("擦除完成\n");
                logArea.AppendText("正在烧录...\n");
                sshClient.RunCommand("mtd write /tmp/upgrade.fac ALL");
                logArea.AppendText("烧录成功\n");
                logArea.AppendText("IMEI回写...\n");
                //sshClient.RunCommand("echo -en "+ imei +" | dd of=/dev/mtdblock3 bs=1 seek=1024 conv=notrunc");
                //SshCommand result3 =  sshClient.RunCommand("echo  "+ mac +" | sed 's/:/\\\\x/g' |sed -e 's/^/\\\\x/'");
                //string mac2 = result3.Result;
                //sshClient.RunCommand("echo -en "+ mac2 +" |  dd of=/dev/mtdblock3 bs=1 seek=4 conv=notrunc");
                ///opt/bin/xfwu_imei_uevent.sh 800200806100536H  68:8f:c9:f0:11:10
                sshClient.RunCommand("/opt/bin/xfwu_imei_uevent.sh " + imei + " " + mac);

                SshCommand sshCommand4 = sshClient.RunCommand("hexdump /dev/mtd3 -C -s 1024 -n 16 |head -1 |awk  -F \"|\" '{print $2}' |tr -d \".\"");
                string imei2 = sshCommand4.Result.Trim();
                logArea.AppendText("IMEI: "); logArea.AppendText(imei2); logArea.AppendText("\n");
                SshCommand sshCommand5 = sshClient.RunCommand("hexdump /dev/mtd3 -C -s 4 -n 6 |head -1 |awk  -F  \" \" '{print $2\":\"$3\":\"$4\":\"$5\":\"$6\":\"$7}'");
                string mac3 = sshCommand5.Result.Trim();
                logArea.AppendText("Mac地址: "); logArea.AppendText(mac3); logArea.AppendText("\n");

                logArea.AppendText("写入成功\n");

                successList.WriteLine(DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss") + " - " + imei2);

                logArea.AppendText("升级成功，请等待盒子重启完成！\n");
                sshClient.RunCommand("reboot");
                sshClient.Disconnect();
                label_connectStatus.BackColor = Color.Red;
                button_ConnectPrintBox.Enabled = true;
            }
            catch
            {
                MessageBox.Show("更新失败！");
                sshClient.Disconnect();
                label_connectStatus.BackColor = Color.Red;
                button_ConnectPrintBox.Enabled = true;
            }
        }

        private bool Upload()
        {
            logArea.AppendText("准备推送固件...\n");
            
            SessionOptions sessionOptions = new SessionOptions
            {
                Protocol = Protocol.Scp,
                //HostName = "192.168.11.123",
                //UserName = "root",
                //Password = "admin",
                HostName = ip,
                UserName = username,
                Password = password,
                //SshHostKeyFingerprint = ftpSSHFingerPrint
                GiveUpSecurityAndAcceptAnySshHostKey = true
            };

            string sourcefilepath = "C:\\tmp\\upgrade.fac";
            string filename = Path.GetFileName(sourcefilepath); 
            string ftpfullpath = "/tmp/" + filename;

            using (WinSCP.Session session = new WinSCP.Session())
            {
                try
                {
                    // Connect
                    session.Open(sessionOptions);
                    logArea.AppendText("正在推送...\n");

                    // Upload files
                    TransferOptions transferOptions = new TransferOptions();
                    transferOptions.TransferMode = TransferMode.Binary;

                    TransferOperationResult transferResult = session.PutFiles(sourcefilepath, ftpfullpath, false, transferOptions);
                    if (!transferResult.IsSuccess)
                    {
                        MessageBox.Show("固件推送失败！");
                        return false;
                    }
                    logArea.AppendText("固件推送成功\n");
                }
                catch
                {
                    MessageBox.Show("固件推送失败！");
                    return false;
                }
                
            }
            return true;
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }
    }
}
