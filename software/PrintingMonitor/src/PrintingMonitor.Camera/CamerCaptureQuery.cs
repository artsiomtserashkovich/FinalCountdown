using System;
using OpenCvSharp;
using PrintingMonitor.Printer.Queries;

namespace PrintingMonitor.Camera
{
    internal class OpenCVCameraCaptureQuery : ICameraCaptureQuery
    {
        public string GetBase64Capture()
        {
            using (var capture = new VideoCapture(0))
            using (var frame = new Mat())
            {
                capture.Read(frame);

                return Convert.ToBase64String(frame.ToBytes());
            }
        }
    }
}
