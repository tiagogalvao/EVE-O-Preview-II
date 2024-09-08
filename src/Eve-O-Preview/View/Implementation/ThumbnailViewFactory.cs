using System;
using System.Drawing;
using EveOPreview.Configuration.Interface;
using EveOPreview.Services.Interface;
using EveOPreview.View.Interface;

namespace EveOPreview.View.Implementation
{
	internal sealed class ThumbnailViewFactory : IThumbnailViewFactory
	{
		private readonly IWindowManager _windowManager;
		private readonly bool _isCompatibilityModeEnabled;

		public ThumbnailViewFactory(IWindowManager windowManager, IThumbnailConfiguration configuration)
		{
			_windowManager = windowManager;
			_isCompatibilityModeEnabled = configuration.EnableCompatibilityMode;
		}

		public IThumbnailView Create(IntPtr id, string title, Size size)
		{
			IThumbnailView view = _isCompatibilityModeEnabled
				? new StaticThumbnailView(_windowManager)
				: new LiveThumbnailView(_windowManager);

			view.Id = id;
			view.Title = title;
			view.ThumbnailSize = size;

			return view;
		}
	}
}