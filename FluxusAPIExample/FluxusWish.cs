using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FluxusAPIExample
{
  public partial class FluxusWish : Form
  {
    FluxAPI.API API = new FluxAPI.API();

    public FluxusWish()
    {
      InitializeComponent();
      Random random = new Random();
      switch (random.Next(0,5))
      {
        case 1:
          Text = "Fluxus on a Diet";
          break;
        case 2:
          Text = "Fluxus Lite: Now in dark";
          break;
        case 3:
          Text = "Fluxus Lite Dark: Not even fastcoloredtextbox";
          break;
        case 4:
          Text = "Fluxus Lite with less eye-pain";
          break;
        default:
          Text = "Fluxus from Wish.com";
          break;
      }
    }

    private void button1_Click(object sender, EventArgs e)
    {
      API.Execute(ScriptBox.Text);
    }

    private void button2_Click(object sender, EventArgs e)
    {
      API.Inject();
    }
    private void button3_Click(object sender, EventArgs e)
    {
      ScriptBox.Text = "";
    }

    private void Form1_Load(object sender, EventArgs e)
    {
      FluxAPI.API.OnInputRecived += delegate (string input) //OnInputRecived will be called when the DLL sends data to the UI
      {
        this.Invoke(new MethodInvoker(delegate //THIS IS REQUIRED, YOU WILL ERROR WITHOUT THIS
              {
            richTextBox2.Text = richTextBox2.Text + $"[Console {DateTime.Now.ToString("HH:mm:ss")}] {input}\n";
          }));
      };

      FluxAPI.API.OnInject += delegate ()
      {
        // API.Execute("print('Fluxus has injected!')"); //Execute this when Fluxus has injected.

        if (checkBox1.Checked) //If the Internal UI toggle is checked, toggle it!
          API.ToggleInternalUI();

        if (checkBox2.Checked) //If Unlock FPS is checked, enable it!
          API.SetFPSCap(); //If you do not give a number, it will set it to max.
      };
    }

    private void checkBox1_CheckedChanged(object sender, EventArgs e)
    {
      API.ToggleInternalUI(); //Toggles the internal UI from on/off
    }

    private void checkBox2_CheckedChanged(object sender, EventArgs e)
    {
      if (checkBox2.Checked) //If this is true, the user wants to unlock the fps
        API.SetFPSCap(); //If you do not give a number, it will set it to max.
      else //Its false so they want to lock it again
        API.SetFPSCap(60); //Roblox cap is 60 fps, so restore it to 60.
    }

    private void richTextBox2_TextChanged(object sender, EventArgs e)
    {

    }
  }
}
