using System;

namespace EveOPreview.Services.Interface
{
    public interface IDwmThumbnail
    {
        void Register(nint destination, nint source);
        void Unregister();
        void Move(int left, int top, int right, int bottom);
        void Update();
    }
}