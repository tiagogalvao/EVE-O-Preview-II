using EveOPreview.View.Interface;

namespace EveOPreview.View.Implementation
{
    internal sealed class ThumbnailDescription : IThumbnailDescription
    {
        public ThumbnailDescription(string title, bool isDisabled)
        {
            Title = title;
            IsDisabled = isDisabled;
        }

        public string Title { get; set; }
        public bool IsDisabled { get; set; }
    }
}