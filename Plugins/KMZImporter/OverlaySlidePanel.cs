using System;
using System.Drawing;
using System.Windows.Forms;

namespace KMZImporter
{
    internal class OverlaySlidePanel : Panel
    {
        private readonly Timer _timer = new Timer { Interval = 10 };
        private bool _isOpen = false;
        private int _panelWidth = 260;
        private int _step = 20;
        private Control _parent;

        public int PanelWidth
        {
            get => _panelWidth;
            set { _panelWidth = value; Width = value; }
        }

        public OverlaySlidePanel() {
            Width = _panelWidth;
            Visible = false;  // start hidden
            BackColor = Color.FromArgb(45, 45, 48);

            _timer.Tick += (s, e) =>
            {
                int target = _isOpen
                    ? _parent.ClientSize.Width - _panelWidth
                    : _parent.ClientSize.Width;

                int delta = Left < target ? _step : -_step;

                if (Math.Abs(Left - target) <= _step) {
                    Left = target;
                    _timer.Stop();

                    // Hide after closing animation completes
                    if (!_isOpen)
                        Visible = false;
                } else
                    Left += delta;
            };
        }

        public void AttachTo(Control parent) {
            _parent = parent;
            Height = parent.ClientSize.Height;
            Left = parent.ClientSize.Width;  // start off-screen
            Top = 0;

            parent.Controls.Add(this);
            BringToFront();

            parent.Resize += (s, e) =>
            {
                Height = parent.ClientSize.Height;
                if (!_isOpen)
                    Left = parent.ClientSize.Width;
                else
                    Left = parent.ClientSize.Width - _panelWidth;
            };
        }

        public void Open() {
            if (_isOpen) return;
            _isOpen = true;

            // Make visible BEFORE animation starts
            Left = _parent.ClientSize.Width;  // reset to hidden position
            Visible = true;
            BringToFront();
            _timer.Start();
        }

        public void Close() {
            if (!_isOpen) return;
            _isOpen = false;
            _timer.Start();
            // Visible = false is set AFTER animation completes in Tick
        }

        public void Toggle() {
            if (_isOpen) Close();
            else Open();
        }
    }
}