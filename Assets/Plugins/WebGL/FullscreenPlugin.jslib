
mergeInto(LibraryManager.library, {
    requestFullscreen: function () {
        console.log("Fullscreen function was called from Unity.");
        const canvas = document.getElementById("unity-canvas");
        if (canvas.requestFullscreen) {
            canvas.requestFullscreen();
        } else if (canvas.webkitRequestFullscreen) { // Safari compatibility
            canvas.webkitRequestFullscreen();
        } else if (canvas.msRequestFullscreen) { // Older Edge compatibility
            canvas.msRequestFullscreen();
        }
    },
    resizeCanvas: function() {
    console.log("Resize function was called from Unity." + window.innerWidth + window.innerHeight);
    const canvas = document.getElementById("unity-canvas");
    canvas.style.width = window.innerWidth + "px";
    canvas.style.height = window.innerHeight + "px";
    canvas.width = window.innerWidth;
    canvas.height = window.innerHeight;
  }
});

