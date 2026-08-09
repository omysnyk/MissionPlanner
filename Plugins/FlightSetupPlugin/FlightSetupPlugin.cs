using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using MissionPlanner;
using MissionPlanner.Plugin;
using ReaLTaiizor.Controls;
using Button = System.Windows.Forms.Button;
using Panel = System.Windows.Forms.Panel;

namespace FlightSetupPlugin
{
    public class FlightSetupPlugin : Plugin
    {
        public override string Name => "Flight Setup";
        public override string Version  => "0.0.1";
        public override string Author => "Marquise de Carabas";
        public override bool Init()
        {
            
            
            var pluginPanel = Host.MainForm.FlightData.panel_persistent;
            pluginPanel.Dock = DockStyle.Top;
            var btnMargin = 3;
            pluginPanel.Height = 30 + btnMargin * 2;
            var buttonPanel = new FlowLayoutPanel
            {
                FlowDirection = FlowDirection.LeftToRight,
                WrapContents  = false,
                AutoSize      = true,
                AutoSizeMode  = AutoSizeMode.GrowAndShrink,
                Dock          = DockStyle.Fill,
                Padding       = new Padding(btnMargin)
            };

            foreach (var btn in CreateButtons())
                buttonPanel.Controls.Add(btn);
            
            pluginPanel.MinimumSize = new Size(0, 35);

            pluginPanel.Controls.Add(buttonPanel);
            pluginPanel.Width  = buttonPanel.PreferredSize.Width;
            pluginPanel.Height = buttonPanel.PreferredSize.Height;

            Console.WriteLine($@"[{Name}] Plugin INITIALIZED");
            
            return true;
        }

        private IEnumerable<HopeButton> CreateButtons()
        {
            return new[] {
                CreateButton($@"Setup Quick"),
                CreateButton($@"Pre Flight"),
                CreateButton($@"Post Launch"), 
                CreateButton($@"Approach")};
        }

        private static HopeButton CreateButton(string text)
        {
            return new HopeButton
            {
                Text = text,
                PrimaryColor = Color.BlueViolet,
                
                Height    = 32,
                AutoSize  = true,  // width fits text automatically
                Margin    = new Padding(2),
                Padding   = new Padding(6, 0, 6, 0),  // horizontal text padding
                Cursor    = Cursors.Hand,
            };
        }

        public override bool Loaded()
        {
            
            
            return true;
        }

        public override bool Exit()
        {
            return true;
        }
    }
}