using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using SonyBankUsageRecordParse.src.subsystems.Tagging;

namespace SonyBankUsageRecordParse.src
{
	public partial class StoreNameTagLists : Form
	{
		public StoreNameTagLists()
		{
			InitializeComponent();
			LoadStoreConfigToTreeView();
		}

		private void LoadStoreConfigToTreeView()
		{
			StoreNameTagConfig storeConfig = new();

			treeViewStoreConfig.Nodes.Clear();

			var expenseGroups = new Dictionary<string, List<string>>();

			foreach (var tag in storeConfig.StoreNameTags)
			{
				var parts = tag.Split(':');
				if (parts.Length == 2)
				{
					var expenseCategory = parts[0];
					var storeName = parts[1];
					if (!expenseGroups.ContainsKey(expenseCategory))
					{
						expenseGroups[expenseCategory] = new List<String>();
					}
					expenseGroups[expenseCategory].Add(storeName);
				}
			}

			foreach (var expenseGroup in expenseGroups)
			{
				var expenseNode = new TreeNode(expenseGroup.Key); // 費用項目ノード
				foreach (var store in expenseGroup.Value)
				{
					expenseNode.Nodes.Add(new TreeNode(store)); // 店名ノードを追加
				}
				treeViewStoreConfig.Nodes.Add(expenseNode); // TreeViewに費用項目を追加
			}

		}

		private void ExpandStoreNameTagListsbutton_Click(object sender, EventArgs e)
		{
			foreach (TreeNode node in treeViewStoreConfig.Nodes)
			{
				node.Expand();
				foreach (TreeNode childNode in node.Nodes)
				{ 
					childNode.Expand(); 
				}
			}
		}
	}
}
