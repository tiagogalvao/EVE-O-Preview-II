using System;
using System.Collections.Concurrent;
using System.Diagnostics;
using System.Drawing;
using EveOPreview.Configuration.Interface;
using EveOPreview.Core.Action;
using EveOPreview.Core.Configuration.Interface;
using EveOPreview.Mediator.Messages.Configuration;
using EveOPreview.Mediator.Messages.Services;
using EveOPreview.Mediator.Messages.Thumbnails;
using EveOPreview.Presenters.Interface;
using EveOPreview.View.Interface;
using MediatR;
using System.Collections.Generic;
using System.Threading.Tasks;
using EveOPreview.View.Implementation;

namespace EveOPreview.Presenters.Implementation
{
    public class MainFormPresenter : Presenter<IMainFormView>, IMainFormPresenter
    {
        #region Private constants

        private static readonly string Version = "7.1.2-alpha";
        private static readonly Uri ForumUrl = new("https://forums.eveonline.com/t/eve-o-preview-v5-1-2-fork-multi-client-preview-switcher-2022-05-09-limited-linux-support/361804");

        #endregion

        #region Private fields

        private readonly IMediator _mediator;
        private readonly IThumbnailConfiguration _configuration;
        private readonly IStorage _configurationStorage;
        private readonly ConcurrentDictionary<string, IThumbnailDescription> _descriptionsCache = new();

        private bool _suppressSizeNotifications;
        private bool _exitApplication;

        #endregion

        public MainFormPresenter(
            IMainFormView view,
            IMediator mediator,
            IThumbnailConfiguration configuration,
            IStorage configurationStorage)
            : base(view)
        {
            _mediator = mediator;
            _configuration = configuration;
            _configurationStorage = configurationStorage;

            View.FormActivated = Activate;
            View.FormMinimized = Minimize;
            View.FormCloseRequested = Close;
            View.ApplicationSettingsChanged = SaveApplicationSettings;
            View.ThumbnailsSizeChanged = UpdateThumbnailsSize;
            View.ThumbnailStateChanged = UpdateThumbnailState;
            View.DocumentationLinkActivated = OpenDocumentationLink;
            View.ApplicationExitRequested = ExitApplication;
        }

        private void Activate()
        {
            _suppressSizeNotifications = true;
            LoadApplicationSettings();
            View.SetDocumentationUrl(ForumUrl.AbsoluteUri);
            View.SetVersionInfo(GetApplicationVersion());

            if (_configuration.MinimizeToTray)
            {
                View.Minimize();
            }

            _mediator.Send(new StartService());
            _suppressSizeNotifications = false;
        }

        private void Minimize()
        {
            if (_configuration.MinimizeToTray)
            {
                View.Hide();
            }
        }

        private async Task Close(ViewCloseRequest request)
        {
            if (_exitApplication || !View.MinimizeToTray)
            {
                await _mediator.Send(new StopService());
                _configurationStorage.Save();
                request.Allow = true;
                Environment.Exit(0);
            }
            else
            {
                request.Allow = false;
                View.Minimize();
            }
        }

        private async Task UpdateThumbnailsSize()
        {
            if (!_suppressSizeNotifications)
            {
                SaveApplicationSettings();
                await _mediator.Publish(new ThumbnailConfiguredSizeUpdated());
            }
        }

        private void LoadApplicationSettings()
        {
            _configurationStorage.Load();

            View.MinimizeToTray = _configuration.MinimizeToTray;
            View.ThumbnailOpacity = _configuration.ThumbnailOpacity;
            View.EnableClientLayoutTracking = _configuration.EnableClientLayoutTracking;
            View.HideActiveClientThumbnail = _configuration.HideActiveClientThumbnail;
            View.MinimizeInactiveClients = _configuration.MinimizeInactiveClients;
            View.ShowThumbnailsAlwaysOnTop = _configuration.ShowThumbnailsAlwaysOnTop;
            View.HideThumbnailsOnLostFocus = _configuration.HideThumbnailsOnLostFocus;
            View.EnablePerClientThumbnailLayouts = _configuration.EnablePerClientThumbnailLayouts;

            View.SetThumbnailSizeLimitations(_configuration.ThumbnailMinimumSize, _configuration.ThumbnailMaximumSize);
            View.ThumbnailSize = _configuration.ThumbnailSize;
            View.ThumbnailFontSize = _configuration.ThumbnailFontSize;

            View.EnableThumbnailZoom = _configuration.ThumbnailZoomEnabled;
            View.ThumbnailZoomFactor = _configuration.ThumbnailZoomFactor;
            View.ThumbnailZoomAnchor = _configuration.ThumbnailZoomAnchor;

            View.ShowThumbnailOverlays = _configuration.ShowThumbnailOverlays;
            View.ShowThumbnailFrames = _configuration.ShowThumbnailFrames;
            View.EnableActiveClientHighlight = _configuration.EnableActiveClientHighlight;
            View.ActiveClientHighlightColor = _configuration.ActiveClientHighlightColor;
        }

        private async Task SaveApplicationSettings()
        {
            _configuration.MinimizeToTray = View.MinimizeToTray;
            _configuration.ThumbnailOpacity = (float)View.ThumbnailOpacity;

            _configuration.EnableClientLayoutTracking = View.EnableClientLayoutTracking;
            _configuration.HideActiveClientThumbnail = View.HideActiveClientThumbnail;
            _configuration.MinimizeInactiveClients = View.MinimizeInactiveClients;
            _configuration.ShowThumbnailsAlwaysOnTop = View.ShowThumbnailsAlwaysOnTop;
            _configuration.HideThumbnailsOnLostFocus = View.HideThumbnailsOnLostFocus;
            _configuration.EnablePerClientThumbnailLayouts = View.EnablePerClientThumbnailLayouts;

            _configuration.ThumbnailSize = View.ThumbnailSize;
            _configuration.ThumbnailFontSize = View.ThumbnailFontSize;

            _configuration.ThumbnailZoomEnabled = View.EnableThumbnailZoom;
            _configuration.ThumbnailZoomFactor = View.ThumbnailZoomFactor;
            _configuration.ThumbnailZoomAnchor = View.ThumbnailZoomAnchor;

            _configuration.ShowThumbnailOverlays = View.ShowThumbnailOverlays;

            if (_configuration.ShowThumbnailFrames != View.ShowThumbnailFrames)
            {
                _configuration.ShowThumbnailFrames = View.ShowThumbnailFrames;
                await _mediator.Publish(new ThumbnailFrameSettingsUpdated());
            }

            _configuration.EnableActiveClientHighlight = View.EnableActiveClientHighlight;
            _configuration.ActiveClientHighlightColor = View.ActiveClientHighlightColor;

            _configurationStorage.Save();
            View.RefreshZoomSettings();
            await _mediator.Send(new SaveConfiguration());
        }

        public void AddThumbnails(IReadOnlyList<string> thumbnailTitles)
        {
            var descriptions = new List<IThumbnailDescription>(thumbnailTitles.Count);
            foreach (var title in thumbnailTitles)
            {
                if (!_descriptionsCache.ContainsKey(title))
                {
                    var description = CreateThumbnailDescription(title);
                    _descriptionsCache[title] = description;
                    descriptions.Add(description);
                }
            }

            View.AddThumbnails(descriptions);
        }

        public void RemoveThumbnails(IReadOnlyList<string> thumbnailTitles)
        {
            var descriptions = new List<IThumbnailDescription>(thumbnailTitles.Count);
            foreach (var title in thumbnailTitles)
            {
                if (_descriptionsCache.TryRemove(title, out var description))
                {
                    descriptions.Add(description);
                }
            }

            View.RemoveThumbnails(descriptions);
        }

        private IThumbnailDescription CreateThumbnailDescription(string title) =>
            new ThumbnailDescription(title, _configuration.IsThumbnailDisabled(title));

        private async Task UpdateThumbnailState(string title)
        {
            if (_descriptionsCache.TryGetValue(title, out var description))
            {
                _configuration.ToggleThumbnail(title, description.IsDisabled);
            }

            await _mediator.Send(new SaveConfiguration());
        }

        public void UpdateThumbnailSize(Size size)
        {
            _suppressSizeNotifications = true;
            View.ThumbnailSize = size;
            _suppressSizeNotifications = false;
        }

        private void OpenDocumentationLink()
        {
            var documentationPsi = new ProcessStartInfo
            {
                FileName = ForumUrl.AbsoluteUri,
                UseShellExecute = true
            };
            Process.Start(documentationPsi);
        }

        private string GetApplicationVersion() => Version;

        private void ExitApplication()
        {
            _exitApplication = true;
            View.Close();
        }
    }
}