using System.Collections.Generic;
using System.Drawing;

namespace EveOPreview.Presenters.Interface
{
    interface IMainFormPresenter
    {
        void AddThumbnails(IReadOnlyList<string> thumbnailTitles);
        void RemoveThumbnails(IReadOnlyList<string> thumbnailTitles);
        void UpdateThumbnailSize(Size size);
    }
}