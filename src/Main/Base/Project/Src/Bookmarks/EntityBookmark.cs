﻿// Copyright (c) AlphaSierraPapa for the SharpDevelop Team (for details please see \doc\copyright.txt)
// This code is distributed under the GNU LGPL (for details please see \doc\license.txt)

using System;
using System.Windows;
using System.Windows.Input;
using ICSharpCode.Core;
using ICSharpCode.Core.Presentation;
using ICSharpCode.NRefactory.TypeSystem;

namespace ICSharpCode.SharpDevelop.Bookmarks
{
	/// <summary>
	/// Bookmark used to give additional operations for entities.
	/// Does not derive from SDBookmark because it is not stored in the central BookmarkManager,
	/// but only in the document's BookmarkManager.
	/// </summary>
	public class EntityBookmark : IBookmark
	{
		IEntity entity;
		
		public IEntity Entity {
			get {
				return entity;
			}
		}
		
		public EntityBookmark(IEntity entity)
		{
			this.entity = entity;
		}
		
		public const string ContextMenuPath = "/SharpDevelop/ViewContent/TextEditor/EntityContextMenu";
		
		public virtual IImage Image {
			get { return ClassBrowserIconService.GetIcon(entity); }
		}
		
		public int LineNumber {
			get { return entity.Region.BeginLine; }
		}
		
		public virtual void MouseDown(MouseButtonEventArgs e)
		{
			if (e.ChangedButton == MouseButton.Left) {
				var f = AnalyticsMonitorService.TrackFeature("ICSharpCode.SharpDevelop.Bookmarks.EntityBookmark.ShowContextMenu");
				var ctx = MenuService.ShowContextMenu(e.Source as UIElement, entity, ContextMenuPath);
				ctx.Closed += delegate { f.EndTracking(); };
				e.Handled = true;
			}
		}
		
		public virtual void MouseUp(MouseButtonEventArgs e)
		{
		}
		
		int IBookmark.ZOrder {
			get { return -10; }
		}
		
		bool IBookmark.CanDragDrop {
			get { return false; }
		}
		
		void IBookmark.Drop(int lineNumber)
		{
			throw new NotSupportedException();
		}
	}
}
