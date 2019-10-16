using System;
namespace Checkin.Interface
{
    public interface IImageConverter
    {
        byte[] ResizeImageIOS(byte[] imageData, float width, float height);
    }
}
