window.createChart = (chartId, chartType, chartData) => {
    const ctx = document.getElementById(chartId).getContext('2d');

    // Check if the chart already exists and destroy it
    if (window[chartId]) {
        window[chartId].destroy();
    }

    // Create a new chart instance
    window[chartId] = new Chart(ctx, {
        type: chartType,
        data: chartData,
        options: {
            responsive: true,
            maintainAspectRatio: false,
            plugins: {
                legend: {
                    display: true,
                    position: 'top',
                }
            }
        }
    });
};
