mergeInto(LibraryManager.library, {
    DetectDeviceInfo: function () {
        // Get user agent details
        var userAgent = navigator.userAgent || navigator.vendor || window.opera;

        // Determine device type
        var deviceType = "Desktop";
        if (/windows phone/i.test(userAgent)) {
            deviceType = "Windows Phone";
        } else if (/android/i.test(userAgent)) {
            deviceType = "Android";
        } else if (/iPad|iPhone|iPod/.test(userAgent) && !window.MSStream) {
            deviceType = "iOS";
        }

        // Pass the result back to Unity
        SendMessage('DeviceManager', 'OnDeviceInfoDetected', deviceType);
    }
});
