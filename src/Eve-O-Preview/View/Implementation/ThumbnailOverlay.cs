using System;
using System.Windows.Forms;
using EveOPreview.Core.Internal.Interop.Windows;

namespace EveOPreview.View.Implementation
{
    public partial class ThumbnailOverlay : Form
    {
        #region Private fields

        private readonly Action<object, MouseEventArgs> _areaClickAction;

        #endregion

        public ThumbnailOverlay(Form owner, Action<object, MouseEventArgs> areaClickAction)
        {
            Owner = owner;
            _areaClickAction = areaClickAction;

            InitializeComponent();
        }

        private void OverlayArea_Click(object sender, MouseEventArgs e)
        {
            _areaClickAction(this, e);
        }

        public void SetOverlayLabel(string label)
        {
            OverlayLabel.Text = label;
        }

        public void EnableOverlayLabel(bool enable)
        {
            OverlayLabel.Visible = enable;
        }

        protected override CreateParams CreateParams
        {
            get
            {
                var createParams = base.CreateParams;
                createParams.ExStyle |= (int)InteropConstants.WS_EX_TOOLWINDOW;
                return createParams;
            }
        }
    }
}