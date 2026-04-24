using System;
using System.Linq;
using System.Windows.Forms;
using KMZImporter;
using SharpKml.Dom;
using Document = SharpKml.Dom.Document;
using Feature = SharpKml.Dom.Feature;
using Folder = SharpKml.Dom.Folder;
using Placemark = SharpKml.Dom.Placemark;

namespace KMZUtils
{
    public partial class SelectKmzItemsForm : Form
    {
        private bool _isUpdating; // prevent recursive event firing
        public Document Document;
        public Placemark[] SelectedPlaceMarks = { };

        public SelectKmzItemsForm()
        {
            InitializeComponent();
        }

        public SelectKmzItemsForm(Feature rootNodeFeature)
        {
            InitializeComponent();

            treeView1.BeginUpdate(); // prevents UI flicker
            treeView1.Nodes.Clear();

            //var nodes = _dataProvider.GetNodes();
            treeView1.Nodes.AddRange(ExtractNodes(rootNodeFeature));

            treeView1.ExpandAll(); // optional
            treeView1.EndUpdate();
        }


        private TreeNode[] ExtractNodes(Element element)
        {
            switch (element)
            {
                case Document document:
                {
                    // var node = new TreeNode
                    // {
                    //     Name = document.Name,
                    //     Checked = true
                    // };
                    Document = document;

                    //foreach (var feat in document.Features) node.Nodes.AddRange(ExtractNodes(feat));

                    return document.Features.SelectMany(ExtractNodes).ToArray();
                }
                case Folder folder:
                {
                    var node = new TreeNode
                    {
                        Text = folder.Name,
                        Checked = true
                    };

                    foreach (var feat in folder.Features) node.Nodes.AddRange(ExtractNodes(feat));

                    return new[] { node };
                }
                case Placemark placemark:
                {
                    if (placemark.Geometry == null) return new TreeNode[] { };

                    var node = new TreeNode(placemark.Name)
                    {
                        Text = placemark.Name,
                        Tag = placemark,
                        Checked = true
                    };

                    return new[] { node };
                }
            }

            return new TreeNode[] { };
        }

        private void treeView1_AfterCheck(object sender, TreeViewEventArgs e)
        {
            if (_isUpdating) return;
            if (e.Action == TreeViewAction.Unknown) return; // ignore programmatic changes

            _isUpdating = true;
            try
            {
                // Check/uncheck all children
                SetChildrenChecked(e.Node, e.Node.Checked);

                // Update parent state based on siblings
                UpdateParentCheckedState(e.Node.Parent);
            }
            finally
            {
                _isUpdating = false;
            }
        }

        private void SetChildrenChecked(TreeNode node, bool isChecked)
        {
            foreach (TreeNode child in node.Nodes)
            {
                child.Checked = isChecked;
                SetChildrenChecked(child, isChecked); // recurse
            }
        }

        private void UpdateParentCheckedState(TreeNode parent)
        {
            if (parent == null) return;

            var allChecked = true;
            var anyChecked = false;

            foreach (TreeNode sibling in parent.Nodes)
                if (sibling.Checked) anyChecked = true;
                else allChecked = false;

            parent.Checked = anyChecked; // or use allChecked for strict behavior
            UpdateParentCheckedState(parent.Parent); // recurse up
        }

        private void buttonOK_Click(object sender, EventArgs e)
        {
            var treeNodes = treeView1.Nodes.Flatten();
            var enumerable = treeNodes as TreeNode[] ?? treeNodes.ToArray();
            var allNodesArray = enumerable.ToArray();

            SelectedPlaceMarks = enumerable
                .Where(node => node.Checked && node.Tag is Placemark)
                .Select(node => node.Tag)
                .Cast<Placemark>()
                .ToArray();

            DialogResult = DialogResult.OK;

            Close();
        }

        private void buttonCancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }
    }
}