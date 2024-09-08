namespace EveOPreview.Services.Interface
{
    public interface IThumbnailManager
    {
        void Start();
        void Stop();
        void UpdateThumbnailsSize();
        void UpdateThumbnailFrames();
    }
}