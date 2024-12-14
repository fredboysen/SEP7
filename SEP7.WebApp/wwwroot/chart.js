window.Setup = (canvasId, config) => {
    console.log("Canvas ID:", canvasId);
    console.log("Config:", config);

    const canvas = document.getElementById(canvasId);
    if (!canvas) {
        console.error("Canvas not found with ID:", canvasId);
        return;
    }

    const ctx = canvas.getContext("2d");
    new Chart(ctx, config);
};
